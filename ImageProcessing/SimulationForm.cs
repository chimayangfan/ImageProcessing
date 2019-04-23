using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ImageProcessing
{
    /// <summary>
    /// 声明委托
    /// 仿真测试窗口向主窗口发送异步请求
    /// </summary>
    public delegate void SimulationDelegate();
    public partial class SimulationForm : DevExpress.XtraEditors.XtraForm
    {
        Program.Infomation Result = new Program.Infomation();
        /// <summary>
        /// 仿真测试界面初始化
        /// </summary>
        public SimulationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 参数导入按键事件
        /// </summary>
        private void ParmeterImportbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "参数配置文件(*.ini)|*.ini";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                IniFile myIni = new IniFile(fileDialog.FileNames[0]);//多选文件默认导入第一个文件
                string admin = myIni.ReadString("System", "admin", "");
                if(admin == "Administrator")
                {
                    ImgThresholdBox.Text = myIni.ReadString("Parameter", "ImageThreshold", "");//导入二值化阈值
                    MinLengthBox.Text = myIni.ReadString("Parameter", "MinLength", "");//导入标志点周长范围最小值
                    MaxLengthBox.Text = myIni.ReadString("Parameter", "MaxLength", "");//导入标志点周长范围最大值
                    MinAreaBox.Text = myIni.ReadString("Parameter", "MinArea", "");//导入标志点面积范围最小值
                    MaxAreaBox.Text = myIni.ReadString("Parameter", "MaxArea", "");//导入标志点面积范围最大值
                    Form1.PictureNum = myIni.ReadInt("Parameter", "PictureNum", 1);//导入拼图数
                    Form1.PicBlockSize = myIni.ReadInt("Parameter", "PicBlockSize", 16);//导入线程块尺寸
                    switch (Form1.PicBlockSize)
                    {
                        case 8: GPUBlockSizeBox.SelectedIndex = 0; break;//线程块尺寸8*8
                        case 16: GPUBlockSizeBox.SelectedIndex = 1; break;//线程块尺寸16*16
                        case 32: GPUBlockSizeBox.SelectedIndex = 2; break;//线程块尺寸32*32
                        default: break;
                    }
                    Form1.CompressionRatio = myIni.ReadInt("Parameter", "CompressionRatio", 2000);//导入压缩比
                    switch (Form1.CompressionRatio)
                    {
                        case 1: CompressionRatioBox.SelectedIndex = 0; break;//压缩比1:10
                        case 200: CompressionRatioBox.SelectedIndex = 1; break;//压缩比1:15
                        case 2000: CompressionRatioBox.SelectedIndex = 2; break;//压缩比1:20
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
        /// 委托事件
        /// 仿真测试窗口向主窗口发送异步请求
        /// </summary>
        public event SimulationDelegate ConformEvent;
        /// <summary>
        /// 确认按键事件
        /// </summary>
        private void confirmbutton_Click(object sender, EventArgs e)
        {
            bool flag;
            //if (Form1.MemoryFlag == true)
            //{
            //    Program.Memory_release();//释放上幅图申请的内存
            //    Form1.MemoryFlag = false;
            //}
            if (this.PathBox.Text != string.Empty)
            {
                Program.Parameter value = new Program.Parameter();               
                //判断参数是否全部设置
                if (this.ImgThresholdBox.Text == string.Empty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("图像二值化阈值未设置！");
                }
                else if (Convert.ToInt32(this.ImgThresholdBox.Text) > 255)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("图像二值化阈值错误，取值范围为0~255！");
                }
                else if (this.MaxLengthBox.Text == string.Empty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("标志点周长最大范围未设置！");
                }
                //else if (this.AreaBox2.Text == string.Empty)
                //{
                //    MessageBox.Show("标志点面积最大范围未设置！");
                //}
                else
                {
                    //结构体初值设为null,-1则不传值
                    value.ImgReadPath = null;//图像读取路径
                    value.ImgSavePath = null;//图像保存路径
                    value.DataReadPath = null;//数据保存路径
                    value.ImgBitDeep = -1;//位深
                    value.ImgChannelNum = -1;//通道数
                    value.ImgHeight = -1;//行数
                    value.ImgWidth = -1;//列数
                    value.ImgMakeborderWidth = -1; //填充像素后的宽度
                    value.Threshold = -1; //二值化的阈值    
                    value.LengthMin = -1; //周长的最小值 
                    value.LengthMax = -1; //周长的最大值
                    value.PicBlockSize = -1;//GPU线程块尺寸
                    value.ColThreadNum = -1; //列方向分块数量
                    value.RowThreadNum = -1; //行方向块数量
                    value.AreaMin = -1;//面积的最小值 
                    value.AreaMax = -1;//面积的最大值
                    value.CompressionRatio = -1;//图像压缩比
                    value.PictureNum = -1;//拼图数量
                    value.TerminateFlag = -1;//是否终止实验
                    value.RecModelFlag = -1;//全图模式|矩形模式
                    value.RecPadding = -1;//包围盒填充像素数目
                    //导入需要传递参数的值
                    value.ImgReadPath = Properties.Settings.Default.ImgReadPath;
                    value.ImgSavePath = Properties.Settings.Default.ImgSavePath;
                    value.DataReadPath = Properties.Settings.Default.DataSavePath;
                    value.ImgBitDeep = Form1.ImgBitDeep;
                    value.ImgHeight = int.Parse(ImgHeightBox.Text);
                    value.ImgWidth = int.Parse(ImgWidthBox.Text);
                    value.Threshold = int.Parse(ImgThresholdBox.Text);
                    Form1.ImgThreshold = int.Parse(ImgThresholdBox.Text);
                    if (this.MinLengthBox.Text == string.Empty)
                    {
                        value.LengthMin = 0;//周长最小范围不输入则默认为0
                        MinLengthBox.SelectedText = value.LengthMin.ToString();
                    }
                    else
                        value.LengthMin = int.Parse(MinLengthBox.Text);
                    value.LengthMax = int.Parse(MaxLengthBox.Text);
                    if (this.MinAreaBox.Text == string.Empty)
                    {
                        value.AreaMin = 0;//面积最小范围不输入则默认为0
                        MinAreaBox.SelectedText = value.AreaMin.ToString();
                    }
                    else
                        value.AreaMin = int.Parse(MinAreaBox.Text);
                    if (this.MaxAreaBox.Text == string.Empty)
                    {
                        value.AreaMax = 99999;//面积最大范围不输入则默认为99999
                        MaxAreaBox.SelectedText = value.AreaMax.ToString();
                    }
                    else                       
                        value.AreaMax = int.Parse(MaxAreaBox.Text);
                    value.CompressionRatio = Form1.CompressionRatio;//图像压缩比
                    value.PictureNum = Form1.PictureNum;//拼图数量
                    value.PicBlockSize = Form1.PicBlockSize;//GPU线程块尺寸
                    value.TerminateFlag = 0;//是否终止实验
                    value.RecModelFlag = Form1.ImgInputModeFlag;//全图|矩形模式设置
                    value.RecPadding = 5;//包围盒填充像素数目
                    if (value.LengthMax < value.LengthMin)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("周长设置范围有误，请重新设置！");
                        return;
                    }
                    if (value.AreaMax < value.AreaMin)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("面积设置范围有误，请重新设置！");
                        return;
                    }
                    Form1.LengthMin = value.LengthMin;
                    Form1.LengthMax = value.LengthMax;
                    Form1.AreaMin = value.AreaMin;
                    Form1.AreaMax = value.AreaMax;
                    flag = Program.SetParameter(ref value, 17);//结构体传递参数值
                    //传参成功
                    if (flag == true)
                    {
                        //Program.Memory_application();//申请本幅图的内存
                        //Form1.MemoryFlag = true;
                        //bool back = Program.SimulationImageTest(Form1.ImagePath, ref Result);
                        closeThread();
                        SimulationThread = new Thread(new ThreadStart(ChooseExperiment));
                        SimulationThread.IsBackground = true;
                        SimulationThread.Start();
                        //Program.SimulationTestSynchronize(Form1.ImagePath, ref Result);
                        //Form1.GPUDevices = Result.DeviceCount;
                        //Form1.CPUThreads = Result.CPUThreadCount;
                        //Result.PointNumbers = 14825;
                        //Result.ImgProcessingNumbers = 50;
                        //Result.DeviceCount = 3;
                        //触发事件
                        ConformEvent();
                    }
                    if (flag == false)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("参数未设置成功，请重新设置！");
                        return;
                    }
                    //System.Threading.Thread.Sleep(15000);
                    //Result.SynchronizeTimes = 0.35f;
                    //Result.SynchronizeSpeed = 3571.43f;
                    ListViewDisplay(Result);
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入测试图像！");
            }
        }

        ///<summary>
        ///模块编号：子线程执行模块
        ///作用：用于在线实验过程调用DLL实验函数，防止主线程堵塞
        ///作者：
        ///编写日期：
        ///</summary>
        Thread SimulationThread = null;
        //delegate void ExperimentDelegate(int i);

        //Experiment子线程，调用test函数
        private void ChooseExperiment()
        {
            try
            {
                SimulationTest();
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
            if (SimulationThread != null)
            {
                if (SimulationThread.IsAlive)
                {
                    SimulationThread.Abort();
                }
            }
        }

        /// <summary>
        /// 子线程实际执行函数
        /// </summary> 
        private void SimulationTest()
        {
            Program.SimulationTestSynchronize(Form1.ImagePath, ref Result);
            ListViewDisplay(Result);
        }

        /// <summary>
        /// 仿真测试结果显示
        /// </summary>
        /// <param name="Info">DLL返回结构体</param>
        private void ListViewDisplay(Program.Infomation Info)
        {
            this.ReportlistView.Clear();
            this.ReportlistView.GridLines = true; //显示表格线
            this.ReportlistView.View = View.Details;//显示表格细节
            this.ReportlistView.LabelEdit = true; //是否可编辑,ListView只可编辑第一列。
            this.ReportlistView.Scrollable = true;//有滚动条
            this.ReportlistView.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
            this.ReportlistView.FullRowSelect = true;//是否可以选择行

            //this.listView1.HotTracking = true;// 当选择此属性时则HoverSelection自动为true和Activation属性为oneClick
            //this.listView1.HoverSelection = true;
            //this.listView1.Activation = ItemActivation.Standard; //
            //添加表头
            this.ReportlistView.Columns.Add("序号", 50, HorizontalAlignment.Center);
            this.ReportlistView.Columns.Add("测试项", 160, HorizontalAlignment.Center);
            this.ReportlistView.Columns.Add("结果", 120, HorizontalAlignment.Center);
            this.ReportlistView.Columns.Add("单位", 70, HorizontalAlignment.Center);
            //添加各项
            ListViewItem[] p = new ListViewItem[5];
            p[0] = new ListViewItem(new string[] { "1", "标志点数目", Info.PointNumbers.ToString(), " 个" });
            p[1] = new ListViewItem(new string[] { "2", "测试图复制数", Info.ImgProcessingNumbers.ToString(), " 张" });
            p[2] = new ListViewItem(new string[] { "3", "使用GPU设备数", Info.DeviceCount.ToString(), " 个" });
            //p[3] = new ListViewItem(new string[] { "4", "单提点耗时", Info.ExtractPointTimes.ToString(), " S" });
            //p[4] = new ListViewItem(new string[] { "5", "单提点速度", Info.ExtractPointSpeed.ToString(), " Mb/S" });
            //p[5] = new ListViewItem(new string[] { "6", "单压缩耗时", Info.CompressionTimes.ToString(), " S" });
            //p[6] = new ListViewItem(new string[] { "7", "单压缩速度", Info.CompressionSpeed.ToString(), " Mb/S" });
            p[3] = new ListViewItem(new string[] { "3", "提点与压缩同步耗时", Info.SynchronizeTimes.ToString(), " S" });
            p[4] = new ListViewItem(new string[] { "4", "提点与压缩同步速度", Info.SynchronizeSpeed.ToString(), " Mb/S" });
            //p[0].SubItems[0].BackColor = Color.Red; //用于设置某行的背景颜色

            ListViewItem strReport = new ListViewItem();
            //strReport = new ListViewItem("ok");           
            //this.ReportlistView.Items.Add(strReport);
            this.ReportlistView.Items.AddRange(p);
            //也可以用this.listView1.Items.Add();不过需要在使用的前后添加Begin... 和End...防止界面自动刷新
            // 添加分组
            this.ReportlistView.Groups.Add(new ListViewGroup("Data"));
            this.ReportlistView.Groups.Add(new ListViewGroup("Conclusion"));

            for (int i = 0; i < p.Length; i++)
            {
                this.ReportlistView.Items[i].Group = this.ReportlistView.Groups[0];
            }
        }

        /// <summary>
        /// 文本框输入限制，只允许输入数字
        /// </summary>
        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 取消按键事件
        /// </summary>
        private void cancelbutton_Click(object sender, EventArgs e)
        {
            closeThread(); //停止子线程
            this.Close(); //----关闭窗体
        }

        /// <summary>
        /// 仿真测试窗口加载事件
        /// </summary>
        private void simulationform_Load(object sender, EventArgs e)
        {
            //显示加载文本
            PathBox.SelectedText = Form1.ImagePath;
            ImgHeightBox.SelectedText = Form1.ImageHeight.ToString();
            ImgWidthBox.SelectedText = Form1.ImageWidth.ToString();
            //手工添加ComboBoxEdit的数据源 
            CameraTypeBoxTest.Properties.Items.Clear();
            foreach (DataRow row in this.CameraData.Rows)
            {
                CameraTypeBoxTest.Properties.Items.Add(row["CameraType"]);
            }
            CameraTypeBoxTest.SelectedIndex = 0;//默认选择8位黑白
            //CameraTypeBox.SelectedIndex = 1;//默认选择24位彩色
            //手工添加CompressionBox的数据源 
            CompressionRatioBox.Properties.Items.Clear();
            foreach (DataRow row in this.CompressionData.Rows)
            {
                CompressionRatioBox.Properties.Items.Add(row["CompressionRatio"]);
            }
            CompressionRatioBox.SelectedIndex = 2;//默认选择1:20
            //手工添加PicblocksizeBox的数据源 
            GPUBlockSizeBox.Properties.Items.Clear();
            foreach (DataRow row in this.GPUblocksize.Rows)
            {
                GPUBlockSizeBox.Properties.Items.Add(row["blocksize"]);
            }
            GPUBlockSizeBox.SelectedIndex = 1;//默认选择16*16
        }

        /// <summary>
        /// 参数导出按键事件
        /// </summary>
        private void parmeterexportbutton_Click(object sender, EventArgs e)
        {
            //string localFilePath, fileNameExt, newFileName, FilePath;  
            string localFilePath = String.Empty;
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "配置文件（*.ini）|*.ini";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString(); //获得文件路径 
                IniFile myIni = new IniFile(localFilePath);
                myIni.WriteString("System", "admin", "Administrator");
                myIni.WriteString("Parameter", "ImageThreshold", ImgThresholdBox.Text);//导出二值化阈值
                myIni.WriteString("Parameter", "ImageHeight", ImgHeightBox.Text);//导出标志点周长范围最小值
                myIni.WriteString("Parameter", "ImageWidth", ImgWidthBox.Text);//导出标志点周长范围最大值
                myIni.WriteString("Parameter", "MinLength", MinLengthBox.Text);//导出标志点周长范围最小值
                myIni.WriteString("Parameter", "MaxLength", MaxLengthBox.Text);//导出标志点周长范围最大值
                myIni.WriteString("Parameter", "MinArea", MinAreaBox.Text);//导出标志点周长范围最小值
                myIni.WriteString("Parameter", "MaxArea", MaxAreaBox.Text);//导出标志点周长范围最大值
                myIni.WriteString("Parameter", "PictureNum", Form1.PictureNum.ToString());//导出拼图数
                myIni.WriteString("Parameter", "PicBlockSize", Form1.PicBlockSize.ToString());//导出线程块尺寸
                myIni.WriteString("Parameter", "CompressionRatio", Form1.CompressionRatio.ToString());//导出压缩比
            }
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
        /// CameraTypeBox选项改变
        /// </summary>
        private void CameraTypeBoxTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CameraTypeBoxTest.SelectedIndex)
            {
                case 0: Form1.ImgBitDeep = 8; break;//选择8位黑白相机
                case 1: Form1.ImgBitDeep = 24; break;//选择24位彩色相机
                case 2: Form1.ImgBitDeep = 32; break;//选择32位彩色相机
                default: break;
            }
        }

        /// <summary>
        /// CompressionRatioBox选项改变
        /// </summary>
        private void CompressionRatioBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CompressionRatioBox.SelectedIndex)
            {
                case 0: Form1.CompressionRatio = 1; break;//压缩比1:10
                case 1: Form1.CompressionRatio = 200;break;//压缩比1:15
                case 2: Form1.CompressionRatio = 2000; break;//压缩比1:20
                default: break;
            }
        }

        /// <summary>
        /// PicblocksizeBox选项改变事件
        /// </summary>
        private void GPUBlockSizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (GPUBlockSizeBox.SelectedIndex)
            {
                case 0: Form1.PicBlockSize = 8; break;//线程块尺寸8*8
                case 1: Form1.PicBlockSize = 16; break;//线程块尺寸16*16
                case 2: Form1.PicBlockSize = 32; break;//线程块尺寸32*32
                default: break;
            }
        }
    }
}
