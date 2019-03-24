using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.newform
{
    public partial class BindUnbind : Form
    {
        public BindUnbind()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        //绑定
        private void button1_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//对新增人员时进行一些列的判断
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        int a = Convert.ToInt32(textBox1.Text.Trim());
                        int b = Convert.ToInt32(textBox2.Text.Trim());

                        //员工号是否存在且未绑定
                        string sql = "select * from member where " + a + " in(mno) and cno is null";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        Object result = cmd.ExecuteScalar();
                        //门禁卡号是否存在且未绑定
                        string sql12 = "select * from card where " + b + " in(cno) and bind='未绑定'";
                        MySqlCommand cmd12 = new MySqlCommand(sql12, conn);
                        string sql13 = "select * from member where mno="+a+" and cno="+b+";";
                        MySqlCommand cmd13 = new MySqlCommand(sql13, conn);
                        //查询有结果或返回null
                        Object result12 = cmd12.ExecuteScalar();
                        object result3 = cmd13.ExecuteScalar();
                        if (result == null || result12 == null)
                        {
                            if (result3 != null)
                            {
                                MessageBox.Show("员工号和门禁卡号已绑定！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else {
                                MessageBox.Show("员工号或者门禁卡号错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {//更新员工表
                            string sql21 = "update  member set cno=" + b + " where mno=" + a + ";";
                            MySqlCommand cmd21 = new MySqlCommand(sql21, conn);
                            int result21 = cmd21.ExecuteNonQuery();
                            //更新门禁卡表
                            String sql22 = "update card set bind='绑定' where cno=" + b + ";";
                            MySqlCommand cmd22 = new MySqlCommand(sql22, conn);
                            //插入删除更改返回1或失败
                            int result22 = cmd22.ExecuteNonQuery();
                            if (result21 == 1 && result22 == 1)
                            {
                                this.textBox1.Focus();
                                textBox1.Clear();
                                textBox2.Clear();
                            }
                            else
                            {
                                MessageBox.Show("绑定失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("员工号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        //解绑
        private void button2_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//解绑时进行一些列的判断
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        int a = Convert.ToInt32(textBox1.Text.Trim());
                        int b = Convert.ToInt32(textBox2.Text.Trim());

                        //员工号与门禁卡号是否绑定
                        string sql = "select * from member where mno=" + a + " and cno=" + b + ";";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        Object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("员工号或者门禁卡号错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {//更新员工表
                            string sql21 = "update  member set cno=null where mno=" + a + ";";
                            MySqlCommand cmd21 = new MySqlCommand(sql21, conn);
                            int result21 = cmd21.ExecuteNonQuery();
                            //更新门禁卡表
                            String sql22 = "update card set bind='未绑定' where cno=" + b + ";";
                            MySqlCommand cmd22 = new MySqlCommand(sql22, conn);
                            //插入删除更改返回1或失败
                            int result22 = cmd22.ExecuteNonQuery();
                            if (result21 == 1 && result22 == 1)
                            {
                                this.textBox1.Focus();
                                textBox1.Clear();
                                textBox2.Clear();
                            }
                            else
                            {
                                MessageBox.Show("解绑失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("员工号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }
        
        private void BindUnbind_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemberManage memberManage = new MemberManage();
            memberManage.Query();
        }

        private void BindUnbind_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberManage memberManage = new MemberManage();
            memberManage.Query();
        }
    }
}
