using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderEmployees
{
    public class EmployeesPersistentObject : voidsoft.DataBlock.PersistentObject
    {

        public EmployeesPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable)
        {
        }

    }
}
