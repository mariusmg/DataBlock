

using System;
using System.Windows.Forms;
using System.Collections;           
using voidsoft.DataBlockModeler; 







namespace voidsoft.DataBlockModeler
{

	public class DatabaseDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ComboBox comboBoxDatabaseServer;
		private System.Windows.Forms.GroupBox groupBoxSqlServer;
		private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBoxDatabaseServer;
        private TextBox textBoxConnectionString;
        private Label labelConnectionString;

        private string connectionString;
       

        private void InitializeComponent()
		{
            this.groupBoxSqlServer = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxDatabaseServer = new System.Windows.Forms.GroupBox();
            this.comboBoxDatabaseServer = new System.Windows.Forms.ComboBox();
            this.textBoxConnectionString = new System.Windows.Forms.TextBox();
            this.labelConnectionString = new System.Windows.Forms.Label();
            this.groupBoxSqlServer.SuspendLayout();
            this.groupBoxDatabaseServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSqlServer
            // 
            this.groupBoxSqlServer.Controls.Add(this.textBox1);
            this.groupBoxSqlServer.Location = new System.Drawing.Point(13, 325);
            this.groupBoxSqlServer.Name = "groupBoxSqlServer";
            this.groupBoxSqlServer.Size = new System.Drawing.Size(448, 42);
            this.groupBoxSqlServer.TabIndex = 0;
            this.groupBoxSqlServer.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "textBox1";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(306, 128);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 32);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(384, 128);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 32);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxDatabaseServer
            // 
            this.groupBoxDatabaseServer.Controls.Add(this.comboBoxDatabaseServer);
            this.groupBoxDatabaseServer.Location = new System.Drawing.Point(8, 8);
            this.groupBoxDatabaseServer.Name = "groupBoxDatabaseServer";
            this.groupBoxDatabaseServer.Size = new System.Drawing.Size(448, 56);
            this.groupBoxDatabaseServer.TabIndex = 3;
            this.groupBoxDatabaseServer.TabStop = false;
            this.groupBoxDatabaseServer.Text = "Database server";
            // 
            // comboBoxDatabaseServer
            // 
            this.comboBoxDatabaseServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabaseServer.FormattingEnabled = true;
            this.comboBoxDatabaseServer.Location = new System.Drawing.Point(7, 24);
            this.comboBoxDatabaseServer.Name = "comboBoxDatabaseServer";
            this.comboBoxDatabaseServer.Size = new System.Drawing.Size(177, 21);
            this.comboBoxDatabaseServer.TabIndex = 0;
            // 
            // textBoxConnectionString
            // 
            this.textBoxConnectionString.Location = new System.Drawing.Point(15, 92);
            this.textBoxConnectionString.Name = "textBoxConnectionString";
            this.textBoxConnectionString.Size = new System.Drawing.Size(439, 20);
            this.textBoxConnectionString.TabIndex = 4;
            // 
            // labelConnectionString
            // 
            this.labelConnectionString.AutoSize = true;
            this.labelConnectionString.Location = new System.Drawing.Point(8, 71);
            this.labelConnectionString.Name = "labelConnectionString";
            this.labelConnectionString.Size = new System.Drawing.Size(91, 13);
            this.labelConnectionString.TabIndex = 5;
            this.labelConnectionString.Text = "Connection String";
            // 
            // DatabaseDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(466, 172);
            this.Controls.Add(this.labelConnectionString);
            this.Controls.Add(this.textBoxConnectionString);
            this.Controls.Add(this.groupBoxDatabaseServer);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxSqlServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DatabaseDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DatabaseDialog_Load);
            this.groupBoxSqlServer.ResumeLayout(false);
            this.groupBoxSqlServer.PerformLayout();
            this.groupBoxDatabaseServer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        public DatabaseDialog()
        {
            this.InitializeComponent();
        }

        #region properties
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public DatabaseServer SelectedDatabaseServer
        {
            get
            {
                return (DatabaseServer)Enum.Parse(typeof(DatabaseServer), this.comboBoxDatabaseServer.Text);
            }   
        }

        #endregion


        #region event handlers
        private void DatabaseDialog_Load(object sender, System.EventArgs e)
		{

           string[] names =  Enum.GetNames(typeof(DatabaseServer));
           this.comboBoxDatabaseServer.DataSource = names;

       }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxConnectionString.Text == string.Empty)
            {
                MessageBox.Show("Please enter the connection string", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.connectionString = this.textBoxConnectionString.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion


    
    }
}