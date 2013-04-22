using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
	/// <summary>
	/// Summary description for CodeGeneratorMappingsDialog.
	/// </summary>
    public class CodeGeneratorMappingsDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNamespace;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label labelLanguage;


        private string namespaceName;
        private string path;
        private Language selectedLanguage;
        private PictureBox pictureBox;
        private System.Windows.Forms.Label labelSubtitle;

        public CodeGeneratorMappingsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGeneratorMappingsDialog));
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.labelFolder = new System.Windows.Forms.Label();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxLogo)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label2.Location = new System.Drawing.Point(299, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "Code generator";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.White;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(472, 85);
            this.pictureBoxLogo.TabIndex = 16;
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOK.Location = new System.Drawing.Point(330, 213);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(64, 32);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Location = new System.Drawing.Point(405, 213);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(64, 32);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.labelLanguage);
            this.groupBox.Controls.Add(this.comboBoxLanguage);
            this.groupBox.Controls.Add(this.labelFolder);
            this.groupBox.Controls.Add(this.buttonBrowseFolder);
            this.groupBox.Controls.Add(this.textBoxFolder);
            this.groupBox.Controls.Add(this.textBoxNamespace);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(0, 84);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(472, 123);
            this.groupBox.TabIndex = 20;
            this.groupBox.TabStop = false;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(61, 84);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(55, 13);
            this.labelLanguage.TabIndex = 6;
            this.labelLanguage.Text = "Language";
            this.labelLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(120, 84);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(136, 21);
            this.comboBoxLanguage.TabIndex = 2;
            // 
            // labelFolder
            // 
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(80, 57);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(36, 13);
            this.labelFolder.TabIndex = 4;
            this.labelFolder.Text = "Folder";
            this.labelFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBrowseFolder.Location = new System.Drawing.Point(432, 58);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(24, 20);
            this.buttonBrowseFolder.TabIndex = 1;
            this.buttonBrowseFolder.Text = "...";
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Location = new System.Drawing.Point(120, 58);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.ReadOnly = true;
            this.textBoxFolder.Size = new System.Drawing.Size(304, 20);
            this.textBoxFolder.TabIndex = 2;
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Location = new System.Drawing.Point(120, 23);
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(336, 20);
            this.textBoxNamespace.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Namespace";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.BackColor = System.Drawing.Color.White;
            this.labelSubtitle.Location = new System.Drawing.Point(136, 56);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(326, 13);
            this.labelSubtitle.TabIndex = 21;
            this.labelSubtitle.Text = "Generates domain objects, business objects  and data access code";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(12, 7);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(75, 71);
            this.pictureBox.TabIndex = 22;
            this.pictureBox.TabStop = false;
            // 
            // CodeGeneratorMappingsDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(474, 255);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelSubtitle);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeGeneratorMappingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Domain objects generator";
            this.Load += new System.EventHandler(this.CodeGeneratorMappingsDialog_Load);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxLogo)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void CodeGeneratorMappingsDialog_Load(object sender, System.EventArgs e)
        {
            Language lng = new Language();
            string[] values = System.Enum.GetNames(lng.GetType());

            this.comboBoxLanguage.DataSource = values;
            this.comboBoxLanguage.SelectedIndex = 0;

            this.textBoxNamespace.Text = RootWindow.LastUsedNamespace;

            this.textBoxFolder.Text = Application.StartupPath + @"\Output";
        }


        private void buttonOK_Click(object sender, System.EventArgs e)
        {

            if (this.textBoxNamespace.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the namespace", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.path = this.textBoxFolder.Text;
            this.namespaceName = this.textBoxNamespace.Text.Trim();
            this.selectedLanguage = (Language) Enum.Parse(this.selectedLanguage.GetType(), this.comboBoxLanguage.SelectedItem.ToString());

            RootWindow.LastUsedNamespace = this.textBoxNamespace.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void buttonBrowseFolder_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = null;

            try
            {
                folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Please choose the folder where the generated files will be placed";
                folderBrowser.ShowNewFolderButton = true;
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxFolder.Text = folderBrowser.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
        }

        #region properties
        public Language SelectedLanguage
        {
            get
            {
                return this.selectedLanguage;
            }
        }

        public string SelectedNamespaceName
        {
            get
            {
                return this.namespaceName;
            }
        }

        public string SelectedPath
        {
            get
            {
                return this.path;
            }
        }
        #endregion
    }


}
