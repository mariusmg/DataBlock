using System.Collections.Generic;

namespace voidsoft.DataBlockModeler
{
    public static class GeneratorContext
    {
        private static List<DatabaseTable> databaseTables;

        static GeneratorContext()
        {
            databaseTables = new List<DatabaseTable>();
        }

        public static List<DatabaseTable> CurrentDatabaseTables
        {
            get
            {
                return databaseTables;
            }

            set
            {
                databaseTables = value;
            }
        }


        public static DatabaseServer currentDatabaseServer;
        public static string currentConnectionString;
    }
}