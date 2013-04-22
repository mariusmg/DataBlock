using System;
using System.Data;
using voidsoft.DataBlock;

namespace BookManager.Mappings
{

    [Serializable()]
    public class Author : TableMetadata
    {
        private DatabaseField[] _fields;

        public Author()
        {
            _fields = new DatabaseField[4];
            _fields[0] = new DatabaseField(DbType.Int32, "AuthorId", true, true, null);
            _fields[1] = new DatabaseField(DbType.String, "Name", false, false, null);
            _fields[2] = new DatabaseField(DbType.Int32, "Age", false, false, null);
            _fields[3] = new DatabaseField(DbType.String, "Location", false, false, null);

            this.currentTableName = "Author";

            this.listRelations.Add(new ParentTableRelation("BookAuthors", "AuthorId", TableRelationCardinality.OneToMany, true));
            this.listRelations.Add(new ManyToManyTableRelation("Book", "BookAuthors", "AuthorId", "BookId"));

        }


        public override DatabaseField[] TableFields
        {
            get
            {
                return _fields;
            }
            set
            {
                _fields = value;
            }
        }

        public System.Int32 AuthorId
        {
            get
            {
                return (System.Int32)(this.GetField("AuthorId")).fieldValue;
            }

            set
            {
                this.SetFieldValue("AuthorId", value);
            }
        }


        public System.String Name
        {
            get
            {
                return (System.String)(this.GetField("Name")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Name", value);
            }
        }


        public System.Int32 Age
        {
            get
            {
                return (System.Int32)(this.GetField("Age")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Age", value);
            }
        }


        public System.String Location
        {
            get
            {
                return (System.String)(this.GetField("Location")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Location", value);
            }
        }

        public Array GetBookAuthors()
        {
            Type relatedType = Type.GetType("BookManager.Mappings.BookAuthors");
            return this.GetRelatedTableData(relatedType);
        }
        public Array GetBook()
        {
            Type relatedType = Type.GetType("BookManager.Mappings.Book");
            Type intermediaryType = Type.GetType("BookManager.Mappings.BookAuthors");
            return this.GetRelatedTableData(relatedType, intermediaryType);
        }
    }
}
