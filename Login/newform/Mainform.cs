using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.newform
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        private void tabControl1_Selected_1(object sender, TabControlEventArgs e)
        {

        }
        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        #region 添加tabpage
        public void Add_TabPage(string str, Form myForm)
        {
            if (tabControlCheckHave(this.tabControl1, str))
            {
                return;
            }
            else
            {
                tabControl1.TabPages.Add(str);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.Dock = DockStyle.Fill;
                myForm.TopLevel = false;
                myForm.Show();
                myForm.Parent = tabControl1.SelectedTab;
            }
        }
        #endregion
        #region 判断页面是否存在
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
        #endregion
        #region //绘制
        private void tabControl1_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            try
            {
                this.tabControl1.TabPages[e.Index].BackColor = Color.White;
                Rectangle tabRect = this.tabControl1.GetTabRect(e.Index);
                SolidBrush solidBrush = new SolidBrush(Color.White);
                e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.ControlText, (float)(tabRect.X + 2), (float)(tabRect.Y + 2));
                using (Pen pen = new Pen(Color.White))
                {
                    tabRect.Offset(tabRect.Width - 15, 2);
                    tabRect.Width = 15;
                    tabRect.Height = 15;
                    e.Graphics.DrawRectangle(pen, tabRect);
                }
                Color color = (e.State == DrawItemState.Selected) ? Color.White : Color.White;
                using (Brush brush = new SolidBrush(Color.White))
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

                //填充tabControl的上部除了TabPage标题的backcolor
                //Graphics g = e.Graphics;
                //Rectangle endPageRect = tabControl1.GetTabRect(tabControl1.TabPages.Count - 1); //最后一个标题栏的范围   
                //Rectangle TitleRect = tabControl1.GetTabRect(e.Index);              //当前标题栏的范围   
                //Rectangle HeaderBackRect = Rectangle.Empty;
                //HeaderBackRect = new Rectangle(new Point(endPageRect.X + endPageRect.Width, endPageRect.Y),
                //new Size(tabControl1.Width - endPageRect.X - endPageRect.Width, endPageRect.Height));
                //Brush b = new SolidBrush(Color.Red);
                //g.FillRectangle(b, HeaderBackRect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 鼠标在按钮区点击的位置
        private void tabControl1_MouseDown_1(object sender, MouseEventArgs e)
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
        #endregion
        private void toolStripSplitButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripSplitButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripSplitButton7_Click(object sender, EventArgs e)
        {
            
        }
        //测试页面的加载
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AccessControl_Test accessControl_Test = new AccessControl_Test(this);
            this.Hide();
            accessControl_Test.Show();
        }
        //日志页面的加载
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Swing_Log swing_Log = new Swing_Log(this);
            this.Hide();
            swing_Log.Show();
        }
        //员工信息维护页面的加载
        private void 绑定与解绑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemberManage memberManage = new MemberManage();
            memberManage.mainform = this;
            Add_TabPage("员工信息维护  ", memberManage);
        }
        //员工与门禁卡绑定页面的加载
        private void 员工与门禁卡绑定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind bind = new Bind();
            bind.mainform = this;
            Add_TabPage("员工与门禁卡绑定  ", bind);
        }
        //解绑
        private void 员工与门禁卡解绑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unbind unbind = new Unbind();
            unbind.mainform = this;
            Add_TabPage("员工与门禁卡解绑  ", unbind);
        }
        //门禁卡管理页面的加载
        private void toolStripSplitButton3_Click_1(object sender, EventArgs e)
        {
            CardManage cardManage = new CardManage();
            cardManage.mainform = this;
            Add_TabPage("门禁卡管理  ", cardManage);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

           
        }
        //门禁授权页面的加载
        private void 取消授权ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessAccredit accessAccredit = new AccessAccredit();
            accessAccredit.mainform = this;
            Add_TabPage("门禁授权  ", accessAccredit);
           
        }
        //门禁信息维护页面的加载
        private void 门禁分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessManage accessManage = new AccessManage();
            accessManage.mainform = this;
            Add_TabPage("门禁信息维护  ", accessManage);
        }
        //取消授权页面的加载
        private void 取消授权ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UnAccredit unAccredit = new UnAccredit();
            unAccredit.mainform = this;
            Add_TabPage("取消授权  ", unAccredit);
        }
        //门禁分组页面的加载
        private void 门禁分组ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Group group = new Group();
            group.mainform = this;
            Add_TabPage("门禁分组  ", group);
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            
        }
    }
}


