using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;//必须添加，不然DllImport报错
using System.Threading;
using System.Collections;

namespace ImageProcessing
{
    public partial class Unzip : DevExpress.XtraEditors.XtraForm
    {
        public enum UnzipObject
        {
            ImageBin = 1,
            FeatureBin = 2
        }
        UnzipObject ObjectChoose;
        private delegate void choosefiles(ArrayList filenames, UnzipObject UnzipObject);
        private List<string> binfilenames = new List<string>();

        public Unzip()
        {
            InitializeComponent();
        }

        //UnzipThread子线程，调用BinUnzip函数
        Thread UnzipThread = null;
        private void UnzipBinfiles()
        {
            try
            {
                BinUnzip(binfilenames, ObjectChoose);
            }
            catch (System.Exception e1)
            {
                return;
            }
            closeThread();
        }

        //结束子线程  
        private void closeThread()
        {
            if (UnzipThread != null)
            {
                if (UnzipThread.IsAlive)
                {
                    UnzipThread.Abort();
                }
            }
        }

        /// <summary>
        /// 子线程实际执行函数
        /// </summary> 
        private void BinUnzip(List<string> binfilenames, UnzipObject UnzipObject)
        {
            string UnzipPath = System.IO.Path.GetDirectoryName(binfilenames[0]);//获取文件所在路径
            ArrayList BinfileIndex = new ArrayList();
            switch (UnzipObject)
            {
                case UnzipObject.ImageBin:
                    foreach (string file in binfilenames)
                    {
                        BinfileIndex.Add(Convert.ToInt32(System.IO.Path.GetFileNameWithoutExtension(file)));//获取文件编号
                    }
                    Program.UnzipMultiImgBins(UnzipPath, binfilenames.Count, (int[])BinfileIndex.ToArray(typeof(int)));//调用解包函数
                    DevExpress.XtraEditors.XtraMessageBox.Show("解压完成");
                    break;
                case UnzipObject.FeatureBin:
                    foreach (string file in binfilenames)
                    {
                        BinfileIndex.Add(Convert.ToInt32(System.IO.Path.GetFileNameWithoutExtension(file)));//获取文件编号
                    }
                    Program.UnzipMultiFeatureBins(UnzipPath, binfilenames.Count, (int[])BinfileIndex.ToArray(typeof(int)));//调用解包函数
                    DevExpress.XtraEditors.XtraMessageBox.Show("解压完成");
                    break;
                default:break;
            }
        }

        /// <summary>
        /// 选择图片压缩文件事件
        /// </summary>
        private void ImgBinButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".bin";
            dlg.Filter = "图片打包文件|*.bin";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImgUnzipList.Clear();//图像打包文本框清空
                FeatureUnzipList.Clear();//特征打包文件文本框清空
                ObjectChoose = UnzipObject.ImageBin;
                binfilenames.Clear();
                foreach (string file in dlg.FileNames)
                {
                    binfilenames.Add(file);
                    ImgUnzipList.AppendText(file);//显示文件名
                    ImgUnzipList.AppendText(Environment.NewLine);//换行
                }
            }
        }

        private void FeatureBinButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".bin";
            dlg.Filter = "特征打包文件|*.bin";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImgUnzipList.Clear();//图像打包文本框清空
                FeatureUnzipList.Clear();//特征打包文件文本框清空
                ObjectChoose = UnzipObject.FeatureBin;
                binfilenames.Clear();
                foreach (string file in dlg.FileNames)
                {
                    binfilenames.Add(file);
                    FeatureUnzipList.AppendText(file);//显示文件名
                    FeatureUnzipList.AppendText(Environment.NewLine);//换行
                }
            }
        }

        private void ImgUnzip_Click(object sender, EventArgs e)
        {
            if (ObjectChoose != UnzipObject.ImageBin)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择文件解压");
                return;
            }
            if (DevExpress.XtraEditors.XtraMessageBox.Show("解压文件是否选择正确?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                closeThread();
                UnzipThread = new Thread(new ThreadStart(UnzipBinfiles));//子线程解压
                UnzipThread.IsBackground = true;
                UnzipThread.Start();
            }
        }

        private void FeatureUnzip_Click(object sender, EventArgs e)
        {
            if(ObjectChoose != UnzipObject.FeatureBin)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择图像解压");
                return;
            }
            if (DevExpress.XtraEditors.XtraMessageBox.Show("解压文件是否选择正确?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                closeThread();
                UnzipThread = new Thread(new ThreadStart(UnzipBinfiles));//子线程解压
                UnzipThread.IsBackground = true;
                UnzipThread.Start();
            }
        }
    }
}
