using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
    public class Program
    {
        [STAThread()]
        public static void Main()
        {
            //create the output folder
            if (!Directory.Exists(Application.StartupPath + @"\Output"))
            {
                try
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\Output");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Warning. Failed to create the output folder. " + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.Run(new RootWindow());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show((e.Exception.Message));
        }
    }
}