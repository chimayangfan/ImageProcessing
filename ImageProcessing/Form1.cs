using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Runtime.InteropServices;//必须添加，不然DllImport报错
using DevExpress.XtraTreeList.Nodes;
using System.Threading;
using System.Configuration;
using System.Drawing.Imaging;
using DevExpress.XtraReports.UI;

namespace ImageProcessing
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        static public int GPUDevices;//GPU设备数
        static public int CPUThreads;//CPU线程数
        static public int ImageHeight, ImageWidth;//图像像素值
        static public int Bufferlength;//相机缓冲区的长度
        static public int ImgBitDeep;//位深
        static public int ImgThreshold;//二值化阈值
        static public int LengthMin, LengthMax;//标志点周长范围
        static public int AreaMin, AreaMax;//标志点周长范围
        static public int CompressionRatio;//图像压缩比
        static public int PicBlockSize;//GPU线程块尺寸
        static public int PictureNum;//图像拼接张数
        static public int gHostPathImgNumber;//离线文件夹图像数量
        static public string ImagePath;//图像路径
        static public int ImgProcessingNumbers;//处理图像数量
        static public string FolderPath;//文件夹路径
        static public string[] ImageNames;//图像名
        static public int RunState;//运行状态。1、测试就绪，2、在线实验就绪，3、离线实验就绪
        static public int OperatMode;//操作模式。1、默认不操作，2、单提点，3、单压缩，4、提点压缩
        static public bool ParameterReady;//实验参数准备完成标志位
        static public bool gRunStatic;//初始状态:false , 正在运行:true
        static public bool MemoryFlag;//全局内存申请标志位
        static public bool ImagePathImportFlag = false;//图片导入成功标志位
        static public int ImgInputModeFlag;//0：全图模式，1：包围盒模式
        Program.Parameter GlobValue = new Program.Parameter();
        DevExpress.XtraTreeList.TreeList treeList = new DevExpress.XtraTreeList.TreeList();//图像路径列表
        DevExpress.XtraTreeList.TreeList MarkList = new DevExpress.XtraTreeList.TreeList();//标志点数据列表

            ///<summary>
            ///标记点信息的内部类
            ///</summary>
            public class MarkInfo
        {
            public Color MarkColor = new Color();//全部标注颜色
            public Color ChooseMarkColor = new Color();//选中标注颜色
            public byte MarkLength;//全部标注长度
            public byte ChooseMarkLength;//选中标注长度
            public byte MarkLinewidth;//全部标注线宽
            public byte ChooseMarkLinewidth;//选中标注线宽
            public PointF MarkCenterPos = new PointF();//轮廓重心位置
            public int MarkNumber;//标志点数目
            public int MarkIndex;//标志点编号
            public List<PointF> AllMarkCenter = new List<PointF>();//存放所有轮廓点重心
            public Dictionary<int, PointF> AllMarkDictionary = new Dictionary<int, PointF>();////存放所有轮廓点重心表
        };
        static public MarkInfo markInfo = new MarkInfo();

        /// <summary>
        /// 主界面初始化
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;//设置子线程可以安全的访问UI线程窗体控件
        }

        /// <summary>
        /// 主界面--定时器执行函数
        /// 在线实验过程刷新显示区图像
        /// </summary>
        private void ImageReflash_Tick(object sender, EventArgs e)
        {
            //imageRefreshtimer.Enabled = false; //不可用

            int Refreshflag;
            if (ImgBitDeep == 8)
            {                
                byte[] imggray = new byte[ImageWidth * ImageHeight];
                Refreshflag = Program.OnlineImageRefresh(ref imggray[0]);
                ExperimentImage.Image = IMGDataTrans.ToGrayBitmap(imggray, ImageWidth, ImageHeight);//读取bmp到ExperimentImage
            }
            else if (ImgBitDeep == 24)
            {
                byte[] imgcolor = new byte[ImageWidth * ImageHeight * 3];
                Refreshflag = Program.OnlineImageRefresh(ref imgcolor[0]);
                ExperimentImage.Image = ImageProcessing.IMGDataTrans.ToRGBBitmap(imgcolor, ImageWidth, ImageHeight);//读取bmp到picturebox
            }

            //imageRefreshtimer.Enabled = true; //可用
        }

        /// <summary>
        /// 主界面--参数设置按键执行函数
        /// </summary>
        private void OpenParmeterForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditForm editForm = new EditForm();
            editForm.Edit_Event += EditConform_Event;
            if (editForm.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("标注设置界面！");
                //btnFind_Click(sender, e); //这个是当前页面的重新加载的查询事件                
            }
        }

        /// <summary>
        /// 主界面--标记设置按键执行函数
        /// </summary>
        private void OpenMarkEditForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MarkDraw markDrawForm = new MarkDraw();
            //注册事件
            markDrawForm.MarkSet_Event += MarkSetConform_Event;
            if (markDrawForm.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                //btnFind_Click(sender, e); //这个是当前页面的重新加载的查询事件                
            }
        }

        /// <summary>
        /// 主界面--性能报告按键执行函数
        /// </summary>
        private void PerformanceReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ImagePath == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入仿真测试图！");
                return;
            }
            XtraReport report = new XtraReport();
            // Show the report's preview.
            report.ShowPreview();
        }

        /// <summary>
        /// 主界面--图片解压按键执行函数
        /// </summary>
        private void Unzip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (DevExpress.XtraEditors.XtraMessageBox.Show("解压图像保存文件夹下所有图片?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //    Program.UnzipPictureFiles("C:\\pic\\img_write");
            //    DevExpress.XtraEditors.XtraMessageBox.Show("图像解压完成！");
            //}
            Unzip UnzipForm = new Unzip();
            if (UnzipForm.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                //btnFind_Click(sender, e); //这个是当前页面的重新加载的查询事件                
            }
        }

        /// <summary>
        /// 主界面--清除数据按键事件
        /// </summary>
        private void ClearData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Program.HardwareInfo HardwareProp = new Program.HardwareInfo();
            int a = Program.HardwareInit(ref HardwareProp);//硬件初始化
            DevExpress.XtraEditors.XtraMessageBox.Show("HardwareProp:"+ HardwareProp.DeviceCount);
            switch (a)
            {
                case 0:break;
                case 1: DevExpress.XtraEditors.XtraMessageBox.Show("存储磁盘空间不足!"); System.Environment.Exit(0); break;
                case 2: DevExpress.XtraEditors.XtraMessageBox.Show("可用GPU设备数错误!"); System.Environment.Exit(0); break;
            }
            if (DevExpress.XtraEditors.XtraMessageBox.Show("清空所有数据?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                AllInit();
                DevExpress.XtraEditors.XtraMessageBox.Show("配置参数已恢复为默认值！");
            }
        }

        ///<summary>
        ///模块编号：仿真测试
        ///作用：仿真测试实验过程事件
        ///作者：
        ///编写日期：
        ///</summary>

        /// <summary>
        /// 仿真测试--打开图片按键执行函数
        /// </summary>
        private void simulationOpenImage_ItemClick(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "图像文件(*.bmp)|*.bmp";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] names = fileDialog.FileNames;
                foreach (string file in names)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    SimulationImage.Image = System.Drawing.Image.FromStream(fs);
                    fs.Close();
                    ImageHeight = SimulationImage.Image.Height;
                    ImageWidth = SimulationImage.Image.Width;
                    ImagePath = file;
                    RunState = 1;//仿真实验准备就绪
                    break;
                }
            }
        }

        /// <summary>
        /// 仿真测试--仿真参数按键执行函数
        /// </summary>
        private void simulationParmeter_ItemClick(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SimulationForm simulationForm = new SimulationForm();
            //注册事件
            simulationForm.ConformEvent += Simulation_Event;
            if (simulationForm.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                //btnFind_Click(sender, e); //这个是当前页面的重新加载的查询事件
            }
        }

        /// <summary>
        /// 仿真测试--结果显示按键执行函数
        /// </summary>
        private void SimulationResultDispaly(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ImagePathImportFlag == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("图片未处理！");
            }
            else
            {
                Bitmap OfflineExperimentImage = new Bitmap(ImagePath);
                Bitmap bmp = new Bitmap(OfflineExperimentImage.Width, OfflineExperimentImage.Height, PixelFormat.Format32bppArgb);
                Graphics Draw = Graphics.FromImage(bmp);
                Draw.DrawImage(OfflineExperimentImage, 0, 0, OfflineExperimentImage.Width, OfflineExperimentImage.Height);
                try
                {
                    ImageDrawMark(bmp, @"C:\pic\img_data\1.bin");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                SimulationImage.Image.Dispose();//使用前先将前张图片释放，否则内存累加
                SimulationImage.Image = bmp;
                Draw.Dispose();
                OfflineExperimentImage.Dispose();
                ImagePathImportFlag = false;//导入标志位重置
            }
        }

        /// <summary>
        /// 仿真测试--提标志点按键事件
        /// </summary>
        private void SimulationReportButton_ItemClick(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SimulationParForm simulationParForm = new SimulationParForm();
            //注册事件
            simulationParForm.ReportConformEvent += SimulationParForm_Event;
            if (simulationParForm.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                //btnFind_Click(sender, e); //这个是当前页面的重新加载的查询事件
            }
        }

        /// <summary>
        /// 仿真测试--图像显示区域鼠标悬停事件：获取鼠标焦点
        /// </summary>
        private void SimulationImage_MouseEnter(object sender, EventArgs e)
        {
            this.SimulationImage.Focus();
        }

        /// <summary>
        /// 仿真测试--图像显示区域鼠标滚轮事件：图片缩放
        /// </summary>
        private void SimulationImage_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.SimulationImage.Dock = System.Windows.Forms.DockStyle.None;
            this.SimulationImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            //判断上滑还是下滑
            if (e.Delta < 0)
            {
                //计算缩放大小
                if(SimulationImage.Width > employeesNavigationPage.Width || SimulationImage.Height > employeesNavigationPage.Height)
                {
                    this.SimulationImage.Width = this.SimulationImage.Width * 9 / 10;
                    this.SimulationImage.Height = this.SimulationImage.Height * 9 / 10;
                }
            }
            else
            {
                this.SimulationImage.Width = this.SimulationImage.Width * 11 / 10;
                this.SimulationImage.Height = this.SimulationImage.Height * 11 / 10;
            }
        }

        int SimulationxPos;//鼠标x坐标
        int SimulationyPos;//鼠标y坐标
        bool SimulationMoveFlag;//鼠标按下移动标志位

        /// <summary>
        /// 仿真测试--图像显示区域鼠标指针在组件上方并释放鼠标按钮事件
        /// </summary>
        private void SimulationImage_MouseUp(object sender, MouseEventArgs e)
        {
            //鼠标已经抬起
            SimulationMoveFlag = false;
        }

        /// <summary>
        /// 仿真测试--图像显示区域鼠标指针移过组件事件
        /// </summary>
        private void SimulationImage_MouseMove(object sender, MouseEventArgs e)
        {
            //只在鼠标按下时绘制移动
            if (SimulationMoveFlag)
            {
                SimulationImage.Left += Convert.ToInt16(e.X - SimulationxPos);//设置x坐标.
                SimulationImage.Top += Convert.ToInt16(e.Y - SimulationyPos);//设置y坐标.
            }
        }

        /// <summary>
        /// 仿真测试--图像显示区域鼠标指针在组件上方并按下鼠标按钮事件
        /// </summary>
        private void SimulationImage_MouseDown(object sender, MouseEventArgs e)
        {
            if(this.SimulationImage.Dock == System.Windows.Forms.DockStyle.Fill)
            {
                return;
            }
            this.SimulationImage.Focus();
            SimulationMoveFlag = true;//已经按下.
            SimulationxPos = e.X;//当前x坐标.
            SimulationyPos = e.Y;//当前y坐标.
        }

        /// <summary>
        /// 仿真测试--图像显示区域鼠标双击事件
        /// </summary>
        private void SimulationImage_DoubleClick(object sender, EventArgs e)
        {
            //图像位置重置
            SimulationImage.Location = new System.Drawing.Point(0, 0);
            SimulationImage.Width = employeesNavigationPage.Width;
            SimulationImage.Height = employeesNavigationPage.Height;
            this.SimulationImage.Dock = System.Windows.Forms.DockStyle.Fill;            
        }

        /// <summary>
        /// 在线、离线实验--图像显示区域鼠标悬停事件：获取鼠标焦点
        /// </summary>
        private void ExperimentImage_MouseEnter(object sender, EventArgs e)
        {
            this.ExperimentImage.Focus();
        }

        /// <summary>
        /// 在线、离线实验--图像显示区域鼠标滚轮事件：图片缩放
        /// </summary>
        private void ExperimentImage_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.ExperimentImage.Dock = System.Windows.Forms.DockStyle.None;
            this.ExperimentImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            //判断上滑还是下滑
            if (e.Delta < 0)
            {
                //计算缩放大小
                if (SimulationImage.Width > Experiment_splitContainerControl.Panel1.Width || SimulationImage.Height > Experiment_splitContainerControl.Panel1.Height)
                {
                    this.ExperimentImage.Width = this.ExperimentImage.Width * 9 / 10;
                    this.ExperimentImage.Height = this.ExperimentImage.Height * 9 / 10;
                }
            }
            else
            {
                this.ExperimentImage.Width = this.ExperimentImage.Width * 11 / 10;
                this.ExperimentImage.Height = this.ExperimentImage.Height * 11 / 10;
            }
        }

        int ExperimentxPos;//鼠标x坐标
        int ExperimentyPos;//鼠标y坐标
        bool ExperimentMoveFlag;//鼠标按下移动标志位

        /// <summary>
        /// 在线、离线实验--图像显示区域鼠标指针在组件上方并释放鼠标按钮事件
        /// </summary>
        private void ExperimentImage_MouseUp(object sender, MouseEventArgs e)
        {
            //鼠标已经抬起
            ExperimentMoveFlag = false;
        }

        /// <summary>
        /// 在线、离线实验--图像显示区域鼠标指针移过组件事件
        /// </summary>
        private void ExperimentImage_MouseMove(object sender, MouseEventArgs e)
        {
            //只在鼠标按下时绘制移动
            if (ExperimentMoveFlag)
            {
                ExperimentImage.Left += Convert.ToInt16(e.X - ExperimentxPos);//设置x坐标.
                ExperimentImage.Top += Convert.ToInt16(e.Y - ExperimentyPos);//设置y坐标.
            }
        }

        /// <summary>
        /// 在线、离线实验--图像显示区域鼠标指针在组件上方并按下鼠标按钮事件
        /// </summary>
        private void ExperimentImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.ExperimentImage.Dock == System.Windows.Forms.DockStyle.Fill)
            {
                return;
            }
            this.ExperimentImage.Focus();
            ExperimentMoveFlag = true;//已经按下.
            ExperimentxPos = e.X;//当前x坐标.
            ExperimentyPos = e.Y;//当前y坐标.
        }

        /// <summary>
        /// 在线、离线实验--图像显示区域鼠标双击事件
        /// </summary>
        private void ExperimentImage_DoubleClick(object sender, EventArgs e)
        {
            //图像位置重置
            ExperimentImage.Location = new System.Drawing.Point(0, 0);
            ExperimentImage.Width = Experiment_splitContainerControl.Panel1.Width;
            ExperimentImage.Height = Experiment_splitContainerControl.Panel1.Height;
            this.ExperimentImage.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        ///<summary>
        ///模块编号：在线实验
        ///作用：在线实验过程事件
        ///作者：
        ///编写日期：
        ///</summary>

        /// <summary>
        /// 在线实验--导入参数按键事件
        /// </summary>
        private void OnlineImportParmeter_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "参数配置文件(*.ini)|*.ini";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                IniFile myIni = new IniFile(fileDialog.FileNames[0]);//多选文件默认导入第一个文件
                string admin = myIni.ReadString("System", "admin", "");
                if (admin == "Administrator")
                {
                    ImgBitDeep = myIni.ReadInt("Parameter", "ImgBitDeep", 8);//导入图像位深度
                    ImgThreshold = myIni.ReadInt("Parameter", "ImageThreshold", 60);//导入二值化阈值
                    LengthMin = myIni.ReadInt("Parameter", "MinLength", 0);//导入标志点周长范围最小值
                    LengthMax = myIni.ReadInt("Parameter", "MaxLength", 99999);//导入标志点周长范围最大值
                    AreaMin = myIni.ReadInt("Parameter", "MinArea", 0);//导入标志点周长范围最小值
                    AreaMax = myIni.ReadInt("Parameter", "MaxArea", 99999);//导入标志点周长范围最大值
                    PictureNum = myIni.ReadInt("Parameter", "PictureNum", 1);//导入拼图数
                    PicBlockSize = myIni.ReadInt("Parameter", "PicBlockSize", 16);//导入线程块尺寸
                    CompressionRatio = myIni.ReadInt("Parameter", "CompressionRatio", 2000);//导入压缩比
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("配置文件有误，请重新选择！");
                }
            }
        }

        /// <summary>
        /// 在线实验--模拟实验按键事件
        /// </summary>
        private void OnlineFullImage_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //OnlineForm debugForm = new OnlineForm();
            ////注册事件
            //debugForm.InitEvent += TreeListInit_Event;
            //if (debugForm.ShowDialog() == DialogResult.OK)
            //{
            //    //界面打开成功
            //}
            if (MemoryFlag == true)
            {
                Program.Memory_release();//释放上幅图申请的内存
                MemoryFlag = false;
            }
            Program.Memory_application();//申请本次实验的内存
            MemoryFlag = true;
            RunState = 2;            
            DevExpress.XtraEditors.XtraMessageBox.Show("在线实验就绪！");
        }

        /// <summary>
        /// 在线实验--停止实验按键事件
        /// </summary>
        private void OnlineStopButton_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (RunState == 2)
            {
                Program.Parameter value = new Program.Parameter();
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

                value.TerminateFlag = 1;//终止实验
                bool flag = Program.SetParameter(ref value, 1);//结构体传递参数值
                closeThread();
                if (imageRefreshtimer.Enabled == false)
                {
                    imageRefreshtimer.Enabled = true;
                    imageRefreshtimer.Stop();//停止定时刷新
                }
                else
                    imageRefreshtimer.Stop();//停止定时刷新
                if (Form1.MemoryFlag == true)
                {
                    Program.Memory_release();//释放上幅图申请的内存
                    Form1.MemoryFlag = false;
                }
                DevExpress.XtraEditors.XtraMessageBox.Show("在线实验已终止！");
                value.TerminateFlag = 0;//终止实验标志位重置
                flag = Program.SetParameter(ref value, 1);//结构体传递参数值
            }
        }

        ///<summary>
        ///模块编号：离线实验
        ///作用：离线实验过程事件
        ///作者：
        ///编写日期：
        ///</summary>

        /// <summary>
        /// 离线实验--导入图片文件夹按键事件
        /// </summary>
        private void OfflineImportFloder_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                DevExpress.XtraEditors.XtraMessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FolderPath = foldPath;//更新全局文件夹路径
                InitRightTree();//加载图像序列树视图
            }
        }

        /// <summary>
        /// 离线实验--导入图片按键事件
        /// </summary>
        private void OfflineImportImages_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (Form1.MemoryFlag == true)
            //{
            //    Program.Memory_release();//释放上幅图申请的内存
            //    Form1.MemoryFlag = false;
            //}
            if (treeList.DataSource == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入文件夹！");
            }
            else
            {
                string TreeListRowPath = treeList.FocusedNode.GetDisplayText(0);//获取所选树列表行内容(图像路径)
                System.IO.FileStream fs = new System.IO.FileStream(TreeListRowPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                ExperimentImage.Image = System.Drawing.Image.FromStream(fs);
                fs.Close();
                ImageHeight = ExperimentImage.Image.Height;
                ImageWidth = ExperimentImage.Image.Width;
                ImagePath = TreeListRowPath;//更新全局图像路径                
                //Program.Memory_application();
                //Form1.MemoryFlag = true;
                MarktreeList.Controls.Clear();
                RunState = 3;//离线实验准备就绪
            }

        }

        /// <summary>
        /// 判断图片是否索引像素格式,是否是引发异常的像素格式
        /// </summary>
        /// <param name="imagePixelFormat">图片的像素格式</param>
        /// <returns></returns>
        private bool IsIndexedPixelFormat(System.Drawing.Imaging.PixelFormat imagePixelFormat)
        {
            PixelFormat[] pixelFormatArray = {
                                            PixelFormat.Format1bppIndexed
                                            ,PixelFormat.Format4bppIndexed
                                            ,PixelFormat.Format8bppIndexed
                                            ,PixelFormat.Undefined
                                            ,PixelFormat.DontCare
                                            ,PixelFormat.Format16bppArgb1555
                                            ,PixelFormat.Format16bppGrayScale
                                        };
            foreach (PixelFormat pf in pixelFormatArray)
            {
                if (imagePixelFormat == pf)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 离线实验--结果显示按键事件
        /// </summary>
        private void OfflineFullImage_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (ExperimentImage.Image == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请导入图像！");
                return;
            }
            if (RunState == 3)//离线实验
            {
                string Binpath = Properties.Settings.Default.DataSavePath + @"\OffLine.bin";
                if ( !File.Exists(@Binpath) )
                {
                    Program.SinglePictureExtractPoint(ImagePath, Properties.Settings.Default.DataSavePath);
                }
                Bitmap OfflineExperimentImage = new Bitmap(ImagePath);
                Bitmap bmp = new Bitmap(OfflineExperimentImage.Width, OfflineExperimentImage.Height, PixelFormat.Format32bppArgb);
                Graphics Draw = Graphics.FromImage(bmp);
                Draw.DrawImage(OfflineExperimentImage, 0, 0, OfflineExperimentImage.Width, OfflineExperimentImage.Height);                
                ImageDrawMark(bmp, @Binpath);
                ExperimentImage.Image.Dispose();//使用前先将前张图片释放，否则内存累加
                ExperimentImage.Image = bmp;
                Draw.Dispose();
                OfflineExperimentImage.Dispose();
                LoadMarkTree();
            }
        }

        ///<summary>
        ///模块编号：子线程执行模块
        ///作用：用于在线实验过程调用DLL实验函数，防止主线程堵塞
        ///作者：
        ///编写日期：
        ///</summary>
        Thread ExperimentThread = null;
        delegate void ExperimentDelegate(int i);

        //Experiment子线程，调用test函数
        private void ChooseExperiment()
        {
            try
            {
                test(OperatMode);
            }
            catch (System.Exception e1)
            {
                return;
            }
            closeThread();
        }

        /// <summary>
        /// 子线程实际执行函数
        /// </summary> 
        /// <param name="i">选择执行的操作</param>
        private void test(int i)
        {
            switch (i)
            {
                case 1: GenerateSimulationReport(); break;
                case 2: SingleExtractPoint(); break;
                case 3: SingleCompression(); break;
                case 4: ExtractCompression(); break;
                default: break;
            }
        }

        //生成仿真报告
        static public int[] ExtractPointArray = new int[3];//标志点数量数组
        static public float[] ExtractPointSpeedArray = new float[12];//单提点速度数组
        static public float[] CompressionSpeedArray = new float[3];//单压缩速度数组
        static public float[] SynchronizeSppedArray = new float[36];//单压缩速度数组
        private void GenerateSimulationReport()
        {
            if (RunState == 1)//仿真实验
            {
                Program.Parameter value = new Program.Parameter();//传入参数
                Program.Infomation Result = new Program.Infomation();//返回信息
                value = ParameterReset(GlobValue);
                /****  调试用代码开始  ****/
                value.ImgReadPath = Properties.Settings.Default.ImgReadPath;
                value.ImgSavePath = Properties.Settings.Default.ImgSavePath;
                value.DataReadPath = Properties.Settings.Default.DataSavePath;
                value.ImgHeight = GlobValue.ImgHeight;
                value.ImgWidth = GlobValue.ImgWidth;
                value.ImgBitDeep = GlobValue.ImgBitDeep;
                value.Threshold = GlobValue.Threshold;
                value.LengthMin = GlobValue.LengthMin;
                value.LengthMax = GlobValue.LengthMax;
                value.AreaMin = GlobValue.AreaMin;
                value.AreaMax = GlobValue.AreaMax;
                value.TerminateFlag = GlobValue.TerminateFlag;
                value.RecModelFlag = GlobValue.RecModelFlag;
                value.RecPadding = GlobValue.RecPadding;
                /****  调试用代码结束  ****/
                #region 仿真报告生成代码段
                //当压缩比为1:10时，拼图数(1~4或3),GPU块尺寸(8x8,16x16,32x32)
                value.CompressionRatio = 1;//压缩比1:10
                int index = 0;                
                for(value.PictureNum = 1; value.PictureNum <= 4; value.PictureNum++)
                {
                    bool Flag = Program.SetParameter(ref value, 16);//结构体传递参数值
                    if (Flag == true)
                    {
                        Program.Memory_application();//申请本幅图的内存(用于压缩)
                    }
                    else
                        MessageBox.Show("内存申请失败！");
                    for (value.PicBlockSize = 8; value.PicBlockSize <= 32; value.PicBlockSize *= 2)
                    {
                        if (value.ImgHeight >= 5120 && value.PictureNum > 3)//限制条件
                        {
                            ExtractPointSpeedArray[index] = 0;
                            SynchronizeSppedArray[index] = 0;
                            index++;
                            RunStatusEditItem.EditValue = 100 * index / SynchronizeSppedArray.Length;//更新进度条  
                            continue;//25M图最多拼三张
                        }
                        Program.SetParameter(ref value, 17);//结构体传递参数值
                        Program.SimulationImageTest(Form1.ImagePath, ref Result);
                        if(index < 3)
                        {
                            ExtractPointArray[index] = Result.PointNumbers;
                        }
                        ExtractPointSpeedArray[index] = Result.ExtractPointSpeed;
                        SynchronizeSppedArray[index] = Result.SynchronizeSpeed;
                        index++;
                        RunStatusEditItem.EditValue = 100 * index / SynchronizeSppedArray.Length;//更新进度条                       
                    }
                    if (value.PictureNum == 1)
                    {
                        CompressionSpeedArray[0] = Result.CompressionSpeed;
                    }
                    value.PicBlockSize = -1;//参数重置
                    Program.Memory_release();//释放申请的内存
                }
                //当压缩比为1:15时，拼图数(1~4或3),GPU块尺寸(8x8,16x16,32x32)
                value.CompressionRatio = 200;//压缩比1:15
                for (value.PictureNum = 1; value.PictureNum <= 4; value.PictureNum++)
                {
                    bool Flag = Program.SetParameter(ref value, 16);//结构体传递参数值
                    if (Flag == true)
                    {
                        Program.Memory_application();//申请本幅图的内存(用于压缩)
                    }
                    else
                        MessageBox.Show("内存申请失败！");
                    for (value.PicBlockSize = 8; value.PicBlockSize <= 32; value.PicBlockSize *= 2)
                    {
                        if (value.ImgHeight >= 5120 && value.PictureNum > 3)//限制条件
                        {
                            SynchronizeSppedArray[index] = 0;
                            index++;
                            RunStatusEditItem.EditValue = 100 * index / SynchronizeSppedArray.Length;//更新进度条  
                            continue;//25M图最多拼三张
                        }
                        Program.SetParameter(ref value, 17);//结构体传递参数值
                        Program.SimulationImageTest(Form1.ImagePath, ref Result);
                        SynchronizeSppedArray[index] = Result.SynchronizeSpeed;
                        index++;
                        RunStatusEditItem.EditValue = 100 * index / SynchronizeSppedArray.Length;//更新进度条                       
                    }
                    if (value.PictureNum == 1)
                    {
                        CompressionSpeedArray[1] = Result.CompressionSpeed;
                    }
                    value.PicBlockSize = -1;//参数重置
                    Program.Memory_release();//释放申请的内存
                }
                //当压缩比为1:20时，拼图数(1~4或3),GPU块尺寸(8x8,16x16,32x32)
                value.CompressionRatio = 2000;//压缩比1:20
                for (value.PictureNum = 1; value.PictureNum <= 4; value.PictureNum++)
                {
                    bool Flag = Program.SetParameter(ref value, 16);//结构体传递参数值
                    if (Flag == true)
                    {
                        Program.Memory_application();//申请本幅图的内存(用于压缩)
                    }
                    else
                        MessageBox.Show("内存申请失败！");
                    for (value.PicBlockSize = 8; value.PicBlockSize <= 32; value.PicBlockSize *= 2)
                    {
                        if (value.ImgHeight >= 5120 && value.PictureNum > 3)//限制条件
                        {
                            SynchronizeSppedArray[index] = 0;
                            index++;
                            RunStatusEditItem.EditValue = 100 * index / SynchronizeSppedArray.Length;//更新进度条  
                            continue;//25M图最多拼三张
                        }
                        Program.SetParameter(ref value, 17);//结构体传递参数值
                        Program.SimulationImageTest(Form1.ImagePath, ref Result);
                        SynchronizeSppedArray[index] = Result.SynchronizeSpeed;
                        index++;
                        RunStatusEditItem.EditValue = 100 * index / SynchronizeSppedArray.Length;//更新进度条                       
                    }
                    if (value.PictureNum == 1)
                    {
                        CompressionSpeedArray[2] = Result.CompressionSpeed;
                    }
                    value.PicBlockSize = -1;//参数重置
                    Program.Memory_release();//释放申请的内存
                }

                #endregion
                DevExpress.XtraEditors.XtraMessageBox.Show("仿真实验成功，结果报告已生成！");
                //进度条重置
                RunStatusEditItem.Caption = "准备就绪";
                RunStatusEditItem.EditValue = 0;
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("仿真实验启动失败！", "报错提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 传值结构体重设初值
        /// </summary> 
        /// <param name="Program.Parameter">传值结构体</param>
        private Program.Parameter ParameterReset(Program.Parameter value)
        {
            //结构体初值设为null,-1则不传值
            value.ImgReadPath = null;//图像读取路径
            value.ImgSavePath = null;//图像保存路径
            value.DataReadPath = null;//数据保存路径
            value.ImgBitDeep = -1;//位深
            value.ImgChannelNum = -1;//通道
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

            return value;
        }

        //提点操作
        private void SingleExtractPoint()
        {
            if (RunState == 2)//在线实验
            {
                Program.Infomation OnlineResult = new Program.Infomation();
                //ChooseMode = 1(单提点)
                //bool Returnvalue = Program.OnlineImageExperiment(1, FolderPath, ref OnlineResult);
                bool Returnvalue = Program.OnlineImageRecExperiment(1, ref OnlineResult);              
                if (!Returnvalue)
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show("模拟在线提标志点实验完成，共处理" + ImgProcessingNumbers + "幅图像。\n"
                    //    + "耗时：" + OnlineResult.ExtractPointTimes + " S\n"
                    //    + "实际速度：" + OnlineResult.ExtractPointSpeed + "Mb/S", "执行完成提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(imageRefreshtimer.Enabled == false )
                    {
                        imageRefreshtimer.Enabled = true;
                        imageRefreshtimer.Stop();//停止定时刷新
                    }
                    else
                        imageRefreshtimer.Stop();//停止定时刷新
                    if (MemoryFlag == true)
                    {
                        Program.Memory_release();//释放上幅图申请的内存
                        MemoryFlag = false;
                    }
                    DevExpress.XtraEditors.XtraMessageBox.Show("在线提标志点实验完成！");
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("在线实验失败！", "执行完成提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else if (RunState == 3)//离线实验
            {
                if(ImagePath == string.Empty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("请先导入图片！");
                    return;
                }
                bool Returnvalue = Program.SinglePictureExtractPoint(ImagePath, Properties.Settings.Default.DataSavePath);
                if (!Returnvalue)
                {                    
                    DevExpress.XtraEditors.XtraMessageBox.Show("提点数据已生成！");                    
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("离线实验失败！", "执行完成提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        //压缩操作
        private void SingleCompression()
        {
            if (RunState == 2)//在线实验
            {
                Program.Infomation OnlineResult = new Program.Infomation();
                //ChooseMode = 2(单压缩)
                bool Returnvalue = Program.OnlineImageRecExperiment(2, ref OnlineResult);
                if (!Returnvalue)
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show("模拟在线压缩实验完成，共处理" + ImgProcessingNumbers + "幅图像。\n"
                    //    + "耗时：" + OnlineResult.CompressionTimes + " S\n"
                    //    + "实际速度：" + OnlineResult.CompressionSpeed + "Mb/S", "执行完成提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (imageRefreshtimer.Enabled == false)
                    {
                        imageRefreshtimer.Enabled = true;
                        imageRefreshtimer.Stop();//停止定时刷新
                    }
                    else
                        imageRefreshtimer.Stop();//停止定时刷新
                    if (MemoryFlag == true)
                    {
                        Program.Memory_release();//释放上幅图申请的内存
                        MemoryFlag = false;
                    }
                    DevExpress.XtraEditors.XtraMessageBox.Show("在线压缩实验完成！");
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("在线实验失败！", "执行完成提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else if (RunState == 3)//离线实验
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("离线实验无图像压缩操作！");
            }
        }

        //提点压缩操作
        private void ExtractCompression()
        {
            if (RunState == 2)//在线实验
            {
                Program.Infomation OnlineResult = new Program.Infomation();
                //ChooseMode = 3(提点压缩)
                //bool Returnvalue = Program.OnlineImageExperiment(3, FolderPath, ref OnlineResult);
                bool Returnvalue = Program.OnlineImageRecExperiment(3, ref OnlineResult);
                if (!Returnvalue)
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show("模拟在线压缩实验完成，共处理" + ImgProcessingNumbers + "幅图像。\n"
                    //    + "耗时：" + OnlineResult.SynchronizeTimes + " S\n"
                    //    + "实际速度：" + OnlineResult.SynchronizeSpeed + "Mb/S", "执行完成提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (imageRefreshtimer.Enabled == false)
                    {
                        imageRefreshtimer.Enabled = true;
                        imageRefreshtimer.Stop();//停止定时刷新
                    }
                    else
                        imageRefreshtimer.Stop();//停止定时刷新
                    if (MemoryFlag == true)
                    {
                        Program.Memory_release();//释放上幅图申请的内存
                        MemoryFlag = false;
                    }
                    DevExpress.XtraEditors.XtraMessageBox.Show("在线提点压缩实验完成！");
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("在线实验失败！", "执行完成提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else if (RunState == 3)//离线实验
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("离线实验无提点压缩操作！");
            }
        }

        //结束子线程  
        private void closeThread()
        {
            if (ExperimentThread != null)
            {
                if (ExperimentThread.IsAlive)
                {
                    ExperimentThread.Abort();
                }
            }
        }

        /// <summary>
        /// 在线、离线实验--提标志点按键事件
        /// </summary>
        private void ExtractPointButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OperatMode = 2;//单提点操作
            closeThread();
            ExperimentThread = new Thread(new ThreadStart(ChooseExperiment));
            ExperimentThread.IsBackground = true;
            ExperimentThread.Start();

            if( RunState == 2 )//在线实验实时刷新显示区
            {
                imageRefreshtimer.Start();//开始定时刷新
            }
        }

        /// <summary>
        /// 在线、离线实验--压缩按键事件
        /// </summary>
        private void CompressionButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OperatMode = 3;//单压缩操作
            closeThread();
            ExperimentThread = new Thread(new ThreadStart(ChooseExperiment));
            ExperimentThread.IsBackground = true;
            ExperimentThread.Start();

            if (RunState == 2)//在线实验实时刷新显示区
            {
                imageRefreshtimer.Start();//开始定时刷新
            }
        }

        /// <summary>
        /// 在线、离线实验--提点压缩按键事件
        /// </summary>
        private void ExtractCompressionButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OperatMode = 4;//提点压缩操作
            closeThread();
            ExperimentThread = new Thread(new ThreadStart(ChooseExperiment));
            ExperimentThread.IsBackground = true;
            ExperimentThread.Start();

            if (RunState == 2)//在线实验实时刷新显示区
            {
                imageRefreshtimer.Start();//开始定时刷新
            }
        }

        ///<summary>
        ///模块编号：事件处理模块
        ///作用：用于不同窗体界面间的异步请求
        ///作者：
        ///编写日期：
        ///</summary>
        //事件处理方法1
        void TreeListInit_Event()
        {
            InitRightTree();
            RunState = 2;//在线实验准备就绪
        }

        //事件处理方法2,仿真测试界面委托事件
        void Simulation_Event()
        {
            ImagePathImportFlag = true;//仿真图已经导入
        }

        //事件处理方法3，仿真报告窗口参数传入
        void SimulationParForm_Event(int[] ParameterArray)
        {
            GlobValue.ImgHeight = Form1.ImageHeight;
            GlobValue.ImgWidth = Form1.ImageWidth;
            GlobValue.ImgBitDeep = ParameterArray[0];
            GlobValue.Threshold = ParameterArray[1];
            GlobValue.LengthMin = ParameterArray[2];
            GlobValue.LengthMax = ParameterArray[3];
            GlobValue.AreaMin = ParameterArray[4];
            GlobValue.AreaMax = ParameterArray[5];            
            GlobValue.TerminateFlag = ParameterArray[6];
            GlobValue.RecModelFlag = ParameterArray[7];
            GlobValue.RecPadding = ParameterArray[8];
            if (ImagePath == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入仿真测试图!");
                return;
            }

            //进度条
            RunStatusEditItem.Caption = "生成报告";
            RunStatusEditItem.EditValue = 0;

            OperatMode = 1;//生成仿真报告
            closeThread();
            ExperimentThread = new Thread(new ThreadStart(ChooseExperiment));
            ExperimentThread.IsBackground = true;
            ExperimentThread.Start();
        }

        //参数设置界面确认事件处理方法
        void EditConform_Event(int[] modifyparmeter)
        {
            bool flag;
            Program.Parameter value = new Program.Parameter();
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
            //参数修改界面传参给主界面
            ImageWidth = modifyparmeter[0];
            ImageHeight = modifyparmeter[1];
            ImgThreshold = modifyparmeter[2];
            LengthMin = modifyparmeter[3];
            LengthMax = modifyparmeter[4];
            AreaMin = modifyparmeter[5];
            AreaMax = modifyparmeter[6];
            PictureNum = modifyparmeter[7];
            Bufferlength = modifyparmeter[8];
            //导入需要传递参数的值
            value.ImgReadPath = Properties.Settings.Default.ImgReadPath;
            value.ImgSavePath = Properties.Settings.Default.ImgSavePath;
            value.DataReadPath = Properties.Settings.Default.DataSavePath;
            value.ImgBitDeep = Form1.ImgBitDeep;
            value.ImgWidth = ImageWidth;
            value.ImgHeight = ImageHeight;
            value.Threshold = ImgThreshold;
            value.LengthMin = LengthMin;
            value.LengthMax = LengthMax;
            value.AreaMin = AreaMin;
            value.AreaMax = AreaMax;
            value.CompressionRatio = CompressionRatio;
            value.PictureNum = PictureNum;
            value.PicBlockSize = PicBlockSize;
            value.TerminateFlag = 0;//是否终止实验
            value.RecModelFlag = Form1.ImgInputModeFlag;//全图|矩形模式设置 
            value.RecPadding = 5;//包围盒填充像素数目
            if (value.LengthMax < value.LengthMin)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("周长设置范围有误，请重新设置！");
                return;
            }
            LengthMin = value.LengthMin;
            LengthMax = value.LengthMax;
            if (value.AreaMax < value.AreaMin)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("面积设置范围有误，请重新设置！");
                return;
            }
            AreaMin = value.AreaMin;
            AreaMax = value.AreaMax;
            flag = Program.SetParameter(ref value, 17);//结构体传递参数值
            //传参成功
            if (flag == true)
            {
                Program.SetCameraPar(Bufferlength);
                DevExpress.XtraEditors.XtraMessageBox.Show("参数设置成功！");
            }
            //传参失败
            if (flag == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("参数未设置成功，请重新设置！");
                return;
            }
        }

        //标记设置界面确认事件处理方法
        void MarkSetConform_Event(Color[] MarkColor, byte[] MarkParSet)
        {
            //参数从标记设置界面传递过来
            markInfo.MarkColor = MarkColor[0];
            markInfo.MarkLength = MarkParSet[0];
            markInfo.MarkLinewidth = MarkParSet[1];
            markInfo.ChooseMarkColor = MarkColor[1];
            markInfo.ChooseMarkLength = MarkParSet[2];
            markInfo.ChooseMarkLinewidth = MarkParSet[3];

            DevExpress.XtraEditors.XtraMessageBox.Show("标注设置成功！");
        }

        /// <summary>
        /// 主界面--界面加载事件
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            AllInit();
            Program.SetCameraPar(Bufferlength);
        }

        /// <summary>
        /// 主界面--参数初始化
        /// 读取.setting属性表数据
        /// </summary>
        private void AllInit()
        {
            Program.HardwareInfo HardwareProp = new Program.HardwareInfo();       
            int a = Program.HardwareInit(ref HardwareProp);//硬件初始化  
            switch (a)
            {
                case 0: break;
                case 1: MessageBox.Show("存储磁盘空间不足!"); System.Environment.Exit(0); break;
                case 2: MessageBox.Show("可用GPU设备数错误!"); System.Environment.Exit(0); break;
            }
            Bufferlength = Properties.Settings.Default.Bufferlength;//缓冲区长度（图像数量）
            ImageHeight = Properties.Settings.Default.ImgHeight;//图像行数
            ImageWidth = Properties.Settings.Default.ImgWidth;//图像列数
            ImgBitDeep = Properties.Settings.Default.ImgBitDeep;//位深
            AreaMin = Properties.Settings.Default.AreaMin;//默认面积不筛选
            AreaMax = Properties.Settings.Default.AreaMax;//默认面积不筛选
            CompressionRatio = Properties.Settings.Default.CompressionRatio;//默认压缩比1:20
            PictureNum = Properties.Settings.Default.PictureNum;//默认拼图数
            PicBlockSize = Properties.Settings.Default.PicBlockSize;//默认GPU线程块尺寸
            RunState = 0;//运行状态。1、测试就绪，2、在线实验就绪，3、离线实验就绪
            OperatMode = 0;//操作模式。1、默认不操作，2、单提点，3、单压缩，4、提点压缩
            ParameterReady = false;
            gRunStatic = false;//初始状态:false , 正在运行:true
            MemoryFlag = false;
            ImagePathImportFlag = false;
            ImgInputModeFlag = Properties.Settings.Default.RecModelFlag;//全图模式
            ImgProcessingNumbers = 0;
            if (ImgInputModeFlag == 0)
            {
                ImgInputModeCheck.Caption = "全图模式";
            }
            else if (ImgInputModeFlag == 1)
            {
                ImgInputModeCheck.Caption = "矩形模式";
            }           
            markInfo.MarkColor = Properties.MarkSettings.Default.MarkColor;
            markInfo.MarkLength = Properties.MarkSettings.Default.MarkLength;//默认标注长度
            markInfo.MarkLinewidth = Properties.MarkSettings.Default.MarkLinewidth;//默认标注线宽
            markInfo.ChooseMarkColor = Properties.MarkSettings.Default.ChooseMarkColor;
            markInfo.ChooseMarkLength = Properties.MarkSettings.Default.ChooseMarkLength;//默认选中标注长度
            markInfo.ChooseMarkLinewidth = Properties.MarkSettings.Default.ChooseMarkLinewidth;//默认选中标注线宽
        }

        /// <summary>
        /// 主界面--不同实验选项卡跳转事件
        /// </summary>
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            SimulationPage.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        /// <summary>
        /// 主界面--不同实验选项卡跳转事件
        /// </summary>
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = Model.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }
           
        /// <summary>
        /// 主界面--全图模式/矩形模式按键事件
        /// </summary>
        private void ImgInputModeCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ImgInputModeFlag = ImgInputModeFlag ^ 1;
            if (ImgInputModeFlag == 0)
            {
                ImgInputModeCheck.Caption = "全图模式";
            }
            else if (ImgInputModeFlag == 1)
            {
                ImgInputModeCheck.Caption = "矩形模式";
            }
        }

        /// <summary>
        /// 在线、离线实验--图像列表加载
        /// 二级菜单点击，初始化树
        /// </summary>
        public void InitRightTree()
        {
            ImagetreeList.Controls.Clear();
            treeList.OptionsBehavior.Editable = false;//是否可编辑
            treeList.OptionsView.ShowCheckBoxes = false;
            treeList.Dock = DockStyle.Fill;
            ImagetreeList.Controls.Add(treeList);
            DataTable dt = GetTestData();
            treeList.DataSource = dt;
            //设置树的ParentFieldName 属性
            //设置树的KeyFieldName 属性
            treeList.ParentFieldName = "ParentFieldName";
            treeList.KeyFieldName = "KeyFieldName";
        }

        /// <summary>
        /// 此处获得绑定图像路径树的数据,正确设置图像文件路径后使用
        /// </summary>
        /// <returns></returns>
        public DataTable GetTestData()
        {
            DataTable tree = new DataTable();
            DataColumn treeColumn1 = new DataColumn("KeyFieldName");
            DataColumn treeColumn3 = new DataColumn("ParentFieldName");
            DataColumn treeColumn2 = new DataColumn("ImagePath");
            tree.Columns.Add(treeColumn1);
            tree.Columns.Add(treeColumn2);
            tree.Columns.Add(treeColumn3);
            //C#遍历指定文件夹中的所有文件 
            //DirectoryInfo TheFolder = new DirectoryInfo(FolderPath);
            //FileInfo[] files = TheFolder.GetFiles();
            //遍历文件夹
            //foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            //    this.listBox1.Items.Add(NextFolder.Name);
            //遍历文件
            //foreach (FileInfo NextFile in TheFolder.GetFiles())
            //    this.listBox2.Items.Add(NextFile.Name);
            string[] filterfiles = Directory.GetFiles(FolderPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp")).ToArray();
            // 文件名的升序
            Array.Sort(filterfiles, new FileNameSort());
            for (int index = 0; index < filterfiles.Length; index++)
            {
                DataRow dr = tree.NewRow();
                dr["KeyFieldName"] = index;
                dr["ParentFieldName"] = index.ToString();
                dr["ImagePath"] = filterfiles[index];
                //dr["Size"] = ((ImageHeight >> 5) * (ImageWidth >> 5)) / 1000 + "M";
                tree.Rows.Add(dr);
            }
            return tree;
        }

        /// <summary>
        /// 在线、离线实验--标志点列表加载
        /// 二级菜单点击，初始化树
        /// </summary>
        public void LoadMarkTree()
        {
            MarktreeList.Controls.Clear();
            //DevExpress.XtraTreeList.TreeList MarkList = new DevExpress.XtraTreeList.TreeList();//标志点数据列表
            MarkList.OptionsBehavior.Editable = false;//是否可编辑
            MarkList.OptionsView.ShowCheckBoxes = false;
            MarkList.Dock = DockStyle.Fill;
            MarkList.ClearNodes();
            MarktreeList.Controls.Add(MarkList);
            DataTable dt = GetMarkTreeData();
            MarkList.DataSource = dt;//更新列表，用于编辑
            //设置树的ParentFieldName 属性
            //设置树的KeyFieldName 属性
            MarkList.ParentFieldName = "ParentFieldName";
            MarkList.KeyFieldName = "KeyFieldName";
            MarktreeListBox.Text = "选择图像：" + ImagePath;
        }

        /// <summary>
        /// 此处获得绑定标志点数据树的数据,正确提取标志点后使用
        /// </summary>
        /// <returns></returns>
        public DataTable GetMarkTreeData()
        {
            DataTable tree = new DataTable();
            DataColumn treeColumn1 = new DataColumn("KeyFieldName");
            DataColumn treeColumn3 = new DataColumn("ParentFieldName");
            DataColumn treeColumn2 = new DataColumn("MarkPoint Coordinate");
            tree.Columns.Add(treeColumn1);
            tree.Columns.Add(treeColumn2);
            tree.Columns.Add(treeColumn3);

            for (int index = 0; index < markInfo.AllMarkCenter.Count; index++)
            {
                DataRow dr = tree.NewRow();
                dr["KeyFieldName"] = index;
                dr["ParentFieldName"] = index.ToString();
                dr["MarkPoint Coordinate"] = index.ToString() +" "+ markInfo.AllMarkDictionary[index];
                tree.Rows.Add(dr);             
            }
            return tree;
        }

        /// <summary>
        /// 此处获得选中标志点节点，并重绘标志
        /// </summary>
        /// <returns></returns>
        private void MarkPosButton_Click(object sender, EventArgs e)
        {
            if (RunState == 3)//离线实验
            {
                if (MarkList.FocusedNode == null)
                {
                    return;
                }
                string TreeListRow = MarkList.FocusedNode.GetDisplayText(0);//获取所选树列表行内容(图像路径)
                string[] ListRowSplit = TreeListRow.Split(new char[3] { '=', ',' , '}'});//字符串分割获取坐标值
                PointF Choosemarkpoint = new PointF();
                Choosemarkpoint.X = float.Parse(ListRowSplit[1]);//string转int
                Choosemarkpoint.Y = float.Parse(ListRowSplit[3]);//string转int
                Bitmap bmp = new Bitmap(ExperimentImage.Image.Width, ExperimentImage.Image.Height, PixelFormat.Format32bppArgb);
                Graphics Draw = Graphics.FromImage(bmp);
                Draw.DrawImage(ExperimentImage.Image, 0, 0, ExperimentImage.Image.Width, ExperimentImage.Image.Height);
                ImageDrawChooseMark(bmp, Choosemarkpoint);//绘制选中标志
                ExperimentImage.Image.Dispose();//使用前先将前张图片释放，否则内存累加
                ExperimentImage.Image = bmp;
                Draw.Dispose();
            }
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }
        private void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void ImageList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }
        private void ImageList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        /// <summary>
        /// 设置子节点的状态
        /// 选择某一节点时,该节点的子节点全部选择  取消某一节点时,该节点的子节点全部取消选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        /// <summary>
        /// 设置父节点的状态
        /// 某节点的子节点全部选择时,该节点选择   某节点的子节点未全部选择时,该节点不选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        /// <summary>
        /// 文件夹操作
        /// 删除路径下所有文件
        /// </summary>
        /// <param name="srcPath">文件夹路径</param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void Test_Click(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Program.Parameter gParameter = new Program.Parameter();
            Program.GetParameter(ref gParameter);
            MessageBox.Show("gParameter.ImgReadPath:" + gParameter.ImgReadPath +
                "\ngParameter.ImgSavePath:" + gParameter.ImgSavePath +
                "\ngParameter.DataReadPath:" + gParameter.DataReadPath +
                "\ngParameter.ImgBitDeep:" + gParameter.ImgBitDeep +
                "\ngParameter.ImgChannelNum:" + gParameter.ImgChannelNum +
                "\ngParameter.PicBlockSize:" + gParameter.PicBlockSize);
        }

        ///<summary>
        ///内部类
        ///功能：按文件名升序排序
        ///</summary>
        public class FileNameSort : IComparer<object>
        {
            //调用DLL
            [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
            private static extern int StrCmpLogicalW(string param1, string param2);
            //前后文件名进行比较。
            public int Compare(object name1, object name2)
            {
                if (null == name1 && null == name2)
                {
                    return 0;
                }
                if (null == name1)
                {
                    return -1;
                }
                if (null == name2)
                {
                    return 1;
                }
                return StrCmpLogicalW(name1.ToString(), name2.ToString());
            }
        }

        /// <summary>
        /// 全部标记画图操作
        /// 在全部标志点位置画十字架
        /// </summary>
        /// <param name="srcImg">待操作图片</param>
        /// <param name="binfilePath">bin文件路径</param>
        private Bitmap ImageDrawMark(Bitmap srcImg, string binfilePath)
        {
            FileStream binfiledata = new FileStream(binfilePath, FileMode.OpenOrCreate, FileAccess.Read);//bin文件内容读入文件流
            byte[] Circleinfobytearray = new byte[binfiledata.Length];//初始化字节数组
            binfiledata.Read(Circleinfobytearray, 0, Circleinfobytearray.Length);//读取流中数据到字节数组中
            binfiledata.Close();//关闭文件流
            double[] Circleinfoshortarray = new double[Circleinfobytearray.Length / sizeof(double)];//初始化short数组
            Buffer.BlockCopy(Circleinfobytearray, 0, Circleinfoshortarray, 0, Circleinfoshortarray.Length * sizeof(double));//数据拷贝

            Graphics Draw = Graphics.FromImage(srcImg);
            Pen DrawPen = new Pen(markInfo.MarkColor, markInfo.MarkLinewidth);
            if (markInfo.AllMarkCenter != null)
            {
                markInfo.AllMarkCenter.Clear();
            }
            if (markInfo.AllMarkDictionary != null)
            {
                markInfo.AllMarkDictionary.Clear();
            }
            for (int i = 0; i < Circleinfoshortarray.Length; i += 3)
            {
                markInfo.MarkCenterPos.X = (float)Circleinfoshortarray[i + 2];
                markInfo.MarkCenterPos.Y = (float)Circleinfoshortarray[i + 1];
                markInfo.AllMarkCenter.Add(markInfo.MarkCenterPos);
                markInfo.AllMarkDictionary.Add(i/3, markInfo.MarkCenterPos);//标志位字典
                //画十字标记，考虑线宽偏移
                Draw.DrawLine(DrawPen, markInfo.MarkCenterPos.X, markInfo.MarkCenterPos.Y - markInfo.MarkLength / 2, 
                    markInfo.MarkCenterPos.X, markInfo.MarkCenterPos.Y + markInfo.MarkLength / 2);
                Draw.DrawLine(DrawPen, markInfo.MarkCenterPos.X - markInfo.MarkLength / 2, 
                    markInfo.MarkCenterPos.Y, markInfo.MarkCenterPos.X + markInfo.MarkLength / 2, markInfo.MarkCenterPos.Y);
                //Draw.FillEllipse(Brushes.Red, Pos.X, Pos.Y, 10, 10);
            }
            DrawPen.Dispose();
            Draw.Dispose();
            return srcImg;
        }

        /// <summary>
        /// 选中标记画图操作
        /// 在选中标志点位置画十字架
        /// </summary>
        /// <param name="srcImg">待操作图片</param>
        /// <param name="markpoint">选中标志点</param>
        private Bitmap ImageDrawChooseMark(Bitmap srcImg,PointF markpoint)
        {
            Graphics Draw = Graphics.FromImage(srcImg);
            Pen DrawPen = new Pen(markInfo.ChooseMarkColor, markInfo.ChooseMarkLinewidth);
            //画十字标记，考虑线宽偏移
            Draw.DrawLine(DrawPen, markpoint.X, markpoint.Y - markInfo.ChooseMarkLength / 2, markpoint.X, markpoint.Y + markInfo.ChooseMarkLength / 2);
            Draw.DrawLine(DrawPen, markpoint.X - markInfo.ChooseMarkLength / 2, markpoint.Y, markpoint.X + markInfo.ChooseMarkLength / 2, markpoint.Y);
            //Draw.FillEllipse(Brushes.Red, Pos.X, Pos.Y, 10, 10);
            DrawPen.Dispose();
            Draw.Dispose();
            return srcImg;
        }
    }
}