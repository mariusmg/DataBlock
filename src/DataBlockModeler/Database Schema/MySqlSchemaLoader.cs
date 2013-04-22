using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;

namespace voidsoft.DataBlockModeler
{
    internal class MySqlSchemaLoader : ISchemaLoader
    {
        #region ISchemaLoader Members

        public List<string> GetTableList(string connectionString)
        {
            OdbcConnection icon = null;
            OdbcDataReader reader = null;
            OdbcCommand icmd = null;
            List<string> list = null;

            try
            {
                icon = new OdbcConnection(connectionString);
                list = new List<string>();
                icmd = new OdbcCommand();

                icmd.Connection = icon;

                icon.Open();


                icmd.CommandText = "SHOW TABLES";

                reader = icmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }

                reader.Close();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
                if (icon != null && icon.State != ConnectionState.Closed)
                {
                    icon.Close();
                }
            }
        }


        public DatabaseColumn[] GetColumnInfo(string tableName, string connectionString)
        {
            OdbcConnection ocon = null;
            OdbcCommand ocmd = null;
            StringCollection scData = null;
            IDataReader iread = null;

            DatabaseColumn[] info = null;

            try
            {
                ocon = new OdbcConnection(connectionString);
                ocmd = new OdbcCommand();

                ocmd.CommandText = "SELECT * FROM " + tableName;
                ocmd.Connection = ocon;


                scData = new StringCollection();

                ocon.Open();

                iread = ocmd.ExecuteReader(CommandBehavior.SchemaOnly);

                DataTable dt = iread.GetSchemaTable();
                iread.Close();

                info = new DatabaseColumn[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //info[i] = new DatabaseColumn(dt.Rows[i]["ColumnName"].ToString(), dt.Rows[i]["DataType"].ToString(), Convert.ToBoolean(dt.Rows[i]["IsKey"]), Convert.ToBoolean(dt.Rows[i]["IsAutoincrement"]));
                }

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}