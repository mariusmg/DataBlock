using System;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
    public partial class AspnetCodeGenerationDialog : Form
    {
        private string namespaceName;
        private bool generateContentPlaceholder = false;


        public AspnetCodeGenerationDialog()
        {
            InitializeComponent();
        }


        public string NamespaceName
        {
            get
            {
                return this.namespaceName;
            }
            set
            {
                this.namespaceName = value;
            }
        }


        public bool GenerateContentPlaceholder
        {
            get
            {
                return this.generateContentPlaceholder;
            }
            set
            {
                this.generateContentPlaceholder = value;
            }
        }

        #region event handlers

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxNamespace.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the namespace");
                return;
            }

            this.namespaceName = this.textBoxNamespace.Text;
            this.generateContentPlaceholder = this.checkBoxGeneratePlaceholder.Checked;


            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}