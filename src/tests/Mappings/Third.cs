using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

      [Serializable()]
		public class Third : TableMetadata
		{

                   public enum ThirdFields
                   {
                      Id,
                      ThirdStuff,
                      SecondId
                  }


			    private DatabaseField[] _fields;

		    	public Third()
			    {
					    _fields = new DatabaseField[3];
                    _fields[0] = new DatabaseField(DbType.Int32,"Id",true,true,null);
                    _fields[1] = new DatabaseField(DbType.String,"ThirdStuff",false,false,null);
                    _fields[2] = new DatabaseField(DbType.Int32,"SecondId",false,false,null);
 
                        this.currentTableName = "Third";

this.listRelations.Add(new ChildTableRelation("Second",TableRelationCardinality.OneToOne,"Id","SecondId"));

                  }


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}
          public Third Clone()
          {
                 return this.Clone<Third>();
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


public System.String ThirdStuff
{
    get
    {
          return (System.String) (this.GetField("ThirdStuff")).fieldValue;
    }

    set
    {
          this.SetFieldValue("ThirdStuff", value);
    }
}


public System.Int32? SecondId
{
    get
    {
          return (System.Int32? ) (this.GetField("SecondId")).fieldValue;
    }

    set
    {
          this.SetFieldValue("SecondId", value);
    }
}

public TableMetadata GetSecond()
{
    Type relatedType = Type.GetType("Extender.Second");
    TableMetadata[] result = this.GetRelatedTableData(relatedType);
    return result.Length > 0 ? result[0] : null; 
}
}
}
