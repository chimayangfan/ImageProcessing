namespace ImageProcessing
{
    partial class Unzip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.FeatureUnzipBox = new System.Windows.Forms.GroupBox();
            this.FeatureUnzipButton = new DevExpress.XtraEditors.SimpleButton();
            this.FeatureUnzipList = new System.Windows.Forms.TextBox();
            this.FeatureBinButton = new DevExpress.XtraEditors.SimpleButton();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.ImgUnzipBox = new System.Windows.Forms.GroupBox();
            this.ImgUnzipButton = new DevExpress.XtraEditors.SimpleButton();
            this.ImgUnzipList = new System.Windows.Forms.TextBox();
            this.ImgBinButton = new DevExpress.XtraEditors.SimpleButton();
            this.FeatureUnzipBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.ImgUnzipBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // FeatureUnzipBox
            // 
            this.FeatureUnzipBox.Controls.Add(this.FeatureUnzipButton);
            this.FeatureUnzipBox.Controls.Add(this.FeatureUnzipList);
            this.FeatureUnzipBox.Controls.Add(this.FeatureBinButton);
            this.FeatureUnzipBox.Location = new System.Drawing.Point(273, 12);
            this.FeatureUnzipBox.Name = "FeatureUnzipBox";
            this.FeatureUnzipBox.Size = new System.Drawing.Size(255, 243);
            this.FeatureUnzipBox.TabIndex = 1;
            this.FeatureUnzipBox.TabStop = false;
            this.FeatureUnzipBox.Text = "特征解压";
            // 
            // FeatureUnzipButton
            // 
            this.FeatureUnzipButton.Location = new System.Drawing.Point(57, 201);
            this.FeatureUnzipButton.Name = "FeatureUnzipButton";
            this.FeatureUnzipButton.Size = new System.Drawing.Size(139, 27);
            this.FeatureUnzipButton.TabIndex = 2;
            this.FeatureUnzipButton.Text = "特征解压";
            this.FeatureUnzipButton.Click += new System.EventHandler(this.FeatureUnzip_Click);
            // 
            // FeatureUnzipList
            // 
            this.FeatureUnzipList.Location = new System.Drawing.Point(7, 23);
            this.FeatureUnzipList.Multiline = true;
            this.FeatureUnzipList.Name = "FeatureUnzipList";
            this.FeatureUnzipList.Size = new System.Drawing.Size(241, 121);
            this.FeatureUnzipList.TabIndex = 1;
            // 
            // FeatureBinButton
            // 
            this.FeatureBinButton.Location = new System.Drawing.Point(57, 167);
            this.FeatureBinButton.Name = "FeatureBinButton";
            this.FeatureBinButton.Size = new System.Drawing.Size(139, 27);
            this.FeatureBinButton.TabIndex = 0;
            this.FeatureBinButton.Text = "选择特征压缩文件";
            this.FeatureBinButton.Click += new System.EventHandler(this.FeatureBinButton_Click);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // ImgUnzipBox
            // 
            this.ImgUnzipBox.Controls.Add(this.ImgUnzipButton);
            this.ImgUnzipBox.Controls.Add(this.ImgUnzipList);
            this.ImgUnzipBox.Controls.Add(this.ImgBinButton);
            this.ImgUnzipBox.Location = new System.Drawing.Point(12, 12);
            this.ImgUnzipBox.Name = "ImgUnzipBox";
            this.ImgUnzipBox.Size = new System.Drawing.Size(255, 243);
            this.ImgUnzipBox.TabIndex = 0;
            this.ImgUnzipBox.TabStop = false;
            this.ImgUnzipBox.Text = "图片解压";
            // 
            // ImgUnzipButton
            // 
            this.ImgUnzipButton.Location = new System.Drawing.Point(57, 201);
            this.ImgUnzipButton.Name = "ImgUnzipButton";
            this.ImgUnzipButton.Size = new System.Drawing.Size(139, 27);
            this.ImgUnzipButton.TabIndex = 2;
            this.ImgUnzipButton.Text = "图像解压";
            this.ImgUnzipButton.Click += new System.EventHandler(this.ImgUnzip_Click);
            // 
            // ImgUnzipList
            // 
            this.ImgUnzipList.Location = new System.Drawing.Point(7, 23);
            this.ImgUnzipList.Multiline = true;
            this.ImgUnzipList.Name = "ImgUnzipList";
            this.ImgUnzipList.Size = new System.Drawing.Size(241, 121);
            this.ImgUnzipList.TabIndex = 1;
            // 
            // ImgBinButton
            // 
            this.ImgBinButton.Location = new System.Drawing.Point(57, 167);
            this.ImgBinButton.Name = "ImgBinButton";
            this.ImgBinButton.Size = new System.Drawing.Size(139, 27);
            this.ImgBinButton.TabIndex = 0;
            this.ImgBinButton.Text = "选择图片压缩文件";
            this.ImgBinButton.Click += new System.EventHandler(this.ImgBinButton_Click);
            // 
            // Unzip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 361);
            this.Controls.Add(this.ImgUnzipBox);
            this.Controls.Add(this.FeatureUnzipBox);
            this.Name = "Unzip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文件解压";
            this.FeatureUnzipBox.ResumeLayout(false);
            this.FeatureUnzipBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ImgUnzipBox.ResumeLayout(false);
            this.ImgUnzipBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private System.Windows.Forms.GroupBox FeatureUnzipBox;
        private DevExpress.XtraEditors.SimpleButton FeatureUnzipButton;
        private System.Windows.Forms.TextBox FeatureUnzipList;
        private DevExpress.XtraEditors.SimpleButton FeatureBinButton;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.GroupBox ImgUnzipBox;
        private DevExpress.XtraEditors.SimpleButton ImgUnzipButton;
        private System.Windows.Forms.TextBox ImgUnzipList;
        private DevExpress.XtraEditors.SimpleButton ImgBinButton;
    }
}