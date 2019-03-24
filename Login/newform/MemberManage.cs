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
    public partial class MemberManage : Form
    {
        internal Mainform mainform;

        public MemberManage()
        {
            InitializeComponent();
        }
        #region // 查询数据
        public void Query()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from member";
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
                reader.Close();
                Console.ReadLine();
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                db.Close();
            }
        }
        #endregion
        #region //全选以及反选
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
        #region // 添加员工
        private void button8_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                //对新增人员时进行一些列的判断
                Regex reg = new Regex(@"[^0-9]");
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))

                {//1员工号；2员工姓名；3是门禁卡号；4.性别；5.年龄
                    if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        if (!String.IsNullOrEmpty(textBox5.Text.Trim()) && !reg.IsMatch(textBox5.Text.Trim()))
                        {
                            int a = Convert.ToInt32(textBox1.Text.Trim());
                            string s = textBox2.Text.Trim();
                            string sex = comboBox1.Text.Trim();
                            int age = Convert.ToInt32(textBox5.Text.Trim());

                            string sql = "select * from member where " + a + " in(mno)";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            Object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                MessageBox.Show("员工号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string sql2 = "insert into member value(" + a + ",'" + s + "',null,'" + sex + "'," + age + ",null)";
                                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                                int result2 = cmd2.ExecuteNonQuery();
                                if (result2 == 1)
                                {
                                    TBClear();
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
                            MessageBox.Show("年龄不能为空且为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //int cq = CountQuery();
        }
        #endregion
        #region // 倒序查询显示数据
        public void DXQuery()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select * from member order by xuhao desc";
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
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                reader.Close();
                db.Close();
            }
        }
        #endregion
        // 在员工名字框按下回车触发
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button8_Click(sender, e);
            }
        }
        #region // 删除员工
        private void button9_Click(object sender, EventArgs e)
        {
            Boolean t = false;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked == true)
                {
                    //MessageBox.Show(listView1.Items[i].Text);//复选框对应的第i+1行第一列数据
                    //MessageBox.Show(listView1.Items[i].SubItems[2].Text);//复选框对应第i+1行第3列的文本
                    int a = Convert.ToInt32(listView1.Items[i].Text);
                    if (listView1.Items[i].SubItems[2].Text.Equals(""))
                    {
                        DeleteQuery2(a);
                    }
                    else
                    {
                        int b = Convert.ToInt32(listView1.Items[i].SubItems[2].Text.ToString());
                        DeleteQuery(a, b);
                    }
                    t = true;//说明有选中的
                }
            }
            if (t)
            {//listview里面有选中项
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
                MySqlConnection conn = new MySqlConnection(connetStr);
                try
                {
                    conn.Open();
                    Regex reg = new Regex(@"[^0-9]");
                    if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                    {
                        if (!reg.IsMatch(textBox1.Text.ToString().Trim()))
                        {
                            int a = Convert.ToInt32(textBox1.Text.Trim());
                            string sql = "select * from member where " + a + " in(mno)";
                            MySqlCommand cmd = new MySqlCommand(sql, conn);
                            Object result = cmd.ExecuteScalar();
                            if (result == null)
                            {
                                MessageBox.Show("此员工号的人员已不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DeleteQuery2(a);
                            }
                        }
                        else
                        {
                            MessageBox.Show("员工号只能是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (cmd.ExecuteScalar() == null)
                        {
                            MessageBox.Show("人员已不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DeleteQuery2(a);
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
            TBClear();
            Query();
        }
        #endregion
        /// <summary>
        /// 删除更新数据
        /// </summary>
        /// <param name="a">传参员工号</param>
        /// <param name="b">传参门禁卡号</param>
        public void DeleteQuery(int a, int b)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = null;
            String sql2 = null;
            MySqlCommand cmd = null;
            MySqlCommand cmd2 = null;
            db.Open(); //打开连接
            try
            {
                if (b == 0)
                {
                    sql = "delete from member where mno=" + a + ";";
                    cmd = new MySqlCommand(sql, db);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    sql = "delete from member where mno=" + a + ";";
                    cmd = new MySqlCommand(sql, db);
                    cmd.ExecuteNonQuery();//执行查询，并将结果返回给读取器

                    sql2 = "update card set bind='未绑定' where cno=" + b + ";";
                    cmd2 = new MySqlCommand(sql2, db);
                    cmd2.ExecuteNonQuery();//执行查询，并将结果返回给读取器
                }
                db.Close();
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 删除数据2
        /// </summary>
        /// <param name="a">传入的员工号</param>
        public void DeleteQuery2(int a)
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
                sql = "select cno from member where mno=" + a + ";";
                cmd = new MySqlCommand(sql, db);
                object result = cmd.ExecuteScalar();//执行查询，并将结果返回给读取器

                sql = "delete from member where mno=" + a + ";";
                int result2 = new MySqlCommand(sql, db).ExecuteNonQuery();
                if (result != null && result2 == 1)
                {
                    int b = Convert.ToInt32(result);
                    sql2 = "update card set bind='未绑定' where cno=" + b + ";";
                    cmd2 = new MySqlCommand(sql2, db);
                    cmd2.ExecuteNonQuery();
                    this.textBox1.Focus();
                }
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
            finally
            {
                db.Close();
            }
        }
        // 窗体第一次显示时发生
        private void MemberManage_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            Query();
           // int cq = CountQuery();
        }
        #region //条件查询
        private void button14_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from member where 1=1 ";
            Regex reg = new Regex(@"[^0-9]");
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int mno = Convert.ToInt32(textBox1.Text.Trim());
                sql += " and mno=" + mno + " ";
            }
            
            //if (!String.IsNullOrEmpty(textBox3.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
            //{
            //    int cno = Convert.ToInt32(textBox3.Text.Trim());
            //    sql += " and cno=" + cno + " ";
            //}
            string sex = comboBox1.Text.Trim();
            sql += " and sex='" + sex + "' ";
            if (!String.IsNullOrEmpty(textBox5.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
            {
                int age = Convert.ToInt32(textBox5.Text.Trim());
                sql += " and age=" + age + " ";
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
            TBClear();
        }
#endregion
        //清空
        public void TBClear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
        }

        private void MemberManage_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            Query();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }
        #region //修改
        private void button1_Click(object sender, EventArgs e)
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {//1员工号；2员工姓名；3是门禁卡号；4.性别；5.年龄
                    int mno = Convert.ToInt32(textBox1.Text.Trim());
                    string sql = "select * from member where " + mno + " in(mno)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    Object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("员工号不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sql2 = "update  member set xuhao=xuhao ";
                        if (!String.IsNullOrEmpty(textBox2.Text.Trim()))
                        {
                            string name = textBox2.Text.Trim();
                            sql2 += " , name='" + name + "' ";
                        }
                        string sex = comboBox1.Text.Trim();
                        if (!String.IsNullOrEmpty(textBox5.Text.Trim()))
                        {
                            int age = Convert.ToInt32(textBox5.Text.Trim());
                            sql2 += " , age=" + age + " ";

                        }
                        sql2 += " where mno= " + mno + ";";

                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        int result2 = cmd2.ExecuteNonQuery();
                        if (result2 == 1)
                        {
                            TBClear();
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
            Query();
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
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

        private void MemberManage_Activated(object sender, EventArgs e)
        {
            Query();
        }

        private void MemberManage_MouseEnter(object sender, EventArgs e)
        {
            Query();
        }
    }
}
#region 转移
//绑定
//private void button11_Click(object sender, EventArgs e)
//{
//    String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
//    MySqlConnection conn = new MySqlConnection(connetStr);
//    try
//    {
//        conn.Open();//对新增人员时进行一些列的判断
//        Regex reg = new Regex(@"[^0-9]");
//        if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
//        {
//            if (!String.IsNullOrEmpty(textBox3.Text.Trim()) && !reg.IsMatch(textBox3.Text.Trim()))
//            {
//                int a = Convert.ToInt32(textBox1.Text.Trim());
//                int b = Convert.ToInt32(textBox3.Text.Trim());

//                //员工号是否存在且未绑定
//                string sql = "select * from member where " + a + " in(mno) and cno is null";
//                MySqlCommand cmd = new MySqlCommand(sql, conn);
//                Object result = cmd.ExecuteScalar();
//                //门禁卡号是否存在且未绑定
//                string sql12 = "select * from card where " + b + " in(cno) and bind='未绑定'";
//                MySqlCommand cmd12 = new MySqlCommand(sql12, conn);
//                //查询有结果或返回null
//                Object result12 = cmd12.ExecuteScalar();

//                if (result == null || result12 == null)
//                {
//                    MessageBox.Show("员工号或者门禁卡号错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//                else
//                {//更新员工表
//                    string sql21 = "update  member set cno=" + b + " where mno=" + a + ";";
//                    MySqlCommand cmd21 = new MySqlCommand(sql21, conn);
//                    int result21 = cmd21.ExecuteNonQuery();
//                    //更新门禁卡表
//                    String sql22 = "update card set bind='绑定' where cno=" + b + ";";
//                    MySqlCommand cmd22 = new MySqlCommand(sql22, conn);
//                    //插入删除更改返回1或失败
//                    int result22 = cmd22.ExecuteNonQuery();
//                    if (result21 == 1 && result22 == 1)
//                    {
//                        this.textBox1.Focus();
//                        TBClear();
//                        Query();
//                    }
//                    else
//                    {
//                        MessageBox.Show("绑定失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show("卡号不能为空且为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        else
//        {
//            MessageBox.Show("员工号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }
//    }
//    catch (MySqlException ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//    finally
//    {
//        conn.Close();
//    }
//}
////解绑
//private void button10_Click(object sender, EventArgs e)
//{
//    String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
//    MySqlConnection conn = new MySqlConnection(connetStr);
//    try
//    {
//        conn.Open();//解绑时进行一些列的判断
//        Regex reg = new Regex(@"[^0-9]");
//        if (!String.IsNullOrEmpty(textBox1.Text.Trim()) && !reg.IsMatch(textBox1.Text.Trim()))
//        {
//            if (!String.IsNullOrEmpty(textBox3.Text.Trim()) && !reg.IsMatch(textBox3.Text.Trim()))
//            {
//                int a = Convert.ToInt32(textBox1.Text.Trim());
//                int b = Convert.ToInt32(textBox3.Text.Trim());

//                //员工号与门禁卡号是否绑定
//                string sql = "select * from member where mno=" + a + " and cno=" + b + ";";
//                MySqlCommand cmd = new MySqlCommand(sql, conn);
//                Object result = cmd.ExecuteScalar();
//                if (result == null)
//                {
//                    MessageBox.Show("员工号或者门禁卡号错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                else
//                {//更新员工表
//                    string sql21 = "update  member set cno=null where mno=" + a + ";";
//                    MySqlCommand cmd21 = new MySqlCommand(sql21, conn);
//                    int result21 = cmd21.ExecuteNonQuery();
//                    //更新门禁卡表
//                    String sql22 = "update card set bind='未绑定' where cno=" + b + ";";
//                    MySqlCommand cmd22 = new MySqlCommand(sql22, conn);
//                    //插入删除更改返回1或失败
//                    int result22 = cmd22.ExecuteNonQuery();
//                    if (result21 == 1 && result22 == 1)
//                    {
//                        this.textBox1.Focus();
//                        TBClear();
//                        Query();
//                    }
//                    else
//                    {
//                        MessageBox.Show("解绑失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show("卡号不能为空且为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        else
//        {
//            MessageBox.Show("员工号不能为空且是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }
//    }
//    catch (MySqlException ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//    finally
//    {
//        conn.Close();
//    }
//}
// 计数查询
//public int CountQuery()
//{
//    int a = 0;
//    String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
//    MySqlConnection db = new MySqlConnection(connetStr);

//    try
//    {
//        db.Open(); //打开连接
//        String sql = "select count(*) from member ";
//        MySqlCommand cmd = new MySqlCommand(sql, db);
//        a = Convert.ToInt32(cmd.ExecuteScalar());
//    }
//    catch (Exception exc) { Console.WriteLine(exc.Message); }
//    finally
//    {
//        db.Close();
//    }
//    return a;
//}
#endregion