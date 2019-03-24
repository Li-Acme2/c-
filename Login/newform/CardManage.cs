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

namespace Login.newform
{
    public partial class CardManage : Form
    {
        internal Mainform mainform;

        public CardManage()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Query();
        }
        #region    /// 计数查询
        public void CountQuery()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select count(*) from member ";
            MySqlCommand cmd = new MySqlCommand(sql, db);
            try
            {
                db.Open(); //打开连接
                int a = (int)cmd.ExecuteScalar();//执行查询，并将结果返回给读取器
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                db.Close();
            }
        }
        #endregion
        private void CardManage_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            Query();
        }
        #region //刷新查询
        public void Query()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            string sql2 = "update  card set card.agno=null where card.agno not in (SELECT agno from access where agno is not null)";
            
            String sql = "select * from card";
            MySqlCommand cmd = new MySqlCommand(sql, db);
            MySqlDataReader reader = null;
            try
            {
                db.Open(); //打开连接
                new MySqlCommand(sql2, db).ExecuteNonQuery();
                reader = cmd.ExecuteReader();//执行查询，并将结果返回给读取器
                listView1.Items.Clear();
                while (reader.Read())//开始循环遍历赋值
                {
                    ListViewItem first = new ListViewItem(reader["cno"].ToString());
                    first.SubItems.Add(reader["agno"].ToString());
                    first.SubItems.Add(reader["accredit"].ToString());
                    first.SubItems.Add(reader["clevel"].ToString());
                    first.SubItems.Add(reader["bind"].ToString());
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
        #region      //全选和反选
        private void button12_Click(object sender, EventArgs e)
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
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }
        #region   //添加门禁卡
        private void button8_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                Regex reg = new Regex(@"[^0-9]");
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (comboBox1.Text.Trim() != "NULL")
                    {
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
                            string sql2 = "insert into card value(" + a + ",null,'未授权'," + b + ",'未绑定',null)";
                            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                            int result2 = cmd2.ExecuteNonQuery();
                            if (result2 == 1)
                            {
                                textBox1.Clear();
                                this.textBox1.Focus();
                                DXQuery();
                            }
                            else
                            {
                                MessageBox.Show("新增失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("门禁卡级别不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("卡号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #region    //倒序查询
        public void DXQuery()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from card order by xuhao desc";
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
                    first.SubItems.Add(reader["agno"].ToString());
                    first.SubItems.Add(reader["accredit"].ToString());
                    first.SubItems.Add(reader["clevel"].ToString());
                    first.SubItems.Add(reader["bind"].ToString());
                    this.listView1.Items.Add(first);//将所有的内容添加到表格内容中
                }
                Console.ReadLine();
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                reader.Close();
                db.Close();
            }
        }
#endregion
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }

        private void button8_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }
        #region    //门禁卡的删除
        private void button9_Click(object sender, EventArgs e)
        {
            Boolean t = false;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked == true)
                {
                    //MessageBox.Show(listView1.Items[i].Text);//复选框对应的第i行第一列数据
                    //MessageBox.Show(listView1.Items[i].SubItems[2].Text);//复选框对应第i+1行第3列的文本
                    int a = Convert.ToInt32(listView1.Items[i].Text);//卡号
                    DeleteQuery(a);
                    t = true;//说明有选中的
                }
            }
            if (t)
            {//listview里面有选中项
                Regex reg = new Regex(@"[^0-9]");
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (!reg.IsMatch(textBox1.Text.Trim()))
                    {
                        int a = Convert.ToInt32(textBox1.Text.Trim());
                        DeleteQuery(a);
                    }
                    else
                    {
                        MessageBox.Show("卡号只能是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                textBox1.Clear();
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {//正常删除
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                try
                {
                    conn.Open();
                    Regex reg = new Regex(@"[^0-9]");
                    if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                    {
                        int a = Convert.ToInt32(textBox1.Text.Trim());//获得门禁卡号
                        DeleteQuery(a);
                        textBox1.Clear();
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("卡号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exc) { Console.WriteLine(exc.Message); }
                finally
                {
                    conn.Close();
                }
            }
            Query();
        }
#endregion
        #region      //删除数据
        public void DeleteQuery(int a)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = null;
            String sql2 = null;
            MySqlCommand cmd = null;
            MySqlCommand cmd2 = null;
            try
            {
                db.Open(); //打开连接
                sql = "delete from card where cno=" + a + ";";
                int result2 = new MySqlCommand(sql, db).ExecuteNonQuery();
                if (result2 == 1)
                {
                    sql = "select mno from member where cno=" + a + ";";
                    cmd = new MySqlCommand(sql, db);
                    object result = cmd.ExecuteScalar();//执行查询，并将结果返回给读取器
                    if (result == null)
                    {
                    }
                    else
                    {
                        int b = Convert.ToInt32(cmd.ExecuteScalar());
                        sql2 = "update member set cno=null where mno=" + b + ";";
                        cmd2 = new MySqlCommand(sql2, db);
                        cmd2.ExecuteNonQuery();
                    }
                    string sql3 = "select cno from card_access where cno=" + a + ";";
                    cmd = new MySqlCommand(sql3, db);
                    object result3 = cmd.ExecuteScalar();//执行查询，并将结果返回给读取器
                    if (result3 != null)
                    {
                        sql2 = "delete from card_access where cno=" + a + ";";
                        cmd2 = new MySqlCommand(sql2, db);
                        cmd2.ExecuteNonQuery();
                    }
                    this.textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                db.Close();
            }
        }
#endregion
        #region //门禁卡重置
        private void button11_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (comboBox1.Text != "NULL")
                    {
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
                        {//重置等级需要判断卡对应的门是否要删除
                            string sql6 = "select * from card where cno=" + a + " and accredit='授权' ";
                            MySqlCommand cmd6 = new MySqlCommand(sql6, conn);
                            Object result6 = cmd6.ExecuteScalar();
                            if (result != null)
                            {
                                string sql2 = "update card set  agno=null ,clevel=" + b + " where cno=" + a + ";";
                                string sql3 = "update card_access set clevel=" + b + " where cno=" + a + "; ";
                                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                                int result2 = cmd2.ExecuteNonQuery();
                                int result3 = cmd3.ExecuteNonQuery();
                                if (result2 == 1)
                                {
                                    if (result3 > 0)
                                    {
                                        string sql4 = "delete from card_access where clevel < alevel;";
                                        new MySqlCommand(sql4, conn).ExecuteNonQuery();
                                        string sql5 = "update card set accredit='未授权' where " + a + " not in (SELECT cno from card_access) and card.cno=" + a + ";";
                                        new MySqlCommand(sql5, conn).ExecuteNonQuery();
                                    }
                                    textBox1.Clear();
                                }
                                else
                                {
                                    MessageBox.Show("重置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else {
                        MessageBox.Show("门禁卡级别不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                else
                {
                    MessageBox.Show("卡号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                conn.Close();
            }
            Query();
        }
#endregion
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }
        //门禁卡页面加载
        private void CardManage_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 2;
        }
        #region //条件查询
        private void button1_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from card where 1=1 ";
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int cno = Convert.ToInt32(textBox1.Text.Trim());
                sql += " and cno=" + cno + " ";
            }
            if (comboBox2.Text.Trim() != "NULL")
            {
                string accredit = comboBox2.Text.Trim();
                sql += " and accredit='" + accredit + "' ";
            }
            if (comboBox3.Text.Trim() != "NULL")
            {
                string bind = comboBox3.Text.Trim();
                sql += " and bind='" + bind + "' ";
            }
            if (comboBox1.Text.Trim() != "NULL")
            {
                int clevel = Convert.ToInt32(comboBox1.Text.Trim());
                sql += " and clevel=" + clevel + " ";
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
                        first.SubItems.Add(reader["agno"].ToString());
                        first.SubItems.Add(reader["accredit"].ToString());
                        first.SubItems.Add(reader["clevel"].ToString());
                        first.SubItems.Add(reader["bind"].ToString());
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
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CardManage_Activated(object sender, EventArgs e)
        {
            Query();
        }
    }
}
#endregion
#region 改进之前
/*string sql2 = "select * from card where " + a + " in(cno)";//门禁卡号是否存在
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        result = cmd2.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("卡号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {//卡号存在则删除
                            string sql3 = "delete from card where cno=" + a + ";";
                            MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                            int result3 = cmd3.ExecuteNonQuery();
                            if (result3 == 1)
                            {
                                string sql = "select mno from member where cno=" + a + ";";
                                MySqlCommand cmd = new MySqlCommand(sql, conn);
                                result = cmd.ExecuteScalar();//执行查询，并将结果返回给读取器
                                if (result == null)
                                {
                                }
                                else
                                {
                                    int b = Convert.ToInt32(cmd.ExecuteScalar());
                                    sql2 = "update member set cno=null where mno=" + b + ";";
                                    new MySqlCommand(sql2, conn).ExecuteNonQuery();
                                }
                                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox1.Clear();
                                this.textBox1.Focus();
                                Query();
                            }
                            else
                            {
                                MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }*/
#endregion