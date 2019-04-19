using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    /// <summary>
    /// 声明委托
    /// 参数设置窗口向主窗口发送异步请求
    /// </summary>
    public delegate void EditDelegate(int[] modifyparmeter);
    public partial class EditForm : DevExpress.XtraEditors.XtraForm
    {
        private bool ParameterLocked = false;

        /// <summary>
        /// 参数设置界面初始化
        /// </summary>
        public EditForm()
        {
            InitializeComponent();           
        }

        /// <summary>
        /// 参数设置界面加载事件
        /// </summary>
        private void Editform_Load(object sender, EventArgs e)
        {
            //参数设置未锁定，点击确认按键后锁定设置
            if (ParameterLocked == false)
            {
                //仿真测试完成后参数导入文本框显示
                PixelWidthBox.Text = Form1.ImageWidth.ToString();
                PixelHeightBox.Text = Form1.ImageHeight.ToString();
                ImgThresholdspin.Text = Form1.ImgThreshold.ToString();
                LengthMinspin.Text = Form1.LengthMin.ToString();
                LengthMaxspin.Text = Form1.LengthMax.ToString();
                AreaMinspin.Text = Form1.AreaMin.ToString();
                AreaMaxspin.Text = Form1.AreaMax.ToString();
                Bufferlengthspin.Value = Properties.Settings.Default.Bufferlength;//读取默认属性表
                ImageReadPathEdit.Text = Properties.Settings.Default.ImgReadPath;//读取默认属性表
                ImageSavePathEdit.Text = Properties.Settings.Default.ImgSavePath;//读取默认属性表
                DataSavePathEdit.Text = Properties.Settings.Default.DataSavePath;//读取默认属性表
            }
            if (Form1.ParameterReady == true)
            {
                Form1.ParameterReady = false;
            }
            //手工添加CameraTypeBox的数据源 
            CameraTypeBox.Properties.Items.Clear();
            foreach (DataRow row in this.CameraData.Rows)
            {
                CameraTypeBox.Properties.Items.Add(row["CameraType"]);
            }
            CameraTypeBox.SelectedIndex = 0;//默认选择8位黑白
            //CameraTypeBox.SelectedIndex = 1;//默认选择24位彩色
            //手工添加CompressionBox的数据源 
            CompressionBox.Properties.Items.Clear();
            foreach (DataRow row in this.CompressionData.Rows)
            {
                CompressionBox.Properties.Items.Add(row["CompressionRatio"]);
            }
            CompressionBox.SelectedIndex = 2;//默认选择1:20
            //手工添加PicblocksizeBox的数据源 
            PicblocksizeBox.Properties.Items.Clear();
            foreach (DataRow row in this.GPUblocksize.Rows)
            {
                PicblocksizeBox.Properties.Items.Add(row["blocksize"]);
            }
            PicblocksizeBox.SelectedIndex = 1;//默认选择16*16
        }

        /// <summary>
        /// 相机类型数据绑定
        /// </summary>
        private DataTable CameraData
        {
            get
            {
                DataTable CameraChoose = new DataTable();
                CameraChoose.Columns.Add("CameraType", typeof(string));
                CameraChoose.Columns.Add("Value", typeof(char));
                CameraChoose.Rows.Add(new object[] { "8位黑白", 0 });
                CameraChoose.Rows.Add(new object[] { "24位彩色", 1 });
                CameraChoose.Rows.Add(new object[] { "32位彩色", 2 });
                CameraChoose.AcceptChanges();

                return CameraChoose;
            }
        }

        /// <summary>
        /// 压缩参数数据绑定
        /// </summary>
        private DataTable CompressionData
        {
            get
            {
                DataTable CompressionParams = new DataTable();
                CompressionParams.Columns.Add("CompressionRatio", typeof(string));
                CompressionParams.Columns.Add("Value", typeof(char));
                CompressionParams.Rows.Add(new object[] { "1:10", 0 });
                CompressionParams.Rows.Add(new object[] { "1:15", 1 });
                CompressionParams.Rows.Add(new object[] { "1:20", 2 });
                CompressionParams.AcceptChanges();

                return CompressionParams;
            }
        }

        /// <summary>
        /// 线程块尺寸数据绑定
        /// </summary>
        private DataTable GPUblocksize
        {
            get
            {
                DataTable GPUblocksize = new DataTable();
                GPUblocksize.Columns.Add("blocksize", typeof(string));
                GPUblocksize.Columns.Add("Value", typeof(char));
                GPUblocksize.Rows.Add(new object[] { "8*8", 0 });
                GPUblocksize.Rows.Add(new object[] { "16*16", 1 });
                GPUblocksize.Rows.Add(new object[] { "32*32", 2 });
                GPUblocksize.AcceptChanges();

                return GPUblocksize;
            }
        }

        /// <summary>
        /// 委托事件
        /// 参数设置窗口向主窗口发送异步请求
        /// </summary>
        public event EditDelegate Edit_Event;
        /// <summary>
        /// 参数设置完成确认按键事件
        /// </summary>
        private void Editformconform_Click(object sender, EventArgs e)
        {
            Form1.ParameterReady = true;//实验参数准备完成
            ParameterLocked = true;//参数锁定
            this.ImgThresholdspin.ReadOnly = true;
            this.LengthMinspin.ReadOnly = true;
            this.LengthMaxspin.ReadOnly = true;
            this.AreaMinspin.ReadOnly = true;
            this.AreaMaxspin.ReadOnly = true;

            int[] modifyparmeter = 
            {
                int.Parse(PixelWidthBox.Text),
                int.Parse(PixelHeightBox.Text),
                int.Parse(ImgThresholdspin.Text),
                int.Parse(LengthMinspin.Text),
                int.Parse(LengthMaxspin.Text),
                int.Parse(AreaMinspin.Text),
                int.Parse(AreaMaxspin.Text),
                int.Parse(PictureNumspin.Text),
                int.Parse(Bufferlengthspin.Text)
            };
            Edit_Event(modifyparmeter);
            this.Close(); //----关闭窗体
            //Form1.gHostPathImgNumber = Program.Image_path_check();
        }

        /// <summary>
        /// 参数设置完成取消按键事件
        /// </summary>
        private void Editformcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 参数导入按键事件
        /// </summary>
        private void GParmeterImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "配置文件(*.ini)|*.ini";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                IniFile myIni = new IniFile(fileDialog.FileNames[0]);//多选文件默认导入第一个文件
                string admin = myIni.ReadString("System", "admin", "");
                if (admin == "Administrator")
                {
                    ImgThresholdspin.Text = myIni.ReadString("Parameter", "ImageThreshold", "");//导入二值化阈值
                    LengthMinspin.Text = myIni.ReadString("Parameter", "MinLength", "");//导入标志点周长范围最小值
                    LengthMaxspin.Text = myIni.ReadString("Parameter", "MaxLength", "");//导入标志点周长范围最大值
                    AreaMinspin.Text = myIni.ReadString("Parameter", "MinArea", "");//导入标志点面积范围最小值
                    AreaMaxspin.Text = myIni.ReadString("Parameter", "MaxArea", "");//导入标志点面积范围最大值
                    Form1.PictureNum = myIni.ReadInt("Parameter", "PictureNum", 1);//导入拼图数
                    PictureNumspin.Text = Form1.PictureNum.ToString();
                    Form1.PicBlockSize = myIni.ReadInt("Parameter", "PicBlockSize", 16);//导入线程块尺寸
                    switch (Form1.PicBlockSize)
                    {
                        case 8: PicblocksizeBox.SelectedIndex = 0; break;//线程块尺寸8*8
                        case 16: PicblocksizeBox.SelectedIndex = 1; break;//线程块尺寸16*16
                        case 32: PicblocksizeBox.SelectedIndex = 2; break;//线程块尺寸32*32
                        default: break;
                    }
                    Form1.CompressionRatio = myIni.ReadInt("Parameter", "CompressionRatio", 2000);//导入压缩比
                    switch (Form1.CompressionRatio)
                    {
                        case 1: CompressionBox.SelectedIndex = 0; break;//压缩比1:10
                        case 200: CompressionBox.SelectedIndex = 1; break;//压缩比1:15
                        case 2000: CompressionBox.SelectedIndex = 2; break;//压缩比1:20
                        default: break;
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("配置文件有误，请重新选择！");
                }
            }
        }

        /// <summary>
        /// CameraTypeBox选项改变事件
        /// </summary>
        private void CameraTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(CameraTypeBox.SelectedIndex)
            {
                case 0: Form1.ImgBitDeep = 8;break;//选择8位黑白相机
                case 1: Form1.ImgBitDeep = 24; break;//选择24位彩色相机
                case 2: Form1.ImgBitDeep = 32; break;//选择32位彩色相机
                default: break;
            }
        }

        /// <summary>
        /// CompressionBox选项改变事件
        /// </summary>
        private void CompressionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CompressionBox.SelectedIndex)
            {
                case 0: Form1.CompressionRatio = 1; break;//压缩比1:10
                case 1: Form1.CompressionRatio = 200; break;//压缩比1:15
                case 2: Form1.CompressionRatio = 2000; break;//压缩比1:20
                default: break;
            }
        }

        /// <summary>
        /// PicblocksizeBox选项改变事件
        /// </summary>
        private void PicblocksizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (PicblocksizeBox.SelectedIndex)
            {
                case 0: Form1.PicBlockSize = 8; break;//线程块尺寸8*8
                case 1: Form1.PicBlockSize = 16; break;//线程块尺寸16*16
                case 2: Form1.PicBlockSize = 32; break;//线程块尺寸32*32
                default: break;
            }
        }

        private void ImageReadPathButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageReadPathEdit.Text = dialog.SelectedPath;
                // Modify the setting value
                Properties.Settings.Default.ImgReadPath = ImageReadPathEdit.Text;
                // Save setting value
                Properties.Settings.Default.Save();
            }
        }

        private void ImageSavePathButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageSavePathEdit.Text = dialog.SelectedPath;
                // Modify the setting value
                Properties.Settings.Default.ImgSavePath = ImageSavePathEdit.Text;
                // Save setting value
                Properties.Settings.Default.Save();
            }
        }

        private void DataReadPathButton_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataSavePathEdit.Text = dialog.SelectedPath;
                // Modify the setting value
                Properties.Settings.Default.DataSavePath = DataSavePathEdit.Text;
                // Save setting value
                Properties.Settings.Default.Save();
            }
        }
    }
}

