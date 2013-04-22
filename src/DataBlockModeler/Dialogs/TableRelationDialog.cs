using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
    public partial class TableRelationDialog : Form
    {
        private DatabaseTable table;

        private bool loadedTableList = false;


        public TableRelationDialog(DatabaseTable table)
        {
            InitializeComponent();
            this.table = table;
        }


        private TableRelation relation = null;

        #region

        public TableRelation Relation
        {
            get
            {
                return relation;
            }
        }

        #endregion

        private void TableRelationDialog_Load(object sender, EventArgs e)
        {
            groupBoxManyToMany.Location = new Point(6, 55);
            groupBoxChildToParent.Location = new Point(6, 55);


            comboBoxRelationType.SelectedIndex = 0;
            comboBoxParentChildRelationCardinality.SelectedIndex = 0;
            comboBoxChildParentCardinality.SelectedIndex = 0;


            LoadRelatedTableList();
            LoadForeignKeys();

            Text = "Relations for entity: " + table.TableName;
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            //do some checks
            try
            {
                switch (comboBoxRelationType.SelectedIndex)
                {
                    case 0:

                        if (CheckParentChildRelation())
                        {
                            //create the relation
                            ParentTableRelation relation = new ParentTableRelation();

                            this.relation = relation;

                            relation.CascadeDeleteChildren = checkBoxEnableCascadeDelete.Checked;

                            if (comboBoxParentChildRelationCardinality.SelectedIndex == 0)
                            {
                                relation.RelationCardinality = RelationCardinality.OneToOne;
                            }
                            else
                            {
                                relation.RelationCardinality = RelationCardinality.OneToMany;
                            }

                            relation.RelatedTableName = comboBoxParentChildRelatedTable.SelectedItem.ToString();
                            relation.ForeignKeyName = comboBoxParentChildFields.SelectedItem.ToString();


                            DialogResult = DialogResult.OK;

                            this.relation = (TableRelation) relation;

                            Close();
                        }
                        break;


                    case 1:
                        if (CheckChildParentRelation())
                        {
                            ChildTableRelation relation = new ChildTableRelation();

                            relation.RelationCardinality = RelationCardinality.OneToOne;


                            relation.RelatedTableName = comboBoxChildParentRelatedTable.SelectedItem.ToString();

                            relation.RelatedTableKeyName = comboBoxChildParentPrimaryKeyFields.SelectedItem.ToString();
                            relation.ForeignKeyName = comboBoxChildParentForeignKeyFields.SelectedItem.ToString();


                            DialogResult = DialogResult.OK;

                            this.relation = (TableRelation) relation;
                            Close();
                        }
                        break;


                    case 2:
                        if (CheckManyToManyRelation())
                        {
                            ManyToManyTableRelation relation = new ManyToManyTableRelation();

                            relation.RelationCardinality = RelationCardinality.ManyToMany;

                            relation.RelatedTableName = comboBoxManyToManyRelatedTable.SelectedItem.ToString();

                            relation.IntermediaryTableName = comboBoxManyToManyIntermediaryTable.SelectedItem.ToString();

                            relation.IntermediaryKeyFieldFromChildTable = comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.SelectedItem.ToString();
                            relation.IntermediaryKeyFieldFromParentTable = comboBoxManyToManyIntermediaryForeignKeyOurEntity.SelectedItem.ToString();

                            this.relation = (TableRelation) relation;

                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBoxParentChildRelatedTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fill the columns for the selected table
            comboBoxParentChildFields.Items.Clear();

            string selectedTable = comboBoxParentChildRelatedTable.SelectedItem.ToString();


            for (int i = 0; i < GeneratorContext.CurrentDatabaseTables.Count; i++)
            {
                if (GeneratorContext.CurrentDatabaseTables[i].TableName == selectedTable)
                {
                    foreach (DatabaseColumn ci in GeneratorContext.CurrentDatabaseTables[i].Columns)
                    {
                        comboBoxParentChildFields.Items.Add(ci.Name);
                    }


                    comboBoxParentChildFields.SelectedIndex = 0;
                    break;
                }
            }
        }


        private void comboBoxRelationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxRelationType.SelectedIndex)
            {
                case 0:
                    groupBoxParentToChild.Visible = true;
                    groupBoxManyToMany.Visible = false;
                    groupBoxChildToParent.Visible = false;
                    groupBoxParentToChild.BringToFront();
                    Refresh();
                    break;

                case 1:
                    groupBoxParentToChild.Visible = false;
                    groupBoxManyToMany.Visible = false;
                    groupBoxChildToParent.Visible = true;
                    groupBoxChildToParent.BringToFront();
                    Refresh();
                    break;

                case 2:
                    groupBoxParentToChild.Visible = false;
                    groupBoxManyToMany.Visible = true;
                    groupBoxChildToParent.Visible = false;
                    groupBoxManyToMany.BringToFront();
                    Refresh();
                    break;


                default:
                    break;
            }
        }

        #region internal implementation

        /// <summary>
        /// 
        /// </summary>
        private void LoadRelatedTableList()
        {
            List<string> listSources = null;

            try
            {
                listSources = new List<string>();

                foreach (DatabaseTable var in GeneratorContext.CurrentDatabaseTables)
                {
                    if (var.TableName != table.TableName)
                    {
                        //check if the table is already in a relation
                        // bool isInRelation = false;

                        //  foreach (TableRelation relation in table.Relations)
                        //{
                        //    if (relation.RelatedTableName == var.TableName)
                        //    {
                        //        isInRelation = true;
                        //        break;
                        //    }
                        //}

                        //if (isInRelation == false)
                        //{
                        listSources.Add(var.TableName);
                        //                        }
                    }
                }

                //parent -> child
                comboBoxParentChildRelatedTable.DataSource = listSources;

                //child -> parent
                comboBoxChildParentRelatedTable.DataSource = listSources;


                foreach (string var in listSources)
                {
                    //many to many
                    comboBoxManyToManyRelatedTable.Items.Add(var); //  = listSources;
                    comboBoxManyToManyIntermediaryTable.Items.Add(var); // = listSources;
                }

                loadedTableList = true;

                comboBoxManyToManyRelatedTable.SelectedIndex = 0;
                comboBoxManyToManyIntermediaryTable.SelectedIndex = 1;
            }
            catch
            {
                throw;
            }
        }


        private void LoadForeignKeys()
        {
            comboBoxChildParentForeignKeyFields.Items.Clear();

            for (int i = 0; i < table.Columns.Length; i++)
            {
                comboBoxChildParentForeignKeyFields.Items.Add(table.Columns[i].Name);
            }

            comboBoxChildParentForeignKeyFields.SelectedIndex = 0;
        }


        /// <summary>
        /// 
        /// </summary>
        private bool CheckParentChildRelation()
        {
            try
            {
                //check for related entity
                if (comboBoxParentChildRelatedTable.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the related entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                //please check the type of the related fields.

                //get the PK field type.
                string pkFieldType = string.Empty;

                for (int i = 0; i < table.Columns.Length; i++)
                {
                    if (table.Columns[i].isPrimaryKey)
                    {
                        pkFieldType = table.Columns[i].columnDataType;
                    }
                }


                string relatedEntity = comboBoxParentChildRelatedTable.SelectedItem.ToString();

                string relatedForeignKey = comboBoxParentChildFields.SelectedItem.ToString();

                string selectedForeignKeyDataType = string.Empty;

                for (int i = 0; i < GeneratorContext.CurrentDatabaseTables.Count; i++)
                {
                    if (GeneratorContext.CurrentDatabaseTables[i].TableName == relatedEntity)
                    {
                        foreach (DatabaseColumn ci in GeneratorContext.CurrentDatabaseTables[i].Columns)
                        {
                            if (ci.Name == relatedForeignKey)
                            {
                                selectedForeignKeyDataType = ci.columnDataType;
                            }
                        }
                    }
                }


                //check the data types

                if (pkFieldType != selectedForeignKeyDataType)
                {
                    MessageBox.Show("Data type mismatch. The data type of the primary key entity is different from the data type of the related foreign key entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //check if we already have relations with this table
                foreach (TableRelation var in table.Relations)
                {
                    if (var.RelatedTableName == relatedEntity)
                    {
                        MessageBox.Show("There is already a relation defined with entity " + relatedEntity, "DataBlockModeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }


                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Chekc the Child -> PArent relation
        /// </summary>
        /// <returns></returns>
        private bool CheckChildParentRelation()
        {
            try
            {
                //check for related entity
                if (comboBoxChildParentRelatedTable.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the related entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }


                //check for related entity
                if (comboBoxChildParentPrimaryKeyFields.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the primary key field of the parent entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                //check for related entity
                if (comboBoxChildParentForeignKeyFields.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the foreign key field", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                //compare data types for the primary and foreign key
                string relatedEntity = comboBoxChildParentRelatedTable.SelectedItem.ToString();

                string relatedPrimaryKey = comboBoxChildParentPrimaryKeyFields.SelectedItem.ToString();

                string relatedForeignKey = comboBoxParentChildFields.SelectedItem.ToString();


                string fkType = string.Empty;
                string pkType = string.Empty;

                foreach (DatabaseColumn var in table.Columns)
                {
                    if (var.Name == relatedForeignKey)
                    {
                        fkType = var.columnDataType;
                        break;
                    }
                }

                foreach (DatabaseTable tb in GeneratorContext.CurrentDatabaseTables)
                {
                    if (tb.TableName == relatedEntity)
                    {
                        foreach (DatabaseColumn ci in tb.Columns)
                        {
                            if (ci.Name == relatedPrimaryKey)
                            {
                                pkType = ci.columnDataType;
                                break;
                            }
                        }

                        break;
                    }
                }


                //check the data types
                if (fkType != pkType)
                {
                    MessageBox.Show("Data type mismatch. The data type of the primary key entity is different from the data type of the related foreign key entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                //check if we already have relations with this table
                foreach (TableRelation var in table.Relations)
                {
                    if (var.RelatedTableName == relatedEntity)
                    {
                        MessageBox.Show("There is already a relation defined with entity " + relatedEntity, "DataBlockModeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        /// <summary>
        /// Check the many to many relation
        /// </summary>
        /// <returns></returns>
        private bool CheckManyToManyRelation()
        {
            try
            {
                #region checks

                //check for related entity
                if (comboBoxManyToManyRelatedTable.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the related entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                //check for related entity
                if (comboBoxManyToManyIntermediaryTable.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the intermediary entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                //check for related FK
                if (comboBoxManyToManyRelatedKey.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the foreign key field from the related table", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                //check for related FK
                if (comboBoxManyToManyIntermediaryForeignKeyOurEntity.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the intermediary table foreign key related to our entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                //check for related FK
                if (comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.SelectedItem.ToString() == string.Empty)
                {
                    MessageBox.Show("Please choose the intermediary table foreign key to the related entity", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                //check if the related table and the intermediary table are not the same
                string relatedTable = comboBoxManyToManyRelatedTable.SelectedItem.ToString();
                string intermediaryTable = comboBoxManyToManyIntermediaryTable.SelectedItem.ToString();


                if (relatedTable == intermediaryTable)
                {
                    MessageBox.Show("The related table cannot be the same as the intermediary table", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                #endregion

                string intermediaryTableFKOurTable = comboBoxManyToManyIntermediaryForeignKeyOurEntity.SelectedItem.ToString();
                string intermediaryTableFKOurTableType = string.Empty;

                string intermediaryTableFKRelatedTable = comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.SelectedItem.ToString();
                string intermediaryTableFKRelatedTableType = string.Empty;

                string relatedTableFK = comboBoxManyToManyRelatedKey.SelectedItem.ToString();
                string relatedTableFKType = string.Empty;


                string pkType = string.Empty;

                foreach (DatabaseColumn ci in table.Columns)
                {
                    if (ci.isPrimaryKey)
                    {
                        pkType = ci.columnDataType;
                    }
                }


                foreach (DatabaseTable var in GeneratorContext.CurrentDatabaseTables)
                {
                    if (var.TableName == intermediaryTable)
                    {
                        foreach (DatabaseColumn ci in var.Columns)
                        {
                            if (ci.Name == intermediaryTableFKOurTable)
                            {
                                intermediaryTableFKOurTableType = ci.columnDataType;
                            }
                            else if (ci.Name == intermediaryTableFKRelatedTable)
                            {
                                intermediaryTableFKRelatedTableType = ci.columnDataType;
                            }
                        }
                    }
                    else if (var.TableName == relatedTable)
                    {
                        foreach (DatabaseColumn ci in var.Columns)
                        {
                            if (ci.Name == relatedTableFK)
                            {
                                relatedTableFKType = ci.columnDataType;
                                break;
                            }
                        }
                    }
                }


                //check the type now
                if (intermediaryTableFKOurTableType != pkType)
                {
                    MessageBox.Show("Invalid data type. The type of our entity primary key is not the same as the intermediary table foreign key", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                if (intermediaryTableFKRelatedTableType != relatedTableFKType)
                {
                    MessageBox.Show("Invalid data type. The type of our related entity primary key is not the same as the intermediary table foreign key", "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "DataBlock Modeler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        private void comboBoxChildParentRelatedTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fill the columns for the selected table
            comboBoxChildParentPrimaryKeyFields.Items.Clear();

            string selectedTable = comboBoxChildParentRelatedTable.SelectedItem.ToString();


            for (int i = 0; i < GeneratorContext.CurrentDatabaseTables.Count; i++)
            {
                if (GeneratorContext.CurrentDatabaseTables[i].TableName == selectedTable)
                {
                    foreach (DatabaseColumn ci in GeneratorContext.CurrentDatabaseTables[i].Columns)
                    {
                        comboBoxChildParentPrimaryKeyFields.Items.Add(ci.Name);
                    }


                    comboBoxChildParentPrimaryKeyFields.SelectedIndex = 0;
                    break;
                }
            }
        }

        private void comboBoxManyToManyRelatedTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string entityName = comboBoxManyToManyRelatedTable.SelectedItem.ToString();
                comboBoxManyToManyRelatedKey.Items.Clear();

                foreach (DatabaseTable var in GeneratorContext.CurrentDatabaseTables)
                {
                    if (var.TableName == entityName)
                    {
                        foreach (DatabaseColumn ci in var.Columns)
                        {
                            comboBoxManyToManyRelatedKey.Items.Add(ci.Name);
                        }

                        break;
                    }
                }

                comboBoxManyToManyRelatedKey.SelectedIndex = 0;
            }
            catch
            {
                throw;
            }
        }

        private void comboBoxManyToManyIntermediaryTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadedTableList == false)
            {
                return;
            }

            string selectedEntity = comboBoxManyToManyIntermediaryTable.SelectedItem.ToString();

            comboBoxManyToManyIntermediaryForeignKeyOurEntity.Items.Clear();
            comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.Items.Clear();


            foreach (DatabaseTable var in GeneratorContext.CurrentDatabaseTables)
            {
                if (var.TableName == selectedEntity)
                {
                    foreach (DatabaseColumn ci in var.Columns)
                    {
                        comboBoxManyToManyIntermediaryForeignKeyOurEntity.Items.Add(ci.Name);
                        comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.Items.Add(ci.Name);
                    }

                    break;
                }
            }

            comboBoxManyToManyIntermediaryForeignKeyOurEntity.SelectedIndex = 0;
            comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.SelectedIndex = 0;
        }

        private void comboBoxParentChildRelationCardinality_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}