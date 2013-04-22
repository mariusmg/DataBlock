

using System;
using System.Data;
using voidsoft.DataBlock;


namespace Extender
{
    public class BlobPersistentObject : voidsoft.DataBlock.PersistentObject
    {

        public BlobPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable)
            : base(database, connectionString, mainTable)
        {
        }

        public BlobPersistentObject(Session s, TableMetadata main)
            : base(s, main)
        {

        }

        public BlobPersistentObject(TableMetadata t)
            : base(t)
        {
        }
    }
}




