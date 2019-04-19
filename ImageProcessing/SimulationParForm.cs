using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    /// <summary>
    /// 声明委托
    /// 仿真测试窗口向主窗口发送异步请求
    /// </summary>
    public delegate void SimulationParFormDelegate(int[] ParameterArray);
    public partial class SimulationParForm : DevExpress.XtraEditors.XtraForm
    {
        public SimulationParForm()
        {
            InitializeComponent();
        }

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
                if (admin == "Administrator")
                {
                    ImgThresholdBox.Text = myIni.ReadString("Parameter", "ImageThreshold", "");//导入二值化阈值
                    MinLengthBox.Text = myIni.ReadString("Parameter", "MinLength", "");//导入标志点周长范围最小值
                    MaxLengthBox.Text = myIni.ReadString("Parameter", "MaxLength", "");//导入标志点周长范围最大值
                    MinAreaBox.Text = myIni.ReadString("Parameter", "MinArea", "");//导入标志点面积范围最小值
                    MaxAreaBox.Text = myIni.ReadString("Parameter", "MaxArea", "");//导入标志点面积范围最大值
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
        public event SimulationParFormDelegate ReportConformEvent;
        private void Confirmbutton_Click(object sender, EventArgs e)
        {
            int[] ParameterArray = new int[9];//存放参数数组
            if (this.ReportImgPath.Text != string.Empty)
            {
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
                else
                {
                    //需要传递参数的值
                    ParameterArray[0] = Form1.ImgBitDeep;
                    ParameterArray[1] = int.Parse(ImgThresholdBox.Text);
                    Form1.ImgThreshold = int.Parse(ImgThresholdBox.Text);//更新全局二值化阈值
                    if (this.MinLengthBox.Text == string.Empty)
                    {
                        ParameterArray[2] = 0;//周长最小范围不输入则默认为0
                        MinLengthBox.SelectedText = ParameterArray[2].ToString();
                    }
                    else
                        ParameterArray[2] = int.Parse(MinLengthBox.Text);
                    ParameterArray[3] = int.Parse(MaxLengthBox.Text);
                    if (this.MinAreaBox.Text == string.Empty)
                    {
                        ParameterArray[4] = 0;//面积最小范围不输入则默认为0
                        MinAreaBox.SelectedText = ParameterArray[4].ToString();
                    }
                    else
                        ParameterArray[4] = int.Parse(MinAreaBox.Text);
                    if (this.MaxAreaBox.Text == string.Empty)
                    {
                        ParameterArray[5] = 99999;//面积最大范围不输入则默认为99999
                        MaxAreaBox.SelectedText = ParameterArray[5].ToString();
                    }
                    else
                        ParameterArray[5] = int.Parse(MaxAreaBox.Text);
                    ParameterArray[6] = 0;//是否终止实验
                    ParameterArray[7] = Form1.ImgInputModeFlag;//全图|矩形模式设置
                    ParameterArray[8] = 5;//包围盒填充像素数目
                    if (ParameterArray[3] < ParameterArray[2])
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("周长设置范围有误，请重新设置！");
                        return;
                    }
                    if (ParameterArray[5] < ParameterArray[4])
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("面积设置范围有误，请重新设置！");
                        return;
                    }
                    Form1.LengthMin = ParameterArray[2];//更新全局筛选阈值
                    Form1.LengthMax = ParameterArray[3];
                    Form1.AreaMin = ParameterArray[4];
                    Form1.AreaMax = ParameterArray[5];
                    //触发事件，参数传递
                    ReportConformEvent(ParameterArray);
                    this.Close();
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请先导入测试图像！");
            }
        }

        /// <summary>
        /// 文本框输入限制，只允许输入数字
        /// </summary>
        private void Value_KeyPress(object sender, KeyPressEventArgs e)
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
        private void Cancelbutton_Load(object sender, EventArgs e)
        {
            this.Close(); //----关闭窗体
        }

        /// <summary>
        /// 仿真测试窗口加载事件
        /// </summary>
        private void SimulationParForm_Load(object sender, EventArgs e)
        {
            //显示加载文本
            ReportImgPath.SelectedText = Form1.ImagePath;
            ImgHeightBox.SelectedText = Form1.ImageHeight.ToString();
            ImgWidthBox.SelectedText = Form1.ImageWidth.ToString();
            //手工添加ComboBoxEdit的数据源 
            CameraTypeBoxTest.Properties.Items.Clear();
            foreach (DataRow row in this.CameraData.Rows)
            {
                CameraTypeBoxTest.Properties.Items.Add(row["CameraType"]);
            }
            CameraTypeBoxTest.SelectedIndex = 0;//默认选择8位黑白
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
    }
}
