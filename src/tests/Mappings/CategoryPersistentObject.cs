

using System;
using System.Data;
using voidsoft.DataBlock;


namespace Extender
{
    public class CategoryPersistentObject : voidsoft.DataBlock.PersistentObject
    {

        public CategoryPersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable)
        {
        }

        public CategoryPersistentObject(Session s, TableMetadata main) : base(s, main)
        {

        }

        public CategoryPersistentObject(TableMetadata t)
            : base(t)
        {
        }


        protected override void BeforeExecutingQueries(Operation operation, 
                                                        ref System.Collections.Generic.List<ExecutionQuery> listQueries)
        {

            foreach (ExecutionQuery var in listQueries)
            {
                Console.WriteLine(var);
            }

        }  
    }
}




