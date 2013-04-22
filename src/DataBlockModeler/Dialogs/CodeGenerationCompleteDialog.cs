/*







*/


using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using DataBlockModeler;


namespace voidsoft.DataBlockModeler
{

	public class CodeGenerationCompleteDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelNamespace;
		private System.Windows.Forms.Label labelTableMetadata;
		private System.Windows.Forms.TextBox textBoxNamespace;
		private System.Windows.Forms.TextBox textBoxMappingFilename;
		private System.Windows.Forms.Button buttonBrowseMapping;
		private System.Windows.Forms.Button buttonCancel;


        private string namespaceName;
       
        private ComboBox comboBoxLanguage;
        private Label labelLanguage;

        private Label labelPersistentObject;
        private TextBox textBoxPersistentFileName;
        private Button buttonBrowsePersistent;


        private  Language selectedLanguage;
        private string mappingFileName;
        private ComboBox comboBoxTables;
        private Label label1;
        private PictureBox pictureBoxLogo;
        private Label label2;
        private string persistentFileName;
		private System.Windows.Forms.GroupBox groupBox;
        private PictureBox pictureBox;


        private string[] selectedTables;

        public CodeGenerationCompleteDialog()
		{
			this.InitializeComponent();
		}

        public CodeGenerationCompleteDialog(string[] selectedTables)
        {
            this.InitializeComponent();
            this.selectedTables = selectedTables;
        }


        private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGenerationCompleteDialog));
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.textBoxMappingFilename = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelNamespace = new System.Windows.Forms.Label();
            this.labelTableMetadata = new System.Windows.Forms.Label();
            this.buttonBrowseMapping = new System.Windows.Forms.Button();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.labelPersistentObject = new System.Windows.Forms.Label();
            this.textBoxPersistentFileName = new System.Windows.Forms.TextBox();
            this.buttonBrowsePersistent = new System.Windows.Forms.Button();
            this.comboBoxTables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxLogo)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOK.Location = new System.Drawing.Point(299, 247);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 32);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Location = new System.Drawing.Point(121, 19);
            this.textBoxNamespace.Multiline = true;
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(327, 20);
            this.textBoxNamespace.TabIndex = 1;
            // 
            // textBoxMappingFilename
            // 
            this.textBoxMappingFilename.Location = new System.Drawing.Point(120, 80);
            this.textBoxMappingFilename.Multiline = true;
            this.textBoxMappingFilename.Name = "textBoxMappingFilename";
            this.textBoxMappingFilename.ReadOnly = true;
            this.textBoxMappingFilename.Size = new System.Drawing.Size(299, 20);
            this.textBoxMappingFilename.TabIndex = 2;
            this.textBoxMappingFilename.Text = "Code generator";
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Location = new System.Drawing.Point(383, 247);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 32);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelNamespace
            // 
            this.labelNamespace.Location = new System.Drawing.Point(20, 19);
            this.labelNamespace.Name = "labelNamespace";
            this.labelNamespace.Size = new System.Drawing.Size(90, 16);
            this.labelNamespace.TabIndex = 5;
            this.labelNamespace.Text = "Namespace";
            this.labelNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTableMetadata
            // 
            this.labelTableMetadata.AutoSize = true;
            this.labelTableMetadata.Location = new System.Drawing.Point(50, 80);
            this.labelTableMetadata.Name = "labelTableMetadata";
            this.labelTableMetadata.Size = new System.Drawing.Size(64, 13);
            this.labelTableMetadata.TabIndex = 6;
            this.labelTableMetadata.Text = "Mapping file";
            this.labelTableMetadata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonBrowseMapping
            // 
            this.buttonBrowseMapping.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBrowseMapping.Location = new System.Drawing.Point(425, 80);
            this.buttonBrowseMapping.Name = "buttonBrowseMapping";
            this.buttonBrowseMapping.Size = new System.Drawing.Size(24, 20);
            this.buttonBrowseMapping.TabIndex = 7;
            this.buttonBrowseMapping.Text = "...";
            this.buttonBrowseMapping.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(121, 138);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(91, 21);
            this.comboBoxLanguage.TabIndex = 1;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(59, 141);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(55, 13);
            this.labelLanguage.TabIndex = 0;
            this.labelLanguage.Text = "Language";
            this.labelLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPersistentObject
            // 
            this.labelPersistentObject.AutoSize = true;
            this.labelPersistentObject.Location = new System.Drawing.Point(8, 106);
            this.labelPersistentObject.Name = "labelPersistentObject";
            this.labelPersistentObject.Size = new System.Drawing.Size(103, 13);
            this.labelPersistentObject.TabIndex = 9;
            this.labelPersistentObject.Text = "Persistent Object file";
            this.labelPersistentObject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxPersistentFileName
            // 
            this.textBoxPersistentFileName.Location = new System.Drawing.Point(120, 106);
            this.textBoxPersistentFileName.Multiline = true;
            this.textBoxPersistentFileName.Name = "textBoxPersistentFileName";
            this.textBoxPersistentFileName.ReadOnly = true;
            this.textBoxPersistentFileName.Size = new System.Drawing.Size(299, 20);
            this.textBoxPersistentFileName.TabIndex = 10;
            this.textBoxPersistentFileName.Text = "Code generator";
            // 
            // buttonBrowsePersistent
            // 
            this.buttonBrowsePersistent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBrowsePersistent.Location = new System.Drawing.Point(424, 106);
            this.buttonBrowsePersistent.Name = "buttonBrowsePersistent";
            this.buttonBrowsePersistent.Size = new System.Drawing.Size(24, 20);
            this.buttonBrowsePersistent.TabIndex = 11;
            this.buttonBrowsePersistent.Text = "...";
            this.buttonBrowsePersistent.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // comboBoxTables
            // 
            this.comboBoxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTables.FormattingEnabled = true;
            this.comboBoxTables.Location = new System.Drawing.Point(121, 45);
            this.comboBoxTables.Name = "comboBoxTables";
            this.comboBoxTables.Size = new System.Drawing.Size(327, 21);
            this.comboBoxTables.TabIndex = 12;
            this.comboBoxTables.SelectedIndexChanged += new System.EventHandler(this.comboBoxTables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Persistent Object";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.White;
            this.pictureBoxLogo.Location = new System.Drawing.Point(-2, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(468, 80);
            this.pictureBoxLogo.TabIndex = 14;
            this.pictureBoxLogo.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label2.Location = new System.Drawing.Point(296, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 23);
            this.label2.TabIndex = 15;
            this.label2.Text = "Code generator";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.textBoxMappingFilename);
            this.groupBox.Controls.Add(this.comboBoxTables);
            this.groupBox.Controls.Add(this.labelLanguage);
            this.groupBox.Controls.Add(this.comboBoxLanguage);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.buttonBrowsePersistent);
            this.groupBox.Controls.Add(this.labelTableMetadata);
            this.groupBox.Controls.Add(this.textBoxPersistentFileName);
            this.groupBox.Controls.Add(this.labelPersistentObject);
            this.groupBox.Controls.Add(this.textBoxNamespace);
            this.groupBox.Controls.Add(this.labelNamespace);
            this.groupBox.Controls.Add(this.buttonBrowseMapping);
            this.groupBox.Location = new System.Drawing.Point(-2, 77);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(468, 164);
            this.groupBox.TabIndex = 16;
            this.groupBox.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(7, 8);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(72, 63);
            this.pictureBox.TabIndex = 23;
            this.pictureBox.TabStop = false;
            // 
            // CodeGenerationCompleteDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(467, 287);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeGenerationCompleteDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Code generator";
            this.Load += new System.EventHandler(this.CodeGenerationDialog_Load);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxLogo)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}


        #region event handlers

        private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
		
			try
			{
				if(this.textBoxNamespace.Text.Trim() == "")
				{
					MessageBox.Show("Please insert the namespace", "DataBlockModeler", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}


				if(this.textBoxMappingFilename.Text.Trim() == "")
				{
					MessageBox.Show("Please choose a filename");
					return;
				}


				this.namespaceName = this.textBoxNamespace.Text.Trim();
				this.mappingFileName = this.textBoxMappingFilename.Text.Trim();
                this.persistentFileName = this.textBoxPersistentFileName.Text.Trim();

                MainWindow.LastUsedNamespace = this.textBoxNamespace.Text;

                this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch(Exception ex)
			{
			}
			finally
			{
			}
		}


        private void buttonBrowse_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog sfDialog = null;
		     
			try
			{
				sfDialog = new SaveFileDialog();
				sfDialog.RestoreDirectory = true;
                if (this.comboBoxLanguage.SelectedIndex == 0)   //csharp
                {
                    sfDialog.Filter = "C# class files|*.cs";
                }
                else
                {                                               //vbnet
                    sfDialog.Filter = "VB.NET class files|*.vb";
                }
                if(sfDialog.ShowDialog() == DialogResult.OK)
				{
					this.textBoxMappingFilename.Text = sfDialog.FileName;
				}
			}
			catch(Exception ex)
			{

			}
			finally
			{
				if(sfDialog != null)
				{
					sfDialog.Dispose();
				}
			}
        }
        #endregion


        #region properties
        public Language LanguageName
        {
            get
            {
				return (Language) Enum.Parse (this.selectedLanguage.GetType(), this.comboBoxLanguage.SelectedItem.ToString());
            }
        }

        public string NamespaceName
		{
			get
			{
				return this.namespaceName; 
			}
		}

		public string MappingFileName
		{
			get
			{
				return this.mappingFileName;
			}
		}

        public string PersistentFileName
        {
            get
            {
                return this.persistentFileName;
            }
        }

		public string PersistentObject
		{
			get
			{
				return this.comboBoxTables.SelectedItem.ToString();

			}
		}
        #endregion


        private void CodeGenerationDialog_Load(object sender, EventArgs e)
        {

            this.textBoxNamespace.Text = MainWindow.LastUsedNamespace;

			Language lng = new Language();
			string[] values = System.Enum.GetNames(lng.GetType());

			this.comboBoxLanguage.DataSource = values;
			this.comboBoxLanguage.SelectedIndex = 0;

			this.comboBoxTables.DataSource = this.selectedTables;

            string customName = this.comboBoxTables.SelectedItem.ToString();

            if (this.comboBoxTables.Items.Count == 1)
            {
                this.comboBoxTables.Enabled = false;
            }

            this.SetFileNames(customName);
        }


        private void SetFileNames(string customName)
        {
            int selectedIndex = this.comboBoxLanguage.SelectedIndex;

            this.textBoxNamespace.Text = "Extender" + Util.RemoveEmptySpaces(customName);

            this.textBoxMappingFilename.Text = customName + "TableMetadata.cs";
            this.textBoxPersistentFileName.Text = customName + "PersistentObject.cs";

            if (selectedIndex == -1)
            {
                this.comboBoxLanguage.SelectedIndex = 0;
            }
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.textBoxMappingFilename.Text.Length > 0)
            {
                string mappingFileName = this.textBoxMappingFilename.Text.Substring(0, this.textBoxMappingFilename.Text.LastIndexOf("."));
                string persistentFileName = this.textBoxPersistentFileName.Text.Substring(0, this.textBoxPersistentFileName.Text.LastIndexOf("."));

                if (this.comboBoxLanguage.SelectedIndex == 0) //csharp
                {
                    this.textBoxMappingFilename.Text = mappingFileName + ".cs";
                    this.textBoxPersistentFileName.Text = persistentFileName + ".cs";
                }
                else
                {                                            //vbnet
                    this.textBoxMappingFilename.Text = mappingFileName + ".vb";
                    this.textBoxPersistentFileName.Text = persistentFileName + ".vb";
                }
            }
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetFileNames(this.comboBoxTables.SelectedItem.ToString());
        }
	}
}
