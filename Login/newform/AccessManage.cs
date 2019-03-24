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
    public partial class AccessManage : Form
    {
        internal Mainform mainform;

        public AccessManage()
        {
            InitializeComponent();
        }
        #region   //门禁添加按钮
        private void button8_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                Regex reg = new Regex(@"[^0-9]");
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
                {
                    int num = Convert.ToInt32(textBox1.Text.Trim());
                    int alevel = 1;
                    string aspposition;
                    string aczposition;
                    if (comboBox2.Text.Trim() != "NULL")
                    {
                        alevel = Convert.ToInt32(comboBox2.Text.Trim());
                    }
                    else {
                        alevel = 1;
                    }
                    if (comboBox3.Text.Trim() != "NULL")
                    {
                        aspposition = comboBox3.Text.Trim();
                    }
                    else
                    {
                        aspposition = "左";
                    }
                    if (comboBox4.Text.Trim() != "NULL")
                    {
                        aczposition = comboBox4.Text.Trim();
                    }
                    else
                    {
                        aczposition = "7层";
                    }
                    string sql = "select * from access where " + num + " in(ano)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    Object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("门禁号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sql2 = "insert into access value(" + num + ",null," + alevel + ",'" + aspposition + "','" + aczposition + "',null);";
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
                    MessageBox.Show("门禁号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #region    //倒序显示数据
        public void DXQuery()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from access order by xuhao desc";
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
        #region     //顺序查数据
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
        private void AccessManage_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            Query();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
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
        #region    //全选与反选
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
        #region  //删除门禁
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
                        textBox1.Clear();
                    }
                    else
                    {
                        MessageBox.Show("卡号只能是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {//正常删除
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                try
                {
                    conn.Open();
                    Regex reg = new Regex(@"[^0-9]");
                    if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
                    {
                        int a = Convert.ToInt32(textBox1.Text.Trim());//获得门禁号
                        DeleteQuery(a);
                        textBox1.Clear();
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
            this.textBox1.Focus();
            Query();
        }

#endregion
        /// <summary>
        /// 删除函数
        /// </summary>
        /// <param name="a">参数门禁号</param>
        public void DeleteQuery(int a)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = null;
            String sql2 = null;
            try
            {
                db.Open(); //打开连接
                sql = "delete from access where ano=" + a + ";";
                int result2 = new MySqlCommand(sql, db).ExecuteNonQuery();
                if (result2 == 1)
                {
                    sql2 = "delete from card_access where ano=" + a + ";";
                    int result = new MySqlCommand(sql2, db).ExecuteNonQuery();
                    string sql3 = "update card set accredit='未授权' where  card.cno not in (SELECT cno from card_access);";
                    new MySqlCommand(sql3, db).ExecuteNonQuery();
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
        #region  //条件查询
        private void button14_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from access where 1=1 ";
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int ano = Convert.ToInt32(textBox1.Text.Trim());
                sql += " and ano=" + ano + " ";
            }
            if (comboBox1.Text.Trim() != "NULL")
            {
                int agno = Convert.ToInt32(comboBox1.Text.Trim());
                sql += " and agno=" + agno + " ";
            }
            if (comboBox2.Text.Trim() != "NULL")
            {
                int alevel = Convert.ToInt32(comboBox2.Text.Trim());
                sql += " and alevel=" + alevel + " ";
            }
            if (comboBox3.Text.Trim() != "NULL")
            {
                string aspposition = comboBox3.Text.Trim();
                sql += " and aspposition='" + aspposition + "' ";
            }
            if (comboBox4.Text.Trim() != "NULL")
            {
                string aczposition = comboBox4.Text.Trim();
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
            textBox1.Clear();
        }
#endregion
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void AccessManage_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            Query();
        }
        #region //门禁信息修改
        private void button11_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    int ano = Convert.ToInt32(textBox1.Text.Trim());
                    string sql = "select * from access where " + ano + " in(ano)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    Object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("门禁号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int alevelchange=0;
                        string sql2 = "update  access set xuhao=xuhao ";
                        if (comboBox1.Text.Trim() != "NULL")
                        {
                            int agno = Convert.ToInt32(comboBox1.Text.Trim());
                            sql2 += " , agno=" + agno + " ";
                        }
                        if (comboBox2.Text.Trim() != "NULL")
                        {
                            int alevel = Convert.ToInt32(comboBox2.Text.Trim());
                            alevelchange = alevel;
                            sql2 += " ,alevel=" + alevel + " ";
                        }
                        if (comboBox3.Text.Trim() != "NULL")
                        {
                            string aspposition = comboBox3.Text.Trim();
                            sql2 += " ,aspposition='" + aspposition + "' ";

                        }
                        if (comboBox4.Text.Trim() != "NULL")
                        {
                            string aczposition = comboBox4.Text.Trim();
                            sql2 += " ,aczposition='" + aczposition + "' ";

                        }
                        sql2 += " where ano= " + ano + ";";

                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        int result2 = cmd2.ExecuteNonQuery();
                        if (result2 == 1)
                        {
                            if (alevelchange != 0) {
                            string sql3 = "update card_access set alevel="+alevelchange+" where ano="+ano+"; ";
                                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                                int result3 = cmd3.ExecuteNonQuery();
                                if (result3>0) {
                                    string sql4 = "delete from card_access where clevel < alevel;";
                                    new MySqlCommand(sql4, conn).ExecuteNonQuery();
                                    string sql5 = "update card set accredit='未授权' where cno not in (SELECT cno from card_access);";
                                    new MySqlCommand(sql5, conn).ExecuteNonQuery();
                                }
                            }
                            
                            textBox1.Clear();
                            this.textBox1.Focus();
                        }
                        else
                        {
                            MessageBox.Show("修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("门禁号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void button1_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void AccessManage_Activated(object sender, EventArgs e)
        {
            Query();
        }
    }
}
#region 改进之前
////条件查询
//private void button13_Click(object sender, EventArgs e)
//{
//    String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
//    MySqlConnection db = new MySqlConnection(connetStr);
//    db.Open(); //打开连接
//    MySqlDataReader reader = null;
//    String sql = "select * from access where 1=1 ";
//    if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
//    {
//        int ano = Convert.ToInt32(textBox1.Text.Trim());
//        sql += " and ano=" + ano + " ";
//    }
//    if (!String.IsNullOrEmpty(comboBox1.Text.Trim()) && comboBox1.Text.Trim() != "NULL")
//    {
//        int agno = Convert.ToInt32(comboBox1.Text.Trim());
//        sql += " and agno=" + agno + " ";
//    }
//    if (comboBox2.Text.Trim() != "NULL")
//    {
//        int alevel = Convert.ToInt32(comboBox2.Text.Trim());
//        sql += " and alevel=" + alevel + " ";
//    }
//    if (comboBox3.Text.Trim() != "NULL")
//    {
//        string aspposition = comboBox3.Text.Trim();
//        sql += " and aspposition='" + aspposition + "' ";
//    }
//    if (comboBox4.Text.Trim() != "NULL")
//    {
//        string aczposition = comboBox4.Text.Trim();
//        sql += " and aczposition='" + aczposition + "' ";
//    }
//    MySqlCommand cmd = new MySqlCommand(sql, db);
//    try
//    {
//        Object result = cmd.ExecuteScalar();
//        if (result != null)
//        {
//            reader = cmd.ExecuteReader();//执行查询，并将结果返回给读取器
//            listView1.Items.Clear();
//            while (reader.Read())//开始循环遍历赋值
//            {
//                ListViewItem first = new ListViewItem(reader["ano"].ToString());
//                first.SubItems.Add(reader["agno"].ToString());
//                first.SubItems.Add(reader["alevel"].ToString());
//                first.SubItems.Add(reader["aspposition"].ToString());
//                first.SubItems.Add(reader["aczposition"].ToString());
//                this.listView1.Items.Add(first);//将所有的内容添加到表格内容中
//            }
//            Console.ReadLine();
//            reader.Close();
//        }
//        else
//        {
//            MessageBox.Show("未查询到有关数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }
//    }
//    catch (Exception exc) { Console.WriteLine(exc.Message); }
//    finally
//    {
//        db.Close();
//    }
//    textBox1.Clear();
//}
#endregion
