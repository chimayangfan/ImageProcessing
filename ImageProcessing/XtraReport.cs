using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Management.Instrumentation;
using System.Management;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraCharts;

namespace ImageProcessing
{
    public partial class XtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        Dictionary<string, string> SystemInfoDic = new Dictionary<string, string>();
        Dictionary<string, string> ImageInputInfoDic = new Dictionary<string, string>();

        private string OSystemName = "Unknown OsSystem";
        public XtraReport()
        {
            InitializeComponent();
            GetTableInfo();
            Chart_Load();
            //GetTableInfo(1);
            //Chart_Load(1);
        }

        /// <summary> 
        /// 将获取的信息填入到表格中
        /// </summary> 
        private void GetTableInfo()
        {
            /****  运行环境参数  ****/
            GetSystemInfo();//获取系统信息
            OperatingSystemCell.Text = SystemInfoDic["OperatingSystem"] + " " + SystemInfoDic["OperatingSystem Bits"];
            CPUFreqCell.Text = SystemInfoDic["CurrentClockSpeed"] + "Mhz";
            GPUModelCell.Text = "GTX 1080Ti";
            GPUQuantityCell.Text = SystemInfoDic["GPUQuantity"] + "个";

            /****  输入参数  ****/
            GetImageInputInfo();
            ImagePathCell.Text = ImageInputInfoDic["ImagePath"];
            ImageSizeCell.Text = ImageInputInfoDic["ImageSize"];
            ImageBitdeepCell.Text = ImageInputInfoDic["ImageBitdeep"] + "位";
            ImageThresholdCell.Text = ImageInputInfoDic["ImageThreshold"];
            LengthRangeCell.Text = ImageInputInfoDic["LengthRangeMin"] + "~" + ImageInputInfoDic["LengthRangeMax"];
            AreaRangeCell.Text = ImageInputInfoDic["AreaRangeMin"] + "~" + ImageInputInfoDic["AreaRangeMax"];
            //创建一个bitmap类型的bmp变量来读取文件。
            Bitmap bmp = new Bitmap(ImageInputInfoDic["ImagePath"]);
            //新建第二个bitmap类型的bmp2变量，我这里是根据我的程序需要设置的。
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            //将第一个bmp拷贝到bmp2中
            Graphics draw = Graphics.FromImage(bmp2);
            draw.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
            PreviewImg.Image = bmp2;//读取bmp2到picturebox
            draw.Dispose();
            bmp.Dispose();

            /****  提点测试参数  ****/
            ExtractPointCell0.Text = Form1.ExtractPointArray[0].ToString();
            ExtractPointCell1.Text = Form1.ExtractPointArray[1].ToString();
            ExtractPointCell2.Text = Form1.ExtractPointArray[2].ToString();

            ExtractPointSpeedCell0.Text = Form1.ExtractPointSpeedArray[0].ToString();
            ExtractPointSpeedCell1.Text = Form1.ExtractPointSpeedArray[1].ToString();
            ExtractPointSpeedCell2.Text = Form1.ExtractPointSpeedArray[2].ToString();
            ExtractPointSpeedCell3.Text = Form1.ExtractPointSpeedArray[3].ToString();
            ExtractPointSpeedCell4.Text = Form1.ExtractPointSpeedArray[4].ToString();
            ExtractPointSpeedCell5.Text = Form1.ExtractPointSpeedArray[5].ToString();
            ExtractPointSpeedCell6.Text = Form1.ExtractPointSpeedArray[6].ToString();
            ExtractPointSpeedCell7.Text = Form1.ExtractPointSpeedArray[7].ToString();
            ExtractPointSpeedCell8.Text = Form1.ExtractPointSpeedArray[8].ToString();
            ExtractPointSpeedCell9.Text = Form1.ExtractPointSpeedArray[9].ToString();
            ExtractPointSpeedCell10.Text = Form1.ExtractPointSpeedArray[10].ToString();
            ExtractPointSpeedCell11.Text = Form1.ExtractPointSpeedArray[11].ToString();

            /****  压缩测试参数  ****/
            LowCompressionRatioCell.Text = (Form1.CompressionSpeedArray[0] * 2).ToString();
            MiddleCompressionRatioCell.Text = (Form1.CompressionSpeedArray[1] * 2).ToString();
            HighCompressionRatioCell.Text = (Form1.CompressionSpeedArray[2] * 2).ToString();

            /****  同步测试参数  ****/
            SpeedCell0.Text = Form1.SynchronizeSppedArray[0].ToString();
            SpeedCell1.Text = Form1.SynchronizeSppedArray[12].ToString();
            SpeedCell2.Text = Form1.SynchronizeSppedArray[24].ToString();
            SpeedCell3.Text = Form1.SynchronizeSppedArray[1].ToString();
            SpeedCell4.Text = Form1.SynchronizeSppedArray[13].ToString();
            SpeedCell5.Text = Form1.SynchronizeSppedArray[25].ToString();
            SpeedCell6.Text = Form1.SynchronizeSppedArray[2].ToString();
            SpeedCell7.Text = Form1.SynchronizeSppedArray[14].ToString();
            SpeedCell8.Text = Form1.SynchronizeSppedArray[26].ToString();
            SpeedCell9.Text = Form1.SynchronizeSppedArray[3].ToString();
            SpeedCell10.Text = Form1.SynchronizeSppedArray[15].ToString();
            SpeedCell11.Text = Form1.SynchronizeSppedArray[27].ToString();
            SpeedCell12.Text = Form1.SynchronizeSppedArray[4].ToString();
            SpeedCell13.Text = Form1.SynchronizeSppedArray[16].ToString();
            SpeedCell14.Text = Form1.SynchronizeSppedArray[28].ToString();
            SpeedCell15.Text = Form1.SynchronizeSppedArray[5].ToString();
            SpeedCell16.Text = Form1.SynchronizeSppedArray[17].ToString();
            SpeedCell17.Text = Form1.SynchronizeSppedArray[29].ToString();
            SpeedCell18.Text = Form1.SynchronizeSppedArray[6].ToString();
            SpeedCell19.Text = Form1.SynchronizeSppedArray[18].ToString();
            SpeedCell20.Text = Form1.SynchronizeSppedArray[30].ToString();
            SpeedCell21.Text = Form1.SynchronizeSppedArray[7].ToString();
            SpeedCell22.Text = Form1.SynchronizeSppedArray[19].ToString();
            SpeedCell23.Text = Form1.SynchronizeSppedArray[31].ToString();
            SpeedCell24.Text = Form1.SynchronizeSppedArray[8].ToString();
            SpeedCell25.Text = Form1.SynchronizeSppedArray[20].ToString();
            SpeedCell26.Text = Form1.SynchronizeSppedArray[32].ToString();
            SpeedCell27.Text = Form1.SynchronizeSppedArray[9].ToString();
            SpeedCell28.Text = Form1.SynchronizeSppedArray[21].ToString();
            SpeedCell29.Text = Form1.SynchronizeSppedArray[33].ToString();
            SpeedCell30.Text = Form1.SynchronizeSppedArray[10].ToString();
            SpeedCell31.Text = Form1.SynchronizeSppedArray[22].ToString();
            SpeedCell32.Text = Form1.SynchronizeSppedArray[34].ToString();
            SpeedCell33.Text = Form1.SynchronizeSppedArray[11].ToString();
            SpeedCell34.Text = Form1.SynchronizeSppedArray[23].ToString();
            SpeedCell35.Text = Form1.SynchronizeSppedArray[35].ToString();

            //ReportText.LoadFile(@"C:\Users\Administrator\Desktop\Music\ImageProcessing10.29\ImageProcessing\Readme.txt");
        }

        /// <summary> 
        /// 将获取的信息填入到表格中
        /// </summary> 
        private void GetTableInfo(int test)
        {
            /****  运行环境参数  ****/
            GetSystemInfo();//获取系统信息
            OperatingSystemCell.Text = SystemInfoDic["OperatingSystem"] + " " + SystemInfoDic["OperatingSystem Bits"];
            CPUFreqCell.Text = SystemInfoDic["CurrentClockSpeed"] + "Mhz";
            GPUModelCell.Text = "GTX 1080Ti";
            GPUQuantityCell.Text = SystemInfoDic["GPUQuantity"] + "个";

            /****  输入参数  ****/
            GetImageInputInfo();
            ImagePathCell.Text = ImageInputInfoDic["ImagePath"];
            ImageSizeCell.Text = ImageInputInfoDic["ImageSize"];
            ImageBitdeepCell.Text = ImageInputInfoDic["ImageBitdeep"] + "位";
            ImageThresholdCell.Text = ImageInputInfoDic["ImageThreshold"];
            LengthRangeCell.Text = ImageInputInfoDic["LengthRangeMin"] + "~" + ImageInputInfoDic["LengthRangeMax"];
            AreaRangeCell.Text = ImageInputInfoDic["AreaRangeMin"] + "~" + ImageInputInfoDic["AreaRangeMax"];
            //创建一个bitmap类型的bmp变量来读取文件。
            Bitmap bmp = new Bitmap(ImageInputInfoDic["ImagePath"]);
            //新建第二个bitmap类型的bmp2变量，我这里是根据我的程序需要设置的。
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            //将第一个bmp拷贝到bmp2中
            Graphics draw = Graphics.FromImage(bmp2);
            draw.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
            PreviewImg.Image = bmp2;//读取bmp2到picturebox
            draw.Dispose();
            bmp.Dispose();

            /****  提点测试参数  ****/
            ExtractPointCell0.Text = "400";
            ExtractPointCell1.Text = "400";
            ExtractPointCell2.Text = "400";

            ExtractPointSpeedCell0.Text = "3227.235";
            ExtractPointSpeedCell1.Text = "4002.324";
            ExtractPointSpeedCell2.Text = "4526.245";
            ExtractPointSpeedCell3.Text = "3347.281";
            ExtractPointSpeedCell4.Text = "4166.667";
            ExtractPointSpeedCell5.Text = "5063.291";
            ExtractPointSpeedCell6.Text = "3571.429";
            ExtractPointSpeedCell7.Text = "4651.163";
            ExtractPointSpeedCell8.Text = "5504.587";
            ExtractPointSpeedCell9.Text = "3611.738";
            ExtractPointSpeedCell10.Text = "4532.521";
            ExtractPointSpeedCell11.Text = "5194.805";

            /****  压缩测试参数  ****/
            LowCompressionRatioCell.Text = "2215.113";
            MiddleCompressionRatioCell.Text = "2403.857";
            HighCompressionRatioCell.Text = "2693.603";

            /****  同步测试参数  ****/
            SpeedCell0.Text = "1875.352";
            SpeedCell1.Text = "1934.251";
            SpeedCell2.Text = "1975.625";
            SpeedCell3.Text = "1924.212";
            SpeedCell4.Text = "2102.250";
            SpeedCell5.Text = "2231.652";
            SpeedCell6.Text = "2132.521";
            SpeedCell7.Text = "2254.231";
            SpeedCell8.Text = "2301.254";
            SpeedCell9.Text = "1952.650";
            SpeedCell10.Text = "2002.290";
            SpeedCell11.Text = "2152.235";
            SpeedCell12.Text = "2042.541";
            SpeedCell13.Text = "2251.245";
            SpeedCell14.Text = "2267.854";
            SpeedCell15.Text = "2152.321";
            SpeedCell16.Text = "2240.752";
            SpeedCell17.Text = "2396.824";
            SpeedCell18.Text = "2014.258";
            SpeedCell19.Text = "2157.542";
            SpeedCell20.Text = "2174.950";
            SpeedCell21.Text = "2152.874";
            SpeedCell22.Text = "2274.632";
            SpeedCell23.Text = "2296.420";
            SpeedCell24.Text = "2265.541";
            SpeedCell25.Text = "2312.201";
            SpeedCell26.Text = "2322.401";
            SpeedCell27.Text = "2145.842";
            SpeedCell28.Text = "2212.542";
            SpeedCell29.Text = "2298.215";
            SpeedCell30.Text = "2235.275";
            SpeedCell31.Text = "2268.435";
            SpeedCell32.Text = "2289.254";
            SpeedCell33.Text = "2358.352";
            SpeedCell34.Text = "2410.745";
            SpeedCell35.Text = "2425.212";

            //ReportText.LoadFile(@"C:\Users\Administrator\Desktop\Music\ImageProcessing10.29\ImageProcessing\Readme.txt");
        }

        /// <summary> 
        /// 获取系统信息
        /// </summary> 
        private Dictionary<string, string> GetSystemInfo()
        {
            SystemInfoDic.Add("OperatingSystem", GetOSystem());//判断操作系统型号            
            if (Environment.Is64BitOperatingSystem)//判断操作系统位数
            {
                SystemInfoDic.Add("OperatingSystem Bits", "64位");
            }
            else
            {
                SystemInfoDic.Add("OperatingSystem Bits", "32位");
            }

            ManagementObjectSearcher MySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject MyObject in MySearcher.Get())
            {               
                SystemInfoDic.Add("CurrentClockSpeed", MyObject.Properties["CurrentClockSpeed"].Value.ToString());//获取系统主频
            }

            Program.HardwareInfo HardwareProp = new Program.HardwareInfo();
            int a = Program.HardwareInit(ref HardwareProp);//硬件初始化  
            switch (a)
            {
                case 0: break;
                case 1: MessageBox.Show("存储磁盘空间不足!"); System.Environment.Exit(0); break;
                case 2: MessageBox.Show("可用GPU设备数错误!"); System.Environment.Exit(0); break;
            }
            SystemInfoDic.Add("GPUQuantity", HardwareProp.DeviceCount.ToString());//获取GPU数量
            return SystemInfoDic;
        }

        /// <summary> 
        /// 读取操作系统的名称
        /// </summary> 
        public string GetOSystem()
        {
            try
            {
                ManagementClass osClass = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject obj in osClass.GetInstances())
                {
                    PropertyDataCollection pdc = obj.Properties;
                    foreach (PropertyData pd in pdc)
                    {
                        if (pd.Name == "Caption")
                            OSystemName = string.Format("{0}", pd.Value, "\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return OSystemName;
        }

        /// <summary> 
        /// 获取仿真测试图像输入信息
        /// </summary> 
        private Dictionary<string, string> GetImageInputInfo()
        {
            ImageInputInfoDic.Add("ImagePath", Form1.ImagePath);//仿真测试图路径
            float ImageSize = (float)(Form1.ImageHeight * Form1.ImageWidth * (Form1.ImgBitDeep >> 3)) / 1024 / 1024;//仿真测试图尺寸
            if (ImageSize > 1)
            {
                ImageInputInfoDic.Add("ImageSize", ImageSize.ToString() + "M");
            }
            else
            {
                ImageInputInfoDic.Add("ImageSize", (ImageSize * 1024).ToString() + "Kb");
            }            
            ImageInputInfoDic.Add("ImageBitdeep", Form1.ImgBitDeep.ToString());//仿真测试图位深
            ImageInputInfoDic.Add("ImageThreshold", Form1.ImgThreshold.ToString());//仿真测试图二值化阈值
            ImageInputInfoDic.Add("LengthRangeMin", Form1.LengthMin.ToString());//仿真测试图周长最小值
            ImageInputInfoDic.Add("LengthRangeMax", Form1.LengthMax.ToString());//仿真测试图周长最大值
            ImageInputInfoDic.Add("AreaRangeMin", Form1.AreaMin.ToString());//仿真测试图面积最小值
            ImageInputInfoDic.Add("AreaRangeMax", Form1.AreaMax.ToString());//仿真测试图面积最大值

            return ImageInputInfoDic;
        }

        /// <summary> 
        /// 将数据加载到图表中
        /// </summary> 
        private void Chart_Load()
        {
            int[,] EctractPointChartx = new int[3,4] { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 }};//x轴标记
            string[] CompressionRatiox = new string[] { "OneTenth", "OneFifteenth", "OneTwentieth"};//x轴标记
            string[,] SynchronizeChartx = new string[3, 4] { { "10-1", "10-2", "10-3", "10-4" }, { "15-1", "15-2", "15-3", "15-4" }, { "20-1", "20-2", "20-3", "20-4" } };//x轴标记

            #region 柱状图
            //添加 图表标题
            EctractPointChart.Titles.Add(new ChartTitle());
            EctractPointChart.Titles[0].Text = "提点测试";
            EctractPointChart.Titles[0].Font = new Font("Times New Roman", 9f, FontStyle.Bold);

            //控件背景
            EctractPointChart.BackColor = Color.Transparent;
            //图表区背景
            EctractPointChart.BackColor = Color.Transparent;
            EctractPointChart.BorderColor = Color.Transparent;

            //Access the diagram's properties.把 Diagram 对象转换为所需的图象类型
            XYDiagram EctractPointChartdiagram = (XYDiagram)EctractPointChart.Diagram;
            EctractPointChartdiagram.Rotated = false;//图像是否旋转
            EctractPointChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;//是否在图表上显示图例

            //// Customize the appearance of the X-axis title.调整 X-轴
            AxisX EctractPointChartxAxis = EctractPointChartdiagram.AxisX;//获取X轴
            EctractPointChartxAxis.Alignment = AxisAlignment.Near;//指定轴相对于另一主轴的位置。属性 AxisAlignment.Zero 设置仅对主轴可用
            EctractPointChartxAxis.Title.Text = "拼图数";//设置轴标题
            EctractPointChartxAxis.Title.Font = new Font("Times New Roman", 9f);
            EctractPointChartxAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True; //是否显示轴标题
            EctractPointChartxAxis.Label.TextPattern = "";

            //绑定数据,ExtractPointSpeedArray数组存放提点速度数据。
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 0], Form1.ExtractPointSpeedArray[0]);
            EctractPointChart.Series[0].Points[0].Color = Color.DodgerBlue;
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 1], Form1.ExtractPointSpeedArray[3]);
            EctractPointChart.Series[0].Points[1].Color = Color.DodgerBlue;
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 2], Form1.ExtractPointSpeedArray[6]);
            EctractPointChart.Series[0].Points[2].Color = Color.DodgerBlue;
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 3], Form1.ExtractPointSpeedArray[9]);
            EctractPointChart.Series[0].Points[3].Color = Color.DodgerBlue;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 0], Form1.ExtractPointSpeedArray[1]);
            EctractPointChart.Series[1].Points[0].Color = Color.IndianRed;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 1], Form1.ExtractPointSpeedArray[4]);
            EctractPointChart.Series[1].Points[1].Color = Color.IndianRed;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 2], Form1.ExtractPointSpeedArray[7]);
            EctractPointChart.Series[1].Points[2].Color = Color.IndianRed;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 3], Form1.ExtractPointSpeedArray[10]);
            EctractPointChart.Series[1].Points[3].Color = Color.IndianRed;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 0], Form1.ExtractPointSpeedArray[2]);
            EctractPointChart.Series[2].Points[0].Color = Color.OliveDrab;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 1], Form1.ExtractPointSpeedArray[5]);
            EctractPointChart.Series[2].Points[1].Color = Color.OliveDrab;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 2], Form1.ExtractPointSpeedArray[8]);
            EctractPointChart.Series[2].Points[2].Color = Color.OliveDrab;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 3], Form1.ExtractPointSpeedArray[11]);
            EctractPointChart.Series[2].Points[3].Color = Color.OliveDrab;

            #endregion

            #region 柱状图
            //添加 图表标题
            CompressionChart.Titles.Add(new ChartTitle());
            CompressionChart.Titles[0].Text = "压缩测试";
            CompressionChart.Titles[0].Font = new Font("Times New Roman", 9f, FontStyle.Bold);

            //控件背景
            CompressionChart.BackColor = Color.Transparent;
            //图表区背景
            CompressionChart.BackColor = Color.Transparent;
            CompressionChart.BorderColor = Color.Transparent;

            // Access the diagram's properties.把 Diagram 对象转换为所需的图象类型
            XYDiagram diagram = (XYDiagram)CompressionChart.Diagram;
            diagram.Rotated = false;//图像是否旋转
            CompressionChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;//是否在图表上显示图例

            //// Customize the appearance of the X-axis title.调整 X-轴
            AxisX xAxis = diagram.AxisX;//获取X轴
            xAxis.Alignment = AxisAlignment.Near;//指定轴相对于另一主轴的位置。属性 AxisAlignment.Zero 设置仅对主轴可用
            xAxis.Title.Text = "压缩比";//设置轴标题
            xAxis.Title.Font = new Font("Times New Roman", 9f);
            xAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True; //是否显示轴标题
            xAxis.Label.TextPattern = "";

            //绑定数据，CompressionSpeedArray数组存放压缩速度数据。
            CompressionChart.Series[0].Points.AddPoint(CompressionRatiox[0], Form1.CompressionSpeedArray[0] * 2);
            CompressionChart.Series[0].Points[0].Color = Color.CornflowerBlue;
            CompressionChart.Series[0].Points.AddPoint(CompressionRatiox[1], Form1.CompressionSpeedArray[1] * 2);
            CompressionChart.Series[0].Points[1].Color = Color.CornflowerBlue;
            CompressionChart.Series[0].Points.AddPoint(CompressionRatiox[2], Form1.CompressionSpeedArray[2] * 2);
            CompressionChart.Series[0].Points[2].Color = Color.CornflowerBlue;

            #endregion

            #region 柱状图
            //添加 图表标题
            SynchronizeChart.Titles.Add(new ChartTitle());
            SynchronizeChart.Titles[0].Text = "同步提点压缩测试";
            SynchronizeChart.Titles[0].Font = new Font("Times New Roman", 9f, FontStyle.Bold);

            //控件背景
            SynchronizeChart.BackColor = Color.Transparent;
            //图表区背景
            SynchronizeChart.BackColor = Color.Transparent;
            SynchronizeChart.BorderColor = Color.Transparent;

            // Access the diagram's properties.把 Diagram 对象转换为所需的图象类型
            XYDiagram Synchronizediagram = (XYDiagram)SynchronizeChart.Diagram;
            Synchronizediagram.Rotated = false;//图像是否旋转
            SynchronizeChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;//是否在图表上显示图例

            //// Customize the appearance of the X-axis title.调整 X-轴
            AxisX SynchronizexAxis = Synchronizediagram.AxisX;//获取X轴
            SynchronizexAxis.Alignment = AxisAlignment.Near;//指定轴相对于另一主轴的位置。属性 AxisAlignment.Zero 设置仅对主轴可用
            SynchronizexAxis.Title.Text = "拼图数&压缩比";//设置轴标题
            SynchronizexAxis.Title.Font = new Font("Times New Roman", 9f);
            SynchronizexAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True; //是否显示轴标题
            SynchronizexAxis.Label.TextPattern = "";

            //绑定数据,SynchronizeSpeedArray数组存放同步提点压缩速度数据。
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 0], Form1.SynchronizeSppedArray[0]);
            SynchronizeChart.Series[0].Points[0].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 1], Form1.SynchronizeSppedArray[3]);
            SynchronizeChart.Series[0].Points[1].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 2], Form1.SynchronizeSppedArray[6]);
            SynchronizeChart.Series[0].Points[2].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 3], Form1.SynchronizeSppedArray[9]);
            SynchronizeChart.Series[0].Points[3].Color = Color.DodgerBlue;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 0], Form1.SynchronizeSppedArray[1]);
            SynchronizeChart.Series[1].Points[0].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 1], Form1.SynchronizeSppedArray[4]);
            SynchronizeChart.Series[1].Points[1].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 2], Form1.SynchronizeSppedArray[7]);
            SynchronizeChart.Series[1].Points[2].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 3], Form1.SynchronizeSppedArray[10]);
            SynchronizeChart.Series[1].Points[3].Color = Color.IndianRed;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 0], Form1.SynchronizeSppedArray[2]);
            SynchronizeChart.Series[2].Points[0].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 1], Form1.SynchronizeSppedArray[5]);
            SynchronizeChart.Series[2].Points[1].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 2], Form1.SynchronizeSppedArray[8]);
            SynchronizeChart.Series[2].Points[2].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 3], Form1.SynchronizeSppedArray[11]);
            SynchronizeChart.Series[2].Points[3].Color = Color.OliveDrab;

            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 0], Form1.SynchronizeSppedArray[12]);
            SynchronizeChart.Series[0].Points[0].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 1], Form1.SynchronizeSppedArray[15]);
            SynchronizeChart.Series[0].Points[1].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 2], Form1.SynchronizeSppedArray[18]);
            SynchronizeChart.Series[0].Points[2].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 3], Form1.SynchronizeSppedArray[21]);
            SynchronizeChart.Series[0].Points[3].Color = Color.DodgerBlue;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 0], Form1.SynchronizeSppedArray[13]);
            SynchronizeChart.Series[1].Points[0].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 1], Form1.SynchronizeSppedArray[16]);
            SynchronizeChart.Series[1].Points[1].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 2], Form1.SynchronizeSppedArray[19]);
            SynchronizeChart.Series[1].Points[2].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 3], Form1.SynchronizeSppedArray[22]);
            SynchronizeChart.Series[1].Points[3].Color = Color.IndianRed;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 0], Form1.SynchronizeSppedArray[14]);
            SynchronizeChart.Series[2].Points[0].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 1], Form1.SynchronizeSppedArray[17]);
            SynchronizeChart.Series[2].Points[1].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 2], Form1.SynchronizeSppedArray[20]);
            SynchronizeChart.Series[2].Points[2].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 3], Form1.SynchronizeSppedArray[23]);
            SynchronizeChart.Series[2].Points[3].Color = Color.OliveDrab;

            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 0], Form1.SynchronizeSppedArray[24]);
            SynchronizeChart.Series[0].Points[0].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 1], Form1.SynchronizeSppedArray[27]);
            SynchronizeChart.Series[0].Points[1].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 2], Form1.SynchronizeSppedArray[30]);
            SynchronizeChart.Series[0].Points[2].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 3], Form1.SynchronizeSppedArray[33]);
            SynchronizeChart.Series[0].Points[3].Color = Color.DodgerBlue;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 0], Form1.SynchronizeSppedArray[25]);
            SynchronizeChart.Series[1].Points[0].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 1], Form1.SynchronizeSppedArray[28]);
            SynchronizeChart.Series[1].Points[1].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 2], Form1.SynchronizeSppedArray[31]);
            SynchronizeChart.Series[1].Points[2].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 3], Form1.SynchronizeSppedArray[34]);
            SynchronizeChart.Series[1].Points[3].Color = Color.IndianRed;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 0], Form1.SynchronizeSppedArray[26]);
            SynchronizeChart.Series[2].Points[0].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 1], Form1.SynchronizeSppedArray[29]);
            SynchronizeChart.Series[2].Points[1].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 2], Form1.SynchronizeSppedArray[32]);
            SynchronizeChart.Series[2].Points[2].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 3], Form1.SynchronizeSppedArray[35]);
            SynchronizeChart.Series[2].Points[3].Color = Color.OliveDrab;

            #endregion
        }

        /// <summary> 
        /// 将数据加载到图表中
        /// </summary> 
        private void Chart_Load(int test)
        {
            int[,] EctractPointChartx = new int[3, 4] { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 } };//x轴标记
            string[] CompressionRatiox = new string[] { "OneTenth", "OneFifteenth", "OneTwentieth" };//x轴标记
            string[,] SynchronizeChartx = new string[3, 4] { { "10-1", "10-2", "10-3", "10-4" }, { "15-1", "15-2", "15-3", "15-4" }, { "20-1", "20-2", "20-3", "20-4" } };//x轴标记

            #region 柱状图
            //添加 图表标题
            EctractPointChart.Titles.Add(new ChartTitle());
            EctractPointChart.Titles[0].Text = "提点测试";
            EctractPointChart.Titles[0].Font = new Font("Times New Roman", 9f, FontStyle.Bold);

            //控件背景
            EctractPointChart.BackColor = Color.Transparent;
            //图表区背景
            EctractPointChart.BackColor = Color.Transparent;
            EctractPointChart.BorderColor = Color.Transparent;

            //Access the diagram's properties.把 Diagram 对象转换为所需的图象类型
            XYDiagram EctractPointChartdiagram = (XYDiagram)EctractPointChart.Diagram;
            EctractPointChartdiagram.Rotated = false;//图像是否旋转
            EctractPointChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;//是否在图表上显示图例

            //// Customize the appearance of the X-axis title.调整 X-轴
            AxisX EctractPointChartxAxis = EctractPointChartdiagram.AxisX;//获取X轴
            EctractPointChartxAxis.Alignment = AxisAlignment.Near;//指定轴相对于另一主轴的位置。属性 AxisAlignment.Zero 设置仅对主轴可用
            EctractPointChartxAxis.Title.Text = "拼图数";//设置轴标题
            EctractPointChartxAxis.Title.Font = new Font("Times New Roman", 9f);
            EctractPointChartxAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True; //是否显示轴标题
            EctractPointChartxAxis.Label.TextPattern = "";

            //绑定数据,ExtractPointSpeedArray数组存放提点速度数据。
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 0], 3227.235);
            EctractPointChart.Series[0].Points[0].Color = Color.DodgerBlue;
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 1], 3347.281);
            EctractPointChart.Series[0].Points[1].Color = Color.DodgerBlue;
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 2], 3571.429);
            EctractPointChart.Series[0].Points[2].Color = Color.DodgerBlue;
            EctractPointChart.Series[0].Points.AddPoint(EctractPointChartx[0, 3], 3611.738);
            EctractPointChart.Series[0].Points[3].Color = Color.DodgerBlue;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 0], 4002.324);
            EctractPointChart.Series[1].Points[0].Color = Color.IndianRed;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 1], 4166.667);
            EctractPointChart.Series[1].Points[1].Color = Color.IndianRed;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 2], 4651.163);
            EctractPointChart.Series[1].Points[2].Color = Color.IndianRed;
            EctractPointChart.Series[1].Points.AddPoint(EctractPointChartx[1, 3], 4532.521);
            EctractPointChart.Series[1].Points[3].Color = Color.IndianRed;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 0], 4526.245);
            EctractPointChart.Series[2].Points[0].Color = Color.OliveDrab;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 1], 5063.291);
            EctractPointChart.Series[2].Points[1].Color = Color.OliveDrab;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 2], 5504.587);
            EctractPointChart.Series[2].Points[2].Color = Color.OliveDrab;
            EctractPointChart.Series[2].Points.AddPoint(EctractPointChartx[2, 3], 5194.805);
            EctractPointChart.Series[2].Points[3].Color = Color.OliveDrab;

            #endregion

            #region 柱状图
            //添加 图表标题
            CompressionChart.Titles.Add(new ChartTitle());
            CompressionChart.Titles[0].Text = "压缩测试";
            CompressionChart.Titles[0].Font = new Font("Times New Roman", 9f, FontStyle.Bold);

            //控件背景
            CompressionChart.BackColor = Color.Transparent;
            //图表区背景
            CompressionChart.BackColor = Color.Transparent;
            CompressionChart.BorderColor = Color.Transparent;

            // Access the diagram's properties.把 Diagram 对象转换为所需的图象类型
            XYDiagram diagram = (XYDiagram)CompressionChart.Diagram;
            diagram.Rotated = false;//图像是否旋转
            CompressionChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;//是否在图表上显示图例

            //// Customize the appearance of the X-axis title.调整 X-轴
            AxisX xAxis = diagram.AxisX;//获取X轴
            xAxis.Alignment = AxisAlignment.Near;//指定轴相对于另一主轴的位置。属性 AxisAlignment.Zero 设置仅对主轴可用
            xAxis.Title.Text = "压缩比";//设置轴标题
            xAxis.Title.Font = new Font("Times New Roman", 9f);
            xAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True; //是否显示轴标题
            xAxis.Label.TextPattern = "";

            //绑定数据，CompressionSpeedArray数组存放压缩速度数据。
            CompressionChart.Series[0].Points.AddPoint(CompressionRatiox[0], 2215.113);
            CompressionChart.Series[0].Points[0].Color = Color.CornflowerBlue;
            CompressionChart.Series[0].Points.AddPoint(CompressionRatiox[1], 2403.857);
            CompressionChart.Series[0].Points[1].Color = Color.CornflowerBlue;
            CompressionChart.Series[0].Points.AddPoint(CompressionRatiox[2], 2693.603);
            CompressionChart.Series[0].Points[2].Color = Color.CornflowerBlue;

            #endregion

            #region 柱状图
            //添加 图表标题
            SynchronizeChart.Titles.Add(new ChartTitle());
            SynchronizeChart.Titles[0].Text = "同步提点压缩测试";
            SynchronizeChart.Titles[0].Font = new Font("Times New Roman", 9f, FontStyle.Bold);

            //控件背景
            SynchronizeChart.BackColor = Color.Transparent;
            //图表区背景
            SynchronizeChart.BackColor = Color.Transparent;
            SynchronizeChart.BorderColor = Color.Transparent;

            // Access the diagram's properties.把 Diagram 对象转换为所需的图象类型
            XYDiagram Synchronizediagram = (XYDiagram)SynchronizeChart.Diagram;
            Synchronizediagram.Rotated = false;//图像是否旋转
            SynchronizeChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;//是否在图表上显示图例

            //// Customize the appearance of the X-axis title.调整 X-轴
            AxisX SynchronizexAxis = Synchronizediagram.AxisX;//获取X轴
            SynchronizexAxis.Alignment = AxisAlignment.Near;//指定轴相对于另一主轴的位置。属性 AxisAlignment.Zero 设置仅对主轴可用
            SynchronizexAxis.Title.Text = "拼图数&压缩比";//设置轴标题
            SynchronizexAxis.Title.Font = new Font("Times New Roman", 9f);
            SynchronizexAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True; //是否显示轴标题
            SynchronizexAxis.Label.TextPattern = "";

            //绑定数据,SynchronizeSpeedArray数组存放同步提点压缩速度数据。
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 0], 1875.352);
            SynchronizeChart.Series[0].Points[0].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 1], 1924.212);
            SynchronizeChart.Series[0].Points[1].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 2], 2132.521);
            SynchronizeChart.Series[0].Points[2].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[0, 3], 1952.650);
            SynchronizeChart.Series[0].Points[3].Color = Color.DodgerBlue;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 0], 1934.251);
            SynchronizeChart.Series[1].Points[0].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 1], 2102.250);
            SynchronizeChart.Series[1].Points[1].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 2], 2254.231);
            SynchronizeChart.Series[1].Points[2].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[0, 3], 2002.290);
            SynchronizeChart.Series[1].Points[3].Color = Color.IndianRed;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 0], 1975.625);
            SynchronizeChart.Series[2].Points[0].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 1], 2231.652);
            SynchronizeChart.Series[2].Points[1].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 2], 2301.254);
            SynchronizeChart.Series[2].Points[2].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[0, 3], 2152.235);
            SynchronizeChart.Series[2].Points[3].Color = Color.OliveDrab;

            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 0], 2042.541);
            SynchronizeChart.Series[0].Points[0].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 1], 2152.321);
            SynchronizeChart.Series[0].Points[1].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 2], 2014.258);
            SynchronizeChart.Series[0].Points[2].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[1, 3], 2152.874);
            SynchronizeChart.Series[0].Points[3].Color = Color.DodgerBlue;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 0], 2251.245);
            SynchronizeChart.Series[1].Points[0].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 1], 2240.752);
            SynchronizeChart.Series[1].Points[1].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 2], 2157.542);
            SynchronizeChart.Series[1].Points[2].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[1, 3], 2274.632);
            SynchronizeChart.Series[1].Points[3].Color = Color.IndianRed;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 0], 2267.854);
            SynchronizeChart.Series[2].Points[0].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 1], 2396.824);
            SynchronizeChart.Series[2].Points[1].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 2], 2174.950);
            SynchronizeChart.Series[2].Points[2].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[1, 3], 2296.420);
            SynchronizeChart.Series[2].Points[3].Color = Color.OliveDrab;

            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 0], 2265.541);
            SynchronizeChart.Series[0].Points[0].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 1], 2145.842);
            SynchronizeChart.Series[0].Points[1].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 2], 2235.275);
            SynchronizeChart.Series[0].Points[2].Color = Color.DodgerBlue;
            SynchronizeChart.Series[0].Points.AddPoint(SynchronizeChartx[2, 3], 2358.352);
            SynchronizeChart.Series[0].Points[3].Color = Color.DodgerBlue;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 0], 2312.201);
            SynchronizeChart.Series[1].Points[0].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 1], 2212.542);
            SynchronizeChart.Series[1].Points[1].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 2], 2268.435);
            SynchronizeChart.Series[1].Points[2].Color = Color.IndianRed;
            SynchronizeChart.Series[1].Points.AddPoint(SynchronizeChartx[2, 3], 2410.745);
            SynchronizeChart.Series[1].Points[3].Color = Color.IndianRed;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 0], 2322.401);
            SynchronizeChart.Series[2].Points[0].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 1], 2298.215);
            SynchronizeChart.Series[2].Points[1].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 2], 2358.352);
            SynchronizeChart.Series[2].Points[2].Color = Color.OliveDrab;
            SynchronizeChart.Series[2].Points.AddPoint(SynchronizeChartx[2, 3], 2425.212);
            SynchronizeChart.Series[2].Points[3].Color = Color.OliveDrab;

            #endregion
        }
    }
}
