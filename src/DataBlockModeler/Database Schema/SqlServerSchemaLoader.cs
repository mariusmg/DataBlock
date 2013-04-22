using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace voidsoft.DataBlockModeler
{
    internal class SqlServerSchemaLoader : ISchemaLoader
    {


        /// <summary>
        /// Gets the column info.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public DatabaseColumn[] GetColumnInfo(string tableName, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand ocmd = null;
            StringCollection scData = null;
            IDataReader iread = null;

            DatabaseColumn[] info = null;

            try
            {


                connection = new SqlConnection(connectionString);
                ocmd = new SqlCommand();


                ocmd.CommandText = "SELECT * FROM " + GetTableName(tableName);

                ocmd.Connection = connection;


                scData = new StringCollection();

                connection.Open();



                iread = ocmd.ExecuteReader(CommandBehavior.KeyInfo);

                DataTable dt = iread.GetSchemaTable();
                iread.Close();

                info = new DatabaseColumn[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    info[i] = new DatabaseColumn(dt.Rows[i]["ColumnName"].ToString(), dt.Rows[i]["DataType"].ToString(), Convert.ToBoolean(dt.Rows[i]["IsKey"]), Convert.ToBoolean(dt.Rows[i]["IsAutoIncrement"]), Convert.ToBoolean(dt.Rows[i]["AllowDBNull"]), Convert.ToInt32(dt.Rows[i]["ColumnSize"]));
                }

                return info;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// Gets the SQL Server table's list for the specified database
        /// </summary>
        public List<string> GetTableList(string connectionString)
        {
            SqlConnection con = null;
            SqlDataAdapter dap = null;
            DataTable dt = null;
            List<string> scData = null;

            try
            {
                con = new SqlConnection(connectionString);
                scData = new List<string>();

                //INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                dap = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'", con);

                dt = new DataTable();

                con.Open();

                dap.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    scData.Add(dt.Rows[i]["TABLE_SCHEMA"].ToString() + "." + dt.Rows[i][2].ToString());
                }

                return scData;
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

                if (dap != null)
                {
                    dap.Dispose();
                }

                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        public void GetTableRelations(string connectionString)
        {
            SqlConnection connection = null;
            SqlDataAdapter dap = null;
            DataTable dt = null;
            List<string> listData = null;

            try
            {

                listData = new List<string>();

                connection = new SqlConnection(connectionString);
               
                dap = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'", connection);

                dt = new DataTable();

                connection.Open();

                dap.Fill(dt);

                connection.Close();






            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }



        }


        private string GetTableName(string name)
        {
            string var;


            if (!name.Contains("."))
            {
                var = name;
            }
            else
            {
                string[] parts = name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder builder = new StringBuilder();

                builder.Append("[" + parts[0] + "].");

                builder.Append("[");

                //this is done to avoid problems with table names that have "." in their name
                for (int i = 1; i < parts.Length; i++)
                {
                    builder.Append(parts[i]);
                }

                builder.Append("]");


                var = builder.ToString();
            }


            return var;
        }
    }
}