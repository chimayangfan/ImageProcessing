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
    /// 参数设置窗口向主窗口发送异步请求
    /// </summary>
    public delegate void MarkSetDelegate(Color[] MarkColor, byte[] MarkParSet);
    public partial class MarkDraw : DevExpress.XtraEditors.XtraForm
    {
        public MarkDraw()
        {
            InitializeComponent();
            //界面参数更新
            MarkColorEdit.Color = Form1.markInfo.MarkColor;
            MarkLengthEdit.Value = Form1.markInfo.MarkLength;
            MarkLinewidthEdit.Value = Form1.markInfo.MarkLinewidth;
            ChooseMarkColorEdit.Color = Form1.markInfo.ChooseMarkColor;
            ChooseMarkLengthEdit.Value = Form1.markInfo.ChooseMarkLength;
            ChooseMarkLineWidthEdit.Value = Form1.markInfo.ChooseMarkLinewidth;
        }

        /// <summary>
        /// 委托事件
        /// 标记设置窗口向主窗口发送异步请求
        /// </summary>
        public event MarkSetDelegate MarkSet_Event;
        private void MarkDrawConfirmButton_Click(object sender, EventArgs e)
        {
            Color[] MarkColor =
            {
                MarkColorEdit.Color,
                ChooseMarkColorEdit.Color
            };
            byte[] MarkParSet =
            {
                (byte)(MarkLengthEdit.Value),//全部标注长度
                (byte)(MarkLinewidthEdit.Value),//全部标注线宽
                (byte)(ChooseMarkLengthEdit.Value),//选中标注长度
                (byte)(ChooseMarkLineWidthEdit.Value)//选中标注线宽
            };
            MarkSet_Event(MarkColor,MarkParSet);
            // Modify the setting value
            Properties.MarkSettings.Default.MarkColor = MarkColorEdit.Color;
            Properties.MarkSettings.Default.MarkLength = (byte)MarkLengthEdit.Value;
            Properties.MarkSettings.Default.MarkLinewidth = (byte)(MarkLinewidthEdit.Value);
            Properties.MarkSettings.Default.ChooseMarkColor = ChooseMarkColorEdit.Color;
            Properties.MarkSettings.Default.ChooseMarkLength = (byte)(ChooseMarkLengthEdit.Value);
            Properties.MarkSettings.Default.ChooseMarkLinewidth = (byte)(ChooseMarkLineWidthEdit.Value);
            // Save setting value
            Properties.MarkSettings.Default.Save();
            this.Close(); //----关闭窗体
        }
    }
}
