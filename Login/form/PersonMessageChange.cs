using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Login
{
    public partial class PersonMessageChange : Form
    {
        public PersonMessageChange()
        {
            InitializeComponent();
        }
        public AccessManage farthform;
        private DialogResult DR = DialogResult.Cancel;
        /// <summary>
        /// 新增人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button1_Click(object sender, EventArgs e)
        {

            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                //对新增人员时进行一些列的判断
                Regex reg = new Regex(@"[^0-9]");
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))

                {
                    if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        int a = Convert.ToInt32(textBox1.Text.Trim());
                        string s = textBox2.Text.Trim();
                        string sql = "select * from member where " + a + " in(mno)";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        Object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show("员工号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string sql2 = "insert into member value(" + a + ",'" + s + "',null,null)";
                            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                            int result2 = cmd2.ExecuteNonQuery();
                            if (result2 == 1)
                            {
                                DR = MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //setDR(DR);
                                textBox1.Clear();
                                textBox2.Clear();
                                string sql3 = "select * from member order by xuhao desc;";
                                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                                object result3 = cmd3.ExecuteScalar();
                                if (result3 != null)
                                {
                                    //DataTable P_dt = null;
                                    //MySqlDataAdapter adapter = new MySqlDataAdapter(sql3, conn);// 读取表数据
                                    //P_dt = new DataTable();//p_dt是一个表类型
                                    //DataTable dt = (DataTable)this.dataGridView1.DataSource;
                                    ////对表进行清理时，行和列都要清理
                                    //dataGridView1.DataSource = dt;
                                    //adapter.Fill(P_dt);//将读取到的表数据添加到p_dt里
                                    //this.dataGridView1.DataSource = P_dt;//数据绑定
                                }
                                else
                                {
                                    MessageBox.Show("数据库展示失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("新增失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("姓名不能为空且不为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("员工号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public DialogResult getDR()
        {
            return DR;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();
                Regex reg = new Regex(@"[^0-9]");
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.ToString().Trim()))

                {
                    int a = Convert.ToInt32(textBox1.Text.Trim());
                    string sql = "select * from member where " + a + " in(mno)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    Object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("人员已不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sql2 = "delete from member where mno=" + a + ";";
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        int result2 = cmd2.ExecuteNonQuery();
                        if (result2 == 1)
                        {
                            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("员工号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
