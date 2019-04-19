namespace ImageProcessing
{
    partial class MarkDraw
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
            this.MarkDrawGroup1 = new System.Windows.Forms.GroupBox();
            this.MarkLinewidthEdit = new DevExpress.XtraEditors.SpinEdit();
            this.MarkLengthEdit = new DevExpress.XtraEditors.SpinEdit();
            this.MarkColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.MarkLinewidthlab = new System.Windows.Forms.Label();
            this.MarkLengthlab = new System.Windows.Forms.Label();
            this.MarkColorlab = new System.Windows.Forms.Label();
            this.MarkDrawCancelButton = new DevExpress.XtraEditors.SimpleButton();
            this.MarkDrawConfirmButton = new DevExpress.XtraEditors.SimpleButton();
            this.MarkDrawGroup2 = new System.Windows.Forms.GroupBox();
            this.ChooseMarkLineWidthlab = new System.Windows.Forms.Label();
            this.ChooseMarkLengthlab = new System.Windows.Forms.Label();
            this.ChooseMarkColorlab = new System.Windows.Forms.Label();
            this.ChooseMarkLineWidthEdit = new DevExpress.XtraEditors.SpinEdit();
            this.ChooseMarkLengthEdit = new DevExpress.XtraEditors.SpinEdit();
            this.ChooseMarkColorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.MarkDrawGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarkLinewidthEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarkLengthEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarkColorEdit.Properties)).BeginInit();
            this.MarkDrawGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseMarkLineWidthEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseMarkLengthEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseMarkColorEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // MarkDrawGroup1
            // 
            this.MarkDrawGroup1.Controls.Add(this.MarkLinewidthEdit);
            this.MarkDrawGroup1.Controls.Add(this.MarkLengthEdit);
            this.MarkDrawGroup1.Controls.Add(this.MarkColorEdit);
            this.MarkDrawGroup1.Controls.Add(this.MarkLinewidthlab);
            this.MarkDrawGroup1.Controls.Add(this.MarkLengthlab);
            this.MarkDrawGroup1.Controls.Add(this.MarkColorlab);
            this.MarkDrawGroup1.Location = new System.Drawing.Point(14, 14);
            this.MarkDrawGroup1.Name = "MarkDrawGroup1";
            this.MarkDrawGroup1.Size = new System.Drawing.Size(210, 140);
            this.MarkDrawGroup1.TabIndex = 0;
            this.MarkDrawGroup1.TabStop = false;
            this.MarkDrawGroup1.Text = "轮廓点中心";
            // 
            // MarkLinewidthEdit
            // 
            this.MarkLinewidthEdit.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MarkLinewidthEdit.Location = new System.Drawing.Point(90, 102);
            this.MarkLinewidthEdit.Name = "MarkLinewidthEdit";
            this.MarkLinewidthEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MarkLinewidthEdit.Properties.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MarkLinewidthEdit.Properties.IsFloatValue = false;
            this.MarkLinewidthEdit.Properties.Mask.EditMask = "N00";
            this.MarkLinewidthEdit.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.MarkLinewidthEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MarkLinewidthEdit.Size = new System.Drawing.Size(82, 20);
            this.MarkLinewidthEdit.TabIndex = 5;
            // 
            // MarkLengthEdit
            // 
            this.MarkLengthEdit.EditValue = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.MarkLengthEdit.Location = new System.Drawing.Point(90, 67);
            this.MarkLengthEdit.Name = "MarkLengthEdit";
            this.MarkLengthEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MarkLengthEdit.Properties.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MarkLengthEdit.Properties.IsFloatValue = false;
            this.MarkLengthEdit.Properties.Mask.EditMask = "N00";
            this.MarkLengthEdit.Properties.MaxValue = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.MarkLengthEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MarkLengthEdit.Size = new System.Drawing.Size(82, 20);
            this.MarkLengthEdit.TabIndex = 4;
            // 
            // MarkColorEdit
            // 
            this.MarkColorEdit.EditValue = System.Drawing.Color.Red;
            this.MarkColorEdit.Location = new System.Drawing.Point(90, 32);
            this.MarkColorEdit.Name = "MarkColorEdit";
            this.MarkColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MarkColorEdit.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MarkColorEdit.Properties.ShowColorDialog = false;
            this.MarkColorEdit.Properties.ShowSystemColors = false;
            this.MarkColorEdit.Properties.ShowWebColors = false;
            this.MarkColorEdit.Size = new System.Drawing.Size(82, 20);
            this.MarkColorEdit.TabIndex = 3;
            // 
            // MarkLinewidthlab
            // 
            this.MarkLinewidthlab.AutoSize = true;
            this.MarkLinewidthlab.Location = new System.Drawing.Point(7, 104);
            this.MarkLinewidthlab.Name = "MarkLinewidthlab";
            this.MarkLinewidthlab.Size = new System.Drawing.Size(67, 14);
            this.MarkLinewidthlab.TabIndex = 2;
            this.MarkLinewidthlab.Text = "标注线宽：";
            // 
            // MarkLengthlab
            // 
            this.MarkLengthlab.AutoSize = true;
            this.MarkLengthlab.Location = new System.Drawing.Point(7, 69);
            this.MarkLengthlab.Name = "MarkLengthlab";
            this.MarkLengthlab.Size = new System.Drawing.Size(67, 14);
            this.MarkLengthlab.TabIndex = 1;
            this.MarkLengthlab.Text = "标注长度：";
            // 
            // MarkColorlab
            // 
            this.MarkColorlab.AutoSize = true;
            this.MarkColorlab.Location = new System.Drawing.Point(7, 34);
            this.MarkColorlab.Name = "MarkColorlab";
            this.MarkColorlab.Size = new System.Drawing.Size(67, 14);
            this.MarkColorlab.TabIndex = 0;
            this.MarkColorlab.Text = "标注颜色：";
            // 
            // MarkDrawCancelButton
            // 
            this.MarkDrawCancelButton.Location = new System.Drawing.Point(416, 322);
            this.MarkDrawCancelButton.Name = "MarkDrawCancelButton";
            this.MarkDrawCancelButton.Size = new System.Drawing.Size(58, 27);
            this.MarkDrawCancelButton.TabIndex = 1;
            this.MarkDrawCancelButton.Text = "取消";
            // 
            // MarkDrawConfirmButton
            // 
            this.MarkDrawConfirmButton.Location = new System.Drawing.Point(351, 322);
            this.MarkDrawConfirmButton.Name = "MarkDrawConfirmButton";
            this.MarkDrawConfirmButton.Size = new System.Drawing.Size(58, 27);
            this.MarkDrawConfirmButton.TabIndex = 2;
            this.MarkDrawConfirmButton.Text = "确认";
            this.MarkDrawConfirmButton.Click += new System.EventHandler(this.MarkDrawConfirmButton_Click);
            // 
            // MarkDrawGroup2
            // 
            this.MarkDrawGroup2.Controls.Add(this.ChooseMarkLineWidthlab);
            this.MarkDrawGroup2.Controls.Add(this.ChooseMarkLengthlab);
            this.MarkDrawGroup2.Controls.Add(this.ChooseMarkColorlab);
            this.MarkDrawGroup2.Controls.Add(this.ChooseMarkLineWidthEdit);
            this.MarkDrawGroup2.Controls.Add(this.ChooseMarkLengthEdit);
            this.MarkDrawGroup2.Controls.Add(this.ChooseMarkColorEdit);
            this.MarkDrawGroup2.Location = new System.Drawing.Point(262, 14);
            this.MarkDrawGroup2.Name = "MarkDrawGroup2";
            this.MarkDrawGroup2.Size = new System.Drawing.Size(210, 140);
            this.MarkDrawGroup2.TabIndex = 3;
            this.MarkDrawGroup2.TabStop = false;
            this.MarkDrawGroup2.Text = "选中标记中心";
            // 
            // ChooseMarkLineWidthlab
            // 
            this.ChooseMarkLineWidthlab.AutoSize = true;
            this.ChooseMarkLineWidthlab.Location = new System.Drawing.Point(16, 104);
            this.ChooseMarkLineWidthlab.Name = "ChooseMarkLineWidthlab";
            this.ChooseMarkLineWidthlab.Size = new System.Drawing.Size(67, 14);
            this.ChooseMarkLineWidthlab.TabIndex = 5;
            this.ChooseMarkLineWidthlab.Text = "标注线宽：";
            // 
            // ChooseMarkLengthlab
            // 
            this.ChooseMarkLengthlab.AutoSize = true;
            this.ChooseMarkLengthlab.Location = new System.Drawing.Point(16, 69);
            this.ChooseMarkLengthlab.Name = "ChooseMarkLengthlab";
            this.ChooseMarkLengthlab.Size = new System.Drawing.Size(67, 14);
            this.ChooseMarkLengthlab.TabIndex = 4;
            this.ChooseMarkLengthlab.Text = "标注长度：";
            // 
            // ChooseMarkColorlab
            // 
            this.ChooseMarkColorlab.AutoSize = true;
            this.ChooseMarkColorlab.Location = new System.Drawing.Point(16, 34);
            this.ChooseMarkColorlab.Name = "ChooseMarkColorlab";
            this.ChooseMarkColorlab.Size = new System.Drawing.Size(67, 14);
            this.ChooseMarkColorlab.TabIndex = 3;
            this.ChooseMarkColorlab.Text = "标注颜色：";
            // 
            // ChooseMarkLineWidthEdit
            // 
            this.ChooseMarkLineWidthEdit.EditValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.ChooseMarkLineWidthEdit.Location = new System.Drawing.Point(89, 101);
            this.ChooseMarkLineWidthEdit.Name = "ChooseMarkLineWidthEdit";
            this.ChooseMarkLineWidthEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChooseMarkLineWidthEdit.Properties.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ChooseMarkLineWidthEdit.Properties.IsFloatValue = false;
            this.ChooseMarkLineWidthEdit.Properties.Mask.EditMask = "N00";
            this.ChooseMarkLineWidthEdit.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.ChooseMarkLineWidthEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ChooseMarkLineWidthEdit.Size = new System.Drawing.Size(82, 20);
            this.ChooseMarkLineWidthEdit.TabIndex = 2;
            // 
            // ChooseMarkLengthEdit
            // 
            this.ChooseMarkLengthEdit.EditValue = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.ChooseMarkLengthEdit.Location = new System.Drawing.Point(89, 67);
            this.ChooseMarkLengthEdit.Name = "ChooseMarkLengthEdit";
            this.ChooseMarkLengthEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChooseMarkLengthEdit.Properties.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ChooseMarkLengthEdit.Properties.IsFloatValue = false;
            this.ChooseMarkLengthEdit.Properties.Mask.EditMask = "N00";
            this.ChooseMarkLengthEdit.Properties.MaxValue = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.ChooseMarkLengthEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ChooseMarkLengthEdit.Size = new System.Drawing.Size(82, 20);
            this.ChooseMarkLengthEdit.TabIndex = 1;
            // 
            // ChooseMarkColorEdit
            // 
            this.ChooseMarkColorEdit.EditValue = System.Drawing.Color.Blue;
            this.ChooseMarkColorEdit.Location = new System.Drawing.Point(89, 32);
            this.ChooseMarkColorEdit.Name = "ChooseMarkColorEdit";
            this.ChooseMarkColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChooseMarkColorEdit.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ChooseMarkColorEdit.Properties.ShowColorDialog = false;
            this.ChooseMarkColorEdit.Properties.ShowSystemColors = false;
            this.ChooseMarkColorEdit.Properties.ShowWebColors = false;
            this.ChooseMarkColorEdit.Size = new System.Drawing.Size(82, 20);
            this.ChooseMarkColorEdit.TabIndex = 0;
            // 
            // MarkDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.MarkDrawGroup2);
            this.Controls.Add(this.MarkDrawConfirmButton);
            this.Controls.Add(this.MarkDrawCancelButton);
            this.Controls.Add(this.MarkDrawGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MarkDraw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "标记设置";
            this.MarkDrawGroup1.ResumeLayout(false);
            this.MarkDrawGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarkLinewidthEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarkLengthEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarkColorEdit.Properties)).EndInit();
            this.MarkDrawGroup2.ResumeLayout(false);
            this.MarkDrawGroup2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseMarkLineWidthEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseMarkLengthEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseMarkColorEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MarkDrawGroup1;
        private DevExpress.XtraEditors.SpinEdit MarkLinewidthEdit;
        private DevExpress.XtraEditors.SpinEdit MarkLengthEdit;
        private DevExpress.XtraEditors.ColorEdit MarkColorEdit;
        private System.Windows.Forms.Label MarkLinewidthlab;
        private System.Windows.Forms.Label MarkLengthlab;
        private System.Windows.Forms.Label MarkColorlab;
        private DevExpress.XtraEditors.SimpleButton MarkDrawCancelButton;
        private DevExpress.XtraEditors.SimpleButton MarkDrawConfirmButton;
        private System.Windows.Forms.GroupBox MarkDrawGroup2;
        private System.Windows.Forms.Label ChooseMarkLineWidthlab;
        private System.Windows.Forms.Label ChooseMarkLengthlab;
        private System.Windows.Forms.Label ChooseMarkColorlab;
        private DevExpress.XtraEditors.SpinEdit ChooseMarkLineWidthEdit;
        private DevExpress.XtraEditors.SpinEdit ChooseMarkLengthEdit;
        private DevExpress.XtraEditors.ColorEdit ChooseMarkColorEdit;
    }
}