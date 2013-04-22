using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

      [Serializable()]
		public class First : TableMetadata
		{

                   public enum FirstFields
                   {
                      Id,
                      Name
                  }


			    private DatabaseField[] _fields;

		    	public First()
			    {
					    _fields = new DatabaseField[2];
                    _fields[0] = new DatabaseField(DbType.Int32,"Id",true,true,null);
                    _fields[1] = new DatabaseField(DbType.String,"Name",false,false,null);
 
                        this.currentTableName = "First";

this.listRelations.Add(new ParentTableRelation("Second", "FirstId",TableRelationCardinality.OneToMany,true));

                  }


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}
          public First Clone()
          {
                 return this.Clone<First>();
          }

public System.Int32? Id
{
    get
    {
          return (System.Int32? ) (this.GetField("Id")).fieldValue;
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

public Array GetSecond()
{
    Type relatedType = Type.GetType("Extender.Second");
    return this.GetRelatedTableData(relatedType);
}
}
}
