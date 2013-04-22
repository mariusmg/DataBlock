/*







*/


using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using DataBlockModeler;


namespace voidsoft.DataBlockModeler
{

	public class CodeGenerationDialog : System.Windows.Forms.Form
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


        private string languageName;
        private string mappingFileName;
        private ComboBox comboBoxTables;
        private Label label1;
        private PictureBox pictureBoxLogo;
        private Label label2;
        private string persistentFileName;


        private string[] selectedTables;

        public CodeGenerationDialog()
		{
			this.InitializeComponent();
		}

        public CodeGenerationDialog(string[] selectedTables)
        {
            this.InitializeComponent();
            this.selectedTables = selectedTables;
        }


        private void InitializeComponent()
		{
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
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOK.Location = new System.Drawing.Point(306, 216);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(72, 32);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "&OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textBoxNamespace
			// 
			this.textBoxNamespace.AutoSize = false;
			this.textBoxNamespace.Location = new System.Drawing.Point(121, 76);
			this.textBoxNamespace.Multiline = true;
			this.textBoxNamespace.Name = "textBoxNamespace";
			this.textBoxNamespace.Size = new System.Drawing.Size(338, 20);
			this.textBoxNamespace.TabIndex = 1;
			this.textBoxNamespace.Text = "";
			// 
			// textBoxMappingFilename
			// 
			this.textBoxMappingFilename.AutoSize = false;
			this.textBoxMappingFilename.Location = new System.Drawing.Point(121, 130);
			this.textBoxMappingFilename.Multiline = true;
			this.textBoxMappingFilename.Name = "textBoxMappingFilename";
			this.textBoxMappingFilename.ReadOnly = true;
			this.textBoxMappingFilename.Size = new System.Drawing.Size(307, 20);
			this.textBoxMappingFilename.TabIndex = 2;
			this.textBoxMappingFilename.Text = "Code generator";
			// 
			// buttonCancel
			// 
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonCancel.Location = new System.Drawing.Point(387, 216);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(72, 32);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// labelNamespace
			// 
			this.labelNamespace.Location = new System.Drawing.Point(20, 76);
			this.labelNamespace.Name = "labelNamespace";
			this.labelNamespace.Size = new System.Drawing.Size(90, 16);
			this.labelNamespace.TabIndex = 5;
			this.labelNamespace.Text = "Namespace";
			this.labelNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelTableMetadata
			// 
			this.labelTableMetadata.Location = new System.Drawing.Point(27, 130);
			this.labelTableMetadata.Name = "labelTableMetadata";
			this.labelTableMetadata.Size = new System.Drawing.Size(83, 20);
			this.labelTableMetadata.TabIndex = 6;
			this.labelTableMetadata.Text = "Mapping file";
			this.labelTableMetadata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonBrowseMapping
			// 
			this.buttonBrowseMapping.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonBrowseMapping.Location = new System.Drawing.Point(435, 130);
			this.buttonBrowseMapping.Name = "buttonBrowseMapping";
			this.buttonBrowseMapping.Size = new System.Drawing.Size(24, 20);
			this.buttonBrowseMapping.TabIndex = 7;
			this.buttonBrowseMapping.Text = "...";
			this.buttonBrowseMapping.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// comboBoxLanguage
			// 
			this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLanguage.Items.AddRange(new object[] {
																  "C#",
																  "VB.NET"});
			this.comboBoxLanguage.Location = new System.Drawing.Point(121, 190);
			this.comboBoxLanguage.Name = "comboBoxLanguage";
			this.comboBoxLanguage.Size = new System.Drawing.Size(91, 21);
			this.comboBoxLanguage.TabIndex = 1;
			this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
			// 
			// labelLanguage
			// 
			this.labelLanguage.AutoSize = true;
			this.labelLanguage.Location = new System.Drawing.Point(55, 190);
			this.labelLanguage.Name = "labelLanguage";
			this.labelLanguage.Size = new System.Drawing.Size(55, 16);
			this.labelLanguage.TabIndex = 0;
			this.labelLanguage.Text = "Language";
			this.labelLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelPersistentObject
			// 
			this.labelPersistentObject.AutoSize = true;
			this.labelPersistentObject.Location = new System.Drawing.Point(8, 166);
			this.labelPersistentObject.Name = "labelPersistentObject";
			this.labelPersistentObject.Size = new System.Drawing.Size(108, 16);
			this.labelPersistentObject.TabIndex = 9;
			this.labelPersistentObject.Text = "Persistent Object file";
			this.labelPersistentObject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxPersistentFileName
			// 
			this.textBoxPersistentFileName.AutoSize = false;
			this.textBoxPersistentFileName.Location = new System.Drawing.Point(121, 163);
			this.textBoxPersistentFileName.Multiline = true;
			this.textBoxPersistentFileName.Name = "textBoxPersistentFileName";
			this.textBoxPersistentFileName.ReadOnly = true;
			this.textBoxPersistentFileName.Size = new System.Drawing.Size(307, 20);
			this.textBoxPersistentFileName.TabIndex = 10;
			this.textBoxPersistentFileName.Text = "Code generator";
			// 
			// buttonBrowsePersistent
			// 
			this.buttonBrowsePersistent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonBrowsePersistent.Location = new System.Drawing.Point(435, 163);
			this.buttonBrowsePersistent.Name = "buttonBrowsePersistent";
			this.buttonBrowsePersistent.Size = new System.Drawing.Size(24, 20);
			this.buttonBrowsePersistent.TabIndex = 11;
			this.buttonBrowsePersistent.Text = "...";
			this.buttonBrowsePersistent.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// comboBoxTables
			// 
			this.comboBoxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTables.Location = new System.Drawing.Point(121, 103);
			this.comboBoxTables.Name = "comboBoxTables";
			this.comboBoxTables.Size = new System.Drawing.Size(338, 21);
			this.comboBoxTables.TabIndex = 12;
			this.comboBoxTables.SelectedIndexChanged += new System.EventHandler(this.comboBoxTables_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 103);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Persistent Object";
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.BackColor = System.Drawing.Color.White;
			this.pictureBoxLogo.Location = new System.Drawing.Point(-2, 0);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(471, 63);
			this.pictureBoxLogo.TabIndex = 14;
			this.pictureBoxLogo.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.White;
			this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(296, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(159, 27);
			this.label2.TabIndex = 15;
			this.label2.Text = "Code generator";
			// 
			// CodeGenerationDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(467, 260);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxTables);
			this.Controls.Add(this.labelLanguage);
			this.Controls.Add(this.comboBoxLanguage);
			this.Controls.Add(this.buttonBrowsePersistent);
			this.Controls.Add(this.textBoxPersistentFileName);
			this.Controls.Add(this.labelPersistentObject);
			this.Controls.Add(this.buttonBrowseMapping);
			this.Controls.Add(this.labelTableMetadata);
			this.Controls.Add(this.labelNamespace);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.textBoxMappingFilename);
			this.Controls.Add(this.textBoxNamespace);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CodeGenerationDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Code generator";
			this.Load += new System.EventHandler(this.CodeGenerationDialog_Load);
			this.ResumeLayout(false);

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
        public string LanguageName
        {
            get
            {
                if (this.comboBoxLanguage.SelectedIndex == 0)
                {
                    return "csharp";
                }
                else
                {
                    return "vbnet";
                }
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

            this.comboBoxTables.DataSource = this.selectedTables;
            this.comboBoxTables.SelectedIndex = 0;

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
