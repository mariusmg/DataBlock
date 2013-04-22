using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ExtenderTerritories;
using voidsoft.NorthwindSample;
using voidsoft.DataBlock;

using ExtenderOrders;

using ExtenderRegion;

namespace voidsoft.NorthwindSample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonTerritories;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxConnectionString;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonRegions;
		private System.Windows.Forms.Button buttonOrders;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBoxServers;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.buttonTerritories = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxConnectionString = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonRegions = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonOrders = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBoxServers = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// buttonTerritories
			// 
			this.buttonTerritories.Location = new System.Drawing.Point(16, 128);
			this.buttonTerritories.Name = "buttonTerritories";
			this.buttonTerritories.Size = new System.Drawing.Size(80, 48);
			this.buttonTerritories.TabIndex = 0;
			this.buttonTerritories.Text = "Territories";
			this.buttonTerritories.Click += new System.EventHandler(this.buttonTerritories_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(80, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(368, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "DataBlock Demo Application";
			// 
			// textBoxConnectionString
			// 
			this.textBoxConnectionString.Location = new System.Drawing.Point(112, 88);
			this.textBoxConnectionString.Name = "textBoxConnectionString";
			this.textBoxConnectionString.Size = new System.Drawing.Size(304, 20);
			this.textBoxConnectionString.TabIndex = 3;
			this.textBoxConnectionString.Text = "";
			this.textBoxConnectionString.TextChanged += new System.EventHandler(this.textBoxConnectionString_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Connection String";
			// 
			// buttonRegions
			// 
			this.buttonRegions.BackColor = System.Drawing.SystemColors.Control;
			this.buttonRegions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRegions.Location = new System.Drawing.Point(464, 128);
			this.buttonRegions.Name = "buttonRegions";
			this.buttonRegions.Size = new System.Drawing.Size(112, 48);
			this.buttonRegions.TabIndex = 5;
			this.buttonRegions.Text = "&Regions";
			this.buttonRegions.Click += new System.EventHandler(this.button1_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Northwind database";
			// 
			// buttonOrders
			// 
			this.buttonOrders.Location = new System.Drawing.Point(112, 128);
			this.buttonOrders.Name = "buttonOrders";
			this.buttonOrders.Size = new System.Drawing.Size(144, 48);
			this.buttonOrders.TabIndex = 7;
			this.buttonOrders.Text = "&Orders - normal mode";
			this.buttonOrders.Click += new System.EventHandler(this.buttonOrders_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(296, 128);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(152, 48);
			this.button1.TabIndex = 8;
			this.button1.Text = "&Orders - with lazy loading";
			this.button1.Click += new System.EventHandler(this.buttonOrdersLazy_Lick);
			// 
			// comboBoxServers
			// 
			this.comboBoxServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxServers.Items.AddRange(new object[] {
																 "SqlServer",
																 "Access",
																 "MySql"});
			this.comboBoxServers.Location = new System.Drawing.Point(424, 88);
			this.comboBoxServers.Name = "comboBoxServers";
			this.comboBoxServers.Size = new System.Drawing.Size(136, 21);
			this.comboBoxServers.TabIndex = 9;
			this.comboBoxServers.SelectedIndexChanged += new System.EventHandler(this.comboBoxServers_SelectedIndexChanged);
			// 
			// MainWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(586, 205);
			this.Controls.Add(this.comboBoxServers);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.buttonOrders);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonRegions);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxConnectionString);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonTerritories);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Main Window";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			
			Application.EnableVisualStyles();
			Application.Run(new MainWindow());
		}

		private void MainWindow_Load(object sender, System.EventArgs e)
		{

			this.comboBoxServers.SelectedIndex = 0;
			
		}

		private void buttonTerritories_Click(object sender, System.EventArgs e)
		{
		
			TerritoriesUI tr = null;

			try
			{
				tr = new TerritoriesUI();
				tr.ShowDialog();
                
			}
			catch(Exception ex)
			{

			}
			finally
			{
				if(tr != null)
				{
					tr.Dispose();
				}
			}
		}

		private void textBoxConnectionString_TextChanged(object sender, System.EventArgs e)
		{
			SharedData.ConnectionString = this.textBoxConnectionString.Text.Trim();

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			RegionsUI tr = null;

			try
			{
				tr = new  ExtenderRegion.RegionsUI();
				tr.ShowDialog();
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if(tr != null)
				{
					tr.Dispose();
				}
			}

		}

		private void buttonOrders_Click(object sender, System.EventArgs e)
		{

			Orders ors = null;

			try
			{
				ors = new  Orders(false);
				ors.ShowDialog();
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if(ors != null)
				{
					ors.Dispose();
				}
			}

		
		}

		private void buttonOrdersLazy_Lick(object sender, System.EventArgs e)
		{
		
			Orders ors = null;

			try
			{
				ors = new  Orders(true);
				ors.ShowDialog();
			}
			catch(Exception ex)
			{
			}
			finally
			{
				if(ors != null)
				{
					ors.Dispose();
				}
			}

		}

		private void comboBoxServers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.comboBoxServers.SelectedIndex == 0)
			{
				this.textBoxConnectionString.Text = @"server=klamath\klamathpc;user id=sa; password=1234;database = northwind";
				SharedData.DatabaseServer = EDatabase.SqlServer;
			}
			else if(this.comboBoxServers.SelectedIndex == 1)
			{
				this.textBoxConnectionString.Text =@"Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=d:\northwind.mdb";
				SharedData.DatabaseServer = EDatabase.Access;
			}
			else
			{
				this.textBoxConnectionString.Text ="Host=localhost; UserName=root; Password=;Database=Northwind";
				SharedData.DatabaseServer = EDatabase.MySQL;
			}

		}

		
	}
}
