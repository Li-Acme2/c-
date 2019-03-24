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
    public partial class UnAccredit : Form
    {
        internal Mainform mainform;

        public UnAccredit()
        {
            InitializeComponent();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void UnAccredit_Load(object sender, EventArgs e)
        {
            Query();
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 3;
        }
        //刷新
        private void button13_Click(object sender, EventArgs e)
        {
            Query();
        }
        #region 查询
        public void Query()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from card_access";
            MySqlCommand cmd = new MySqlCommand(sql, db);
            MySqlDataReader reader = null;
            try
            {
                db.Open(); //打开连接
                reader = cmd.ExecuteReader();//执行查询，并将结果返回给读取器
                listView1.Items.Clear();
                while (reader.Read())//开始循环遍历赋值
                {
                    ListViewItem first = new ListViewItem(reader["cno"].ToString());
                    first.SubItems.Add(reader["ano"].ToString());
                    first.SubItems.Add(reader["clevel"].ToString());
                    first.SubItems.Add(reader["alevel"].ToString());
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
        #region //条件查询
        private void button14_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from card_access where 1=1 ";
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int cno = Convert.ToInt32(textBox1.Text.Trim());
                sql += " and cno=" + cno + " ";
            }
            if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                int ano = Convert.ToInt32(textBox2.Text.Trim());
                sql += " and ano=" + ano + " ";
            }
            if (comboBox1.Text.Trim() != "NULL")
            {
                int clevel = Convert.ToInt32(comboBox1.Text.Trim());
                sql += " and clevel=" + clevel + " ";
            }
            if (comboBox2.Text.Trim() != "NULL")
            {
                int alevel = Convert.ToInt32(comboBox2.Text.Trim());
                sql += " and alevel=" + alevel + " ";
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
                        ListViewItem first = new ListViewItem(reader["cno"].ToString());
                        first.SubItems.Add(reader["ano"].ToString());
                        first.SubItems.Add(reader["clevel"].ToString());
                        first.SubItems.Add(reader["alevel"].ToString());
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
        }
        #endregion
        #region //取消授权
        private void button1_Click(object sender, EventArgs e)
        {
            int num ;
            int cno ;
            int ano;
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//对新增人员时进行一些列的判断
                bool t = true;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked == true)
                    {
                        //MessageBox.Show(listView1.Items[i].Text);//复选框对应的第i+1行第一列数据
                        //MessageBox.Show(listView1.Items[i].SubItems[2].Text);//复选框对应第i+1行第3列的文本
                        //cno = Convert.ToInt32(listView1.Items[i].Text);//mno
                        //ano = Convert.ToInt32(listView1.Items[i].SubItems[1].Text);
                        t = false;
                    }
                }
                if (!t)
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (listView1.Items[i].Checked == true)
                        {
                            cno = Convert.ToInt32(listView1.Items[i].Text);//mno
                            ano = Convert.ToInt32(listView1.Items[i].SubItems[1].Text);
                            string sql21 = "delete from card_access where cno=" + cno + " and ano=" + ano + " ";
                            MySqlCommand cmd21 = new MySqlCommand(sql21, conn);
                            int result21 = cmd21.ExecuteNonQuery();
                            //更新门禁卡表
                            String sql22 = "update card set accredit='未授权' where card.cno not in(select cno from card_access)";
                            MySqlCommand cmd22 = new MySqlCommand(sql22, conn);
                            //插入删除更改返回1或失败
                            int result22 = cmd22.ExecuteNonQuery();
                            if (result21 == 1)
                            {
                                this.textBox1.Focus();
                            }
                            else
                            {
                                MessageBox.Show("取消授权失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择要取消授权的数据！");
                }
                Query();
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
        #endregion
        #region //全选和反选
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {

                if (listView1.Items[i].Checked == true)
                {
                    listView1.Items[i].Checked = false;
                }
                else
                {
                    listView1.Items[i].Checked = true;
                }
            }
        }
        #endregion
    }
}
