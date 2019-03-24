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
    public partial class HelpWord : Form
    {
        private Mainform mainform = null;

        public HelpWord(Mainform mainform)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.mainform = mainform;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void HelpWord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainform.Visible = true;
        }
    }
}
