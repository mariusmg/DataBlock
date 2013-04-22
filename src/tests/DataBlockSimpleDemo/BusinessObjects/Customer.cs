using System;
using System.Data;
using voidsoft.DataBlock;

namespace voidsoft.Mappings
{

    [Serializable()]
    public class Customer : TableMetadata
    {

        public enum CustomerFields
        {
            CustomerId,
            Name,
            Age,
            LastName
        }


        private DatabaseField[] _fields;

        public Customer()
        {
            _fields = new DatabaseField[4];
            _fields[0] = new DatabaseField(DbType.Int32, "CustomerId", true, true, null);
            _fields[1] = new DatabaseField(DbType.String, "Name", false, false, null);
            _fields[2] = new DatabaseField(DbType.Int16, "Age", false, false, null);
            _fields[3] = new DatabaseField(DbType.String, "LastName", false, false, null);

            this.currentTableName = "Customer";


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
        public Customer Clone()
        {
            return this.Clone<Customer>();
        }

        public System.Int32? CustomerId
        {
            get
            {
                return (System.Int32?) (this.GetField("CustomerId")).fieldValue;
            }

            set
            {
                this.SetFieldValue("CustomerId", value);
            }
        }


        public System.String Name
        {
            get
            {
                object result = this.GetField("Name").fieldValue;
                return (result != null) ? result.ToString() : null;
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
                return (System.Int16?) (this.GetField("Age")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Age", value);
            }
        }



        public System.String LastName
        {
            get
            {
                object result = this.GetField("LastName").fieldValue;
                return (result != null) ? result.ToString() : null;
            }

            set
            {
                this.SetFieldValue("LastName", value);
            }
        }

    }
}
