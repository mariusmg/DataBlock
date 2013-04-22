using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{


    public class FirstTableMetadata : TableMetadata
    {
        private DatabaseField[] _fields;

        public FirstTableMetadata()
        {
            _fields = new DatabaseField[2];
            _fields[0] = new DatabaseField(DbType.Int32, "Id", true, true, null);
            _fields[1] = new DatabaseField(DbType.String, "Name", false, false, null);

            this.currentTableName = "First";

            this.listRelations.Add(new ParentTableRelation("Second", "FirstId", TableRelationCardinality.OneToMany, true));



        }


        public override DatabaseField[] TableFields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public System.Int32 Id
        {
            get
            {
                return (System.Int32)(this.GetField("Id")).fieldValue;
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

        public void AddSecond(SecondTableMetadata s)
        {
            this.AttachTableMetadata(s);
        }

        public SecondTableMetadata[] GetSecond()
        {
            SecondTableMetadata ss = new SecondTableMetadata();

            return (SecondTableMetadata[]) this.GetRelatedTableData(typeof(SecondTableMetadata));

        }

        public void RemoveSecond(SecondTableMetadata s)
        {
            this.RemoveTableMetadata(s);
        }
    }


    public class SecondTableMetadata : TableMetadata
    {
        private DatabaseField[] _fields;


        public ThirdTableMetadata[] GetThird()
        {
            ThirdTableMetadata ss = new ThirdTableMetadata();

            return (ThirdTableMetadata[])this.GetRelatedTableData(typeof(ThirdTableMetadata));

        }


        public SecondTableMetadata()
        {
            _fields = new DatabaseField[4];
            _fields[0] = new DatabaseField(DbType.Int32, "Id", true, true, null);
            _fields[1] = new DatabaseField(DbType.Int32, "FirstId", false, false, null);
            _fields[2] = new DatabaseField(DbType.Int32, "Age", false, false, null);
            _fields[3] = new DatabaseField(DbType.Int32, "Quantity", false, false, null);


            this.listRelations.Add(new ParentTableRelation("Third", "SecondId", TableRelationCardinality.OneToMany, true));




            this.currentTableName = "Second";



        }


        public override DatabaseField[] TableFields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public System.Int32 Id
        {
            get
            {
                return (System.Int32)(this.GetField("Id")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Id", value);
            }
        }


        public System.Int32 FirstId
        {
            get
            {
                return (System.Int32)(this.GetField("FirstId")).fieldValue;
            }

            set
            {
                this.SetFieldValue("FirstId", value);
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


        public System.Int32 Quantity
        {
            get
            {
                return (System.Int32)(this.GetField("Quantity")).fieldValue;
            }

            set
            {
                this.SetFieldValue("Quantity", value);
            }
        }



        public void AddThird(ThirdTableMetadata t)
        {
            this.AttachTableMetadata(t);
        }

    }


    public class ThirdTableMetadata : TableMetadata
    {
        private DatabaseField[] _fields;

        public ThirdTableMetadata()
        {
            _fields = new DatabaseField[3];
            _fields[0] = new DatabaseField(DbType.Int32, "Id", true, true, null);
            _fields[1] = new DatabaseField(DbType.String, "ThirdStuff", false, false, null);
            _fields[2] = new DatabaseField(DbType.Int32, "SecondId", false, false, null);

            this.currentTableName = "Third";


        }


        public override DatabaseField[] TableFields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public System.Int32 Id
        {
            get
            {
                return (System.Int32)(this.GetField("Id")).fieldValue;
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
                return (System.String)(this.GetField("ThirdStuff")).fieldValue;
            }

            set
            {
                this.SetFieldValue("ThirdStuff", value);
            }
        }


        public System.Int32 SecondId
        {
            get
            {
                return (System.Int32)(this.GetField("SecondId")).fieldValue;
            }

            set
            {
                this.SetFieldValue("SecondId", value);
            }
        }

    }
}
