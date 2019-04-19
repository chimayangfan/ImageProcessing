using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using System.Drawing;
using System.Runtime.InteropServices;   //必须添加，不然DllImport报错

namespace ImageProcessing
{
    static class Program
    {
        ///<summary>
        ///用于界面向DLL动态库传递参数的结构体
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Parameter
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string ImgReadPath;      /*>1<*///图像读取路径
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string ImgSavePath;      /*>2<*///图像保存路径
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string DataReadPath;     /*>3<*///数据保存路径
            public int ImgBitDeep;          /*>4<*///图像位深
            public int ImgChannelNum;       /*>5<*///图像通道数
            public int ImgHeight;           /*>6<*///行数
            public int ImgWidth;            /*>7<*///列数
            public int ImgMakeborderWidth;  /*>8<*///填充像素后的宽度
            public int Threshold;           /*>9<*///二值化的阈值 
            public int LengthMin;           /*>10<*///周长的最小值 
            public int LengthMax;           /*>11<*///周长的最大值
            public int PicBlockSize;        /*>12<*///GPU线程块尺寸
            public int ColThreadNum;        /*>13<*///列方向分块数量（这个数量是块填充后的） 计算公式为 Devpar.ColThreadNum=(ImgWidth/8+127)/128*128;
            public int RowThreadNum;        /*>14<*///行方向块数量(行方向的块数量不用填充成128 的整数倍)
            public int AreaMin;             /*>15<*///面积阈值最小值
            public int AreaMax;             /*>16<*///面积阈值最大值
            public int CompressionRatio;    /*>17<*///图像压缩比
            public int PictureNum;          /*>18<*///拼图数量
            public int TerminateFlag;       /*>19<*///是否终止实验
            public int RecModelFlag;        /*>20<*///矩形模式标志位
            public int RecPadding;          /*>21<*///包围盒填充像素数目
        };

        ///<summary>
        ///用于DLL动态库向界面返回信息的结构体
        ///</summary>
        public struct Infomation
        {
            public int DeviceCount;         /*>1<*///GPU设备数
            public int ColThreadNum;        /*>2<*///列方向分块数量（这个数量是块填充后的）
            public int RowThreadNum;        /*>3<*///行方向分块数量
            public int ImgHeight;           /*>4<*///列数
            public int ImgWidth;            /*>5<*///行数
            public int ImgMakeborderWidth;  /*>6<*///填充像素后的宽度
            public int Threshold;           /*>7<*///二值化的阈值 
            public int LengthMin;           /*>8<*///周长的最小值 
            public int LengthMax;           /*>9<*///周长的最大值
            public int ThreadNum;           /*>10<*///GPU配置线程数
            public int CPUThreadCount;      /*>11<*///CPU配置线程数
            public int ImgProcessingNumbers;/*>12<*///处理图像数量
            public int PointNumbers;        /*>13<*///标志点数量
            public float ExtractPointTimes; /*>14<*///提取标志点用时
            public float ExtractPointSpeed; /*>15<*///提取标志点速度
            public float CompressionTimes;  /*>16<*///压缩用时
            public float CompressionSpeed;  /*>17<*///压缩速度
            public float SynchronizeTimes;  /*>18<*///同步处理耗时
            public float SynchronizeSpeed;  /*>19<*///同步处理速度
        };

        ///<summary>
        ///存放硬件相关信息的结构体
        ///用于硬件初始化、分配硬件资源等
        ///</summary>
        public struct HardwareInfo
        {
            public int DeviceCount;         /*>1<*/// GPU设备数
            public int GpuId;				/*>2<*///设备号  这个是硬件上的Gpu的Id号
            public int DeviceID;            /*>3<*///设备编号
            public int regsPerBlock;        /*>4<*///线程块可以使用的32位寄存器的最大值，多处理器上的所有线程快可以同时实用这些寄存器
            public int maxThreadsPerBlock;  /*>5<*///每个块中最大线程数
            public int major;               /*>6<*///计算能力的主代号
            public int minor;               /*>7<*///计算能力的次要代号
            public int deviceOverlap;       /*>8<*///器件是否能同时执行cudaMemcpy()和器件的核心代码
            public int multiProcessorCount; /*>9<*///设备上多处理器的数量
            public int ExtractPointThreads; /*>10<*///CPU提取标志点线程数
            public int CompressionThreads;  /*>11<*///CPU图像压缩线程数
            public int CUDAStreamNum;       /*>12<*///CUDA流数目
            public int DiskRemainingSpace;  /*>13<*///磁盘剩余容量
        };

        /// <summary>
        /// 引入动态库接口
        /// </summary>
        [DllImport("Imgsimulation")]
        public static extern int HardwareInit(ref HardwareInfo HardwareProp);
        [DllImport("Imgsimulation")]
        public static extern int Image_Pretreatment(string path, string exten, int ChooseMode);
        [DllImport("Imgsimulation")]
        public static extern bool SimulationImageTest(string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern void SimulationTestReport(string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern bool SimulationExperient(int ChooseMode);
        [DllImport("Imgsimulation")]
        public static extern void SimulationTestSynchronize(string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern void SimulationTestExtractPoint(string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern void SimulationTestComression(string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern bool OnlineImageExperiment(int ChooseMode, string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern bool OnlineImageRecExperiment(int ChooseMode, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern int OnlineImageRefresh(ref byte p);
        [DllImport("Imgsimulation")]
        public static extern bool OfflineImageExperiment(string path, ref Infomation Info);
        [DllImport("Imgsimulation")]
        public static extern bool SinglePictureExtractPoint(string Imgpath, string outputPath);
        [DllImport("Imgsimulation")]
        public static extern void DrawPointFlag(string pathBin, string pathImg, string pathWrite);
        [DllImport("Imgsimulation")]
        public static extern void Memory_application();
        [DllImport("Imgsimulation")]
        public static extern void Memory_release();
        [DllImport("Imgsimulation")]
        public static extern bool SetCameraPar(int Bufferlength);
        [DllImport("Imgsimulation")]
        public static extern bool SetParameter(ref Parameter info, int len);
        [DllImport("Imgsimulation")]
        public static extern void GetParameter(ref Parameter info);
        [DllImport("Imgsimulation")]
        public static extern void UnzipPictureFiles(string Filepath);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //界面汉化
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            //UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            //DevExpress.Utils.AppearanceObject.DefaultFont = new Font("微软雅黑", 11);
            Application.Run(new Form1());
        }
    }
}
