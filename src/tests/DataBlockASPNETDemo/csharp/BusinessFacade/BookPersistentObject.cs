using System;
using System.Data;
using voidsoft.DataBlock;
namespace BookManager.BusinessFacade
{
    public class BookPersistentObject : voidsoft.DataBlock.PersistentObject
    {

        public BookPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable)
            : base(database, connectionString, mainTable)
        {
        }

        public BookPersistentObject(Session session, TableMetadata mainTable)
            : base(session, mainTable)
        {
        }
        public BookPersistentObject(TableMetadata mainTable)
            : base(mainTable)
        {
        }

    }
}
