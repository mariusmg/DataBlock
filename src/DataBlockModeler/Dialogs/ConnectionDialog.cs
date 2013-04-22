using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
    public class ConnectionDialog : Form
    {
        private ListBox listBoxConnections;

        private Button buttonOK;

        private Button buttonCancel;

        private Label labelTitle;

        private static  StringCollection scDatabaseType = new StringCollection();

        private static StringCollection scValue = new StringCollection();
        
        private string selectedConnection;

        private Panel panel;

        private Button buttonNew;

        private Button buttonRemove;

        private DatabaseServer selectedDatabaseType;

        private Label labelServerType;


        private bool isDirty; //flag to know if the config list is modified


        public ConnectionDialog()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            listBoxConnections = new ListBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            buttonNew = new Button();
            labelTitle = new Label();
            buttonRemove = new Button();
            panel = new Panel();
            labelServerType = new Label();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxConnections
            // 
            listBoxConnections.Location = new Point(8, 64);
            listBoxConnections.Name = "listBoxConnections";
            listBoxConnections.Size = new Size(408, 264);
            listBoxConnections.TabIndex = 0;
            listBoxConnections.DoubleClick += new EventHandler(listBoxConnections_DoubleClick);
            listBoxConnections.SelectedIndexChanged += new EventHandler(listBoxConnections_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            buttonOK.FlatStyle = FlatStyle.System;
            buttonOK.Location = new Point(248, 336);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(80, 32);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "&OK";
            buttonOK.Click += new EventHandler(buttonOK_Click);
            // 
            // buttonCancel
            // 
            buttonCancel.FlatStyle = FlatStyle.System;
            buttonCancel.Location = new Point(336, 336);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(80, 32);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "&Cancel";
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
            // 
            // buttonNew
            // 
            buttonNew.FlatStyle = FlatStyle.System;
            buttonNew.Location = new Point(8, 336);
            buttonNew.Name = "buttonNew";
            buttonNew.Size = new Size(72, 32);
            buttonNew.TabIndex = 3;
            buttonNew.Text = "&New";
            buttonNew.Click += new EventHandler(buttonNew_Click);
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((Byte) (0)));
            labelTitle.Location = new Point(8, 13);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(202, 24);
            labelTitle.TabIndex = 4;
            labelTitle.Text = "Database connections :";
            labelTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonRemove
            // 
            buttonRemove.FlatStyle = FlatStyle.System;
            buttonRemove.Location = new Point(88, 336);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(72, 32);
            buttonRemove.TabIndex = 6;
            buttonRemove.Text = "&Remove";
            buttonRemove.Click += new EventHandler(buttonRemove_Click);
            // 
            // panel
            // 
            panel.BackColor = Color.White;
            panel.Controls.Add(labelServerType);
            panel.Controls.Add(labelTitle);
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(424, 56);
            panel.TabIndex = 7;
            // 
            // labelServerType
            // 
            labelServerType.AutoSize = true;
            labelServerType.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((Byte) (0)));
            labelServerType.Location = new Point(314, 16);
            labelServerType.Name = "labelServerType";
            labelServerType.Size = new Size(14, 22);
            labelServerType.TabIndex = 5;
            labelServerType.Text = "x";
            // 
            // ConnectionDialog
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(420, 374);
            Controls.Add(panel);
            Controls.Add(buttonRemove);
            Controls.Add(buttonNew);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(listBoxConnections);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ConnectionDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Closing += new CancelEventHandler(ConnectionDialog_Closing);
            Load += new EventHandler(DatabaseDialog_Load);
            panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #region event handlers

        private void DatabaseDialog_Load(object sender, EventArgs e)
        {
            try
            {
                if (scDatabaseType.Count == 0)
                {
                    LoadConnectionStrings(scDatabaseType, scValue);
                }

                for (int i = 0; i < scValue.Count; i++)
                {
                    listBoxConnections.Items.Add(scValue[i]);
                }


                //select the first connection by default.
                if (listBoxConnections.Items.Count > 0)
                {
                    listBoxConnections.SelectedIndex = 0;
                }
            }
            catch
            {
                MessageBox.Show("Failed to load the connection strings", "DataBlock Modeler");
            }
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxConnections.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a database connection", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                int index = listBoxConnections.SelectedIndex;


                selectedConnection = scValue[index];

                if (scDatabaseType[index].ToLower() == "access")
                {
                    selectedDatabaseType = DatabaseServer.Access;
                }
                else if (scDatabaseType[index].ToLower() == "sqlserver")
                {
                    selectedDatabaseType = DatabaseServer.SqlServer;
                }
                else if (scDatabaseType[index].ToLower() == "mysql")
                {
                    selectedDatabaseType = DatabaseServer.MySql;
                }
                else if (scDatabaseType[index].ToLower() == "postgresql")
                {
                    selectedDatabaseType = DatabaseServer.PostgreSQL;
                }

                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void listBoxConnections_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listBoxConnections.SelectedItems.Count > 0)
                {
                    int index = listBoxConnections.SelectedIndex;


                    selectedConnection = scValue[index];

                    if (scDatabaseType[index].ToLower() == "access")
                    {
                        selectedDatabaseType = DatabaseServer.Access;
                    }
                    else if (scDatabaseType[index].ToLower() == "sqlserver")
                    {
                        selectedDatabaseType = DatabaseServer.SqlServer;
                    }
                    else if (scDatabaseType[index].ToLower() == "mysql")
                    {
                        selectedDatabaseType = DatabaseServer.MySql;
                    }
                    else if (scDatabaseType[index].ToLower() == "postgresql")
                    {
                        selectedDatabaseType = DatabaseServer.PostgreSQL;
                    }

                    DialogResult = DialogResult.OK;

                    Close();
                }
            }
            catch
            {

            }
        }


        private void listBoxConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = listBoxConnections.SelectedIndex;

                selectedConnection = scValue[index];

                if (scDatabaseType[index].ToLower() == "access")
                {
                    labelServerType.Text = "Access";
                }
                else if (scDatabaseType[index].ToLower() == "sqlserver")
                {
                    labelServerType.Text = "Sql Server";
                }
                else if (scDatabaseType[index].ToLower() == "mysql")
                {
                    labelServerType.Text = "MySql";
                }
                else if (scDatabaseType[index].ToLower() == "postgresql")
                {
                    labelServerType.Text = "PostgreSql";
                }
            }
            catch
            {
                //ignore mismatch exception
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxConnections.SelectedItems.Count > 0)
            {
                int index = listBoxConnections.SelectedIndex;
                scValue.RemoveAt(index);
                scDatabaseType.RemoveAt(index);
                listBoxConnections.Items.RemoveAt(listBoxConnections.SelectedIndex);
                isDirty = true;
            }

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            DatabaseDialog dbDialog = null;

            try
            {
                dbDialog = new DatabaseDialog();

                if (dbDialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxConnections.Items.Add(dbDialog.ConnectionString);
                    scValue.Add(dbDialog.ConnectionString);
                    scDatabaseType.Add(dbDialog.SelectedDatabaseServer.ToString());

                    isDirty = true;
                }
            }
            finally
            {

                if (dbDialog != null)
                {
                    dbDialog.Dispose();
                }
            }
        }

        #region	Connection Strings

        public string SelectedConnection
        {
            get
            {
                return selectedConnection;
            }
        }

        public DatabaseServer SelectedDatabaseType
        {
            get
            {
                return selectedDatabaseType;
            }
        }


        private void SaveConnectionStrings()
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                fs = new FileStream("connection.config", FileMode.Truncate | FileMode.CreateNew, FileAccess.Write);
                sw = new StreamWriter(fs);

                for (int i = 0; i < scValue.Count; i++)
                {
                    sw.WriteLine(scDatabaseType[i] + "/" + scValue[i]);
                }

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save database connection list. " + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }


        private void LoadConnectionStrings(StringCollection scDatabaseType, StringCollection scConnectionStrings)
        {
            FileStream fs = null;
            StreamReader sread = null;

            try
            {
                if (!File.Exists("connection.config"))
                {
                    return;
                }

                fs = new FileStream("connection.config", FileMode.Open, FileAccess.Read);
                sread = new StreamReader(fs);

                string line = sread.ReadLine();

                int index = -1;

                while (line != null)
                {
                    index = line.IndexOf("/");

                    if (index == -1)
                    {
                        continue;
                    }

                    scDatabaseType.Add(line.Substring(0, index));
                    scConnectionStrings.Add(line.Substring(index + 1));

                    line = sread.ReadLine();
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion

        private void ConnectionDialog_Closing(object sender, CancelEventArgs e)
        {
            if (isDirty)
            {
                SaveConnectionStrings();
            }
        }
    }
}