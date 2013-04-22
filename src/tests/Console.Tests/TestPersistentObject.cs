using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderTest
{
public class TestPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public TestPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }
      
      public TestPersistentObject(Session session, TableMetadata mainTable) : base(session, mainTable)
      {
      }
      
}
}
