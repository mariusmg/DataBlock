using System.ComponentModel;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
    partial class RootWindow
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RootWindow));
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
            this.statusBar.Location = new System.Drawing.Point(0, 477);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel,
            this.statusBarPanelSecond});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(713, 21);
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
            this.statusBarPanelSecond.Width = 686;
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
            this.listView.Size = new System.Drawing.Size(713, 449);
            this.listView.TabIndex = 10;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuContextTableProperties});
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
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenDatabase,
            this.toolStripButtonTableProperties,
            this.toolStripSeparator3,
            this.toolStripButtonSelectEverything,
            this.toolStripButtonGenerateMapping,
            this.toolStripSeparator4,
            this.toolStripButtonExit});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(713, 25);
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
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(713, 24);
            this.menuStrip.TabIndex = 13;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
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
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemConnect,
            this.toolStripSeparator1,
            this.menuItemTableProperties});
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
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGenerateMappingFiles,
            this.toolStripSeparator2,
            this.AspnetCodeGenerationToolStripMenuItem});
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.generateToolStripMenuItem.Text = "Generate";
            // 
            // menuItemGenerateMappingFiles
            // 
            this.menuItemGenerateMappingFiles.Name = "menuItemGenerateMappingFiles";
            this.menuItemGenerateMappingFiles.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.menuItemGenerateMappingFiles.Size = new System.Drawing.Size(322, 22);
            this.menuItemGenerateMappingFiles.Text = "Generate domain objects and data access";
            this.menuItemGenerateMappingFiles.Click += new System.EventHandler(this.menuItemGenerateMappingFiles_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(319, 6);
            // 
            // AspnetCodeGenerationToolStripMenuItem
            // 
            this.AspnetCodeGenerationToolStripMenuItem.Name = "AspnetCodeGenerationToolStripMenuItem";
            this.AspnetCodeGenerationToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.AspnetCodeGenerationToolStripMenuItem.Text = "Generate SilverChalice objects";
            this.AspnetCodeGenerationToolStripMenuItem.Click += new System.EventHandler(this.AspnetCodeGenerationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
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
            // RootWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(713, 498);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "RootWindow";
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
    }
}