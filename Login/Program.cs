using Login.newform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Mainform());
            //Application.Run(new AccessManage());
            Login form1 = new Login();

            form1.ShowDialog();//界面转换
            if (form1.DialogResult == DialogResult.OK)
            {
                form1.Dispose();
                Application.Run(new Mainform());
            }
        }
    }
}
