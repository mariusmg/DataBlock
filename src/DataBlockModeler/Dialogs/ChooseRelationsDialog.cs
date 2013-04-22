using System;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler.Dialogs
{
    public partial class ChooseRelationsDialog : Form
    {
        private TableRelationImportType type = TableRelationImportType.UserAdded;

        private string filePath;


        public ChooseRelationsDialog()
        {
            InitializeComponent();
        }



        public TableRelationImportType SelectedImportType
        {
            get
            {
                return type;
            }
        }


        public string SelectedFilePath
        {
            get
            {
                return filePath;
            }
        }




        private void radioButtonImportFile_CheckedChanged(object sender, EventArgs e)
        {
            textBoxFilePath.Enabled = radioButtonImportFile.Checked;
            buttonBrowse.Enabled = radioButtonImportFile.Checked;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = null;

            try
            {
                dialog = new OpenFileDialog();

                dialog.Title = "Please choose the table relations file";
                dialog.Multiselect = false;
                dialog.InitialDirectory = Application.StartupPath;
                dialog.Filter = "Text files(*.txt)|*.txt";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilePath.Text = dialog.FileName;
                }
            }
            finally
            {
                if (dialog != null)
                {
                    dialog.Dispose();
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.radioButtonImportFile.Checked && textBoxFilePath.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please choose the import file");
                return;
            }


            if (radioButtonLoadFromDatabase.Checked)
            {
                type = TableRelationImportType.Database;
            }
            else if (radioButtonAddUser.Checked)
            {
                type = TableRelationImportType.UserAdded;
            }
            else
            {
                type = TableRelationImportType.File;
                this.filePath = textBoxFilePath.Text;
            }


            this.DialogResult = DialogResult.OK;

            this.Close();

        }
    }
}