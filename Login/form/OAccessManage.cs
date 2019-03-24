using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;



namespace Login
{
    public partial class AccessManage : Form
    {
        private DataTable dt;

        /// <summary>
        /// 门禁系统主页
        /// </summary>
        public AccessManage()
        {
            InitializeComponent();
        }

        public AccessManage(DataTable dt)
        {
            this.dt = dt;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AccessManage_Load(object sender, EventArgs e)
        {
            new Form();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// 调用人员数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            String sqlname = "member";
            GetMessage(sqlname);
        }
        /// <summary>
        /// 调用门禁卡数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            String sqlname = "card";
            GetMessage(sqlname);
        }
        /// <summary>
        /// 调用门禁数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            String sqlname = "access";
            GetMessage(sqlname);
        }
        /// <summary>
        /// 从mysql数据库获取sqlname表数据并且显示到dataGridView
        /// </summary>
        /// <param name="sqlname">传入表名</param>
        /// <returns></returns>
        private DataTable GetMessage(string sqlname)
        {
            DataTable P_dt = null;
            // server=127.0.0.1/localhost 代表本机，端口号默认
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=fwyy;charset=UTF8;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                Console.WriteLine("已经建立连接");
                string sql = "select * from " + sqlname + ";";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);
                ////执行ExecuteReader()返回一个MySqlDataReader对象
                ////用于查询数据库。查询结果是返回MySqlDataReader对象，MySqlDataReader包含sql语句执行的结果，并提供一个方法从结果中阅读一行
                //MySqlDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                //{
                //    //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                //    //Console.WriteLine(reader.GetInt32(0)+reader.GetString(1)+reader.GetString(2));
                //    Console.WriteLine(reader.GetInt32("userid") + reader.GetString("username") + reader.GetString("password"));//"userid"是数据库对应的列名，推荐这种方式
                //}
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);// 读取表数据
                P_dt = new DataTable();//p_dt是一个表类型
                if (this.dataGridView1.DataSource != null)

                {
                    DataTable dt = (DataTable)this.dataGridView1.DataSource;
                    //对表进行清理时，行和列都要清理
                    dt.Rows.Clear();
                    dt.Columns.Clear();
                    dataGridView1.DataSource = dt;
                    adapter.Fill(P_dt);//将读取到的表数据添加到p_dt里
                    this.dataGridView1.DataSource = P_dt;//数据绑定
                    switch (sqlname)
                    {
                        case "member":
                            this.dataGridView1.Columns[0].HeaderText = "员工号"; //改列名称
                            this.dataGridView1.Columns[1].HeaderText = "名字";//改列名称
                            this.dataGridView1.Columns[2].HeaderText = "门禁卡号";//改列名称
                            break;
                        case "access":
                            this.dataGridView1.Columns[0].HeaderText = "门禁号"; //改列名称
                            this.dataGridView1.Columns[1].HeaderText = "门禁组号";//改列名称
                            this.dataGridView1.Columns[2].HeaderText = "门禁级别";//改列名称
                            this.dataGridView1.Columns[3].HeaderText = "门禁水平位置";//改列名称
                            this.dataGridView1.Columns[4].HeaderText = "门禁垂直位置";//改列名称
                            break;
                        case "card":
                            this.dataGridView1.Columns[0].HeaderText = "卡号"; //改列名称
                            this.dataGridView1.Columns[1].HeaderText = "门禁号";//改列名称
                            this.dataGridView1.Columns[2].HeaderText = "门禁组号";//改列名称
                            this.dataGridView1.Columns[3].HeaderText = "授权";//改列名称
                            this.dataGridView1.Columns[4].HeaderText = "卡级别";//改列名称
                            this.dataGridView1.Columns[5].HeaderText = "绑定";//改列名称
                            break;
                        default: break;
                    }

                }
                else

                {
                    adapter.Fill(P_dt);//将读取到的表数据添加到p_dt里
                    this.dataGridView1.DataSource = P_dt;//间表p_dt显示到dataGridView上
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
            return P_dt;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            ////PersonMessageChange personMessageChange = new PersonMessageChange();
            ////personMessageChange.ShowDialog();
            ////if (personMessageChange.DialogResult == DialogResult.OK)
            ////{
            ////    GetMessage(sqlname);    //DataGridView控件的值
            ////}
            //string sqlname = "card";

            var child = new PersonMessageChange();
            child.TopLevel = false;
            ////子窗体无边框、也就没有最大化、最小化和关闭按钮
            child.FormBorderStyle = FormBorderStyle.None;
            //子窗体的填充方式为充满它的父控件
            child.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(child);
            child.Show();
            string sqlname = "card";
            DialogResult dr = child.getDR();
            if (dr == DialogResult.OK)
            {
                GetMessage(sqlname);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var child = new CardSet();
            child.TopLevel = false;
            ////子窗体无边框、也就没有最大化、最小化和关闭按钮
            child.FormBorderStyle = FormBorderStyle.None;
            //子窗体的填充方式为充满它的父控件
            child.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(child);
            child.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var child = new CardManage();
            child.TopLevel = false;
            ////子窗体无边框、也就没有最大化、最小化和关闭按钮
            child.FormBorderStyle = FormBorderStyle.None;
            //子窗体的填充方式为充满它的父控件
            child.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(child);
            child.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            var child = new AccessControl();
            child.TopLevel = false;
            ////子窗体无边框、也就没有最大化、最小化和关闭按钮
            child.FormBorderStyle = FormBorderStyle.None;
            //子窗体的填充方式为充满它的父控件
            child.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(child);
            child.Show();
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            var child = new AccessAccredit();
            child.TopLevel = false;
            ////子窗体无边框、也就没有最大化、最小化和关闭按钮
            child.FormBorderStyle = FormBorderStyle.None;
            //子窗体的填充方式为充满它的父控件
            child.Dock = DockStyle.Fill;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(child);
            child.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //var child = new PersonMessageChange();
            //child.TopLevel = false;
            //////子窗体无边框、也就没有最大化、最小化和关闭按钮
            //child.FormBorderStyle = FormBorderStyle.None;
            ////子窗体的填充方式为充满它的父控件
            //child.Dock = DockStyle.Fill;
            //this.panel1.Controls.Clear();
            //this.panel1.Controls.Add(child);
            //child.Show();
            PersonMessageChange personMessageChange = new PersonMessageChange();
            Add_TabPage("f1中华人民共和国窗体  ", personMessageChange);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Add_TabPage(string str, Form myForm)
        {
            //if (tabControlCheckHave(this.tabControl1, str))
            //{
            //    return;
            //}
            //else
            //{
                tabControl1.TabPages.Add(str);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);

                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.Dock = DockStyle.Fill;
                myForm.TopLevel = false;
                myForm.Show();
                myForm.Parent = tabControl1.SelectedTab;
            //}
        }

        public bool tabControlCheckHave(System.Windows.Forms.TabControl tab, String tabName)
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

            try
            {
                this.tabControl1.TabPages[e.Index].BackColor = Color.LightBlue;
                Rectangle tabRect = this.tabControl1.GetTabRect(e.Index);
                e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.ControlText, (float)(tabRect.X + 2), (float)(tabRect.Y + 2));
                using (Pen pen = new Pen(Color.White))
                {
                    tabRect.Offset(tabRect.Width - 15, 2);
                    tabRect.Width = 15;
                    tabRect.Height = 15;
                    e.Graphics.DrawRectangle(pen, tabRect);
                }
                Color color = (e.State == DrawItemState.Selected) ? Color.LightBlue : Color.White;
                using (Brush brush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(brush, tabRect);
                }
                using (Pen pen2 = new Pen(Color.Red))
                {
                    Point point = new Point(tabRect.X + 3, tabRect.Y + 3);
                    Point point2 = new Point((tabRect.X + tabRect.Width) - 3, (tabRect.Y + tabRect.Height) - 3);
                    e.Graphics.DrawLine(pen2, point, point2);
                    Point point3 = new Point(tabRect.X + 3, (tabRect.Y + tabRect.Height) - 3);
                    Point point4 = new Point((tabRect.X + tabRect.Width) - 3, tabRect.Y + 3);
                    e.Graphics.DrawLine(pen2, point3, point4);
                }
                e.Graphics.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               int x = e.X;
                int y = e.Y;

                Rectangle tabRect = this.tabControl1.GetTabRect(this.tabControl1.SelectedIndex);
                tabRect.Offset(tabRect.Width - 0x12, 2);
                tabRect.Width = 15;
                tabRect.Height = 15;
                if ((((x > tabRect.X) && (x < tabRect.Right)) && (y > tabRect.Y)) && (y < tabRect.Bottom))
                {
                    this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
                }
            }
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
