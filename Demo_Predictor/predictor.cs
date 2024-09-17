using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

//Predictor
using Aisvision;

namespace Demo_Predictor
{
    class predictor
    {
        private Predictor mPredict;
        private Predictor.AisvModelInfo mInfo;
        private bool mSuccess = false;

        public struct PredictInfo
        {
            public string modelName;
            public string result;
            public List<string> resultClass;
            public List<Color> resultColor;
            public Bitmap resultImage;
        }

        public predictor(string strModelPath, bool bUseGpu, int nGpuIndex)
        {
            mPredict = new Predictor();
            mPredict.LoadModel(strModelPath, bUseGpu, nGpuIndex);

            mSuccess = mPredict.GetModelInfo(out mInfo);
        }

        ~predictor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (mPredict != null)
            {
                mPredict.Dispose();
            }
            if (bDisposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        private Predictor.AisvImage ConvertBitmapToImage(Bitmap bitmap)
        {
            Predictor.AisvImage image = new Predictor.AisvImage();

            if (bitmap == null)
            {
                return image;
            }

            Bitmap result;
            if (bitmap.Width % 4 != 0)
            {
                int nNewWidth = (bitmap.Width + (4 - (bitmap.Width % 4)));

                result = new Bitmap(bitmap.Width, bitmap.Height);
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                }
                bitmap = result;
            }
            image.Width = bitmap.Width;
            image.Height = bitmap.Height;
            image.Channel = System.Drawing.Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr iPtr = bmpData.Scan0;

            try
            {
                if (image.Channel == 4)
                {
                    int byteSize = image.Width * image.Height * image.Channel;
                    byte[] PixelValues = new byte[byteSize];

                    int newByteSize = image.Width * image.Height * 3;
                    byte[] newData = new byte[newByteSize];

                    Marshal.Copy(iPtr, PixelValues, 0, byteSize);

                    //RGBA2BGR
                    var tmp = PixelValues;
                    for (int i = 0; i < tmp.Length / 4; i++)
                    {
                        newData[i * 3] = tmp[i * 4 + 2];
                        newData[i * 3 + 1] = tmp[i * 4 + 1];
                        newData[i * 3 + 2] = tmp[i * 4];
                    }
                    image.Data = newData;
                    image.Channel = 3;
                }
                else
                {
                    int byteSize = image.Width * image.Height * image.Channel;
                    byte[] PixelValues = new byte[byteSize];

                    Marshal.Copy(iPtr, PixelValues, 0, byteSize);

                    //BGR2RGB
                    var tmp = PixelValues;
                    byte[] newData = new byte[tmp.Length];
                    for (int i = 0; i < tmp.Length; i += 3)
                    {
                        newData[i] = tmp[i + 2];
                        newData[i + 1] = tmp[i + 1];
                        newData[i + 2] = tmp[i];
                    }
                    image.Data = newData;
                }

                bitmap.UnlockBits(bmpData);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return image;
        }

        private Bitmap ConvertImageToBitmap(Predictor.AisvImage image)
        {
            if (image.Data == null)
                return null;
            int w = image.Width;
            int h = image.Height;

            //BGR2RGB
            var a = image.Data;
            byte[] newData;
            if (image.Channel == 3)
            {
                newData = new byte[a.Length];
                for (int i = 0; i < a.Length; i += 3)
                {
                    newData[i] = a[i + 2];
                    newData[i + 1] = a[i + 1];
                    newData[i + 2] = a[i];
                }
            }
            else
            {
                newData = new byte[a.Length * 3];
                for (int i = 0; i < a.Length; i += 1)
                {
                    newData[i * 3] = a[i];
                    newData[i * 3 + 1] = a[i];
                    newData[i * 3 + 2] = a[i];
                }
            }

            int channel = image.Channel;
            byte[] imageData = newData;
            int stride = w * 3;
            if (stride % 4 != 0) stride += (4 - stride % 4);
            Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData bmData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr pNative = bmData.Scan0;
            for (int y = 0; y < h; ++y)
            {
                Marshal.Copy(imageData, y * w * 3, pNative, w * 3);
                pNative += stride;
            }
            bitmap.UnlockBits(bmData);

            return bitmap;
        }

        public PredictInfo RunInference(Bitmap bm_image)
        {
            PredictInfo resultInfo = new PredictInfo
            {
                modelName = "",
                result = "",
                resultClass = new List<string>(),
                resultColor = new List<Color>(),
                resultImage = new Bitmap(bm_image.Width, bm_image.Height)
            };

            if (mPredict.CheckSession())
            {
                Predictor.AisvImage image;
                image = ConvertBitmapToImage(bm_image);

                Predictor.AisvPredictInfo predictInfo = mPredict.Predict(image);

                // predict result info
                switch (mInfo.ModelType.ToString())
                {
                    case "Classification":
                        resultInfo.modelName = "Delos";
                        resultInfo.resultClass.Add(predictInfo.classificationInfo.Name);
                        resultInfo.resultColor.Add(predictInfo.classificationInfo.Color);
                        //Console.WriteLine("------> result={0}/{1}, prob={2}", predictInfo.classificationInfo.CategoryID, predictInfo.classificationInfo.Name, predictInfo.classificationInfo.Prob);
                        break;
                    case "AnomalyDetection":
                        resultInfo.modelName = "Cleo";
                        resultInfo.resultClass.Add(predictInfo.anomalydetectionInfo.Name);
                        resultInfo.resultColor.Add(predictInfo.anomalydetectionInfo.Color);
                        //Console.WriteLine("------> result={0}/{1}", predictInfo.anomalydetectionInfo.CategoryID, predictInfo.anomalydetectionInfo.Name);
                        //Predictor.AisvImage mask = predictInfo.anomalydetectionInfo.Mask;
                        break;
                    case "ObjectDetection":
                        resultInfo.modelName = "Jarvis";
                        var resultList = predictInfo.objectdetectionInfo.ResultList;
                        foreach (var result in resultList)
                        {
                            resultInfo.resultClass.Add(result.Name);
                            resultInfo.resultColor.Add(result.Color);
                            //Console.WriteLine("------> result={0}/{1}, prob={2}", result.CategoryID, result.Name, result.Prob);
                        }
                        break;
                    default: //Segmentation
                        resultInfo.modelName = "Hulk";
                        var itemList = predictInfo.segmentationInfo.CategoryList;
                        foreach (var item in itemList)
                        {
                            resultInfo.resultClass.Add(item.Name);
                            resultInfo.resultColor.Add(item.Color);
                            //Console.WriteLine("------> result={0}/{1}, thresh={2}", item.Index, item.Name, item.Threshold);
                        }
                        Predictor.AisvImage seg_mask = predictInfo.segmentationInfo.Mask;
                        break;
                }

                Predictor.AisvImage annotatedImage = mPredict.VisualizeResult(image, predictInfo, true);
                resultInfo.resultImage = ConvertImageToBitmap(annotatedImage);
            }
            else
            {
                using (Graphics g = Graphics.FromImage(resultInfo.resultImage)) { g.Clear(Color.White); }
                
            }

            return resultInfo;
        }

        public bool GetCheckSession(ref string modelType)
        {
            if (mSuccess)
                modelType = mInfo.ModelType.ToString();

            return mSuccess;
        }
    }
}