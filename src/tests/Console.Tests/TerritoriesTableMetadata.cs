using System;
using System.Data;
using voidsoft.DataBlock;

namespace ExtenderTerritories
{


		public class TerritoriesTableMetadata : TableMetadata
		{
			private DatabaseField[] _fields;

			public TerritoriesTableMetadata()
			{
					_fields = new DatabaseField[3];
 _fields[0] = new DatabaseField(DbType.String,"TerritoryID",true, false,null);
 _fields[1] = new DatabaseField(DbType.String,"TerritoryDescription",false, false,null);
 _fields[2] = new DatabaseField(DbType.Int32,"RegionID",false, false,null);
 
this.currentTableName = "Territories";
}


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
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


public System.String TerritoryDescription
{
    get
    {
          return (System.String) (this.GetField("TerritoryDescription")).fieldValue;
    }

    set
    {
          this.SetFieldValue("TerritoryDescription", value);
    }
}


public System.Int32 RegionID
{
    get
    {
          return (System.Int32) (this.GetField("RegionID")).fieldValue;
    }

    set
    {
          this.SetFieldValue("RegionID", value);
    }
}

}
}
