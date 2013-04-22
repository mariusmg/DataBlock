using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;

namespace voidsoft.DataBlockModeler
{
    public class AccessSchemaLoader : ISchemaLoader
    {
        /// <summary>
        /// Gets the column info.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Array of DatabaseColumn</returns>
        public DatabaseColumn[] GetColumnInfo(string tableName, string connectionString)
        {
            OleDbConnection ocon = null;
            OleDbCommand ocmd = null;
            IDataReader iread = null;
            DatabaseColumn[] info = null;

            try
            {
                ocon = new OleDbConnection(connectionString);

                ocmd = new OleDbCommand();
                ocmd.CommandText = "SELECT * FROM [" + tableName + "]";
                ocmd.Connection = ocon;

                ocon.Open();

                //DataTable result2 = ocon.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });
                iread = ocmd.ExecuteReader(CommandBehavior.SchemaOnly);

                DataTable dt = iread.GetSchemaTable();
                iread.Close();

                info = new DatabaseColumn[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    info[i] = new DatabaseColumn(dt.Rows[i]["ColumnName"].ToString(), dt.Rows[i]["DataType"].ToString(), false, false, Convert.ToBoolean(dt.Rows[i]["IsAutoincrement"]), 0);
                }
                iread.Close();

                //get the primary key now
                DataTable result = ocon.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] {null, null, tableName});

                string pkName = string.Empty;
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    pkName = result.Rows[i]["Column_Name"].ToString();

                    for (int j = 0; j < info.Length; j++)
                    {
                        if (info[j].Name == pkName)
                        {
                            info[j].isPrimaryKey = true;
                            break;
                        }
                    }
                }


                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ocon != null && ocon.State != ConnectionState.Closed)
                {
                    ocon.Close();
                }

                if (iread != null && !iread.IsClosed)
                {
                    iread.Close();
                }
            }
        }


        public List<string> GetTableList(string connectionString)
        {
            OleDbConnection ocon = null;
            List<string> list = null;


            try
            {
                ocon = new OleDbConnection(connectionString);
                list = new  List<string>();

                ocon.Open();
                DataTable dt = ocon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][3].ToString() == "TABLE")
                    {
                        list.Add(dt.Rows[i][2].ToString());
                        //this.checkedListBox.Items.Add(dt.Rows[i][2].ToString());
                    }
                }

                return list;
            }
            finally
            {
                if (ocon != null && ocon.State != ConnectionState.Closed)
                {
                    ocon.Close();
                }
            }
        }
    }
}