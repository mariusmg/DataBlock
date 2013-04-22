using System;
using System.Data;
using voidsoft.DataBlock;

namespace ExtenderTest
{


		public class TestTableMetadata : TableMetadata
		{
			private DatabaseField[] _fields;

			public TestTableMetadata()
			{
					_fields = new DatabaseField[6];
 _fields[0] = new DatabaseField(DbType.Int32,"Id",true,true,null);
 _fields[1] = new DatabaseField(DbType.String,"Name",false,false,null);
 _fields[2] = new DatabaseField(DbType.Boolean,"Ocupat",false,false,null);
 _fields[3] = new DatabaseField(DbType.DateTime,"DataNasterii",false,false,null);
 _fields[4] = new DatabaseField(DbType.Decimal,"Suma",false,false,null);
 _fields[5] = new DatabaseField(DbType.String,"Descriere",false,false,null);
 
this.currentTableName = "Test";


}


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}

public System.Int32 Id
{
    get
    {
          return (System.Int32) (this.GetField("Id")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Id", value);
    }
}


public System.String Name
{
    get
    {
          return (System.String) (this.GetField("Name")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Name", value);
    }
}


public System.Boolean Ocupat
{
    get
    {
          return (System.Boolean) (this.GetField("Ocupat")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Ocupat", value);
    }
}


public System.Byte[] Poza
{
    get
    {
          return (System.Byte[]) (this.GetField("Poza")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Poza", value);
    }
}


public System.DateTime DataNasterii
{
    get
    {
          return (System.DateTime) (this.GetField("DataNasterii")).fieldValue;
    }

    set
    {
          this.SetFieldValue("DataNasterii", value);
    }
}


public System.Decimal Suma
{
    get
    {
          return (System.Decimal) (this.GetField("Suma")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Suma", value);
    }
}


public System.String Descriere
{
    get
    {
          return (System.String) (this.GetField("Descriere")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Descriere", value);
    }
}

}
}
