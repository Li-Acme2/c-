using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class AccessControl : Form
    {
        public AccessControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AccessManage().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        string str = " ";
        private RadioButton cb;

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            String s = CheckSelect();
            switch (s)
            {
                case "新增":
                    //4个都要写
                    Regex reg = new Regex(@"[^0-9]");
                    if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
                    {

                    }
                    else
                    {
                        MessageBox.Show("门禁号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "分组": break;
                case "删除": break;
                case "修改": break;
            }


        }

        private string CheckSelect()
        {
            for (int i = 0; i < this.groupBox1.Controls.Count; i++)
            {
                cb = this.groupBox1.Controls[i] as RadioButton;
                if (cb.Checked == true)
                {
                    str = cb.Text.ToString() + str;
                }
            }
            return str;

        }
    }
}
