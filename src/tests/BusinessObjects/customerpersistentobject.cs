using System;
using System.Data;
using voidsoft.DataBlock;
namespace Extender
{
public class CustomerPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public CustomerPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }
      
      public CustomerPersistentObject(Session session, TableMetadata mainTable) : base(session, mainTable)
      {
      }

    public CustomerPersistentObject(TableMetadata m)
        : base(m)
    {

    }
      
}
}
