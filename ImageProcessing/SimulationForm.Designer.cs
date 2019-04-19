namespace ImageProcessing
{
    partial class SimulationForm
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
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ParmeterImportButton = new DevExpress.XtraEditors.SimpleButton();
            this.ParmeterExportButton = new DevExpress.XtraEditors.SimpleButton();
            this.Confirm = new DevExpress.XtraEditors.SimpleButton();
            this.Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.SinulationParmeterGroup = new DevExpress.XtraEditors.GroupControl();
            this.GPUBlockSizeBox = new DevExpress.XtraEditors.ComboBoxEdit();
            this.GPUBlockSizeLab = new DevExpress.XtraEditors.LabelControl();
            this.CompressionRatioBox = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CompressionRatioLab = new DevExpress.XtraEditors.LabelControl();
            this.MaxAreaBox = new DevExpress.XtraEditors.TextEdit();
            this.MinAreaBox = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.AreaRangelabel = new DevExpress.XtraEditors.LabelControl();
            this.CameraTypeBoxTest = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CameraTypeLab = new DevExpress.XtraEditors.LabelControl();
            this.MaxLengthBox = new DevExpress.XtraEditors.TextEdit();
            this.MinLengthBox = new DevExpress.XtraEditors.TextEdit();
            this.ImgThresholdBox = new DevExpress.XtraEditors.TextEdit();
            this.ImgWidthBox = new DevExpress.XtraEditors.TextEdit();
            this.ImgHeightBox = new DevExpress.XtraEditors.TextEdit();
            this.Tolabel1 = new DevExpress.XtraEditors.LabelControl();
            this.Lengthlabel = new DevExpress.XtraEditors.LabelControl();
            this.ImgThresholdlabel = new DevExpress.XtraEditors.LabelControl();
            this.ImageWidthlabel = new DevExpress.XtraEditors.LabelControl();
            this.ImageHeightlabel = new DevExpress.XtraEditors.LabelControl();
            this.PathBox = new DevExpress.XtraEditors.TextEdit();
            this.ImagePath = new DevExpress.XtraEditors.LabelControl();
            this.SinulationReportGroup = new DevExpress.XtraEditors.GroupControl();
            this.ReportlistView = new System.Windows.Forms.ListView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SinulationParmeterGroup)).BeginInit();
            this.SinulationParmeterGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GPUBlockSizeBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompressionRatioBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAreaBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinAreaBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTypeBoxTest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxLengthBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinLengthBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgThresholdBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgWidthBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgHeightBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinulationReportGroup)).BeginInit();
            this.SinulationReportGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // ParmeterImportButton
            // 
            this.ParmeterImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ParmeterImportButton.Location = new System.Drawing.Point(13, 5);
            this.ParmeterImportButton.Name = "ParmeterImportButton";
            this.ParmeterImportButton.Size = new System.Drawing.Size(75, 23);
            this.ParmeterImportButton.TabIndex = 0;
            this.ParmeterImportButton.Text = "参数导入";
            this.ParmeterImportButton.Click += new System.EventHandler(this.ParmeterImportbutton_Click);
            // 
            // ParmeterExportButton
            // 
            this.ParmeterExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ParmeterExportButton.Location = new System.Drawing.Point(94, 5);
            this.ParmeterExportButton.Name = "ParmeterExportButton";
            this.ParmeterExportButton.Size = new System.Drawing.Size(75, 23);
            this.ParmeterExportButton.TabIndex = 1;
            this.ParmeterExportButton.Text = "参数导出";
            this.ParmeterExportButton.Click += new System.EventHandler(this.parmeterexportbutton_Click);
            // 
            // Confirm
            // 
            this.Confirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Confirm.Location = new System.Drawing.Point(551, 5);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(50, 23);
            this.Confirm.TabIndex = 2;
            this.Confirm.Text = "确认";
            this.Confirm.Click += new System.EventHandler(this.confirmbutton_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(607, 5);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(50, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "取消";
            this.Cancel.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.SinulationParmeterGroup);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.SinulationReportGroup);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(664, 326);
            this.splitContainerControl1.SplitterPosition = 240;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // SinulationParmeterGroup
            // 
            this.SinulationParmeterGroup.Controls.Add(this.GPUBlockSizeBox);
            this.SinulationParmeterGroup.Controls.Add(this.GPUBlockSizeLab);
            this.SinulationParmeterGroup.Controls.Add(this.CompressionRatioBox);
            this.SinulationParmeterGroup.Controls.Add(this.CompressionRatioLab);
            this.SinulationParmeterGroup.Controls.Add(this.MaxAreaBox);
            this.SinulationParmeterGroup.Controls.Add(this.MinAreaBox);
            this.SinulationParmeterGroup.Controls.Add(this.labelControl1);
            this.SinulationParmeterGroup.Controls.Add(this.AreaRangelabel);
            this.SinulationParmeterGroup.Controls.Add(this.CameraTypeBoxTest);
            this.SinulationParmeterGroup.Controls.Add(this.CameraTypeLab);
            this.SinulationParmeterGroup.Controls.Add(this.MaxLengthBox);
            this.SinulationParmeterGroup.Controls.Add(this.MinLengthBox);
            this.SinulationParmeterGroup.Controls.Add(this.ImgThresholdBox);
            this.SinulationParmeterGroup.Controls.Add(this.ImgWidthBox);
            this.SinulationParmeterGroup.Controls.Add(this.ImgHeightBox);
            this.SinulationParmeterGroup.Controls.Add(this.Tolabel1);
            this.SinulationParmeterGroup.Controls.Add(this.Lengthlabel);
            this.SinulationParmeterGroup.Controls.Add(this.ImgThresholdlabel);
            this.SinulationParmeterGroup.Controls.Add(this.ImageWidthlabel);
            this.SinulationParmeterGroup.Controls.Add(this.ImageHeightlabel);
            this.SinulationParmeterGroup.Controls.Add(this.PathBox);
            this.SinulationParmeterGroup.Controls.Add(this.ImagePath);
            this.SinulationParmeterGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SinulationParmeterGroup.Location = new System.Drawing.Point(0, 0);
            this.SinulationParmeterGroup.Name = "SinulationParmeterGroup";
            this.SinulationParmeterGroup.Size = new System.Drawing.Size(240, 326);
            this.SinulationParmeterGroup.TabIndex = 0;
            this.SinulationParmeterGroup.Text = "测试参数";
            // 
            // GPUBlockSizeBox
            // 
            this.GPUBlockSizeBox.Location = new System.Drawing.Point(89, 268);
            this.GPUBlockSizeBox.Name = "GPUBlockSizeBox";
            this.GPUBlockSizeBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GPUBlockSizeBox.Size = new System.Drawing.Size(66, 20);
            this.GPUBlockSizeBox.TabIndex = 21;
            this.GPUBlockSizeBox.SelectedIndexChanged += new System.EventHandler(this.GPUBlockSizeBox_SelectedIndexChanged);
            // 
            // GPUBlockSizeLab
            // 
            this.GPUBlockSizeLab.Location = new System.Drawing.Point(12, 271);
            this.GPUBlockSizeLab.Name = "GPUBlockSizeLab";
            this.GPUBlockSizeLab.Size = new System.Drawing.Size(71, 14);
            this.GPUBlockSizeLab.TabIndex = 20;
            this.GPUBlockSizeLab.Text = "GPU线程块：";
            // 
            // CompressionRatioBox
            // 
            this.CompressionRatioBox.Location = new System.Drawing.Point(89, 301);
            this.CompressionRatioBox.Name = "CompressionRatioBox";
            this.CompressionRatioBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CompressionRatioBox.Size = new System.Drawing.Size(66, 20);
            this.CompressionRatioBox.TabIndex = 19;
            this.CompressionRatioBox.SelectedIndexChanged += new System.EventHandler(this.CompressionRatioBox_SelectedIndexChanged);
            // 
            // CompressionRatioLab
            // 
            this.CompressionRatioLab.Location = new System.Drawing.Point(11, 304);
            this.CompressionRatioLab.Name = "CompressionRatioLab";
            this.CompressionRatioLab.Size = new System.Drawing.Size(72, 14);
            this.CompressionRatioLab.TabIndex = 18;
            this.CompressionRatioLab.Text = "图像压缩比：";
            // 
            // MaxAreaBox
            // 
            this.MaxAreaBox.Location = new System.Drawing.Point(153, 234);
            this.MaxAreaBox.Name = "MaxAreaBox";
            this.MaxAreaBox.Size = new System.Drawing.Size(50, 20);
            this.MaxAreaBox.TabIndex = 17;
            this.MaxAreaBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // MinAreaBox
            // 
            this.MinAreaBox.Location = new System.Drawing.Point(79, 234);
            this.MinAreaBox.Name = "MinAreaBox";
            this.MinAreaBox.Size = new System.Drawing.Size(50, 20);
            this.MinAreaBox.TabIndex = 16;
            this.MinAreaBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(135, 237);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "到";
            // 
            // AreaRangelabel
            // 
            this.AreaRangelabel.Location = new System.Drawing.Point(13, 237);
            this.AreaRangelabel.Name = "AreaRangelabel";
            this.AreaRangelabel.Size = new System.Drawing.Size(60, 14);
            this.AreaRangelabel.TabIndex = 14;
            this.AreaRangelabel.Text = "面积范围：";
            // 
            // CameraTypeBoxTest
            // 
            this.CameraTypeBoxTest.Location = new System.Drawing.Point(79, 72);
            this.CameraTypeBoxTest.Name = "CameraTypeBoxTest";
            this.CameraTypeBoxTest.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CameraTypeBoxTest.Size = new System.Drawing.Size(100, 20);
            this.CameraTypeBoxTest.TabIndex = 13;
            this.CameraTypeBoxTest.SelectedIndexChanged += new System.EventHandler(this.CameraTypeBoxTest_SelectedIndexChanged);
            // 
            // CameraTypeLab
            // 
            this.CameraTypeLab.Location = new System.Drawing.Point(12, 73);
            this.CameraTypeLab.Name = "CameraTypeLab";
            this.CameraTypeLab.Size = new System.Drawing.Size(60, 14);
            this.CameraTypeLab.TabIndex = 12;
            this.CameraTypeLab.Text = "相机类型：";
            // 
            // MaxLengthBox
            // 
            this.MaxLengthBox.Location = new System.Drawing.Point(153, 201);
            this.MaxLengthBox.Name = "MaxLengthBox";
            this.MaxLengthBox.Size = new System.Drawing.Size(50, 20);
            this.MaxLengthBox.TabIndex = 11;
            this.MaxLengthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // MinLengthBox
            // 
            this.MinLengthBox.Location = new System.Drawing.Point(79, 201);
            this.MinLengthBox.Name = "MinLengthBox";
            this.MinLengthBox.Size = new System.Drawing.Size(50, 20);
            this.MinLengthBox.TabIndex = 10;
            this.MinLengthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // ImgThresholdBox
            // 
            this.ImgThresholdBox.Location = new System.Drawing.Point(113, 170);
            this.ImgThresholdBox.Name = "ImgThresholdBox";
            this.ImgThresholdBox.Size = new System.Drawing.Size(66, 20);
            this.ImgThresholdBox.TabIndex = 9;
            this.ImgThresholdBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // ImgWidthBox
            // 
            this.ImgWidthBox.Location = new System.Drawing.Point(79, 137);
            this.ImgWidthBox.Name = "ImgWidthBox";
            this.ImgWidthBox.Size = new System.Drawing.Size(100, 20);
            this.ImgWidthBox.TabIndex = 8;
            this.ImgWidthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // ImgHeightBox
            // 
            this.ImgHeightBox.Location = new System.Drawing.Point(79, 106);
            this.ImgHeightBox.Name = "ImgHeightBox";
            this.ImgHeightBox.Size = new System.Drawing.Size(100, 20);
            this.ImgHeightBox.TabIndex = 7;
            this.ImgHeightBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // Tolabel1
            // 
            this.Tolabel1.Location = new System.Drawing.Point(135, 204);
            this.Tolabel1.Name = "Tolabel1";
            this.Tolabel1.Size = new System.Drawing.Size(12, 14);
            this.Tolabel1.TabIndex = 6;
            this.Tolabel1.Text = "到";
            // 
            // Lengthlabel
            // 
            this.Lengthlabel.Location = new System.Drawing.Point(13, 204);
            this.Lengthlabel.Name = "Lengthlabel";
            this.Lengthlabel.Size = new System.Drawing.Size(60, 14);
            this.Lengthlabel.TabIndex = 5;
            this.Lengthlabel.Text = "周长范围：";
            // 
            // ImgThresholdlabel
            // 
            this.ImgThresholdlabel.Location = new System.Drawing.Point(13, 173);
            this.ImgThresholdlabel.Name = "ImgThresholdlabel";
            this.ImgThresholdlabel.Size = new System.Drawing.Size(96, 14);
            this.ImgThresholdlabel.TabIndex = 4;
            this.ImgThresholdlabel.Text = "图像二值化阈值：";
            // 
            // ImageWidthlabel
            // 
            this.ImageWidthlabel.Location = new System.Drawing.Point(13, 140);
            this.ImageWidthlabel.Name = "ImageWidthlabel";
            this.ImageWidthlabel.Size = new System.Drawing.Size(60, 14);
            this.ImageWidthlabel.TabIndex = 3;
            this.ImageWidthlabel.Text = "图像宽度：";
            // 
            // ImageHeightlabel
            // 
            this.ImageHeightlabel.Location = new System.Drawing.Point(13, 109);
            this.ImageHeightlabel.Name = "ImageHeightlabel";
            this.ImageHeightlabel.Size = new System.Drawing.Size(60, 14);
            this.ImageHeightlabel.TabIndex = 2;
            this.ImageHeightlabel.Text = "图像高度：";
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(12, 44);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(200, 20);
            this.PathBox.TabIndex = 1;
            // 
            // ImagePath
            // 
            this.ImagePath.Location = new System.Drawing.Point(12, 24);
            this.ImagePath.Name = "ImagePath";
            this.ImagePath.Size = new System.Drawing.Size(84, 14);
            this.ImagePath.TabIndex = 0;
            this.ImagePath.Text = "测试图片路径：";
            // 
            // SinulationReportGroup
            // 
            this.SinulationReportGroup.Controls.Add(this.ReportlistView);
            this.SinulationReportGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SinulationReportGroup.Location = new System.Drawing.Point(0, 0);
            this.SinulationReportGroup.Name = "SinulationReportGroup";
            this.SinulationReportGroup.Size = new System.Drawing.Size(419, 326);
            this.SinulationReportGroup.TabIndex = 0;
            this.SinulationReportGroup.Text = "仿真报告";
            // 
            // ReportlistView
            // 
            this.ReportlistView.BackColor = System.Drawing.Color.White;
            this.ReportlistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportlistView.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ReportlistView.Location = new System.Drawing.Point(2, 21);
            this.ReportlistView.Name = "ReportlistView";
            this.ReportlistView.Size = new System.Drawing.Size(415, 303);
            this.ReportlistView.TabIndex = 0;
            this.ReportlistView.UseCompatibleStateImageBehavior = false;
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.Cancel);
            this.panelControl1.Controls.Add(this.Confirm);
            this.panelControl1.Controls.Add(this.ParmeterExportButton);
            this.panelControl1.Controls.Add(this.ParmeterImportButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 326);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(664, 35);
            this.panelControl1.TabIndex = 2;
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 361);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "SimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "仿真测试界面";
            this.Load += new System.EventHandler(this.simulationform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SinulationParmeterGroup)).EndInit();
            this.SinulationParmeterGroup.ResumeLayout(false);
            this.SinulationParmeterGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GPUBlockSizeBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompressionRatioBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAreaBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinAreaBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTypeBoxTest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxLengthBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinLengthBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgThresholdBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgWidthBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgHeightBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinulationReportGroup)).EndInit();
            this.SinulationReportGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraEditors.SimpleButton Cancel;
        private DevExpress.XtraEditors.SimpleButton Confirm;
        private DevExpress.XtraEditors.SimpleButton ParmeterExportButton;
        private DevExpress.XtraEditors.SimpleButton ParmeterImportButton;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl SinulationParmeterGroup;
        private DevExpress.XtraEditors.TextEdit MaxLengthBox;
        private DevExpress.XtraEditors.TextEdit MinLengthBox;
        private DevExpress.XtraEditors.TextEdit ImgThresholdBox;
        private DevExpress.XtraEditors.TextEdit ImgWidthBox;
        private DevExpress.XtraEditors.TextEdit ImgHeightBox;
        private DevExpress.XtraEditors.LabelControl Tolabel1;
        private DevExpress.XtraEditors.LabelControl Lengthlabel;
        private DevExpress.XtraEditors.LabelControl ImgThresholdlabel;
        private DevExpress.XtraEditors.LabelControl ImageWidthlabel;
        private DevExpress.XtraEditors.LabelControl ImageHeightlabel;
        private DevExpress.XtraEditors.LabelControl ImagePath;
        private DevExpress.XtraEditors.GroupControl SinulationReportGroup;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ListView ReportlistView;
        public DevExpress.XtraEditors.TextEdit PathBox;
        private DevExpress.XtraEditors.ComboBoxEdit CameraTypeBoxTest;
        private DevExpress.XtraEditors.LabelControl CameraTypeLab;
        private DevExpress.XtraEditors.TextEdit MaxAreaBox;
        private DevExpress.XtraEditors.TextEdit MinAreaBox;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl AreaRangelabel;
        private DevExpress.XtraEditors.ComboBoxEdit CompressionRatioBox;
        private DevExpress.XtraEditors.LabelControl CompressionRatioLab;
        private DevExpress.XtraEditors.LabelControl GPUBlockSizeLab;
        private DevExpress.XtraEditors.ComboBoxEdit GPUBlockSizeBox;
    }
}