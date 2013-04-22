/*

       file: Schema.cs
description: Database schema loader.


*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// The supported RDBMS schema
    /// </summary>
    public class Schema
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<string> GetTableList(DatabaseServer server,
                                                    string connectionString)
        {
            ISchemaLoader loader = null;

            switch (server)
            {
                case DatabaseServer.Access:
                    loader = new AccessSchemaLoader();
                    return loader.GetTableList(connectionString);
                case DatabaseServer.SqlServer:
                    loader = new SqlServerSchemaLoader();
                    return loader.GetTableList(connectionString);
                case DatabaseServer.MySql:
                    loader = new MySqlSchemaLoader();
                    return loader.GetTableList(connectionString);
                case DatabaseServer.PostgreSQL:
                    loader = new PostgreSqlSchemaLoader();
                    return loader.GetTableList(connectionString);

                default:
                    throw new Exception();
            }
        }


        public static DatabaseColumn[] GetColumnInfo(DatabaseServer server,
                                                 string tableName,
                                                 string connectionString)
        {
            ISchemaLoader loader = null;

            switch (server)
            {
                case DatabaseServer.Access:
                    loader = new AccessSchemaLoader();
                    return loader.GetColumnInfo(tableName, connectionString);

                case DatabaseServer.SqlServer:
                    loader = new SqlServerSchemaLoader();
                    return loader.GetColumnInfo(tableName, connectionString);

                case DatabaseServer.MySql:
                    loader = new MySqlSchemaLoader();
                    return loader.GetColumnInfo(tableName, connectionString);

                case DatabaseServer.PostgreSQL:
                    loader = new PostgreSqlSchemaLoader();
                    return loader.GetColumnInfo(tableName, connectionString);


                default:
                    throw new Exception();
            }
        }
    }
}