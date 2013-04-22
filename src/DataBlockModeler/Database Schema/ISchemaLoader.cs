using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace voidsoft.DataBlockModeler
{
    interface ISchemaLoader
    {

        /// <summary>
        /// Gets the table list.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        List<string> GetTableList(string connectionString);

        /// <summary>
        /// Gets the column info.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        DatabaseColumn[] GetColumnInfo(string tableName, string connectionString);
    }
}
