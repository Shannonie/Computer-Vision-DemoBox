namespace Demo_Predictor
{
    partial class mainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BG_panel = new System.Windows.Forms.Panel();
            this.result_groupBox = new System.Windows.Forms.GroupBox();
            this.result_pictureBox = new System.Windows.Forms.PictureBox();
            this.preview_groupBox = new System.Windows.Forms.GroupBox();
            this.IMG_Box = new System.Windows.Forms.PictureBox();
            this.display_panel = new System.Windows.Forms.Panel();
            this.LoadModel_button = new System.Windows.Forms.Button();
            this.message_richTextBox = new System.Windows.Forms.RichTextBox();
            this.test_button = new System.Windows.Forms.Button();
            this.BG_panel.SuspendLayout();
            this.result_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.result_pictureBox)).BeginInit();
            this.preview_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_Box)).BeginInit();
            this.display_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BG_panel
            // 
            this.BG_panel.AutoSize = true;
            this.BG_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BG_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BG_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BG_panel.Controls.Add(this.result_groupBox);
            this.BG_panel.Controls.Add(this.preview_groupBox);
            this.BG_panel.Controls.Add(this.display_panel);
            this.BG_panel.Controls.Add(this.test_button);
            this.BG_panel.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BG_panel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.BG_panel.Location = new System.Drawing.Point(7, 4);
            this.BG_panel.Margin = new System.Windows.Forms.Padding(10);
            this.BG_panel.Name = "BG_panel";
            this.BG_panel.Size = new System.Drawing.Size(2738, 1583);
            this.BG_panel.TabIndex = 0;
            // 
            // result_groupBox
            // 
            this.result_groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.result_groupBox.BackColor = System.Drawing.Color.Transparent;
            this.result_groupBox.Controls.Add(this.result_pictureBox);
            this.result_groupBox.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.9F);
            this.result_groupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.result_groupBox.Location = new System.Drawing.Point(1375, 263);
            this.result_groupBox.Name = "result_groupBox";
            this.result_groupBox.Size = new System.Drawing.Size(1354, 1315);
            this.result_groupBox.TabIndex = 26;
            this.result_groupBox.TabStop = false;
            this.result_groupBox.Text = "Result";
            // 
            // result_pictureBox
            // 
            this.result_pictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.result_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.result_pictureBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.result_pictureBox.Location = new System.Drawing.Point(16, 122);
            this.result_pictureBox.Margin = new System.Windows.Forms.Padding(15, 0, 15, 3);
            this.result_pictureBox.MinimumSize = new System.Drawing.Size(1320, 1180);
            this.result_pictureBox.Name = "result_pictureBox";
            this.result_pictureBox.Size = new System.Drawing.Size(1320, 1180);
            this.result_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.result_pictureBox.TabIndex = 1;
            this.result_pictureBox.TabStop = false;
            this.result_pictureBox.Invalidate();
            // 
            // preview_groupBox
            // 
            this.preview_groupBox.BackColor = System.Drawing.Color.Transparent;
            this.preview_groupBox.Controls.Add(this.IMG_Box);
            this.preview_groupBox.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preview_groupBox.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.preview_groupBox.Location = new System.Drawing.Point(8, 263);
            this.preview_groupBox.Name = "preview_groupBox";
            this.preview_groupBox.Size = new System.Drawing.Size(1354, 1315);
            this.preview_groupBox.TabIndex = 25;
            this.preview_groupBox.TabStop = false;
            this.preview_groupBox.Text = "Preview";
            // 
            // IMG_Box
            // 
            this.IMG_Box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.IMG_Box.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IMG_Box.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.IMG_Box.Location = new System.Drawing.Point(16, 122);
            this.IMG_Box.Margin = new System.Windows.Forms.Padding(15, 0, 15, 3);
            this.IMG_Box.MinimumSize = new System.Drawing.Size(1320, 1180);
            this.IMG_Box.Name = "IMG_Box";
            this.IMG_Box.Size = new System.Drawing.Size(1320, 1180);
            this.IMG_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IMG_Box.TabIndex = 0;
            this.IMG_Box.TabStop = false;
            // 
            // display_panel
            // 
            this.display_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.display_panel.Controls.Add(this.LoadModel_button);
            this.display_panel.Controls.Add(this.message_richTextBox);
            this.display_panel.Location = new System.Drawing.Point(611, 16);
            this.display_panel.Margin = new System.Windows.Forms.Padding(15, 3, 15, 15);
            this.display_panel.Name = "display_panel";
            this.display_panel.Size = new System.Drawing.Size(2110, 248);
            this.display_panel.TabIndex = 4;
            // 
            // LoadModel_button
            // 
            this.LoadModel_button.BackColor = System.Drawing.Color.DimGray;
            this.LoadModel_button.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.LoadModel_button.Font = new System.Drawing.Font("Franklin Gothic Demi", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadModel_button.ForeColor = System.Drawing.Color.GhostWhite;
            this.LoadModel_button.Location = new System.Drawing.Point(14, 12);
            this.LoadModel_button.Name = "LoadModel_button";
            this.LoadModel_button.Size = new System.Drawing.Size(182, 89);
            this.LoadModel_button.TabIndex = 26;
            this.LoadModel_button.Text = "Load";
            this.LoadModel_button.UseVisualStyleBackColor = false;
            this.LoadModel_button.Click += new System.EventHandler(this.LoadModel_button_Click);
            // 
            // message_richTextBox
            // 
            this.message_richTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.message_richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.message_richTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.message_richTextBox.EnableAutoDragDrop = true;
            this.message_richTextBox.Font = new System.Drawing.Font("Consolas", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message_richTextBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.message_richTextBox.HideSelection = false;
            this.message_richTextBox.Location = new System.Drawing.Point(232, 17);
            this.message_richTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.message_richTextBox.Name = "message_richTextBox";
            this.message_richTextBox.ReadOnly = true;
            this.message_richTextBox.Size = new System.Drawing.Size(1855, 210);
            this.message_richTextBox.TabIndex = 25;
            this.message_richTextBox.Text = "";
            // 
            // test_button
            // 
            this.test_button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.test_button.BackColor = System.Drawing.SystemColors.HighlightText;
            this.test_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.test_button.Enabled = false;
            this.test_button.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.test_button.FlatAppearance.BorderSize = 2;
            this.test_button.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.test_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.test_button.Font = new System.Drawing.Font("Franklin Gothic Demi", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test_button.ForeColor = System.Drawing.Color.LightSlateGray;
            this.test_button.Location = new System.Drawing.Point(19, 16);
            this.test_button.Margin = new System.Windows.Forms.Padding(5, 20, 5, 2);
            this.test_button.Name = "test_button";
            this.test_button.Size = new System.Drawing.Size(572, 248);
            this.test_button.TabIndex = 2;
            this.test_button.Text = "TEST";
            this.test_button.UseVisualStyleBackColor = false;
            this.test_button.Click += new System.EventHandler(this.test_button_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(2753, 1590);
            this.Controls.Add(this.BG_panel);
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mask Predictor";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.BG_panel.ResumeLayout(false);
            this.result_groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.result_pictureBox)).EndInit();
            this.preview_groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IMG_Box)).EndInit();
            this.display_panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.OpenFileDialog loadIMGDlg;
        private System.Windows.Forms.Panel BG_panel;
        private System.Windows.Forms.PictureBox IMG_Box;
        private System.Windows.Forms.Button test_button;
        private System.Windows.Forms.PictureBox result_pictureBox;
        private System.Windows.Forms.Panel display_panel;
        private System.Windows.Forms.GroupBox result_groupBox;
        private System.Windows.Forms.GroupBox preview_groupBox;
        private System.Windows.Forms.RichTextBox message_richTextBox;
        private System.Windows.Forms.Button LoadModel_button;
    }
}

