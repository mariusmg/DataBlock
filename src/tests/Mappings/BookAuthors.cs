using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

      [Serializable()]
		public class BookAuthors : TableMetadata
		{

                   public enum BookAuthorsFields
                   {
                      Id,
                      AuthorId,
                      BookId
                  }


			    private DatabaseField[] _fields;

		    	public BookAuthors()
			    {
					    _fields = new DatabaseField[3];
                    _fields[0] = new DatabaseField(DbType.Int32,"Id",true,true,null);
                    _fields[1] = new DatabaseField(DbType.Int32,"AuthorId",false,false,null);
                    _fields[2] = new DatabaseField(DbType.Int32,"BookId",false,false,null);
 
                        this.currentTableName = "BookAuthors";

this.listRelations.Add(new ChildTableRelation("Author",TableRelationCardinality.OneToOne,"AuthorId","AuthorId"));

                  }


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}
          public BookAuthors Clone()
          {
                 return this.Clone<BookAuthors>();
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


public System.Int32? AuthorId
{
    get
    {
          return (System.Int32? ) (this.GetField("AuthorId")).fieldValue;
    }

    set
    {
          this.SetFieldValue("AuthorId", value);
    }
}


public System.Int32? BookId
{
    get
    {
          return (System.Int32? ) (this.GetField("BookId")).fieldValue;
    }

    set
    {
          this.SetFieldValue("BookId", value);
    }
}

public TableMetadata GetAuthor()
{
    Type relatedType = Type.GetType("Extender.Author");
    TableMetadata[] result = this.GetRelatedTableData(relatedType);
    return result.Length > 0 ? result[0] : null; 
}
}
}
