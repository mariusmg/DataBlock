using System;
using System.Data;
using voidsoft.DataBlock;

namespace ExtenderRegion
{


		public class RegionTableMetadata : TableMetadata
		{
			private DatabaseField[] _fields;

			public RegionTableMetadata()
			{
					_fields = new DatabaseField[2];
 _fields[0] = new DatabaseField(DbType.Int32,"RegionID",true,true,null);
 _fields[1] = new DatabaseField(DbType.String,"RegionDescription",false,false,null);
 
this.tableName = "Region";


}


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
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


public System.String RegionDescription
{
    get
    {
          return (System.String) (this.GetField("RegionDescription")).fieldValue;
    }

    set
    {
          this.SetFieldValue("RegionDescription", value);
    }
}

}
}
