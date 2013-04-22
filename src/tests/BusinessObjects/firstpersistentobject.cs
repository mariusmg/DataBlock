using System;
using System.Data;
using voidsoft.DataBlock;
namespace Extender
{
public class FirstPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public FirstPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }
      
      public FirstPersistentObject(Session session, TableMetadata mainTable) : base(session, mainTable)
      {
      }

    public FirstPersistentObject(TableMetadata main)       : base(main)
    {

    }
}
}
