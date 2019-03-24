using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class PersonManage : Form
    {
        public PersonManage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new PersonMessageChange().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new CardSet().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AccessManage().Show();
            this.Hide();
        }
    }
}
