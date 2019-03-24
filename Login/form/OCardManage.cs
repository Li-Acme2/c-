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
    public partial class CardManage : Form
    {
        public CardManage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            new AccessManage().Show();
            this.Hide();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 门禁卡的添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"[^0-9]");
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
            {
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                int a = Convert.ToInt32(textBox1.Text.Trim());
                int b = Convert.ToInt32(comboBox1.Text);
                string sql = "select * from card where " + a + " in(cno)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                Object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("卡号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql2 = "insert into card value(" + a + ",null,null,'未授权'," + b + ",'未绑定',null)";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    int result2 = cmd2.ExecuteNonQuery();
                    if (result2 == 1)
                    {
                        MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        //string sql3 = "select * from card order by xuhao desc;";
                        //MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                        //object result3 = cmd3.ExecuteScalar();
                        //if (result3 != null)
                        //{
                        //    textBox1.Clear();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("新增失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("卡号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }
        /// <summary>
        /// 卡重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"[^0-9]");
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
            {
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                int a = Convert.ToInt32(textBox1.Text.Trim());
                int b = Convert.ToInt32(comboBox1.Text);
                string sql = "select * from card where " + a + " in(cno)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                Object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("卡号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql2 = "update card set ano=null , agno=null ,accredit='未授权',clevel=" + b + ",bind='绑定' where cno=" + a + ";";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    int result2 = cmd2.ExecuteNonQuery();
                    if (result2 == 1)
                    {
                        MessageBox.Show("重置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        //string sql3 = "select * from card order by xuhao desc;";
                        //MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                        //object result3 = cmd3.ExecuteScalar();
                        //if (result3 != null)
                        //{
                        //    textBox1.Clear();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("重置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("卡号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 门禁卡的删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"[^0-9]");
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
            {
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                int a = Convert.ToInt32(textBox1.Text.Trim());
                int b = Convert.ToInt32(comboBox1.Text);
                //门禁卡号是否存在且未绑定
                string sql2 = "select * from card where " + b + " in(cno) and bind='未绑定'";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                Object result = cmd2.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("卡号不存在或者未解绑！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //卡号存在且解绑则删除
                else
                {
                    string sql3 = "delete from card where cno=" + a + ";";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    int result3 = cmd3.ExecuteNonQuery();
                    if (result3 == 1)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        //string sql3 = "select * from card order by xuhao desc;";
                        //MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                        //object result3 = cmd3.ExecuteScalar();
                        //if (result3 != null)
                        //{
                        //    textBox1.Clear();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("卡号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

