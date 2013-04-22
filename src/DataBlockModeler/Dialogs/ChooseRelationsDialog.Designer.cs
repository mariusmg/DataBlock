namespace voidsoft.DataBlockModeler.Dialogs
{
    partial class ChooseRelationsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.radioButtonAddUser = new System.Windows.Forms.RadioButton();
            this.radioButtonImportFile = new System.Windows.Forms.RadioButton();
            this.radioButtonLoadFromDatabase = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.buttonBrowse);
            this.groupBox.Controls.Add(this.textBoxFilePath);
            this.groupBox.Controls.Add(this.radioButtonAddUser);
            this.groupBox.Controls.Add(this.radioButtonImportFile);
            this.groupBox.Controls.Add(this.radioButtonLoadFromDatabase);
            this.groupBox.Location = new System.Drawing.Point(3, 2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(440, 136);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Enabled = false;
            this.buttonBrowse.Location = new System.Drawing.Point(395, 68);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(31, 20);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "....";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Enabled = false;
            this.textBoxFilePath.Location = new System.Drawing.Point(9, 68);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(380, 20);
            this.textBoxFilePath.TabIndex = 3;
            // 
            // radioButtonAddUser
            // 
            this.radioButtonAddUser.AutoSize = true;
            this.radioButtonAddUser.Location = new System.Drawing.Point(9, 104);
            this.radioButtonAddUser.Name = "radioButtonAddUser";
            this.radioButtonAddUser.Size = new System.Drawing.Size(124, 17);
            this.radioButtonAddUser.TabIndex = 2;
            this.radioButtonAddUser.TabStop = true;
            this.radioButtonAddUser.Text = "I will add them myself";
            this.radioButtonAddUser.UseVisualStyleBackColor = true;
            // 
            // radioButtonImportFile
            // 
            this.radioButtonImportFile.AutoSize = true;
            this.radioButtonImportFile.Location = new System.Drawing.Point(9, 45);
            this.radioButtonImportFile.Name = "radioButtonImportFile";
            this.radioButtonImportFile.Size = new System.Drawing.Size(93, 17);
            this.radioButtonImportFile.TabIndex = 1;
            this.radioButtonImportFile.TabStop = true;
            this.radioButtonImportFile.Text = "Import from file";
            this.radioButtonImportFile.UseVisualStyleBackColor = true;
            this.radioButtonImportFile.CheckedChanged += new System.EventHandler(this.radioButtonImportFile_CheckedChanged);
            // 
            // radioButtonLoadFromDatabase
            // 
            this.radioButtonLoadFromDatabase.AutoSize = true;
            this.radioButtonLoadFromDatabase.Enabled = false;
            this.radioButtonLoadFromDatabase.Location = new System.Drawing.Point(9, 12);
            this.radioButtonLoadFromDatabase.Name = "radioButtonLoadFromDatabase";
            this.radioButtonLoadFromDatabase.Size = new System.Drawing.Size(232, 17);
            this.radioButtonLoadFromDatabase.TabIndex = 0;
            this.radioButtonLoadFromDatabase.TabStop = true;
            this.radioButtonLoadFromDatabase.Text = "Attempt to load them from database schema";
            this.radioButtonLoadFromDatabase.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(355, 159);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 30);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ChooseRelationsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 201);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChooseRelationsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose table relations import";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton radioButtonAddUser;
        private System.Windows.Forms.RadioButton radioButtonImportFile;
        private System.Windows.Forms.RadioButton radioButtonLoadFromDatabase;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonBrowse;
    }
}