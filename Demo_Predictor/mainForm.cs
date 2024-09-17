using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

//DllImport
using System.Runtime.InteropServices;

namespace Demo_Predictor
{
    public partial class mainForm : Form
    {
        //--- for windows resoution
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
        //---

        //--- IO-input for loading setting.ini
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private string settingsPath = Application.StartupPath + "\\setting.ini";
        StringBuilder line = new StringBuilder(255);
        //---

        //--- system
        System.Drawing.Rectangle resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        //---

        //--- images param.s/algo. params.
        System.Diagnostics.Stopwatch timeWatch;

        bool bUseGpu = true;
        int nGpuIndex = 0;

        string strModelPath = "./example.ditox";
        string strImageDir = "./IMAGE";
        string strResultImageDir = "./RESULT";

        int nModelMode = -1;
        predictor aisvModel;
        string modelType = "";
        bool bIsModelLoaded = false;
        bool bIsLoadModel = false;
        bool bIsTest = false;
        bool bIsResult = false;
        //---
        
        public mainForm()
        {
            InitializeComponent();
        }

        private void ScanControls(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    ScanControls(con);
                }
            }
        }

        private void ResizeControls(float scale_x, float scale_y, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] tag = con.Tag.ToString().Split(new char[] { ':' });
                con.Width = (int)(Convert.ToSingle(tag[0]) * scale_x);
                con.Height = (int)(Convert.ToSingle(tag[1]) * scale_y);
                con.Left = (int)(Convert.ToSingle(tag[2]) * scale_x);
                con.Top = (int)(Convert.ToSingle(tag[3]) * scale_y);

                Single currentSize = 0;
                if (scale_y < scale_x)
                {
                    currentSize = Convert.ToSingle(tag[4]) * scale_y;
                }
                else
                {
                    currentSize = Convert.ToSingle(tag[4]) * scale_x;
                }

                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    ResizeControls(scale_x, scale_y, con);
                }
            }
        }

        private void PreviewClear(object sender, PaintEventArgs e)
        {

            e.Graphics.Dispose();
            //e.Graphics.Clear(Color.Transparent);
            //e.Graphics.Clear(Color.FromArgb(80, 80, 80));

        }
        private Bitmap DisplayImage(Bitmap bitmap, int width, int height)
        {
            Point[] points = { new Point(0, 0), new Point(width, 0), new Point(0, height) };
            Rectangle rect = new Rectangle(0, 0, width, height);
            ImageAttributes attributes = new ImageAttributes();

            //--- fit to imageBox
            Bitmap bm = new Bitmap(bitmap, Convert.ToInt32(width), Convert.ToInt32(height));
            Graphics graph = Graphics.FromImage(bm);
            graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graph.DrawImage(bm, points, rect, GraphicsUnit.Pixel);

            return bm;
        }

        private void Predict()
        {
            message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] Start to inference...");

            foreach (string strImage in Directory.GetFiles(strImageDir))
            {
                timeWatch = System.Diagnostics.Stopwatch.StartNew();
                //Bitmap colorBitmap = (Bitmap)Image.FromFile(strImagePath);
                Bitmap inputImage = (Bitmap)Image.FromFile(strImage);

                //Bitmap resultImage = aisvModel.RunInference(inputImage);
                //predictor.PredictInfo predictInfo = new predictor.PredictInfo
                //{
                //    modelName = "",
                //    result = "",
                //    resultClass = new List<string>(),
                //    resultColor = new List<Color>(),
                //    resultImage = new Bitmap(inputImage.Width, inputImage.Height)
                //};
                //Bitmap resIMG = aisvModel.RunInference(ref predictInfo, inputImage);
                predictor.PredictInfo predictInfo = aisvModel.RunInference(inputImage);

                timeWatch.Stop();
                long elapsedMs = timeWatch.ElapsedMilliseconds;
                message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor - " + predictInfo.modelName + "] Inference complete, time spent: " +
                    (elapsedMs / 1000.0).ToString("0.000") + " (s)");
                
                for(int count=0; count< predictInfo.resultClass.Count; count++)
                {
                    Console.WriteLine("class={0}, color={1}", predictInfo.resultClass[count], predictInfo.resultColor[count]);
                }

                string name = strImage.Split('\\')[1];
                predictInfo.resultImage.Save(strResultImageDir + "/" + name);
                //result_pictureBox.Image = resultInfo.resultImage;
                //result_pictureBox.Show();
                
                Thread.Sleep(3000);
            }
            
            bIsResult = true;
            bIsTest = false;
        }

        private void test_button_Click(object sender, EventArgs e)
        {
            if (!bIsTest) // init_mode
            {
                bIsTest = true;
                test_button.ForeColor = System.Drawing.Color.LightSlateGray;
                test_button.Enabled = false;

                Predict();
                bIsResult = false;
            }
        }

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }
        private double GetWindowsScreenScalingFactor(bool percentage = true)
        {
            //Create Graphics object from the current windows handle
            Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);
            //Get Handle to the device context associated with this Graphics object
            IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
            //Call GetDeviceCaps with the Handle to retrieve the Screen Height
            int LogicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.DESKTOPVERTRES);
            //Divide the Screen Heights to get the scaling factor and round it to two decimals
            double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);
            //If requested as percentage - convert it
            if (percentage)
            {
                ScreenScalingFactor *= 100.0;
            }
            //Release the Handle and Dispose of the GraphicsObject object
            GraphicsObject.ReleaseHdc(DeviceContextHandle);
            GraphicsObject.Dispose();

            return ScreenScalingFactor;
        }

        private void LoadPredictor(int modelMode)
        {
            aisvModel = new predictor(strModelPath, bUseGpu, nGpuIndex);
            
            if (aisvModel.GetCheckSession(ref modelType))
            {
                bIsModelLoaded = true;
            }
            else
            {
                MessageBox.Show("Load model failed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        public delegate void InvokeDelegate();

        //private void LoadPredictorListener()
        //{
        //    timeWatch = System.Diagnostics.Stopwatch.StartNew();
        //    //Console.WriteLine("[Model] loading predictor model...");
        //    message_richTextBox.Invoke((MethodInvoker)delegate
        //    {
        //        message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] Loading model...");
        //    });

        //    while (!bIsModelLoaded)
        //    {
        //        Thread.Sleep(300);
        //    }

        //    timeWatch.Stop();
        //    long elapsedMs = timeWatch.ElapsedMilliseconds;
        //    //Console.WriteLine("[Model] load predictor model complete, time spent:" + (elapsedMs / 1000).ToString("0.000") + " (s)");
        //    message_richTextBox.Invoke((MethodInvoker)delegate
        //    {
        //        message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] Load model:" + modelType + " complete, time spent: " +
        //            (elapsedMs / 1000).ToString("0.000") + " (s)");
        //    });

        //    test_button.BeginInvoke(new InvokeDelegate(TestButtonInvokeMethod));
        //}

        private void LoadModel_button_Click(object sender, EventArgs e)
        {
            if (!bIsModelLoaded)
            {
                bIsLoadModel = true;

                Task.Factory.StartNew(() => LoadPredictor(nModelMode));

                Task.Factory.StartNew(() =>
                {
                    timeWatch = System.Diagnostics.Stopwatch.StartNew();
                    //Console.WriteLine("[Model] loading predictor model...");
                    message_richTextBox.Invoke((MethodInvoker)delegate
                    {
                        message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] Loading model...");
                    });

                    while (!bIsModelLoaded)
                    {
                        Thread.Sleep(300);
                    }

                    timeWatch.Stop();
                    long elapsedMs = timeWatch.ElapsedMilliseconds;
                    //Console.WriteLine("[Model] load predictor model complete, time spent:" + (elapsedMs / 1000).ToString("0.000") + " (s)");
                    message_richTextBox.Invoke((MethodInvoker)delegate
                    {
                        message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] Load model:" + modelType + " complete, time spent: " +
                   (elapsedMs / 1000).ToString("0.000") + " (s)");
                    });

                    test_button.BeginInvoke(new InvokeDelegate(TestButtonInvokeMethod));
                    aisvModel.Dispose();
                    bIsModelLoaded = false;
                    message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] Model disposed.");
                });
            }
            else
                message_richTextBox.AppendText(System.Environment.NewLine + "[Predictor] model already loaded.");
        }

        public void TestButtonInvokeMethod()
        {
            test_button.Enabled = true;
            test_button.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            double scale_factor = GetWindowsScreenScalingFactor(false);

            screenWidth = (int)(screenWidth * scale_factor);
            screenHeight = (int)(screenHeight * scale_factor);
            float scaleX = (float)screenWidth / 8 * 5;
            float scaleY = (float)screenHeight / 8 * 5;

            ScanControls(this);
        }
    }
}