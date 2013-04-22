using System;
using System.Data;
using voidsoft.DataBlock;
namespace BookManager.BusinessFacade
{
    public class AuthorPersistentObject : voidsoft.DataBlock.PersistentObject
    {

        public AuthorPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable)
            : base(database, connectionString, mainTable)
        {
        }

        public AuthorPersistentObject(Session session, TableMetadata mainTable)
            : base(session, mainTable)
        {
        }
        public AuthorPersistentObject(TableMetadata mainTable)
            : base(mainTable)
        {
        }

    }
}
