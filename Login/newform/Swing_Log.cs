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
    public partial class Swing_Log : Form
    {
        private Mainform mainform = null;
        public Swing_Log(Mainform mainform)
        {
            InitializeComponent();
            this.mainform = mainform;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        //日志读取
        public void LogReader()
        {
            string sFilePath = "e:\\";
            string sFileName = "rizhi.log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            StreamReader sr1 = new StreamReader(sFileName);
            string nextLine;
            while ((nextLine = sr1.ReadLine()) != null)
            {
                this.richTextBox1.AppendText(nextLine + "\r\n");
            }
            sr1.Close();
        }
        private void Swing_Log_Load(object sender, EventArgs e)
        {
            LogReader();
        }

        private void Swing_Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainform.Visible = true;
        }
    }
}
