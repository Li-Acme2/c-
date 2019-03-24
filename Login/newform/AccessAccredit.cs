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
    public partial class AccessAccredit : Form
    {
        internal Mainform mainform;

        public AccessAccredit()
        {
            InitializeComponent();
        }
        #region 按门禁号授权
        private void button8_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        int cno = Convert.ToInt32(textBox1.Text.Trim());
                        int ano = Convert.ToInt32(textBox2.Text.Trim());
                        string sql = "select clevel from card where " + cno + " in(cno); ";
                        string sql2 = "select alevel from access where " + ano + " in(ano); ";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        Object result = cmd.ExecuteScalar();
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        Object result2 = cmd2.ExecuteScalar();
                        if (result == null || result2 == null)
                        {
                            MessageBox.Show("门禁卡号或者门禁号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string sql5 = "select * from card_access where cno=" + cno + " and ano=" + ano + ";";
                            Object result4 = new MySqlCommand(sql5, conn).ExecuteScalar();
                            if (result4 == null)
                            {
                                int clevel = Convert.ToInt32(result);
                                int alevel = Convert.ToInt32(result2);
                                if (clevel >= alevel)
                                {
                                    string sql3 = "update card set accredit='授权' where cno=" + cno + " ;";
                                    string sql4 = "insert into card_access value(" + cno + "," + ano + "," + clevel + "," + alevel + ",null);";
                                    new MySqlCommand(sql3, conn).ExecuteNonQuery();
                                    new MySqlCommand(sql4, conn).ExecuteNonQuery();
                                    DXQuery();
                                }
                                else
                                {
                                    MessageBox.Show("门禁卡级别小于门禁级别，授权失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("此门禁已授权此门禁卡！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("门禁号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("门禁卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }
        //页面加载
        private void AccessAccredit_Load(object sender, EventArgs e)
        {
            Query();
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 3;
        }
        //刷新按钮
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
        #region 倒序查询
        public void DXQuery()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from card_access order by xuhao desc;";
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
            textBox1.Clear();
            textBox2.Clear();
        }
        #endregion
        #region 条件查询
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
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }
        #region 按门禁级别授权
        private void button11_Click(object sender, EventArgs e)
        {
            DataTable P_dt = null;
            // server=127.0.0.1/localhost 代表本机，端口号默认
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);

            if (!String.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                if (comboBox2.Text.Trim() != "NULL")
                {
                    try
                    {
                        conn.Open();
                        int cno = Convert.ToInt32(textBox3.Text.Trim());
                        int alevel = Convert.ToInt32(comboBox2.Text.Trim());
                        string sql = "select clevel from card where " + cno + " in(cno); ";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        int clevel = Convert.ToInt32(cmd.ExecuteScalar());
                        if (clevel <= 0)
                        {
                            MessageBox.Show("门禁卡号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            if (clevel >= alevel)
                            {
                                string sql3 = "select ano from access where alevel=" + alevel + ";";
                                MySqlDataAdapter adapter = new MySqlDataAdapter(sql3, conn);// 读取表数据
                                P_dt = new DataTable();//p_dt是一个表类型
                                adapter.Fill(P_dt);
                                for (int i = 0; i < P_dt.Rows.Count; i++)
                                {
                                    int ano = Convert.ToInt32(P_dt.Rows[i][0].ToString());
                                    string sql5 = "select * from card_access where cno=" + cno + " and ano=" + ano + ";";
                                    Object result5 = new MySqlCommand(sql5, conn).ExecuteScalar();
                                    if (result5 == null)
                                    {
                                        string sql6 = "update card set accredit='授权' where cno=" + cno + " ;";
                                        string sql7 = "insert into card_access value(" + cno + "," + ano + "," + clevel + "," + alevel + ",null);";
                                        new MySqlCommand(sql6, conn).ExecuteNonQuery();
                                        new MySqlCommand(sql7, conn).ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("门禁级别高于门禁卡级别，授权失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
                else
                {
                    MessageBox.Show("门禁级别不能为NULL！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("门禁卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DXQuery();
            textBox3.Clear();
        }
        #endregion

        #region //按门禁组授权
        private void button9_Click(object sender, EventArgs e)
        {
            DataTable P_dt = null;
            // server=127.0.0.1/localhost 代表本机，端口号默认
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);

            if (!String.IsNullOrEmpty(textBox4.Text.Trim()))
            {
                if (comboBox3.Text.Trim() != "NULL")
                {
                    try
                    {
                        conn.Open();
                        int cno = Convert.ToInt32(textBox4.Text.Trim());
                        int agno = Convert.ToInt32(comboBox3.Text.Trim());
                        string sql = "select clevel from card where " + cno + " in(cno); ";
                        string sql2 = "select max(alevel) from access where agno=" + agno + " ;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        int clevel = Convert.ToInt32(cmd.ExecuteScalar());
                        int b = Convert.ToInt32(cmd2.ExecuteScalar());
                        if (clevel <= 0 || b <= 0)
                        {
                            MessageBox.Show("门禁卡号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (clevel >= b)
                            {
                                string sql3 = "select ano,alevel from access where agno=" + agno + ";";
                                MySqlDataAdapter adapter = new MySqlDataAdapter(sql3, conn);// 读取表数据
                                P_dt = new DataTable();//p_dt是一个表类型
                                adapter.Fill(P_dt);
                                for (int i = 0; i < P_dt.Rows.Count; i++)
                                {
                                    int ano = Convert.ToInt32(P_dt.Rows[i][0].ToString());
                                    int alevel = Convert.ToInt32(P_dt.Rows[i][1].ToString());
                                    string sql5 = "select * from card_access where cno=" + cno + " and ano=" + ano + ";";
                                    Object result5 = new MySqlCommand(sql5, conn).ExecuteScalar();
                                    if (result5 == null)
                                    {
                                        string sql6 = "update card set accredit='授权',agno=" + agno + " where cno=" + cno + " ;";
                                        string sql7 = "insert into card_access value(" + cno + "," + ano + "," + clevel + "," + alevel + ",null);";
                                        new MySqlCommand(sql6, conn).ExecuteNonQuery();
                                        new MySqlCommand(sql7, conn).ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("存在门禁级别高于门禁卡级别，授权失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
                else
                {
                    MessageBox.Show("门禁组号不能为NULL！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("门禁卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DXQuery();
            textBox4.Clear();
        }
        #endregion
        private void AccessAccredit_Activated(object sender, EventArgs e)
        {
            Query();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果不是删除键和数字就不让输入
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
