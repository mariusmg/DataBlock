using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

      [Serializable()]
		public class Second : TableMetadata
		{

                   public enum SecondFields
                   {
                      Id,
                      FirstId,
                      Age,
                      Quantity
                  }


			    private DatabaseField[] _fields;

		    	public Second()
			    {
					    _fields = new DatabaseField[4];
                    _fields[0] = new DatabaseField(DbType.Int32,"Id",true,true,null);
                    _fields[1] = new DatabaseField(DbType.Int32,"FirstId",false,false,null);
                    _fields[2] = new DatabaseField(DbType.Int32,"Age",false,false,null);
                    _fields[3] = new DatabaseField(DbType.Int32,"Quantity",false,false,null);
 
                        this.currentTableName = "Second";

this.listRelations.Add(new ParentTableRelation("Third", "SecondId",TableRelationCardinality.OneToMany,true));
this.listRelations.Add(new ChildTableRelation("First",TableRelationCardinality.OneToOne,"Id","FirstId"));

                  }


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}
          public Second Clone()
          {
                 return this.Clone<Second>();
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


public System.Int32? FirstId
{
    get
    {
          return (System.Int32? ) (this.GetField("FirstId")).fieldValue;
    }

    set
    {
          this.SetFieldValue("FirstId", value);
    }
}


public System.Int32? Age
{
    get
    {
          return (System.Int32? ) (this.GetField("Age")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Age", value);
    }
}


public System.Int32? Quantity
{
    get
    {
          return (System.Int32? ) (this.GetField("Quantity")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Quantity", value);
    }
}

public Array GetThird()
{
    Type relatedType = Type.GetType("Extender.Third");
    return this.GetRelatedTableData(relatedType);
}
public TableMetadata GetFirst()
{
    Type relatedType = Type.GetType("Extender.First");
    TableMetadata[] result = this.GetRelatedTableData(relatedType);
    return result.Length > 0 ? result[0] : null; 
}
}
}
