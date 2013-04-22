using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderCategories
{
public class CategoriesPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public CategoriesPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }
      
      public CategoriesPersistentObject(Session session, TableMetadata mainTable) : base(session, mainTable)
      {
      }
      
}
}
