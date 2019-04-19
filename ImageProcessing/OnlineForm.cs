using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;   //必须添加，不然DllImport报错
using System.Threading;
using static ImageProcessing.Form1;

namespace ImageProcessing
{
    /// <summary>
    /// 声明委托
    /// 在线实验窗口向主窗口发送异步请求
    /// </summary>
    public delegate void TransfDelegate();
    public partial class OnlineForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 在线实验界面初始化
        /// </summary>
        public OnlineForm()
        {
            InitializeComponent();
        }

        public string ImagePath
        {
            get { return ImgSequencePathbutton.Text; }
            set {  }
        }

        /// <summary>
        /// 文件夹导入按键事件
        /// </summary>
        private void ImgFolderPath_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Form1.MemoryFlag == true)
            {
                Program.Memory_release();//释放上幅图申请的内存
                Form1.MemoryFlag = false;
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                DevExpress.XtraEditors.XtraMessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ImgSequencePathbutton.Text = foldPath;
                Form1.FolderPath = foldPath;
                Program.Memory_application();
                Form1.MemoryFlag = true;
            }
        }

        /// <summary>
        /// 委托事件
        /// 在线实验窗口向主窗口发送异步请求
        /// </summary>
        public event TransfDelegate InitEvent;
        /// <summary>
        /// 确认按键事件
        /// </summary>
        private void Confirm_Click(object sender, EventArgs e)
        {
            //DialogResult ImgImportFlag = 0;
            //if (ImgImportFlag == DialogResult.OK)
            //{
            //    return;//已导入图像，再按无效
            //}
            //if (this.ImgSequencePathbutton.Text == string.Empty)
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show("图像序列路径未设置！");
            //}
            //else
            //{
            //    //将路径下的bmp图片导入到内存
            //    ImgImportStatusLabel.Text = "正在运行";
            //    this.Refresh();//界面显示刷新
            //    if(Form1.ImgProcessingNumbers != 0)
            //    {
            //        Form1.ImgProcessingNumbers = Program.Image_Pretreatment(ImgSequencePathbutton.Text, "*.bmp", 2);//释放前一次导入图片
            //    }
            //    Form1.ImgProcessingNumbers = Program.Image_Pretreatment(ImgSequencePathbutton.Text , "*.bmp", 1);
            //    if (Form1.ImgProcessingNumbers == 0)
            //    {
            //        DevExpress.XtraEditors.XtraMessageBox.Show("该路径下无正确格式图像！");
            //    }
            //    else
            //    {                    
            //        ImgImportFlag = DevExpress.XtraEditors.XtraMessageBox.Show("成功导入" + Form1.ImgProcessingNumbers + "张图像！");
            //        Form1.MemoryFlag = true;
            //        if (ImgImportFlag == DialogResult.OK)
            //        {
            //            Form1.FolderPath = ImgSequencePathbutton.Text;
            //            //Form1.InitRightTree();//加载图像序列树视图
            //            //触发事件
            //            InitEvent();
            //            this.Close(); //----关闭窗体
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 取消按键事件
        /// </summary>
        private void Cancel_Click(object sender, EventArgs e)
        {
            if (Form1.MemoryFlag == true)
            {
                Program.Memory_release();//释放上幅图申请的内存
                Form1.MemoryFlag = false;
            }
            this.Close(); //----关闭窗体
        }

    }
}
