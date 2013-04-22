using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

    [Serializable()]
    public class CategoryTableMetadata : TableMetadata
    {

        public enum CategoriesFields
        {
            CategoryID,
            CategoryName,
            Description,
            Picture
        }


        private DatabaseField[] _fields;

        public CategoryTableMetadata()
        {
            _fields = new DatabaseField[4];
            _fields[0] = new DatabaseField(DbType.Int32, "CategoryID", true, true, null);
            _fields[1] = new DatabaseField(DbType.String, "CategoryName", false, false, null);
            _fields[2] = new DatabaseField(DbType.String, "Description", false, false, null);
            _fields[3] = new DatabaseField(DbType.Binary, "Picture", false, false, null);

            this.currentTableName = "Categories";


        }


        public override DatabaseField[] TableFields
        {
            get { return _fields; }
            set { _fields = value; }
        }
        public CategoryTableMetadata Clone()
        {
            return this.Clone<CategoryTableMetadata>();
        }

        public System.Int32? CategoryID
        {
            get
            {
                return (System.Int32?) (this.GetField("CategoryID")).fieldValue;
            }

            set
            {
                this.SetFieldValue("CategoryID", value);
            }
        }


        public System.String CategoryName
        {
            get
            {
                return (System.String) (this.GetField("CategoryName")).fieldValue;
            }

            set
            {
                this.SetFieldValue("CategoryName", value);
            }
        }


        public System.String Description
        {
            get
            {
                return (System.String) (this.GetField("Description")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Description", value);
            }
        }


        public System.Byte[] Picture
        {
            get
            {
                object result = (this.GetField("Picture")).fieldValue;
                if (result == null)
                {
                    return new System.Byte[0];
                }

                return (System.Byte[]) (this.GetField("Picture")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Picture", value);
            }
        }

    }
}
