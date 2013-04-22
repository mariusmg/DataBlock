using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Specialized;
using voidsoft.NorthwindSample;

using voidsoft.DataBlock;
using ExtenderOrders;


namespace ExtenderOrders
{
	/// <summary>
	/// Summary description for Orders.
	/// </summary>
	public class Orders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBoxOrdersId;
		private System.Windows.Forms.Label labelOrderId;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private OrdersTableMetadata order = null;
		private OrderPersistentObject orderPersist = null;
		private System.Windows.Forms.TextBox textBoxCustomerId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelEmployeeId;
		private System.Windows.Forms.TextBox textBoxEmployeeId;
		private System.Windows.Forms.DateTimePicker dateTimePickerOrderDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dateTimePickerRequiredDate;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePickerShippedDate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxShippedVia;
		private System.Windows.Forms.Label labelFreight;
		private System.Windows.Forms.TextBox textBoxFreight;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxShipAddress;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxShipName;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxShipCity;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxShipRegion;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxShipPostalCode;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxShipCountry;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGrid dataGrid;
		private OrdersTableMetadata[] orders = null;
		private OrderDetailsTableMetadata[] orderDetails = null;


		private System.Windows.Forms.Button buttonDelete;

		bool useLazyLoading = false;


		public Orders(bool useLazyLoading)
		{
			InitializeComponent();

			order = new OrdersTableMetadata();
			orderPersist = new OrderPersistentObject(SharedData.DatabaseServer, SharedData.ConnectionString, order); 

			this.useLazyLoading = useLazyLoading;
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
			this.comboBoxOrdersId = new System.Windows.Forms.ComboBox();
			this.labelOrderId = new System.Windows.Forms.Label();
			this.textBoxCustomerId = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelEmployeeId = new System.Windows.Forms.Label();
			this.textBoxEmployeeId = new System.Windows.Forms.TextBox();
			this.dateTimePickerOrderDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerRequiredDate = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.dateTimePickerShippedDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxShippedVia = new System.Windows.Forms.TextBox();
			this.labelFreight = new System.Windows.Forms.Label();
			this.textBoxFreight = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxShipAddress = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxShipName = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxShipCity = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxShipRegion = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBoxShipPostalCode = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxShipCountry = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxOrdersId
			// 
			this.comboBoxOrdersId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxOrdersId.Location = new System.Drawing.Point(16, 24);
			this.comboBoxOrdersId.Name = "comboBoxOrdersId";
			this.comboBoxOrdersId.Size = new System.Drawing.Size(160, 21);
			this.comboBoxOrdersId.TabIndex = 0;
			this.comboBoxOrdersId.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrdersId_SelectedIndexChanged);
			// 
			// labelOrderId
			// 
			this.labelOrderId.Location = new System.Drawing.Point(16, 8);
			this.labelOrderId.Name = "labelOrderId";
			this.labelOrderId.Size = new System.Drawing.Size(88, 16);
			this.labelOrderId.TabIndex = 1;
			this.labelOrderId.Text = "OrderID";
			// 
			// textBoxCustomerId
			// 
			this.textBoxCustomerId.Location = new System.Drawing.Point(24, 104);
			this.textBoxCustomerId.Name = "textBoxCustomerId";
			this.textBoxCustomerId.Size = new System.Drawing.Size(152, 20);
			this.textBoxCustomerId.TabIndex = 2;
			this.textBoxCustomerId.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "CustomerId";
			// 
			// labelEmployeeId
			// 
			this.labelEmployeeId.Location = new System.Drawing.Point(24, 136);
			this.labelEmployeeId.Name = "labelEmployeeId";
			this.labelEmployeeId.Size = new System.Drawing.Size(104, 16);
			this.labelEmployeeId.TabIndex = 4;
			this.labelEmployeeId.Text = "EmployeeId";
			// 
			// textBoxEmployeeId
			// 
			this.textBoxEmployeeId.Location = new System.Drawing.Point(24, 152);
			this.textBoxEmployeeId.Name = "textBoxEmployeeId";
			this.textBoxEmployeeId.Size = new System.Drawing.Size(152, 20);
			this.textBoxEmployeeId.TabIndex = 5;
			this.textBoxEmployeeId.Text = "";
			// 
			// dateTimePickerOrderDate
			// 
			this.dateTimePickerOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerOrderDate.Location = new System.Drawing.Point(24, 200);
			this.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate";
			this.dateTimePickerOrderDate.Size = new System.Drawing.Size(152, 20);
			this.dateTimePickerOrderDate.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Order Date";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 240);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Required Date";
			// 
			// dateTimePickerRequiredDate
			// 
			this.dateTimePickerRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerRequiredDate.Location = new System.Drawing.Point(24, 256);
			this.dateTimePickerRequiredDate.Name = "dateTimePickerRequiredDate";
			this.dateTimePickerRequiredDate.Size = new System.Drawing.Size(152, 20);
			this.dateTimePickerRequiredDate.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 296);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Shipped Date";
			// 
			// dateTimePickerShippedDate
			// 
			this.dateTimePickerShippedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerShippedDate.Location = new System.Drawing.Point(24, 312);
			this.dateTimePickerShippedDate.Name = "dateTimePickerShippedDate";
			this.dateTimePickerShippedDate.Size = new System.Drawing.Size(152, 20);
			this.dateTimePickerShippedDate.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(376, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "Shipped Via";
			// 
			// textBoxShippedVia
			// 
			this.textBoxShippedVia.Location = new System.Drawing.Point(376, 200);
			this.textBoxShippedVia.Name = "textBoxShippedVia";
			this.textBoxShippedVia.Size = new System.Drawing.Size(152, 20);
			this.textBoxShippedVia.TabIndex = 12;
			this.textBoxShippedVia.Text = "";
			// 
			// labelFreight
			// 
			this.labelFreight.Location = new System.Drawing.Point(208, 88);
			this.labelFreight.Name = "labelFreight";
			this.labelFreight.Size = new System.Drawing.Size(112, 16);
			this.labelFreight.TabIndex = 15;
			this.labelFreight.Text = "Freight";
			// 
			// textBoxFreight
			// 
			this.textBoxFreight.Location = new System.Drawing.Point(208, 104);
			this.textBoxFreight.Name = "textBoxFreight";
			this.textBoxFreight.Size = new System.Drawing.Size(152, 20);
			this.textBoxFreight.TabIndex = 14;
			this.textBoxFreight.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(376, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 17;
			this.label6.Text = "ShipAddress";
			// 
			// textBoxShipAddress
			// 
			this.textBoxShipAddress.Location = new System.Drawing.Point(376, 88);
			this.textBoxShipAddress.Name = "textBoxShipAddress";
			this.textBoxShipAddress.Size = new System.Drawing.Size(152, 20);
			this.textBoxShipAddress.TabIndex = 16;
			this.textBoxShipAddress.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(208, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "ShipName";
			// 
			// textBoxShipName
			// 
			this.textBoxShipName.Location = new System.Drawing.Point(208, 152);
			this.textBoxShipName.Name = "textBoxShipName";
			this.textBoxShipName.Size = new System.Drawing.Size(152, 20);
			this.textBoxShipName.TabIndex = 18;
			this.textBoxShipName.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(200, 128);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(112, 16);
			this.label8.TabIndex = 21;
			this.label8.Text = "ShipCity";
			// 
			// textBoxShipCity
			// 
			this.textBoxShipCity.Location = new System.Drawing.Point(200, 144);
			this.textBoxShipCity.Name = "textBoxShipCity";
			this.textBoxShipCity.Size = new System.Drawing.Size(152, 20);
			this.textBoxShipCity.TabIndex = 20;
			this.textBoxShipCity.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(208, 248);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(112, 16);
			this.label9.TabIndex = 23;
			this.label9.Text = "ShipRegion";
			// 
			// textBoxShipRegion
			// 
			this.textBoxShipRegion.Location = new System.Drawing.Point(208, 264);
			this.textBoxShipRegion.Name = "textBoxShipRegion";
			this.textBoxShipRegion.Size = new System.Drawing.Size(152, 20);
			this.textBoxShipRegion.TabIndex = 22;
			this.textBoxShipRegion.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(376, 128);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 16);
			this.label10.TabIndex = 25;
			this.label10.Text = "ShipPostalCode";
			// 
			// textBoxShipPostalCode
			// 
			this.textBoxShipPostalCode.Location = new System.Drawing.Point(376, 144);
			this.textBoxShipPostalCode.Name = "textBoxShipPostalCode";
			this.textBoxShipPostalCode.Size = new System.Drawing.Size(152, 20);
			this.textBoxShipPostalCode.TabIndex = 24;
			this.textBoxShipPostalCode.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(384, 88);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(112, 16);
			this.label11.TabIndex = 27;
			this.label11.Text = "ShipCountry";
			// 
			// textBoxShipCountry
			// 
			this.textBoxShipCountry.Location = new System.Drawing.Point(384, 104);
			this.textBoxShipCountry.Name = "textBoxShipCountry";
			this.textBoxShipCountry.Size = new System.Drawing.Size(152, 20);
			this.textBoxShipCountry.TabIndex = 26;
			this.textBoxShipCountry.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBoxShipAddress);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textBoxShipCity);
			this.groupBox1.Controls.Add(this.textBoxShipPostalCode);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxShippedVia);
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(536, 280);
			this.groupBox1.TabIndex = 28;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Order";
			// 
			// dataGrid
			// 
			this.dataGrid.DataMember = "";
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(8, 16);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new System.Drawing.Size(520, 144);
			this.dataGrid.TabIndex = 29;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.dataGrid);
			this.groupBox2.Location = new System.Drawing.Point(8, 352);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(536, 168);
			this.groupBox2.TabIndex = 30;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Order Details";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(328, 24);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(96, 24);
			this.buttonDelete.TabIndex = 31;
			this.buttonDelete.Text = "&Delete";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// Orders
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(546, 520);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textBoxShipCountry);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textBoxShipRegion);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxShipName);
			this.Controls.Add(this.labelFreight);
			this.Controls.Add(this.textBoxFreight);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dateTimePickerShippedDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dateTimePickerRequiredDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dateTimePickerOrderDate);
			this.Controls.Add(this.textBoxEmployeeId);
			this.Controls.Add(this.labelEmployeeId);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxCustomerId);
			this.Controls.Add(this.labelOrderId);
			this.Controls.Add(this.comboBoxOrdersId);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Orders";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Orders";
			this.Load += new System.EventHandler(this.Orders_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void Orders_Load(object sender, System.EventArgs e)
		{
			this.LoadData();
		}

		private void LoadData()
		{
			try
			{


				if(useLazyLoading == false)	//load normal
				{
					this.orders = (OrdersTableMetadata[]) this.orderPersist.GetTableMetadata();

					this.comboBoxOrdersId.DataSource = this.orders;
					this.comboBoxOrdersId.DisplayMember = "OrderID";
				}
				else  //load lazy
				{
					StringCollection scData = this.orderPersist.GetFieldList(this.order.GetPrimaryKeyField());
					this.comboBoxOrdersId.DataSource = scData;
				}
			}
			catch(Exception ex)
			{

			}
		}

		private void comboBoxOrdersId_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
			try
			{
				int selectedIndex = this.comboBoxOrdersId.SelectedIndex;

				if(this.useLazyLoading == false)
				{
				
					this.order = this.orders[selectedIndex];
				}
				else
				{
					object value = this.comboBoxOrdersId.SelectedItem;

					//lazy load data for the current order
					this.order = (OrdersTableMetadata) this.orderPersist.GetTableMetadata(value);
				}


				this.textBoxCustomerId.Text = this.order.CustomerID;
				this.textBoxEmployeeId.Text = this.order.EmployeeID.ToString();

				this.dateTimePickerOrderDate.Value = this.order.OrderDate;
				this.dateTimePickerRequiredDate.Value = this.order.RequiredDate;
				this.dateTimePickerShippedDate.Value = this.order.ShippedDate;
				this.textBoxFreight.Text = this.order.Freight.ToString();
				this.textBoxShipAddress.Text = this.order.ShipAddress;
				this.textBoxShipCity.Text = this.order.ShipCity;
				this.textBoxShipCountry.Text = this.order.ShipCountry;
				this.textBoxShipName.Text = this.order.ShipName;
				
				if(this.order.IsNull("ShipVia"))
				{
					this.textBoxShippedVia.Text = "";
				}
				else
				{
					this.textBoxShippedVia.Text = this.order.ShipVia.ToString();

				}
					
				
				if(this.order.IsNull("ShipPostalCode"))
				{
					this.textBoxShipPostalCode.Text = "";
				}
				else
				{
					this.textBoxShipPostalCode.Text = this.order.ShipPostalCode;
				}

				if(this.order.IsNull("ShipRegion"))
				{
					this.textBoxShipRegion.Text = "";
				}
				else
				{
					this.textBoxShipRegion.Text = this.order.ShipRegion;
				}
				this.LoadDetails();
			}
			catch(Exception ex)
			{
				

			}
			finally
			{

			}

		}

		/// <summary>
		/// Loads the order details.
		/// </summary>
		private void LoadDetails()
		{
			try
			{
				this.dataGrid.DataSource = null;

				object value = null;

				if(this.useLazyLoading)
				{
					 value = this.comboBoxOrdersId.SelectedItem;
				}
				else
				{
					value = ((OrdersTableMetadata) this.comboBoxOrdersId.SelectedItem).OrderID;
					
				}
				
				this.orderDetails = (OrderDetailsTableMetadata[])this.orderPersist.GetTableMetadata("FK_OrderDetails", "ExtenderOrders.OrderDetailsTableMetadata", value);

				this.dataGrid.DataSource = this.orderDetails;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
		
			try
			{
				if(MessageBox.Show("Are you sure you want to delete the current order ?", "DataBlock Demo Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.orderPersist.Delete(this.order, this.orderDetails);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Failed to delete the current order");
			}
		}
	}
}
