using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderRegion
{
public class RegionPersistentObject : voidsoft.DataBlock.PersistentObject
{ 
 
      public RegionPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable) 
      {
      }

    public RegionPersistentObject(Session s , TableMetadata mainTable) : base (s,mainTable)
    {
    }

}
}
