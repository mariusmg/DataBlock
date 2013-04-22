using System.Collections.Specialized;

namespace voidsoft.DataBlockModeler
{
    public static class ContextData
    {
        public static DatabaseTable[] tables;
        public static string LastUsedNamespace;
        public static string currentConnectionString;
        public static EDatabaseServer currentDatabaseServer;

        /// <summary>
        /// Tables list
        /// </summary>
        public static StringCollection scTableNames;
    }
}