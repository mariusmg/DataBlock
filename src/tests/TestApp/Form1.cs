using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using voidsoft.DataBlock;


using Extender;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configuration.ReadConfigurationFromConfigFile();

            CategoryTableMetadata c = new CategoryTableMetadata();
            CategoryPersistentObject cpo = new CategoryPersistentObject(c);
            CategoryTableMetadata[] fg = (CategoryTableMetadata[])  cpo.GetTableMetadata();



            dataGridView1.DataSource = fg;

        }
    }
}