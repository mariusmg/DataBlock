using System;
using System.Windows.Forms;
using voidsoft.DataBlock;

namespace DataBlockDemo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //read DataBlock config details 
            Configuration.ReadConfigurationFromConfigFile();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}