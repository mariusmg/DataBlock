using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

      [Serializable()]
		public class Book : TableMetadata
		{

                   public enum BookFields
                   {
                      BookId,
                      Name,
                      Pages,
                      ISBN,
                      Genre,
                      Grade
                  }


			    private DatabaseField[] _fields;

		    	public Book()
			    {
					    _fields = new DatabaseField[6];
                    _fields[0] = new DatabaseField(DbType.Int32,"BookId",true,true,null);
                    _fields[1] = new DatabaseField(DbType.String,"Name",false,false,null);
                    _fields[2] = new DatabaseField(DbType.Int32,"Pages",false,false,null);
                    _fields[3] = new DatabaseField(DbType.String,"ISBN",false,false,null);
                    _fields[4] = new DatabaseField(DbType.String,"Genre",false,false,null);
                    _fields[5] = new DatabaseField(DbType.Int16,"Grade",false,false,null);
 
                        this.currentTableName = "Book";

this.listRelations.Add(new ManyToManyTableRelation("Author","BookAuthors","BookId","AuthorId"));

                  }


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}
          public Book Clone()
          {
                 return this.Clone<Book>();
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


public System.Int32? Pages
{
    get
    {
          return (System.Int32? ) (this.GetField("Pages")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Pages", value);
    }
}


public System.String ISBN
{
    get
    {
          return (System.String) (this.GetField("ISBN")).fieldValue;
    }

    set
    {
          this.SetFieldValue("ISBN", value);
    }
}


public System.String Genre
{
    get
    {
          return (System.String) (this.GetField("Genre")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Genre", value);
    }
}


public System.Int16? Grade
{
    get
    {
          return (System.Int16? ) (this.GetField("Grade")).fieldValue;
    }

    set
    {
          this.SetFieldValue("Grade", value);
    }
}

public Array GetAuthor()
{
    Type relatedType = Type.GetType("Extender.Author");
    Type intermediaryType = Type.GetType("Extender.BookAuthors");
    return this.GetRelatedTableData(relatedType, intermediaryType);
}
}
}
