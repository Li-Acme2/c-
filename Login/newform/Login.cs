using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        #region 登录
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {   //判断用户是否输入，如果有空，则报错
                if (txtName.Text == "")
                {
                    MessageBox.Show("用户名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (txtPassword.Text == "")
                    {
                        MessageBox.Show("密码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string admin_id = txtName.Text;//获取账号
                        string admin_psw = txtPassword.Text;//获取密码
                        String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                        MySqlConnection db = new MySqlConnection(connetStr);
                        db.Open();//打开连接
                        //去数据库里面验证输入的用户名和密码是不是存在
                        //查询是否有该条记录，根据账户密码
                        string sql = "select count(*) from login where name='" + admin_id + "' and password='" + admin_psw + "'";
                        MySqlCommand command = new MySqlCommand(sql, db);//sqlcommand表示要向数据库执行sql语句或存储过程
                        int i = Convert.ToInt32(command.ExecuteScalar());//执行后返回记录行数
                        if (i > 0)//如果大于1，说明记录存在，登录成功
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                            this.Close();
                        }

                        else
                        {
                            MessageBox.Show("用户名或者密码错误！");
                        }
                        db.Close();  //关闭数据库连接
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region 注册
        private void button2_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            try
            {
                //注册信息的每一个空都不能为空
                if (txtName.Text == "" || txtPassword.Text == "")
                    MessageBox.Show("请填写所有选项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    db.Open();//打开此数据库连接 
                              // SqlCommand sqlText = new SqlCommand( connectString,connection);//建立数据库命令对象 (这里数据库)
                              //往mysql数据库里面添加数据。
                    string sql = "insert into login values('" + txtName.Text.Trim() + "','" + txtPassword.Text + "')";
                    MySqlCommand result = new MySqlCommand(sql, db);//建立数据库命令对象  //添加数据库命令参数 
                    if (result.ExecuteNonQuery() == 0)
                        MessageBox.Show("注册失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("注册成功,返回登录!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {   //我把用户名当做键值，不允许重名（用户名）
                MessageBox.Show("用户名已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                db.Close();
            }
        }
        #endregion
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtPassword_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }
    }
}
#region 改进之前
//if (!String.IsNullOrEmpty(txtName.Text) && !String.IsNullOrEmpty(txtPassword.Text))
//{
//    if (txtName.Text.Trim() == "root" && txtPassword.Text.Trim() == "123")
//    {
//        this.DialogResult = DialogResult.OK;
//        this.Dispose();
//        this.Close();
//    }
//    else
//    {
//        MessageBox.Show("用户名或密码有错！");
//    }
//}
//else
//{
//    MessageBox.Show("用户名或密码不能为空！");
//}
#endregion

