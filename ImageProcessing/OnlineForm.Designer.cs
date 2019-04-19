namespace ImageProcessing
{
    partial class OnlineForm
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
            this.Debuglabel = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ImgImportStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ImgFolderPath = new DevExpress.XtraEditors.LabelControl();
            this.Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.Confirm = new DevExpress.XtraEditors.SimpleButton();
            this.ImgSequencePathbutton = new DevExpress.XtraEditors.ButtonEdit();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgSequencePathbutton.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Debuglabel
            // 
            this.Debuglabel.AutoSize = true;
            this.Debuglabel.Location = new System.Drawing.Point(17, 31);
            this.Debuglabel.Name = "Debuglabel";
            this.Debuglabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Debuglabel.Size = new System.Drawing.Size(355, 42);
            this.Debuglabel.TabIndex = 0;
            this.Debuglabel.Text = "注释：模拟标志点实时提取、图像压缩操作。实时图像数据是直接\r\n由内存导入到显存中，所以模拟情况下需先将图片序列加载到内存\r\n中。";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.statusStrip1);
            this.panelControl1.Controls.Add(this.ImgFolderPath);
            this.panelControl1.Controls.Add(this.Cancel);
            this.panelControl1.Controls.Add(this.Confirm);
            this.panelControl1.Controls.Add(this.ImgSequencePathbutton);
            this.panelControl1.Controls.Add(this.Debuglabel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(384, 231);
            this.panelControl1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImgImportStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(2, 207);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(380, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ImgImportStatusLabel
            // 
            this.ImgImportStatusLabel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ImgImportStatusLabel.Name = "ImgImportStatusLabel";
            this.ImgImportStatusLabel.Size = new System.Drawing.Size(55, 17);
            this.ImgImportStatusLabel.Text = "准备就绪";
            // 
            // ImgFolderPath
            // 
            this.ImgFolderPath.Location = new System.Drawing.Point(44, 94);
            this.ImgFolderPath.Name = "ImgFolderPath";
            this.ImgFolderPath.Size = new System.Drawing.Size(84, 14);
            this.ImgFolderPath.TabIndex = 5;
            this.ImgFolderPath.Text = "图像序列路径：";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(322, 181);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(50, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "取消";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Confirm
            // 
            this.Confirm.Location = new System.Drawing.Point(266, 181);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(50, 23);
            this.Confirm.TabIndex = 3;
            this.Confirm.Text = "确认";
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // ImgSequencePathbutton
            // 
            this.ImgSequencePathbutton.Location = new System.Drawing.Point(134, 91);
            this.ImgSequencePathbutton.Name = "ImgSequencePathbutton";
            this.ImgSequencePathbutton.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ImgSequencePathbutton.Size = new System.Drawing.Size(203, 20);
            this.ImgSequencePathbutton.TabIndex = 2;
            this.ImgSequencePathbutton.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ImgFolderPath_Click);
            // 
            // OnlineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 231);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OnlineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "模拟在线实验";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgSequencePathbutton.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Debuglabel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ImgImportStatusLabel;
        private DevExpress.XtraEditors.LabelControl ImgFolderPath;
        private DevExpress.XtraEditors.SimpleButton Cancel;
        public DevExpress.XtraEditors.SimpleButton Confirm;
        private DevExpress.XtraEditors.ButtonEdit ImgSequencePathbutton;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}