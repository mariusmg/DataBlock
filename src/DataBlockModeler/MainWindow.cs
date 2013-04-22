using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using voidsoft.DataBlockModeler.ASPNETGenerators;

namespace voidsoft.DataBlockModeler
{
    public partial class MainWindow : Form
    {
        public static string LastUsedNamespace;
        public static StringCollection scTableNames;


        public MainWindow()
        {
            InitializeComponent();
            scTableNames = new StringCollection();
        }


        private ToolStripMenuItem AspnetCodeGenerationToolStripMenuItem;

        private ToolStripPanel BottomToolStripPanel;

        private IContainer components;

        private ToolStripContentPanel ContentPanel;

        private ContextMenu contextMenu;

        private ToolStripMenuItem databaseToolStripMenuItem;

        private ToolStripMenuItem fileToolStripMenuItem;

        private ToolStripMenuItem generateToolStripMenuItem;

        private ToolStripMenuItem helpToolStripMenuItem;

        private ImageList imageList;

        private ToolStripPanel LeftToolStripPanel;

        private ListView listView;

        private MenuItem menuContextTableProperties;

        private MenuItem menuFile;

        private MenuItem menuItem1;

        private MenuItem menuItem2;

        private MenuItem menuItem3;

        private MenuItem menuItem4;

        private ToolStripMenuItem menuItemAbout;

        private ToolStripMenuItem menuItemConnect;

        private ToolStripMenuItem menuItemExit;

        private ToolStripMenuItem menuItemGenerateMappingFiles;

        private ToolStripMenuItem menuItemTableProperties;

        private MenuStrip menuStrip;

        private ToolStripPanel RightToolStripPanel;

        private StatusBar statusBar;

        private StatusBarPanel statusBarPanel;

        private StatusBarPanel statusBarPanelSecond;

        private ToolStrip toolStrip;

        private ToolStripButton toolStripButtonExit;

        private ToolStripButton toolStripButtonGenerateMapping;

        private ToolStripButton toolStripButtonOpenDatabase;

        private ToolStripButton toolStripButtonSelectEverything;

        private ToolStripButton toolStripButtonTableProperties;

        private ToolStripSeparator toolStripSeparator1;

        private ToolStripSeparator toolStripSeparator2;

        private ToolStripSeparator toolStripSeparator3;

        private ToolStripSeparator toolStripSeparator4;

        private ToolStripPanel TopToolStripPanel;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof (MainWindow));
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanelSecond = new System.Windows.Forms.StatusBarPanel();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuFile = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.listView = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuContextTableProperties = new System.Windows.Forms.MenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenDatabase = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTableProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSelectEverything = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGenerateMapping = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTableProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGenerateMappingFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AspnetCodeGenerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            ((System.ComponentModel.ISupportInitialize) (this.statusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.statusBarPanelSecond)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 421);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {this.statusBarPanel, this.statusBarPanelSecond});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(624, 21);
            this.statusBar.TabIndex = 3;
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Width = 10;
            // 
            // statusBarPanelSecond
            // 
            this.statusBarPanelSecond.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanelSecond.Name = "statusBarPanelSecond";
            this.statusBarPanelSecond.Width = 689;
            // 
            // menuItem1
            // 
            this.menuItem1.Index = -1;
            this.menuItem1.Text = "&File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = -1;
            this.menuItem2.Text = "&View";
            // 
            // menuFile
            // 
            this.menuFile.Index = -1;
            this.menuFile.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuFile.Text = "&Database";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.Text = "-";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "&Help";
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(0, 49);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(624, 393);
            this.listView.TabIndex = 10;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.menuContextTableProperties});
            // 
            // menuContextTableProperties
            // 
            this.menuContextTableProperties.Index = 0;
            this.menuContextTableProperties.Text = "TableProperties";
            this.menuContextTableProperties.Click += new System.EventHandler(this.menuTableProperties_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripButtonOpenDatabase, this.toolStripButtonTableProperties, this.toolStripSeparator3, this.toolStripButtonSelectEverything, this.toolStripButtonGenerateMapping, this.toolStripSeparator4, this.toolStripButtonExit});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(624, 25);
            this.toolStrip.TabIndex = 12;
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // toolStripButtonOpenDatabase
            // 
            this.toolStripButtonOpenDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenDatabase.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonOpenDatabase.Image")));
            this.toolStripButtonOpenDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenDatabase.Name = "toolStripButtonOpenDatabase";
            this.toolStripButtonOpenDatabase.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpenDatabase.Text = "Open database";
            // 
            // toolStripButtonTableProperties
            // 
            this.toolStripButtonTableProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTableProperties.Enabled = false;
            this.toolStripButtonTableProperties.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonTableProperties.Image")));
            this.toolStripButtonTableProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTableProperties.Name = "toolStripButtonTableProperties";
            this.toolStripButtonTableProperties.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTableProperties.Text = "Entity properties";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSelectEverything
            // 
            this.toolStripButtonSelectEverything.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectEverything.Enabled = false;
            this.toolStripButtonSelectEverything.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonSelectEverything.Image")));
            this.toolStripButtonSelectEverything.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectEverything.Name = "toolStripButtonSelectEverything";
            this.toolStripButtonSelectEverything.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSelectEverything.Text = "Select everything";
            this.toolStripButtonSelectEverything.ToolTipText = "Selects all the entities";
            // 
            // toolStripButtonGenerateMapping
            // 
            this.toolStripButtonGenerateMapping.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGenerateMapping.Enabled = false;
            this.toolStripButtonGenerateMapping.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonGenerateMapping.Image")));
            this.toolStripButtonGenerateMapping.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGenerateMapping.Name = "toolStripButtonGenerateMapping";
            this.toolStripButtonGenerateMapping.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGenerateMapping.Text = "Generate mapping file(s)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExit.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonExit.Image")));
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExit.Text = "toolStripButton2";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.fileToolStripMenuItem, this.databaseToolStripMenuItem, this.generateToolStripMenuItem, this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(624, 24);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemExit.Size = new System.Drawing.Size(143, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuItemConnect, this.toolStripSeparator1, this.menuItemTableProperties});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.databaseToolStripMenuItem.Text = "&Database";
            // 
            // menuItemConnect
            // 
            this.menuItemConnect.Name = "menuItemConnect";
            this.menuItemConnect.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.menuItemConnect.Size = new System.Drawing.Size(194, 22);
            this.menuItemConnect.Text = "Connect";
            this.menuItemConnect.Click += new System.EventHandler(this.menuConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // menuItemTableProperties
            // 
            this.menuItemTableProperties.Name = "menuItemTableProperties";
            this.menuItemTableProperties.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.menuItemTableProperties.Size = new System.Drawing.Size(194, 22);
            this.menuItemTableProperties.Text = "TableProperties";
            this.menuItemTableProperties.Click += new System.EventHandler(this.menuTableProperties_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuItemGenerateMappingFiles, this.toolStripSeparator2, this.AspnetCodeGenerationToolStripMenuItem});
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.generateToolStripMenuItem.Text = "Generate";
            // 
            // menuItemGenerateMappingFiles
            // 
            this.menuItemGenerateMappingFiles.Name = "menuItemGenerateMappingFiles";
            this.menuItemGenerateMappingFiles.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.menuItemGenerateMappingFiles.Size = new System.Drawing.Size(231, 22);
            this.menuItemGenerateMappingFiles.Text = "Generate mapping files";
            this.menuItemGenerateMappingFiles.Click += new System.EventHandler(this.menuItemGenerateMappingFiles_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // AspnetCodeGenerationToolStripMenuItem
            // 
            this.AspnetCodeGenerationToolStripMenuItem.Name = "AspnetCodeGenerationToolStripMenuItem";
            this.AspnetCodeGenerationToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.AspnetCodeGenerationToolStripMenuItem.Text = "ASP.NET code generation";
            this.AspnetCodeGenerationToolStripMenuItem.Click += new System.EventHandler(this.AspnetCodeGenerationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.menuItemAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(114, 22);
            this.menuItemAbout.Text = "About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(200, 100);
            // 
            // MainWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataBlock Modeler";
            ((System.ComponentModel.ISupportInitialize) (this.statusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.statusBarPanelSecond)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

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

        private void AspnetCodeGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspnetCodeGenerationDialog dialog = null;


            try
            {
                //generate mappings
                menuItemGenerateMappingFiles_Click(null, null);


                //is something selected
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
                    string entityName = string.Empty;
                    string tableName = string.Empty;

                    ArrayList alColumns = new ArrayList();
                    ArrayList alTableRelations = new ArrayList();
                    TableRelation[] relations = new TableRelation[0];


                    int current = -1;


                    //get the entity names
                    for (int i = 0; i < listView.Items.Count; i++)
                    {
                        ++current;

                        if (listView.Items[i].Checked)
                        {
                            entityName = Utilies.RemoveEmptySpaces(Context.databaseTables[i].EntityName);
                            tableName = Context.databaseTables[i].TableName;
                            relations = Context.databaseTables[i].Relations;

                            break;
                        }
                    }

                    string currentFilePath = string.Empty;

                    AspnetCodeGenerator c = new AspnetCodeGenerator();
                    c.Generate(Context.databaseTables[current].Columns, dialog.NamespaceName, entityName, dialog.GenerateContentPlaceholder, Application.StartupPath + @"\Output", relations);

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

        #region database schema loader

        /// <summary>
        /// Loads the database schema
        /// </summary>
        private void StartLoading()
        {
            //clear stuffs
            statusBar.Panels[1].Text = "";
            statusBar.Panels[0].Text = "Loading database schema ";

            //clear the previous 
            listView.Items.Clear();
            Context.databaseTables = null;

            Thread thStart = new Thread(new ThreadStart(OnStartLoading));
            thStart.Priority = ThreadPriority.Highest;
            thStart.Start();
        }


        private void OnStartLoading()
        {
            try
            {
                //load the table names
                scTableNames = Schema.GetTableList(Context.currentDatabaseServer, Context.currentConnectionString);

                // init tables
                Context.databaseTables = new DatabaseTable[scTableNames.Count];

                for (int i = 0; i < scTableNames.Count; i++)
                {
                    try
                    {
                        //load the column info for each table
                        ColumnInfo[] columns = Schema.GetColumnInfo(Context.currentDatabaseServer, scTableNames[i], Context.currentConnectionString);
                        string tableName = Utilies.RemoveEmptySpaces(scTableNames[i]);
                        Context.databaseTables[i] = new DatabaseTable(tableName, columns, new TableRelation[0], tableName);
                    }
                    catch
                    {
                        //remove the table from the list
                        scTableNames.Remove(scTableNames[i]);
                        continue;
                    }
                }

                //invoke the delegate on the UI thread
                FinishedLoading fd = new FinishedLoading(OnFinishedLoading);
                Invoke(fd, new object[1] {string.Empty});
            }
            catch (Exception ex)
            {
                FinishedLoading fd = new FinishedLoading(OnFinishedLoading);
                Invoke(fd, new object[1] {ex.Message});
            }
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
                //load the info into files
                for (int i = 0; i < Context.databaseTables.Length; i++)
                {
                    listView.Items.Add(Context.databaseTables[i].TableName);
                }

                menuItemGenerateMappingFiles.Enabled = true;
                menuItemTableProperties.Enabled = true;

                toolStrip.Items[1].Enabled = true;
                toolStrip.Items[3].Enabled = true;
                toolStrip.Items[4].Enabled = true;
                toolStrip.Items[5].Enabled = true;
                toolStrip.Items[6].Enabled = true;


                statusBar.Panels[0].Text = "ServerType : " + Context.currentDatabaseServer.ToString();
                statusBar.Panels[1].Text = "Connection : " + Context.currentConnectionString;
            }
        }

        #endregion

        #region code generation loader

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="mappingFileName"></param>
        /// <param name="persistentFileName"></param>
        /// <param name="persistentObject"></param>
        /// <param name="languageName"></param>
        private void GenerateComplete(string namespaceName, string mappingFileName, string persistentFileName, string persistentObject, Language selectedLanguage)
        {
            ITableMetadataGenerator generator = null;
            IDataAccessObjectGenerator persistanceGenerator = null;


            try
            {
                if (selectedLanguage == Language.CSharp)
                {
                    generator = new CSharpTableMetadataGenerator();
                    persistanceGenerator = new CSharpDataAccessObjectGenerator();
                }
                else if (selectedLanguage == Language.VbNet)
                {
                    generator = new VBTableMetadataGenerator();
                    persistanceGenerator = new VBDataAccessObjectGenerator();
                }


                string[] entityNames = new string[listView.CheckedItems.Count];
                string[] tableNames = new string[listView.CheckedItems.Count];

                ArrayList alColumns = new ArrayList();
                ArrayList alTableRelations = new ArrayList();

                int current = -1;

                for (int i = 0; i < listView.Items.Count; i++)
                {
                    if (listView.Items[i].Checked)
                    {
                        ++current;
                        entityNames[current] = Context.databaseTables[i].EntityName;
                        tableNames[current] = Context.databaseTables[i].TableName;

                        alColumns.Add(Context.databaseTables[i].Columns);
                        alTableRelations.Add(Context.databaseTables[i].Relations);
                    }
                }

                generator.GenerateTableMetadatata(entityNames, tableNames, alColumns, alTableRelations, mappingFileName, namespaceName);
                persistanceGenerator.GeneratePersistentObject(persistentObject, namespaceName, persistentFileName);
                MessageBox.Show("File sucessfully generated", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                //uncheck items
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    listView.Items[i].Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured \n" + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="path"></param>
        /// <param name="selectedLanguage"></param>
        private void GenerateMappings(string namespaceName, string path, Language selectedLanguage)
        {
            ITableMetadataGenerator generator = null;

            try
            {
                //initialize the generator based on the language

                if (selectedLanguage == Language.CSharp)
                {
                    generator = new CSharpTableMetadataGenerator();
                }
                else if (selectedLanguage == Language.VbNet)
                {
                    generator = new VBTableMetadataGenerator();
                }


                //check path
                if (!path.EndsWith(@"\"))
                {
                    path += @"\";
                }


                string[] entityNames = new string[listView.CheckedItems.Count];
                string[] tableNames = new string[listView.CheckedItems.Count];

                ArrayList alColumns = new ArrayList();
                ArrayList alTableRelations = new ArrayList();

                int current = -1;


                //get the entity names
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    if (listView.Items[i].Checked)
                    {
                        ++current;
                        entityNames[current] = Utilies.RemoveEmptySpaces(Context.databaseTables[i].EntityName);
                        tableNames[current] = Context.databaseTables[i].TableName;
                    }
                }


                string currentFilePath = string.Empty;

                for (int i = 0; i < entityNames.Length; i++)
                {
                    alColumns.Clear();
                    alTableRelations.Clear();

                    foreach (DatabaseTable var in Context.databaseTables)
                    {
                        if (var.EntityName == entityNames[i])
                        {
                            alColumns.Add(var.Columns);
                            alTableRelations.Add(var.Relations);
                            break;
                        }
                    }


                    currentFilePath = path + entityNames[i];

                    generator.GenerateTableMetadatata(new string[1] {entityNames[i]}, new string[1] {tableNames[i]}, alColumns, alTableRelations, currentFilePath, namespaceName);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="path"></param>
        /// <param name="selectedLanguage"></param>
        private void GeneratePersistent(string namespaceName, string path, Language selectedLanguage)
        {
            IDataAccessObjectGenerator generator = null;

            try
            {
                //initialize the generator based on the language

                if (selectedLanguage == Language.CSharp)
                {
                    generator = new CSharpDataAccessObjectGenerator();
                }
                else if (selectedLanguage == Language.VbNet)
                {
                    generator = new VBDataAccessObjectGenerator();
                }


                if (!path.EndsWith(@"\"))
                {
                    path += @"\";
                }

                string[] tableNames = new string[listView.CheckedItems.Count];
                ArrayList alColumns = new ArrayList();
                ArrayList alTableRelations = new ArrayList();

                int current = -1;

                for (int i = 0; i < listView.Items.Count; i++)
                {
                    if (listView.Items[i].Checked)
                    {
                        ++current;
                        tableNames[current] = Utilies.RemoveEmptySpaces(Context.databaseTables[i].EntityName);
                    }
                }


                string currentFilePath = string.Empty;

                for (int i = 0; i < tableNames.Length; i++)
                {
                    alColumns.Clear();
                    alTableRelations.Clear();

                    alColumns.Add(Context.databaseTables[i].Columns);
                    alTableRelations.Add(Context.databaseTables[i].Relations);

                    currentFilePath = path + tableNames[i] + "DataAccess";


                    generator.GeneratePersistentObject(tableNames[i], namespaceName, currentFilePath);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region event handlers

        private void menuConnect_Click(object sender, EventArgs e)
        {
            ConnectionDialog cdialog = null;

            try
            {
                cdialog = new ConnectionDialog();

                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    Context.currentConnectionString = cdialog.SelectedConnection;
                    Context.currentDatabaseServer = cdialog.SelectedDatabaseType;

                    StartLoading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cdialog == null)
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

                    cdialog = new ColumnInfoDialog(Context.databaseTables[index]);

                    if (cdialog.ShowDialog() == DialogResult.OK)
                    {
                        Context.databaseTables[index] = cdialog.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            CodeGeneratorMappingsDialog dialog1 = null;
            try
            {
                if (listView.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please choose the table from which you want to generate the mappings", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (CheckForPrimaryKeys())
                {
                    dialog1 = new CodeGeneratorMappingsDialog();
                    if (dialog1.ShowDialog() == DialogResult.OK)
                    {
                        GenerateMappings(dialog1.SelectedNamespaceName, dialog1.SelectedPath, dialog1.SelectedLanguage);
                        GeneratePersistent(dialog1.SelectedNamespaceName, dialog1.SelectedPath, dialog1.SelectedLanguage);
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
                if (dialog1 != null)
                {
                    dialog1.Dispose();
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

                foreach (DatabaseTable var in Context.databaseTables)
                {
                    if (tableName == var.TableName)
                    {
                        bool hasPK = false;

                        foreach (ColumnInfo c in var.Columns)
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

        #endregion

        #region Nested type: FinishedLoading

        private delegate void FinishedLoading(string errorMessage);

        #endregion
    }
}