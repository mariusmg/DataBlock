using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderOrders
{
public class OrderPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public OrderPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }
      
      public OrderPersistentObject(Session session, TableMetadata mainTable) : base(session, mainTable)
      {
      }
      
}
}
