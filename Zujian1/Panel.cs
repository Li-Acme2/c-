using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zujian1
{
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Add("在窗体中添加一个Panel控件，设置AutoScroll属性为true，" +
                "设置BorderStyle属性为Fixed3D，添加一个Button控件和一个ListBox控件，" +
                "设置Button控件的Text属性为“增加”。");
        }
    }
}
