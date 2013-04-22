using System;
using System.Data;
using voidsoft.DataBlock;
namespace Extender
{
public class BookPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public BookPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }
      
      public BookPersistentObject(Session session, TableMetadata mainTable) : base(session, mainTable)
      {
      }

    public BookPersistentObject(TableMetadata m)
        : base(m)
    {

    }
      
}
}
