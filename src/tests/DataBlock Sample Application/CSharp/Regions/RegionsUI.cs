/*


		This samples demonstrates how to retrieve the data as a dataset
		and how to convert into a TableMetadata object.





*/


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ExtenderRegion;
using voidsoft.NorthwindSample;
using voidsoft.DataBlock;

namespace ExtenderRegion
{
	/// <summary>
	/// Summary description for RegionsUI.
	/// </summary>
	public class RegionsUI : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private RegionTableMetadata region = null;
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonUpdate;
		private RegionPersistentObject regionPersistent = null;
		private System.Windows.Forms.TextBox textRegionDescription;

		private DataSet ds = null;
		private System.Windows.Forms.Label label1;

		private RegionTableMetadata[] regions = null;

		public RegionsUI()
		{
			InitializeComponent();

			region = new RegionTableMetadata();
			regionPersistent = new RegionPersistentObject(SharedData.DatabaseServer, SharedData.ConnectionString, region);
			ds = new  DataSet();

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
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.textRegionDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.DataMember = "";
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(8, 8);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new System.Drawing.Size(344, 304);
			this.dataGrid.TabIndex = 0;
			this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
			// 
			// buttonCreate
			// 
			this.buttonCreate.Location = new System.Drawing.Point(368, 8);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(80, 32);
			this.buttonCreate.TabIndex = 1;
			this.buttonCreate.Text = "&Create";
			this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(368, 48);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(80, 32);
			this.buttonDelete.TabIndex = 2;
			this.buttonDelete.Text = "&Delete";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(368, 96);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(80, 32);
			this.buttonUpdate.TabIndex = 3;
			this.buttonUpdate.Text = "&Update";
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// textRegionDescription
			// 
			this.textRegionDescription.Location = new System.Drawing.Point(8, 336);
			this.textRegionDescription.Name = "textRegionDescription";
			this.textRegionDescription.Size = new System.Drawing.Size(344, 20);
			this.textRegionDescription.TabIndex = 4;
			this.textRegionDescription.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 320);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Region Description";
			// 
			// RegionsUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 371);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textRegionDescription);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonCreate);
			this.Controls.Add(this.dataGrid);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegionsUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RegionsUI";
			this.Load += new System.EventHandler(this.RegionsUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void RegionsUI_Load(object sender, System.EventArgs e)
		{
			try
			{
				//retrieve the data from database as a dataset.
				this.ds = this.regionPersistent.GetDataSet();

				this.dataGrid.DataSource = ds.Tables[0].DefaultView;
			}
			catch(Exception ex)
			{
			}
		}

		
		
		private void buttonCreate_Click(object sender, System.EventArgs e)
		{
			try
			{
				RegionTableMetadata rtm = new RegionTableMetadata();
				Random rnd = new Random();
                rtm.RegionID = rnd.Next(300000);
				rtm.RegionDescription = "New Region";
				this.regionPersistent.Create(rtm);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Failed to create region");
			}
		}

		
		
		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				int index = this.dataGrid.CurrentRowIndex;

				if(index == -1) return;

				DataConvertors.ConvertDataRowToTableMetadata(index, this.ds.Tables[0], this.region);
				
				this.regionPersistent.Delete(this.region, false);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Failed to delete item " + ex.Message);
			}
			finally
			{
			
			}
		}

				
		private void buttonUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				//upates the selected region

				//get the selected index
				int index = this.dataGrid.CurrentRowIndex;

				if(index == -1) return;

				DataConvertors.ConvertDataRowToTableMetadata(index, this.ds.Tables[0], this.region);
				this.region.RegionDescription = "A new description";
				this.regionPersistent.Update(this.region);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Failed to update item " + ex.Message);
			}
			finally
			{
			}
		}


		private void dataGrid_CurrentCellChanged(object sender, System.EventArgs e)
		{
//			
			try
			{
				int index =  this.dataGrid.CurrentRowIndex;

				if(index > -1)
				{
					//get the current region object.
					DataConvertors.ConvertDataRowToTableMetadata(index, this.ds.Tables[0], this.region);
				}
		
				//show the description on the text box
				this.textRegionDescription.Text = this.region.RegionDescription;
			}
			catch(Exception ex)
			{
			}
		}
	}
}
