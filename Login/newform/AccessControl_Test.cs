using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.newform
{
    public partial class AccessControl_Test : Form
    {
        private Mainform mainform = null;

        public AccessControl_Test(Mainform mainform)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.mainform = mainform;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 2;
        }

        private void AccessControl_Test_Load(object sender, EventArgs e)
        {
            Query();
        }
        #region 查询显示数据
        public void Query()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            String sql = "select mno,name,sex,MCA.CAcno,MCA.CAano,aspposition,aczposition from access,(select mno,name,age,sex,CA.cno as CAcno,CA.ano as CAano from member,(select cno,ano from card_access) CA where CA.cno=member.cno ) MCA where MCA.CAano=ano;";
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
                    first.SubItems.Add(reader["sex"].ToString());
                    first.SubItems.Add(reader["CAcno"].ToString());
                    first.SubItems.Add(reader["CAano"].ToString());
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
        private void AccessControl_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainform.Visible = true;
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
        #region   //测试是否可以刷开门
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable P_dt = null;
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();
                int cno = Convert.ToInt32(textBox1.Text.Trim());
                int ano = Convert.ToInt32(textBox2.Text.Trim());
                string sql5 = "select * from card_access where cno=" + cno + " and ano=" + ano + ";";
                Object result4 = new MySqlCommand(sql5, conn).ExecuteScalar();
                if (result4 != null)
                {
                    bool t = true;
                    if (listView1.Items.Count > 0)
                    {
                        foreach (ListViewItem lt in listView1.Items)
                        {
                            int tb_mno = Convert.ToInt32(lt.SubItems[0].Text.Trim());
                            string tb_name = lt.SubItems[1].Text.Trim();
                            string tb_sex = lt.SubItems[2].Text.Trim();
                            int tb_cno = Convert.ToInt32(lt.SubItems[3].Text.Trim());
                            int tb_ano = Convert.ToInt32(lt.SubItems[4].Text.Trim());
                            string tb_aspposition = lt.SubItems[5].Text.Trim();
                            string tb_aczposition = lt.SubItems[6].Text.Trim();
                            if (tb_cno == cno && tb_ano == ano)
                            {
                                t = false;
                                //System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                                //player.SoundLocation = @"";
                                //player.Load();
                                //player.Play();
                                string time = System.DateTime.Now.ToString();
                                string log = time+"   员工号为" + tb_mno + "，性别为" + tb_sex + "的" + tb_name + "所绑定的" + tb_cno + "号门禁卡开启了位于" + tb_aczposition + "楼" + tb_aspposition + "边的" + tb_ano + "号门禁！";
                                LogWrite(log);
                                textBox1.Clear();
                                textBox2.Clear();
                                MessageBox.Show("嘀...顺利开门！");
                            }
                        }
                        if (t)
                        {
                            String sql2 = "select CA.cno,CA.ano,aspposition,aczposition from access,(select cno,ano from card_access) CA where CA.ano=access.ano and CA.ano=" + ano + " and cno=" + cno + ";";
                            MySqlDataAdapter adapter = new MySqlDataAdapter(sql2, conn);// 读取表数据
                            P_dt = new DataTable();//p_dt是一个表类型
                            adapter.Fill(P_dt);
                            string aspposition = P_dt.Rows[0][2].ToString();
                            string aczposition = P_dt.Rows[0][3].ToString();
                            string time = System.DateTime.Now.ToString();
                            string log = time+"   未绑定任何员工的" + cno + "号门禁卡开启了位于" + aczposition + "楼" + aspposition + "层的" + ano + "号门禁！";
                            LogWrite(log);
                            textBox1.Clear();
                            textBox2.Clear();
                            MessageBox.Show("嘀...顺利开门！");
                        }
                    }
                }
                else
                { MessageBox.Show("嘀呜,嘀呜,无法开门！"); }
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
        #region  //记录日志并输出
        public static void LogWrite(string strLog)
        {
            string sFilePath = "e:\\";
            string sFileName = "rizhi.log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            //DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + 
            sw.WriteLine(strLog);
            sw.Close();
            fs.Close();
        }
#endregion
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }
        #region     //条件查询
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable P_dt = null;
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection db = new MySqlConnection(connetStr);
            db.Open(); //打开连接
            MySqlDataReader reader = null;
            String sql = "select * from (select mno,name,sex,MCA.CAcno,MCA.CAano,aspposition,aczposition from access,(select mno,name,age,sex,CA.cno as CAcno,CA.ano as CAano from member,(select cno,ano from card_access) CA where CA.cno=member.cno ) MCA where MCA.CAano=ano) A where 1=1 ";
            
            if (!String.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                int mno = Convert.ToInt32(textBox3.Text.Trim());
                sql += " and A.mno=" + mno + " ";
            }
            if (!String.IsNullOrEmpty(textBox6.Text.Trim()))
            {
                int cno = Convert.ToInt32(textBox6.Text.Trim());
                sql += " and A.CAcno=" + cno + " ";
            }
            if (comboBox1.Text.Trim()!="NULL") {
                string sex = comboBox1.Text.Trim();
                sql += " and A.sex='" + sex + "' ";
            }
            if (!String.IsNullOrEmpty(textBox7.Text.Trim()))
            {
                int ano = Convert.ToInt32(textBox7.Text.Trim());
                sql += " and A.CAano=" + ano + " ";
            }
            if (comboBox2.Text.Trim()!="NULL")
            {
                string aspposition = comboBox2.Text.Trim();
                sql += " and A.aspposition like'" + aspposition + "' ";
            }
            if (comboBox3.Text.Trim()!="NULL")
            {
                string aczposition = comboBox3.Text.Trim();
                sql += " and A.aczposition like'" + aczposition + "' ";
            }
            if (!String.IsNullOrEmpty(textBox4.Text.Trim()))
            {
                string name = textBox4.Text.Trim();
                sql += " and A.name like'%" + name + "%' ";
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
                        first.SubItems.Add(reader["sex"].ToString());
                        first.SubItems.Add(reader["CAcno"].ToString());
                        first.SubItems.Add(reader["CAano"].ToString());
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
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
#endregion
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void AccessControl_Test_Activated(object sender, EventArgs e)
        {
            Query();
        }
    }

}
