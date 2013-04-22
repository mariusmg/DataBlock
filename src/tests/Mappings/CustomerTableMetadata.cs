using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

    [Serializable()]
    public class CustomerTableMetadata : TableMetadata
    {

        public enum CustomerFields
        {
            Id,
            Name,
            Age,
            Birthdate,
            Male
        }


        private DatabaseField[] _fields;

        public CustomerTableMetadata()
        {
            _fields = new DatabaseField[5];
            _fields[0] = new DatabaseField(DbType.Int32, "Id", true, true, null);
            _fields[1] = new DatabaseField(DbType.String, "Name", false, false, null);
            _fields[2] = new DatabaseField(DbType.Int16, "Age", false, false, null);
            _fields[3] = new DatabaseField(DbType.DateTime, "Birthdate", false, false, null);
            _fields[4] = new DatabaseField(DbType.Boolean, "Male", false, false, null);

            this.currentTableName = "Customer";


        }


        public override DatabaseField[] TableFields
        {
            get { return _fields; }
            set { _fields = value; }
        }
        public CustomerTableMetadata Clone()
        {
            return this.Clone<CustomerTableMetadata>();
        }

        public System.Int32? Id
        {
            get
            {
                return (System.Int32?)(this.GetField("Id")).fieldValue;
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
                return (System.String)(this.GetField("Name")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Name", value);
            }
        }


        public System.Int16? Age
        {
            get
            {
                return (System.Int16?)(this.GetField("Age")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Age", value);
            }
        }


        public System.DateTime? Birthdate
        {
            get
            {
                return (System.DateTime?)(this.GetField("Birthdate")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Birthdate", value);
            }
        }


        public System.Boolean? Male
        {
            get
            {
                return (System.Boolean?)(this.GetField("Male")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Male", value);
            }
        }

    }
}
