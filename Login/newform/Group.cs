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
    public partial class Group : Form
    {
        internal Mainform mainform;

        public Group()
        {
            InitializeComponent();
        }

        private void Group_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            Query();
        }
        #region 查询
        public void Query()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from access";
            MySqlCommand cmd = new MySqlCommand(sql, db);
            MySqlDataReader reader = null;
            try
            {
                db.Open(); //打开连接
                reader = cmd.ExecuteReader();//执行查询，并将结果返回给读取器
                listView1.Items.Clear();
                while (reader.Read())//开始循环遍历赋值
                {
                    ListViewItem first = new ListViewItem(reader["ano"].ToString());
                    first.SubItems.Add(reader["agno"].ToString());
                    first.SubItems.Add(reader["alevel"].ToString());
                    first.SubItems.Add(reader["aspposition"].ToString());
                    first.SubItems.Add(reader["aczposition"].ToString());
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
        #region //分组
        private void button1_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                bool t = true;
                int agno = Convert.ToInt32(comboBox4.Text.Trim());
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked == true)
                    {
                        //MessageBox.Show(listView1.Items[i].Text);//复选框对应的第i+1行第一列数据
                        //MessageBox.Show(listView1.Items[i].SubItems[2].Text);//复选框对应第i+1行第3列的文本
                        t = false;
                    }
                }
                if (t)
                {
                    MessageBox.Show("请选择分组数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (listView1.Items[i].Checked == true)
                        {
                            //MessageBox.Show(listView1.Items[i].Text);//复选框对应的第i+1行第一列数据
                            //MessageBox.Show(listView1.Items[i].SubItems[2].Text);//复选框对应第i+1行第3列的文本
                            int ano = Convert.ToInt32(listView1.Items[i].Text);//ano
                            string sql = "update access set agno="+agno+" where ano="+ano+"";
                            new MySqlCommand(sql, conn).ExecuteNonQuery();
                        }
                    }
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
            Query();

        }
#endregion
        #region //条件查询
        private void button2_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from access where 1=1 ";
            if (comboBox1.Text.Trim() != "NULL")
            {
                int alevel = Convert.ToInt32(comboBox1.Text.Trim());
                sql += " and alevel=" + alevel + " ";
            }
            if (comboBox2.Text.Trim() != "NULL")
            {
                string aspposition = comboBox2.Text.Trim();
                sql += " and aspposition='" + aspposition + "' ";
            }
            if (comboBox3.Text.Trim() != "NULL")
            {
                string aczposition = comboBox3.Text.Trim();
                sql += " and aczposition='" + aczposition + "' ";
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
                        ListViewItem first = new ListViewItem(reader["ano"].ToString());
                        first.SubItems.Add(reader["agno"].ToString());
                        first.SubItems.Add(reader["alevel"].ToString());
                        first.SubItems.Add(reader["aspposition"].ToString());
                        first.SubItems.Add(reader["aczposition"].ToString());
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
        }
        #endregion
        #region //全选和反选
        private void button3_Click(object sender, EventArgs e)
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
