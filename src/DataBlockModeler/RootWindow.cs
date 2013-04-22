using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using voidsoft.DataBlockModeler.Dialogs;

namespace voidsoft.DataBlockModeler
{
    public partial class RootWindow : Form
    {
        private RootWindowPresenter presenter = null;

        public static string LastUsedNamespace;


        public RootWindow()
        {
            InitializeComponent();


            presenter = new RootWindowPresenter(this);
        }

        #region event handlers

        private void AspnetCodeGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspnetCodeGenerationDialog dialog = null;

            try
            {
                //check for a single entity
                if (listView.CheckedItems.Count != 1)
                {
                    MessageBox.Show("Please choose a single entity to generate ASP.NET code", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (CheckForPrimaryKeys() == false)
                {
                    return;
                }

                dialog = new AspnetCodeGenerationDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    List<string> listTables = new List<string>();
                    List<string> listEntities = new List<string>();

                    GetSelectedEntities(ref listTables, ref listEntities);

                    presenter.GenerateSilverChaliceObjects(dialog.NamespaceName, string.Empty, dialog.GenerateContentPlaceholder, Language.CSharp, listTables, listEntities);

                    MessageBox.Show("Files were successfully generated.", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while generating the files " + "\n" + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dialog != null)
                {
                    dialog.Dispose();
                }
            }
        }


        private void menuConnect_Click(object sender, EventArgs e)
        {
            ConnectionDialog cdialog = null;

            try
            {
                cdialog = new ConnectionDialog();

                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    GeneratorContext.currentConnectionString = cdialog.SelectedConnection;
                    GeneratorContext.currentDatabaseServer = cdialog.SelectedDatabaseType;


                    statusBarPanel.Text = "Loading schema...........";


                    FinishedLoading loading = OnFinishedLoading;
                    presenter.LoadDatabaseSchema(loading);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cdialog != null)
                {
                    cdialog.Dispose();
                }
            }
        }


        private void menuAbout_Click(object sender, EventArgs e)
        {
            AboutDialog dialog = null;

            try
            {
                dialog = new AboutDialog();
                dialog.ShowDialog();
            }
            finally
            {
                if (dialog != null)
                {
                    dialog.Dispose();
                }
            }
        }


        private void menuTableProperties_Click(object sender, EventArgs e)
        {
            ColumnInfoDialog cdialog = null;

            try
            {
                if (listView.SelectedItems.Count > 0)
                {
                    int index = listView.SelectedItems[0].Index;

                    cdialog = new ColumnInfoDialog(GeneratorContext.CurrentDatabaseTables[index]);

                    if (cdialog.ShowDialog() == DialogResult.OK)
                    {
                        GeneratorContext.CurrentDatabaseTables[index] = cdialog.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. " + ex.Message);
            }
            finally
            {
                if (cdialog != null)
                {
                    cdialog.Dispose();
                }
            }
        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView.SelectedItems.Count > 0)
                {
                    contextMenu.Show(listView, new Point(e.X, e.Y));
                }
            }
        }


        private void menuContextTableProperties_Click(object sender, EventArgs e)
        {
            menuTableProperties_Click(null, null);
        }


        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void menuItemGenerateMappingFiles_Click(object sender, EventArgs e)
        {
            CodeGeneratorMappingsDialog dialog = null;
            try
            {
                if (listView.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please choose the table from which you want to generate the mappings", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (CheckForPrimaryKeys())
                {
                    dialog = new CodeGeneratorMappingsDialog();


                    List<string> listTables = new List<string>();
                    List<string> listEntities = new List<string>();

                    GetSelectedEntities(ref listTables, ref listEntities);


                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        presenter.GenerateDomainObjects(dialog.SelectedNamespaceName, dialog.SelectedPath, dialog.SelectedLanguage, listTables, listEntities);
                        presenter.GenerateDataAccessEntities(dialog.SelectedNamespaceName, dialog.SelectedPath, dialog.SelectedLanguage, listTables, listEntities);
                        presenter.GenerateBusinessObjects(dialog.SelectedNamespaceName + ".BusinessObjects", dialog.SelectedPath, dialog.SelectedLanguage, listTables, listEntities);

                        MessageBox.Show("Files were successfully generated.", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("Error occurred while generating the files \n" + exception1.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            finally
            {
                if (dialog != null)
                {
                    dialog.Dispose();
                }
            }
        }


        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int index = toolStrip.Items.IndexOf(e.ClickedItem);

            switch (index)
            {
                //open database connection
                case 0:
                    menuConnect_Click(null, null);
                    break;

                //show table properties
                case 1:
                    menuTableProperties_Click(null, null);
                    break;

                //select all items
                case 3:

                    if (toolStripButtonSelectEverything.Checked)
                    {
                        toolStripButtonSelectEverything.Checked = false;
                    }
                    else
                    {
                        toolStripButtonSelectEverything.Checked = true;
                    }

                    if (toolStripButtonSelectEverything.Checked)
                    {
                        if (listView.Items.Count > 0)
                        {
                            for (int i = 0; i < listView.Items.Count; i++)
                            {
                                listView.Items[i].Checked = true;
                            }
                        }
                    }
                    else
                    {
                        if (listView.Items.Count > 0)
                        {
                            for (int i = 0; i < listView.Items.Count; i++)
                            {
                                listView.Items[i].Checked = false;
                            }
                        }
                    }
                    break;

                case 4:
                    menuItemGenerateMappingFiles_Click(null, null);
                    break;

                //help
                case 7:
                    break;

                case 8:
                    Application.Exit();
                    break;
            }
        }

        #endregion

        #region internal implementation

        /// <summary>
        /// Checks if the selected tables have PK defined. This is called before generation
        /// </summary>
        /// <returns>Returns true if everything is OK</returns>
        private bool CheckForPrimaryKeys()
        {
            //check if the generated items have primery keys defined
            for (int i = 0; i < listView.CheckedItems.Count; i++)
            {
                string tableName = listView.CheckedItems[i].Text;

                foreach (DatabaseTable var in GeneratorContext.CurrentDatabaseTables)
                {
                    if (tableName == var.TableName)
                    {
                        bool hasPK = false;

                        foreach (DatabaseColumn c in var.Columns)
                        {
                            if (c.isPrimaryKey)
                            {
                                hasPK = true;
                                break;
                            }
                        }

                        if (hasPK == false)
                        {
                            MessageBox.Show("The entity " + tableName + " does not have defined a primary key", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        private void OnFinishedLoading(string errorMessage)
        {
            //check for error messages
            if (errorMessage != string.Empty)
            {
                //an error occurred
                MessageBox.Show("Failed to connect to database. " + errorMessage, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusBar.Panels[0].Text = "";
                statusBar.Panels[1].Text = "";
                return;
            }
            else
            {
                listView.Items.Clear();

                //load the info into files
                for (int i = 0; i < GeneratorContext.CurrentDatabaseTables.Count; i++)
                {
                    listView.Items.Add(GeneratorContext.CurrentDatabaseTables[i].TableName);
                }

                menuItemGenerateMappingFiles.Enabled = true;
                menuItemTableProperties.Enabled = true;

                toolStrip.Items[1].Enabled = true;
                toolStrip.Items[3].Enabled = true;
                toolStrip.Items[4].Enabled = true;
                toolStrip.Items[5].Enabled = true;
                toolStrip.Items[6].Enabled = true;

                SetUIConnectionDetails();


                // this.listView.Enabled = false;

                LoadTableRelations();
            }
        }

        private void SetUIConnectionDetails()
        {
            statusBar.Panels[0].Text = "ServerType : " + GeneratorContext.currentDatabaseServer.ToString();
            statusBar.Panels[1].Text = "Connection : " + GeneratorContext.currentConnectionString;
        }


        private void LoadTableRelations()
        {
            ChooseRelationsDialog dialog = null;

            try
            {
                dialog = new ChooseRelationsDialog();

                dialog.ShowDialog();

                if (dialog.SelectedImportType == TableRelationImportType.File)
                {
                    FinishedLoading finishedLoading = OnFinishedTableRelationsImportFromFile;
                    presenter.LoadTableRelationsFromFile(dialog.SelectedFilePath, finishedLoading);
                }
                else if (dialog.SelectedImportType == TableRelationImportType.Database)
                {
                    //TODO: add database implementation
                    return;
                }
                else if (dialog.SelectedImportType == TableRelationImportType.UserAdded)
                {
                    return;
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


        private void OnFinishedTableRelationsImportFromFile(string message)
        {
            //  listView.Enabled = true;

            if (message.StartsWith("error="))
            {
                string[] parts = message.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                MessageBox.Show("Error. " + parts[2]);
            }
            else
            {
                //imported relations  
                MessageBox.Show(message + " relations imported", "DataBlock Modeler");
            }


            SetUIConnectionDetails();
        }


        private void GetSelectedEntities(ref List<string> listTables, ref List<string> listEntities)
        {
            //get the entity names
            for (int i = 0; i < listView.Items.Count; i++)
            {
                if (listView.Items[i].Checked)
                {
                    listEntities.Add(Utilies.RemoveEmptySpaces(GeneratorContext.CurrentDatabaseTables[i].EntityName));
                    listTables.Add(GeneratorContext.CurrentDatabaseTables[i].TableName);
                }
            }
        }

        #endregion
    }
}