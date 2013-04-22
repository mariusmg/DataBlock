/*

      file : ColumnInfoDialog.cs
description: Dialog to map column info to entities
    author : Marius Gheorghe 


 
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace voidsoft.DataBlockModeler
{

    public class ColumnInfoDialog : Form
    {
        #region Windows Form Designer generated code

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chData;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;


        private DatabaseTable table;
        private List<TableRelation> listRelations = null;


        private TabControl tabControl;
        private TabPage tabPageSchema;
        private TabPage tabPageRelations;

        private string primaryKeyColumnName;


        private ListView listViewRelations;
        private Button buttonRemoveRelation;
        private Button buttonAddRelation;
        private ColumnHeader RelatedTable;
        private ColumnHeader columnHeader1;
        private Label label1;
        private TextBox textBoxEntityName;
        private ColumnHeader columnHeader2;


        public ColumnInfoDialog(DatabaseTable table)
        {
            InitializeComponent();
            this.table = table;


            this.listRelations = new List<TableRelation>();
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


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnInfoDialog));
            this.listView = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chData = new System.Windows.Forms.ColumnHeader();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSchema = new System.Windows.Forms.TabPage();
            this.tabPageRelations = new System.Windows.Forms.TabPage();
            this.buttonRemoveRelation = new System.Windows.Forms.Button();
            this.buttonAddRelation = new System.Windows.Forms.Button();
            this.listViewRelations = new System.Windows.Forms.ListView();
            this.RelatedTable = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEntityName = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPageSchema.SuspendLayout();
            this.tabPageRelations.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                                               {
                                                   this.chID,
                                                   this.chName,
                                                   this.chData
                                               });
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(432, 237);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // chID
            // 
            this.chID.Text = "Is Primary Key";
            this.chID.Width = 85;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 170;
            // 
            // chData
            // 
            this.chData.Text = ".NET Data type";
            this.chData.Width = 120;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOK.Location = new System.Drawing.Point(310, 288);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(70, 28);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Location = new System.Drawing.Point(386, 288);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(62, 28);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                             | System.Windows.Forms.AnchorStyles.Left)
                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageSchema);
            this.tabControl.Controls.Add(this.tabPageRelations);
            this.tabControl.Location = new System.Drawing.Point(8, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(440, 263);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageSchema
            // 
            this.tabPageSchema.Controls.Add(this.listView);
            this.tabPageSchema.Location = new System.Drawing.Point(4, 22);
            this.tabPageSchema.Name = "tabPageSchema";
            this.tabPageSchema.Size = new System.Drawing.Size(432, 237);
            this.tabPageSchema.TabIndex = 0;
            this.tabPageSchema.Text = "Table fields   ";
            this.tabPageSchema.UseVisualStyleBackColor = true;
            // 
            // tabPageRelations
            // 
            this.tabPageRelations.Controls.Add(this.buttonRemoveRelation);
            this.tabPageRelations.Controls.Add(this.buttonAddRelation);
            this.tabPageRelations.Controls.Add(this.listViewRelations);
            this.tabPageRelations.Location = new System.Drawing.Point(4, 22);
            this.tabPageRelations.Name = "tabPageRelations";
            this.tabPageRelations.Size = new System.Drawing.Size(432, 237);
            this.tabPageRelations.TabIndex = 1;
            this.tabPageRelations.Text = "Relations  ";
            this.tabPageRelations.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveRelation
            // 
            this.buttonRemoveRelation.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveRelation.Location = new System.Drawing.Point(330, 198);
            this.buttonRemoveRelation.Name = "buttonRemoveRelation";
            this.buttonRemoveRelation.Size = new System.Drawing.Size(86, 29);
            this.buttonRemoveRelation.TabIndex = 2;
            this.buttonRemoveRelation.Text = "&Remove";
            this.buttonRemoveRelation.Click += new System.EventHandler(this.buttonRemoveRelation_Click);
            // 
            // buttonAddRelation
            // 
            this.buttonAddRelation.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddRelation.Location = new System.Drawing.Point(15, 198);
            this.buttonAddRelation.Name = "buttonAddRelation";
            this.buttonAddRelation.Size = new System.Drawing.Size(86, 29);
            this.buttonAddRelation.TabIndex = 1;
            this.buttonAddRelation.Text = "&Add";
            this.buttonAddRelation.Click += new System.EventHandler(this.buttonAddRelation_Click);
            // 
            // listViewRelations
            // 
            this.listViewRelations.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                    | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRelations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                                                        {
                                                            this.RelatedTable,
                                                            this.columnHeader1,
                                                            this.columnHeader2
                                                        });
            this.listViewRelations.FullRowSelect = true;
            this.listViewRelations.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewRelations.Location = new System.Drawing.Point(3, 3);
            this.listViewRelations.MultiSelect = false;
            this.listViewRelations.Name = "listViewRelations";
            this.listViewRelations.Size = new System.Drawing.Size(426, 189);
            this.listViewRelations.TabIndex = 0;
            this.listViewRelations.UseCompatibleStateImageBehavior = false;
            this.listViewRelations.View = System.Windows.Forms.View.Details;
            // 
            // RelatedTable
            // 
            this.RelatedTable.Text = "RelatedTable";
            this.RelatedTable.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "RelationType";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Relation Cardinality";
            this.columnHeader2.Width = 120;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Entity name :";
            // 
            // textBoxEntityName
            // 
            this.textBoxEntityName.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxEntityName.Location = new System.Drawing.Point(86, 288);
            this.textBoxEntityName.Name = "textBoxEntityName";
            this.textBoxEntityName.Size = new System.Drawing.Size(154, 20);
            this.textBoxEntityName.TabIndex = 5;
            // 
            // ColumnInfoDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(457, 328);
            this.Controls.Add(this.textBoxEntityName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ColumnInfoDialog_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageSchema.ResumeLayout(false);
            this.tabPageRelations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        #region properties

        public DatabaseTable Table
        {
            get
            {
                return table;
            }
        }

        public DatabaseColumn[] Columns
        {
            get
            {
                return table.Columns;
            }
        }

        #endregion

        #region event handlers

        private void ColumnInfoDialog_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < table.Columns.Length; i++)
                {
                    if (table.Columns[i].isPrimaryKey)
                    {
                        primaryKeyColumnName = table.Columns[i].Name;
                    }

                    listView.Items.Add("");
                    listView.Items[i].Checked = table.Columns[i].isPrimaryKey;
                    listView.Items[listView.Items.Count - 1].SubItems.Add(table.Columns[i].Name);
                    listView.Items[listView.Items.Count - 1].SubItems.Add(table.Columns[i].columnDataType);
                    listView.Items[listView.Items.Count - 1].Checked = table.Columns[i].isPrimaryKey;
                }

                Text = "Table: " + table.TableName;

                textBoxEntityName.Text = table.EntityName;


                for (int i = 0; i < table.Relations.Count; i++)
                {
                    //add to list
                    listViewRelations.Items.Add(table.Relations[i].RelatedTableName);
                    listViewRelations.Items[listViewRelations.Items.Count - 1].SubItems.Add(
                      GetRelationTypeName(table.Relations[i]));
                    listViewRelations.Items[listViewRelations.Items.Count - 1].SubItems.Add(
                        table.Relations[i].RelationCardinality.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load table metadata. " + ex.Message, "DataBlock Modeler",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            //check if the user set a primary key

            bool hasPrimaryKey = false;

            for (int i = 0; i < table.Columns.Length; i++)
            {
                if (listView.Items[i].Checked == true)
                {
                    hasPrimaryKey = true;
                }

                table.Columns[i].isPrimaryKey = listView.Items[i].Checked;
            }


            if (textBoxEntityName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter the name of the entity", "DataBlock Modeler");
                return;
            }


            if (hasPrimaryKey == false)
            {
                MessageBox.Show("Please select a primary key for the table", "DataBlock Modeler", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


            table.EntityName = textBoxEntityName.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void buttonAddRelation_Click(object sender, EventArgs e)
        {
            bool hasPrimaryKey = false;
            //check for primary key
            for (int i = 0; i < table.Columns.Length; i++)
            {
                if (listView.Items[i].Checked == true)
                {
                    hasPrimaryKey = true;
                }

                table.Columns[i].isPrimaryKey = listView.Items[i].Checked;
            }

            if (hasPrimaryKey == false)
            {
                MessageBox.Show("Please select a primary key for the table", "DataBlock Modeler", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }






            //add relations
            TableRelationDialog dialog = null;

            try
            {
                dialog = new TableRelationDialog(table);
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    table.Relations.Add(dialog.Relation);

                    //add to list
                    listViewRelations.Items.Add(dialog.Relation.RelatedTableName);
                    if (dialog.Relation is ParentTableRelation)
                    {
                        listViewRelations.Items[listViewRelations.Items.Count - 1].SubItems.Add(
                            "Parent to Child");
                    }
                    else if (dialog.Relation is ChildTableRelation)
                    {
                        listViewRelations.Items[listViewRelations.Items.Count - 1].SubItems.Add(
                            "Child to Parent");
                    }
                    else
                    {
                        listViewRelations.Items[listViewRelations.Items.Count - 1].SubItems.Add("Many to Many");
                    }


                    listViewRelations.Items[listViewRelations.Items.Count - 1].SubItems.Add(
                        dialog.Relation.RelationCardinality.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dialog != null)
                {
                    dialog.Dispose();
                }
            }
        }



        private void buttonRemoveRelation_Click(object sender, EventArgs e)
        {
            if (listViewRelations.SelectedItems[0].Index > -1)
            {
                int index = listViewRelations.SelectedItems[0].Index;
                listViewRelations.Items.RemoveAt(index);

                List<TableRelation> rel = new List<TableRelation>(table.Relations);
                rel.RemoveAt(index);

            }
        }



        private string GetRelationTypeName(TableRelation relation)
        {
            if (relation is ParentTableRelation)
            {
                return "ParentChild";
            }
            else if (relation is ChildTableRelation)
            {
                return "ChildParent";
            }
            else if (relation is ManyToManyTableRelation)
            {
                return "ManyToMany";
            }

            return string.Empty;
        }

        #endregion
    }
}