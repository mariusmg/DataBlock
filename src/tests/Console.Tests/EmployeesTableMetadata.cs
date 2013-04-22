using System;
using System.Data;
using voidsoft.DataBlock;

namespace ExtenderEmployees
{


		public class EmployeesTableMetadata : TableMetadata
		{
			private DatabaseField[] _fields;

			public EmployeesTableMetadata()
			{
					_fields = new DatabaseField[18];
 _fields[0] = new DatabaseField(DbType.Int32,"EmployeeID",true,true,null);
 _fields[1] = new DatabaseField(DbType.String,"LastName",false,false,null);
 _fields[2] = new DatabaseField(DbType.String,"FirstName",false,false,null);
 _fields[3] = new DatabaseField(DbType.String,"Title",false,false,null);
 _fields[4] = new DatabaseField(DbType.String,"TitleOfCourtesy",false,false,null);
 _fields[5] = new DatabaseField(DbType.DateTime,"BirthDate",false,false,null);
 _fields[6] = new DatabaseField(DbType.DateTime,"HireDate",false,false,null);
 _fields[7] = new DatabaseField(DbType.String,"Address",false,false,null);
 _fields[8] = new DatabaseField(DbType.String,"City",false,false,null);
 _fields[9] = new DatabaseField(DbType.String,"Region",false,false,null);
 _fields[10] = new DatabaseField(DbType.String,"PostalCode",false,false,null);
 _fields[11] = new DatabaseField(DbType.String,"Country",false,false,null);
 _fields[12] = new DatabaseField(DbType.String,"HomePhone",false,false,null);
 _fields[13] = new DatabaseField(DbType.String,"Extension",false,false,null);
 _fields[14] = new DatabaseField(DbType.Binary,"Photo",false,false,null);
 _fields[15] = new DatabaseField(DbType.String,"Notes",false,false,null);
 _fields[16] = new DatabaseField(DbType.Int32,"ReportsTo",false,false,null);
 _fields[17] = new DatabaseField(DbType.String,"PhotoPath",false,false,null);
 
this.currentTableName = "Employees";

 this.alRelations.Add(new TableRelation("FK","EmployeeTerritories","EmployeeID"));

}


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
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


public System.String LastName
{
    get
    {
          return (System.String) (this.GetField("LastName")).fieldValue;
    }

    set
    {
          this.SetFieldValue("LastName", value);
    }
}


public System.String FirstName
{
    get
    {
          return (System.String) (this.GetField("FirstName")).fieldValue;
    }

    set
    {
          this.SetFieldValue("FirstName", value);
    }
}


public System.String Title
{
    get
    {
          return (System.String) (this.GetField("Title")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Title", value);
    }
}


public System.String TitleOfCourtesy
{
    get
    {
          return (System.String) (this.GetField("TitleOfCourtesy")).fieldValue;
    }

    set
    {
          this.SetFieldValue("TitleOfCourtesy", value);
    }
}


public System.DateTime BirthDate
{
    get
    {
          return (System.DateTime) (this.GetField("BirthDate")).fieldValue;
    }

    set
    {
          this.SetFieldValue("BirthDate", value);
    }
}


public System.DateTime HireDate
{
    get
    {
          return (System.DateTime) (this.GetField("HireDate")).fieldValue;
    }

    set
    {
          this.SetFieldValue("HireDate", value);
    }
}


public System.String Address
{
    get
    {
          return (System.String) (this.GetField("Address")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Address", value);
    }
}


public System.String City
{
    get
    {
          return (System.String) (this.GetField("City")).fieldValue;
    }

    set
    {
          this.SetFieldValue("City", value);
    }
}


public System.String Region
{
    get
    {
          return (System.String) (this.GetField("Region")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Region", value);
    }
}


public System.String PostalCode
{
    get
    {
          return (System.String) (this.GetField("PostalCode")).fieldValue;
    }

    set
    {
          this.SetFieldValue("PostalCode", value);
    }
}


public System.String Country
{
    get
    {
          return (System.String) (this.GetField("Country")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Country", value);
    }
}


public System.String HomePhone
{
    get
    {
          return (System.String) (this.GetField("HomePhone")).fieldValue;
    }

    set
    {
          this.SetFieldValue("HomePhone", value);
    }
}


public System.String Extension
{
    get
    {
          return (System.String) (this.GetField("Extension")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Extension", value);
    }
}


public System.Byte[] Photo
{
    get
    {
          return (System.Byte[]) (this.GetField("Photo")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Photo", value);
    }
}


public System.String Notes
{
    get
    {
          return (System.String) (this.GetField("Notes")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Notes", value);
    }
}


public System.Int32 ReportsTo
{
    get
    {
          return (System.Int32) (this.GetField("ReportsTo")).fieldValue;
    }

    set
    {
          this.SetFieldValue("ReportsTo", value);
    }
}


public System.String PhotoPath
{
    get
    {
          return (System.String) (this.GetField("PhotoPath")).fieldValue;
    }

    set
    {
          this.SetFieldValue("PhotoPath", value);
    }
}

}


		public class EmployeeTerritoriesTableMetadata : TableMetadata
		{
			private DatabaseField[] _fields;

			public EmployeeTerritoriesTableMetadata()
			{
					_fields = new DatabaseField[2];
 _fields[0] = new DatabaseField(DbType.Int32,"EmployeeID",false,false,null);
 _fields[1] = new DatabaseField(DbType.String,"TerritoryID",false,false,null);
 
this.currentTableName = "EmployeeTerritories";


}


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
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


public System.String TerritoryID
{
    get
    {
          return (System.String) (this.GetField("TerritoryID")).fieldValue;
    }

    set
    {
          this.SetFieldValue("TerritoryID", value);
    }
}

}
}
