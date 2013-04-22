using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ExtenderTerritories;

using voidsoft.DataBlock;
using voidsoft.NorthwindSample;


namespace ExtenderTerritories
{
	/// <summary>
	/// Summary description for TerritoriesUI.
	/// </summary>
	public class TerritoriesUI : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonUpdate;
	

		private TerritoriesTableMetadata[] territoriesList = null;
		private TerritoriesPersistentObject tpo = null;
		private TerritoriesTableMetadata tm = null;
		private System.Windows.Forms.Button buttonExit;

		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TerritoriesUI()
		{
			InitializeComponent();

			tm = new TerritoriesTableMetadata();
			tpo = new TerritoriesPersistentObject(SharedData.DatabaseServer, SharedData.ConnectionString, tm);
		}



		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.buttonCreate = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.DataMember = "";
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(8, 8);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new System.Drawing.Size(392, 336);
			this.dataGrid.TabIndex = 0;
			// 
			// buttonCreate
			// 
			this.buttonCreate.Location = new System.Drawing.Point(416, 24);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(96, 32);
			this.buttonCreate.TabIndex = 1;
			this.buttonCreate.Text = "&Create";
			this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(424, 312);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(88, 32);
			this.buttonExit.TabIndex = 2;
			this.buttonExit.Text = "&Exit";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(416, 72);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(96, 32);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Text = "&Delete";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(416, 120);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(96, 32);
			this.buttonUpdate.TabIndex = 4;
			this.buttonUpdate.Text = "&Update";
			// 
			// TerritoriesUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 355);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonCreate);
			this.Controls.Add(this.dataGrid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TerritoriesUI";
			this.Text = "TerritoriesUI";
			this.Load += new System.EventHandler(this.TerritoriesUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void TerritoriesUI_Load(object sender, System.EventArgs e)
		{
		
			try
			{
				this.territoriesList = (TerritoriesTableMetadata[] ) tpo.GetTableMetadata();
				this.dataGrid.DataSource = this.territoriesList;
				this.dataGrid.DataMember = "TerritoryDescription";

			}
			catch(Exception ex)
			{
				MessageBox.Show("Failed to load territories." + ex.Message);
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			try
			{

				int selectedIndex = this.dataGrid.CurrentRowIndex;

				if(selectedIndex > -1)
				{
						
					TerritoriesTableMetadata tp =  this.territoriesList[selectedIndex];
					this.tpo.Delete(tp,false);

				}
				else
				{
					MessageBox.Show("Please select a item from the grid");
					return;
					
				}


			}
			catch(Exception ex)
			{

			}
			finally
			{

			}
		}

		private void buttonCreate_Click(object sender, System.EventArgs e)
		{
		
		}


	}
}
