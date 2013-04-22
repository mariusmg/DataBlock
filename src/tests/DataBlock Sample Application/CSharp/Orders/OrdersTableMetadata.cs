using System;
using System.Data;
using voidsoft.DataBlock;

namespace ExtenderOrders
{


	public class OrderDetailsTableMetadata : TableMetadata
	{
		private DatabaseField[] _fields;

		public OrderDetailsTableMetadata()
		{
			_fields = new DatabaseField[5];
			_fields[0] = new DatabaseField(DbType.Int32,"OrderID",false,false,null);
			_fields[1] = new DatabaseField(DbType.Int32,"ProductID",false,false,null);
			_fields[2] = new DatabaseField(DbType.Decimal,"UnitPrice",false,false,null);
			_fields[3] = new DatabaseField(DbType.Int16,"Quantity",false,false,null);
			_fields[4] = new DatabaseField(DbType.Single,"Discount",false,false,null);
 
			this.currentTableName = "Order Details";


		}


		public override DatabaseField[] TableFields 
		{
			get{ return _fields;}
			set{_fields = value;}
		}

		public System.Int32 OrderID
		{
			get
			{
				return (System.Int32) (this.GetField("OrderID")).fieldValue;
			}

			set
			{
				this.SetFieldValue("OrderID", value);
			}
		}


		public System.Int32 ProductID
		{
			get
			{
				return (System.Int32) (this.GetField("ProductID")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ProductID", value);
			}
		}


		public System.Decimal UnitPrice
		{
			get
			{
				return (System.Decimal) (this.GetField("UnitPrice")).fieldValue;
			}

			set
			{
				this.SetFieldValue("UnitPrice", value);
			}
		}


		public System.Int16 Quantity
		{
			get
			{
				return (System.Int16) (this.GetField("Quantity")).fieldValue;
			}

			set
			{
				this.SetFieldValue("Quantity", value);
			}
		}


		public System.Single Discount
		{
			get
			{
				return (System.Single) (this.GetField("Discount")).fieldValue;
			}

			set
			{
				this.SetFieldValue("Discount", value);
			}
		}

	}


	public class OrdersTableMetadata : TableMetadata
	{
		private DatabaseField[] _fields;

		public OrdersTableMetadata()
		{
			_fields = new DatabaseField[14];
			_fields[0] = new DatabaseField(DbType.Int32,"OrderID",true,true,null);
			_fields[1] = new DatabaseField(DbType.String,"CustomerID",false,false,null);
			_fields[2] = new DatabaseField(DbType.Int32,"EmployeeID",false,false,null);
			_fields[3] = new DatabaseField(DbType.DateTime,"OrderDate",false,false,null);
			_fields[4] = new DatabaseField(DbType.DateTime,"RequiredDate",false,false,null);
			_fields[5] = new DatabaseField(DbType.DateTime,"ShippedDate",false,false,null);
			_fields[6] = new DatabaseField(DbType.Int32,"ShipVia",false,false,null);
			_fields[7] = new DatabaseField(DbType.Decimal,"Freight",false,false,null);
			_fields[8] = new DatabaseField(DbType.String,"ShipName",false,false,null);
			_fields[9] = new DatabaseField(DbType.String,"ShipAddress",false,false,null);
			_fields[10] = new DatabaseField(DbType.String,"ShipCity",false,false,null);
			_fields[11] = new DatabaseField(DbType.String,"ShipRegion",false,false,null);
			_fields[12] = new DatabaseField(DbType.String,"ShipPostalCode",false,false,null);
			_fields[13] = new DatabaseField(DbType.String,"ShipCountry",false,false,null);
 
			this.currentTableName = "Orders";

			this.alRelations.Add(new TableRelation("FK_OrderDetails", "Order Details", "OrderID"));


		}


		public override DatabaseField[] TableFields 
		{
			get{ return _fields;}
			set{_fields = value;}
		}

		public System.Int32 OrderID
		{
			get
			{
				return (System.Int32) (this.GetField("OrderID")).fieldValue;
			}

			set
			{
				this.SetFieldValue("OrderID", value);
			}
		}


		public System.String CustomerID
		{
			get
			{
				return (System.String) (this.GetField("CustomerID")).fieldValue;
			}

			set
			{
				this.SetFieldValue("CustomerID", value);
			}
		}


		public System.Int32 EmployeeID
		{
			get
			{
				return (System.Int32) (this.GetField("EmployeeID")).fieldValue;
			}

			set
			{
				this.SetFieldValue("EmployeeID", value);
			}
		}


		public System.DateTime OrderDate
		{
			get
			{
				return (System.DateTime) (this.GetField("OrderDate")).fieldValue;
			}

			set
			{
				this.SetFieldValue("OrderDate", value);
			}
		}


		public System.DateTime RequiredDate
		{
			get
			{
				return (System.DateTime) (this.GetField("RequiredDate")).fieldValue;
			}

			set
			{
				this.SetFieldValue("RequiredDate", value);
			}
		}


		public System.DateTime ShippedDate
		{
			get
			{
				return (System.DateTime) (this.GetField("ShippedDate")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShippedDate", value);
			}
		}


		public System.Int32 ShipVia
		{
			get
			{
				return (System.Int32) (this.GetField("ShipVia")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipVia", value);
			}
		}


		public System.Decimal Freight
		{
			get
			{
				return (System.Decimal) (this.GetField("Freight")).fieldValue;
			}

			set
			{
				this.SetFieldValue("Freight", value);
			}
		}


		public System.String ShipName
		{
			get
			{
				return (System.String) (this.GetField("ShipName")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipName", value);
			}
		}


		public System.String ShipAddress
		{
			get
			{
				return (System.String) (this.GetField("ShipAddress")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipAddress", value);
			}
		}


		public System.String ShipCity
		{
			get
			{
				return (System.String) (this.GetField("ShipCity")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipCity", value);
			}
		}


		public System.String ShipRegion
		{
			get
			{
				return (System.String) (this.GetField("ShipRegion")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipRegion", value);
			}
		}


		public System.String ShipPostalCode
		{
			get
			{
				return (System.String) (this.GetField("ShipPostalCode")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipPostalCode", value);
			}
		}


		public System.String ShipCountry
		{
			get
			{
				return (System.String) (this.GetField("ShipCountry")).fieldValue;
			}

			set
			{
				this.SetFieldValue("ShipCountry", value);
			}
		}

	}
}
