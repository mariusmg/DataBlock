
using System;
using System.Text;


namespace tests
{
    public class SharedData
    {
        
        public SharedData()
        {
        }

        public static string mysql_odbc = @"DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=test;USER=sa;PASSWORD=;";
        public static string mysql = @"Host=localhost; UserName=sa; Password="";Database=test;";
        public static string sqlserver = @"Server=ALEGRETTOPC\SQLEXPRESS;user=sas;password=1234;Database=test";
        public static string access = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=test.mdb";
        public static string postgresql = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=1234;Database=test;";
        public static string postgresql_odbc = "DRIVER={PostgreSQL};SERVER=127.0.0.1;port=5432;DATABASE=test;UID=postgres;PWD=1234;"; 

        public static string sqlserver2005 = @"Server=SERVER\SQLEXPRESS;user=sas;password=1234;Database=Test";
        
        public static string LARGE_BLOB_FILE_PATH = "u.jpg";
    }
}
