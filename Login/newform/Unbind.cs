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
    public partial class Unbind : Form
    {
        internal Mainform mainform;

        public Unbind()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            comboBox1.SelectedIndex = 2;
        }
        #region //条件查询
        private void button2_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from member where cno is not null ";
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int mno = Convert.ToInt32(textBox1.Text.Trim());
                sql += " and mno=" + mno + " ";
            }
            if (comboBox1.Text.Trim() != "NULL")
            {
                string sex = comboBox1.Text.Trim();
                sql += " and sex='" + sex + "' ";
            }
            if (!String.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                int cno = Convert.ToInt32(textBox3.Text.Trim());
                sql += " and cno=" + cno + " ";
            }
            if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                string name = textBox2.Text.Trim();
                sql += " and name like '%" + name + "%' ";
            }
            MySqlCommand cmd = new MySqlCommand(sql, db);
            try
            {
                Object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    reader = cmd.ExecuteReader();//执行查询，并将结果返回给读取器
                    listView1.Items.Clear();
                    while (reader.Read())//开始循环遍历赋值
                    {
                        ListViewItem first = new ListViewItem(reader["mno"].ToString());
                        first.SubItems.Add(reader["name"].ToString());
                        first.SubItems.Add(reader["cno"].ToString());
                        first.SubItems.Add(reader["sex"].ToString());
                        first.SubItems.Add(reader["age"].ToString());
                        this.listView1.Items.Add(first);//将所有的内容添加到表格内容中
                    }
                    Console.ReadLine();
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("未查询到有关数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                db.Close();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        #endregion
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void Unbind_Load(object sender, EventArgs e)
        {
            Query();
        }
        #region 查询
        public void Query()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from member where cno is not null ";
            MySqlCommand cmd = new MySqlCommand(sql, db);
            MySqlDataReader reader = null;
            try
            {
                db.Open(); //打开连接
                reader = cmd.ExecuteReader();//执行查询，并将结果返回给读取器
                listView1.Items.Clear();
                while (reader.Read())//开始循环遍历赋值
                {
                    ListViewItem first = new ListViewItem(reader["mno"].ToString());
                    first.SubItems.Add(reader["name"].ToString());
                    first.SubItems.Add(reader["cno"].ToString());
                    first.SubItems.Add(reader["sex"].ToString());
                    first.SubItems.Add(reader["age"].ToString());
                    this.listView1.Items.Add(first);//将所有的内容添加到表格内容中
                }
                Console.ReadLine();
                reader.Close();
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
               
                db.Close();
            }
        }
        #endregion
        #region //解绑
        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            int mno = 0;
            int cno = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked == true)
                {
                    //MessageBox.Show(listView1.Items[i].Text);//复选框对应的第i+1行第一列数据
                    //MessageBox.Show(listView1.Items[i].SubItems[2].Text);//复选框对应第i+1行第3列的文本
                    mno = Convert.ToInt32(listView1.Items[i].Text);//mno
                    cno= Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                    num++;//说明有选中的
                }
            }
            if (num == 1)
            {
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                try
                {
                    conn.Open();//对新增人员时进行一些列的判断
                    string sql21 = "update  member set cno=null where mno=" + mno + ";";
                    MySqlCommand cmd21 = new MySqlCommand(sql21, conn);
                    int result21 = cmd21.ExecuteNonQuery();
                    //更新门禁卡表
                    String sql22 = "update card set bind='未绑定' where cno=" + cno + ";";
                    MySqlCommand cmd22 = new MySqlCommand(sql22, conn);
                    //插入删除更改返回1或失败
                    int result22 = cmd22.ExecuteNonQuery();
                    if (result21 == 1 && result22 == 1)
                    {
                        this.textBox1.Focus();
                        Query();
                    }
                    else
                    {
                        MessageBox.Show("解绑失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                MessageBox.Show("请不要不选或者多选员工和门禁卡！");
            }
        }
        #endregion
        private void Unbind_Activated(object sender, EventArgs e)
        {
            Query();
        }
    }
}
