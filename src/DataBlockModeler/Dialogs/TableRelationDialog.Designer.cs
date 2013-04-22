namespace voidsoft.DataBlockModeler
{
    partial class TableRelationDialog
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
            this.comboBoxRelationType = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxParentToChild = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableCascadeDelete = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxParentChildFields = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxParentChildRelatedTable = new System.Windows.Forms.ComboBox();
            this.labelRelationCardinality = new System.Windows.Forms.Label();
            this.comboBoxParentChildRelationCardinality = new System.Windows.Forms.ComboBox();
            this.groupBoxChildToParent = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxChildParentForeignKeyFields = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxChildParentPrimaryKeyFields = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxChildParentRelatedTable = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxChildParentCardinality = new System.Windows.Forms.ComboBox();
            this.groupBoxManyToMany = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity = new System.Windows.Forms.ComboBox();
            this.comboBoxManyToManyRelatedKey = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxManyToManyIntermediaryTable = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxManyToManyRelatedTable = new System.Windows.Forms.ComboBox();
            this.groupBox.SuspendLayout();
            this.groupBoxParentToChild.SuspendLayout();
            this.groupBoxChildToParent.SuspendLayout();
            this.groupBoxManyToMany.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.comboBoxRelationType);
            this.groupBox.Location = new System.Drawing.Point(6, 6);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(391, 42);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Relation type";
            // 
            // comboBoxRelationType
            // 
            this.comboBoxRelationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRelationType.FormattingEnabled = true;
            this.comboBoxRelationType.Items.AddRange(new object[] {
            "Parent to Child",
            "Child to Parent",
            "Many To Many"});
            this.comboBoxRelationType.Location = new System.Drawing.Point(7, 15);
            this.comboBoxRelationType.Name = "comboBoxRelationType";
            this.comboBoxRelationType.Size = new System.Drawing.Size(378, 21);
            this.comboBoxRelationType.TabIndex = 0;
            this.comboBoxRelationType.SelectedIndexChanged += new System.EventHandler(this.comboBoxRelationType_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(230, 281);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 32);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(322, 281);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 32);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxParentToChild
            // 
            this.groupBoxParentToChild.Controls.Add(this.checkBoxEnableCascadeDelete);
            this.groupBoxParentToChild.Controls.Add(this.label2);
            this.groupBoxParentToChild.Controls.Add(this.comboBoxParentChildFields);
            this.groupBoxParentToChild.Controls.Add(this.label1);
            this.groupBoxParentToChild.Controls.Add(this.comboBoxParentChildRelatedTable);
            this.groupBoxParentToChild.Controls.Add(this.labelRelationCardinality);
            this.groupBoxParentToChild.Controls.Add(this.comboBoxParentChildRelationCardinality);
            this.groupBoxParentToChild.Location = new System.Drawing.Point(6, 55);
            this.groupBoxParentToChild.Name = "groupBoxParentToChild";
            this.groupBoxParentToChild.Size = new System.Drawing.Size(391, 220);
            this.groupBoxParentToChild.TabIndex = 3;
            this.groupBoxParentToChild.TabStop = false;
            this.groupBoxParentToChild.Text = "Properties";
            // 
            // checkBoxEnableCascadeDelete
            // 
            this.checkBoxEnableCascadeDelete.AutoSize = true;
            this.checkBoxEnableCascadeDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxEnableCascadeDelete.Location = new System.Drawing.Point(159, 167);
            this.checkBoxEnableCascadeDelete.Name = "checkBoxEnableCascadeDelete";
            this.checkBoxEnableCascadeDelete.Size = new System.Drawing.Size(140, 18);
            this.checkBoxEnableCascadeDelete.TabIndex = 6;
            this.checkBoxEnableCascadeDelete.Text = "Enable Cascade Delete";
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(65, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Foreign key field";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxParentChildFields
            // 
            this.comboBoxParentChildFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParentChildFields.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxParentChildFields.FormattingEnabled = true;
            this.comboBoxParentChildFields.Location = new System.Drawing.Point(159, 116);
            this.comboBoxParentChildFields.Name = "comboBoxParentChildFields";
            this.comboBoxParentChildFields.Size = new System.Drawing.Size(226, 21);
            this.comboBoxParentChildFields.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Related entity";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxParentChildRelatedTable
            // 
            this.comboBoxParentChildRelatedTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParentChildRelatedTable.FormattingEnabled = true;
            this.comboBoxParentChildRelatedTable.Location = new System.Drawing.Point(159, 74);
            this.comboBoxParentChildRelatedTable.Name = "comboBoxParentChildRelatedTable";
            this.comboBoxParentChildRelatedTable.Size = new System.Drawing.Size(226, 21);
            this.comboBoxParentChildRelatedTable.TabIndex = 2;
            this.comboBoxParentChildRelatedTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxParentChildRelatedTable_SelectedIndexChanged);
            // 
            // labelRelationCardinality
            // 
            this.labelRelationCardinality.AutoSize = true;
            this.labelRelationCardinality.Location = new System.Drawing.Point(53, 35);
            this.labelRelationCardinality.Name = "labelRelationCardinality";
            this.labelRelationCardinality.Size = new System.Drawing.Size(92, 13);
            this.labelRelationCardinality.TabIndex = 1;
            this.labelRelationCardinality.Text = "Relation cardinality";
            // 
            // comboBoxParentChildRelationCardinality
            // 
            this.comboBoxParentChildRelationCardinality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParentChildRelationCardinality.FormattingEnabled = true;
            this.comboBoxParentChildRelationCardinality.Items.AddRange(new object[] {
            "One to One",
            "One to Many"});
            this.comboBoxParentChildRelationCardinality.Location = new System.Drawing.Point(159, 35);
            this.comboBoxParentChildRelationCardinality.Name = "comboBoxParentChildRelationCardinality";
            this.comboBoxParentChildRelationCardinality.Size = new System.Drawing.Size(226, 21);
            this.comboBoxParentChildRelationCardinality.TabIndex = 0;
            this.comboBoxParentChildRelationCardinality.SelectedIndexChanged += new System.EventHandler(this.comboBoxParentChildRelationCardinality_SelectedIndexChanged);
            // 
            // groupBoxChildToParent
            // 
            this.groupBoxChildToParent.Controls.Add(this.label6);
            this.groupBoxChildToParent.Controls.Add(this.comboBoxChildParentForeignKeyFields);
            this.groupBoxChildToParent.Controls.Add(this.label5);
            this.groupBoxChildToParent.Controls.Add(this.comboBoxChildParentPrimaryKeyFields);
            this.groupBoxChildToParent.Controls.Add(this.label4);
            this.groupBoxChildToParent.Controls.Add(this.comboBoxChildParentRelatedTable);
            this.groupBoxChildToParent.Controls.Add(this.label3);
            this.groupBoxChildToParent.Controls.Add(this.comboBoxChildParentCardinality);
            this.groupBoxChildToParent.Location = new System.Drawing.Point(403, 324);
            this.groupBoxChildToParent.Name = "groupBoxChildToParent";
            this.groupBoxChildToParent.Size = new System.Drawing.Size(375, 220);
            this.groupBoxChildToParent.TabIndex = 4;
            this.groupBoxChildToParent.TabStop = false;
            this.groupBoxChildToParent.Text = "Properties";
            this.groupBoxChildToParent.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Child entity foreign key";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxChildParentForeignKeyFields
            // 
            this.comboBoxChildParentForeignKeyFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChildParentForeignKeyFields.FormattingEnabled = true;
            this.comboBoxChildParentForeignKeyFields.Location = new System.Drawing.Point(143, 168);
            this.comboBoxChildParentForeignKeyFields.Name = "comboBoxChildParentForeignKeyFields";
            this.comboBoxChildParentForeignKeyFields.Size = new System.Drawing.Size(226, 21);
            this.comboBoxChildParentForeignKeyFields.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Parent entity primary key";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxChildParentPrimaryKeyFields
            // 
            this.comboBoxChildParentPrimaryKeyFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChildParentPrimaryKeyFields.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxChildParentPrimaryKeyFields.FormattingEnabled = true;
            this.comboBoxChildParentPrimaryKeyFields.Location = new System.Drawing.Point(143, 107);
            this.comboBoxChildParentPrimaryKeyFields.Name = "comboBoxChildParentPrimaryKeyFields";
            this.comboBoxChildParentPrimaryKeyFields.Size = new System.Drawing.Size(226, 21);
            this.comboBoxChildParentPrimaryKeyFields.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Parent entity";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxChildParentRelatedTable
            // 
            this.comboBoxChildParentRelatedTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChildParentRelatedTable.FormattingEnabled = true;
            this.comboBoxChildParentRelatedTable.Location = new System.Drawing.Point(143, 73);
            this.comboBoxChildParentRelatedTable.Name = "comboBoxChildParentRelatedTable";
            this.comboBoxChildParentRelatedTable.Size = new System.Drawing.Size(226, 21);
            this.comboBoxChildParentRelatedTable.TabIndex = 4;
            this.comboBoxChildParentRelatedTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxChildParentRelatedTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Relation cardinality";
            // 
            // comboBoxChildParentCardinality
            // 
            this.comboBoxChildParentCardinality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChildParentCardinality.FormattingEnabled = true;
            this.comboBoxChildParentCardinality.Items.AddRange(new object[] {
            "One to One"});
            this.comboBoxChildParentCardinality.Location = new System.Drawing.Point(143, 30);
            this.comboBoxChildParentCardinality.Name = "comboBoxChildParentCardinality";
            this.comboBoxChildParentCardinality.Size = new System.Drawing.Size(226, 21);
            this.comboBoxChildParentCardinality.TabIndex = 2;
            // 
            // groupBoxManyToMany
            // 
            this.groupBoxManyToMany.Controls.Add(this.label10);
            this.groupBoxManyToMany.Controls.Add(this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity);
            this.groupBoxManyToMany.Controls.Add(this.comboBoxManyToManyRelatedKey);
            this.groupBoxManyToMany.Controls.Add(this.label11);
            this.groupBoxManyToMany.Controls.Add(this.label9);
            this.groupBoxManyToMany.Controls.Add(this.comboBoxManyToManyIntermediaryForeignKeyOurEntity);
            this.groupBoxManyToMany.Controls.Add(this.label8);
            this.groupBoxManyToMany.Controls.Add(this.comboBoxManyToManyIntermediaryTable);
            this.groupBoxManyToMany.Controls.Add(this.label7);
            this.groupBoxManyToMany.Controls.Add(this.comboBoxManyToManyRelatedTable);
            this.groupBoxManyToMany.Location = new System.Drawing.Point(6, 324);
            this.groupBoxManyToMany.Name = "groupBoxManyToMany";
            this.groupBoxManyToMany.Size = new System.Drawing.Size(391, 220);
            this.groupBoxManyToMany.TabIndex = 5;
            this.groupBoxManyToMany.TabStop = false;
            this.groupBoxManyToMany.Text = "Properties";
            this.groupBoxManyToMany.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(316, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Intermediary entity foreign key for the relation with the related entity";
            // 
            // comboBoxManyToManyIntermediaryForeignKeyRelatedEntity
            // 
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.FormattingEnabled = true;
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.Location = new System.Drawing.Point(159, 187);
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.Name = "comboBoxManyToManyIntermediaryForeignKeyRelatedEntity";
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.Size = new System.Drawing.Size(219, 21);
            this.comboBoxManyToManyIntermediaryForeignKeyRelatedEntity.TabIndex = 11;
            // 
            // comboBoxManyToManyRelatedKey
            // 
            this.comboBoxManyToManyRelatedKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManyToManyRelatedKey.FormattingEnabled = true;
            this.comboBoxManyToManyRelatedKey.Location = new System.Drawing.Point(159, 95);
            this.comboBoxManyToManyRelatedKey.Name = "comboBoxManyToManyRelatedKey";
            this.comboBoxManyToManyRelatedKey.Size = new System.Drawing.Size(219, 21);
            this.comboBoxManyToManyRelatedKey.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(65, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Related entity key";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(281, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Intermediary entity foreign key for the relation with our entity";
            // 
            // comboBoxManyToManyIntermediaryForeignKeyOurEntity
            // 
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity.FormattingEnabled = true;
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity.Location = new System.Drawing.Point(159, 141);
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity.Name = "comboBoxManyToManyIntermediaryForeignKeyOurEntity";
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity.Size = new System.Drawing.Size(219, 21);
            this.comboBoxManyToManyIntermediaryForeignKeyOurEntity.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Intermediary entity";
            // 
            // comboBoxManyToManyIntermediaryTable
            // 
            this.comboBoxManyToManyIntermediaryTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManyToManyIntermediaryTable.FormattingEnabled = true;
            this.comboBoxManyToManyIntermediaryTable.Location = new System.Drawing.Point(159, 61);
            this.comboBoxManyToManyIntermediaryTable.Name = "comboBoxManyToManyIntermediaryTable";
            this.comboBoxManyToManyIntermediaryTable.Size = new System.Drawing.Size(219, 21);
            this.comboBoxManyToManyIntermediaryTable.TabIndex = 3;
            this.comboBoxManyToManyIntermediaryTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxManyToManyIntermediaryTable_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Related entity";
            // 
            // comboBoxManyToManyRelatedTable
            // 
            this.comboBoxManyToManyRelatedTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManyToManyRelatedTable.FormattingEnabled = true;
            this.comboBoxManyToManyRelatedTable.Location = new System.Drawing.Point(159, 24);
            this.comboBoxManyToManyRelatedTable.Name = "comboBoxManyToManyRelatedTable";
            this.comboBoxManyToManyRelatedTable.Size = new System.Drawing.Size(219, 21);
            this.comboBoxManyToManyRelatedTable.TabIndex = 0;
            this.comboBoxManyToManyRelatedTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxManyToManyRelatedTable_SelectedIndexChanged);
            // 
            // TableRelationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 319);
            this.Controls.Add(this.groupBoxChildToParent);
            this.Controls.Add(this.groupBoxParentToChild);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.groupBoxManyToMany);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableRelationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relations for ";
            this.Load += new System.EventHandler(this.TableRelationDialog_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBoxParentToChild.ResumeLayout(false);
            this.groupBoxParentToChild.PerformLayout();
            this.groupBoxChildToParent.ResumeLayout(false);
            this.groupBoxChildToParent.PerformLayout();
            this.groupBoxManyToMany.ResumeLayout(false);
            this.groupBoxManyToMany.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxRelationType;
        private System.Windows.Forms.GroupBox groupBoxParentToChild;
        private System.Windows.Forms.Label labelRelationCardinality;
        private System.Windows.Forms.ComboBox comboBoxParentChildRelationCardinality;
        private System.Windows.Forms.ComboBox comboBoxParentChildRelatedTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxParentChildFields;
        private System.Windows.Forms.GroupBox groupBoxChildToParent;
        private System.Windows.Forms.GroupBox groupBoxManyToMany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxEnableCascadeDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxChildParentRelatedTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxChildParentCardinality;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxChildParentPrimaryKeyFields;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxChildParentForeignKeyFields;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxManyToManyRelatedTable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxManyToManyIntermediaryTable;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxManyToManyIntermediaryForeignKeyOurEntity;
        private System.Windows.Forms.ComboBox comboBoxManyToManyRelatedKey;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxManyToManyIntermediaryForeignKeyRelatedEntity;
    }
}