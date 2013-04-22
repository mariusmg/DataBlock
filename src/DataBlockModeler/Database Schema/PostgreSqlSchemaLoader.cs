


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;

namespace voidsoft.DataBlockModeler
{
    internal class PostgreSqlSchemaLoader : ISchemaLoader
    {
        #region ISchemaLoader Members

        private static Dictionary<string, string> listDataTypes = null;
        private static object lockedSearch = new object();
        private static object lockedLoader = new object();


        private void LoadDataTypesMapper(string connectionString)
        {
            OdbcConnection con = null;
            OdbcCommand cmd = null;
            DataTable table;

            try
            {
                lock (lockedLoader)
                {
                    listDataTypes = new Dictionary<string, string>();

                    table = new DataTable();

                    con = new OdbcConnection(connectionString);
                    cmd = new OdbcCommand();
                    cmd.Connection = con;

                    con.Open();

                    DataTable df = con.GetSchema("DataTypes");


                    //HACK : hack to support bool 
                    listDataTypes.Add("bool", "System.Boolean");
                    listDataTypes.Add("bit", "System.Boolean");


                    for (int i = 0; i < df.Rows.Count; i++)
                    {
                        try
                        {
#if DEBUG
                            Console.WriteLine(df.Rows[i][0].ToString() + " mapped to " + df.Rows[i][5].ToString());
#endif

                            //HACK : hack to support char correctly
                            if (df.Rows[i][0].ToString().ToLower() == "char")
                            {
                                listDataTypes.Add("char", "System.Char");
                            }
                            else
                            {
                                //default mapping
                                listDataTypes.Add(df.Rows[i][0].ToString(), df.Rows[i][5].ToString());
                            }
                        }
                        catch
                        {
                            //ignore duplicates
                            continue;
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }



        private string GetExternalType(string type)
        {
            lock (lockedSearch)
            {

                foreach (KeyValuePair<String, string> var in PostgreSqlSchemaLoader.listDataTypes)
                {
                    if (var.Key == type)
                    {
                        return var.Value;
                    }
                }

                return "System.Object";
            }
        }


        public List<string> GetTableList(string connectionString)
        {

            #region 
            //NpgsqlConnection con = null;
            //NpgsqlCommand cmd = null;
            //IDataReader reader = null;

            //StringCollection sc = null;

            //try
            //{
            //    sc = new StringCollection();

            //    con = new NpgsqlConnection(connectionString);
            //    cmd = new NpgsqlCommand();
            //    cmd.Connection = con;

            //    con.Open();


            //    cmd.CommandText = "select oid as id, relname as tablename, relnatts as columncount from pg_class where relkind in ('r') and relname not like 'pg_%' and relname not like 'sql_%'";
            //    reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        sc.Add(reader.GetString(1));
            //    }


            //    return sc;

            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (con != null)
            //    {

            //        con.Clone();
            //        con.Dispose();
            //    }


            //    if (cmd != null)
            //    {
            //        cmd.Dispose();
            //    }
            //}
            #endregion


            OdbcConnection con = null;
            OdbcCommand cmd = null;
            IDataReader reader = null;

            List<string> list = null;

            try
            {
                list = new List<string>();

                con = new OdbcConnection(connectionString);
                cmd = new OdbcCommand();
                cmd.Connection = con;

                con.Open();

                cmd.CommandText = "select oid as id, relname as tablename, relnatts as columncount from pg_class where relkind in ('r') and relname not like 'pg_%' and relname not like 'sql_%'";
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader.GetString(1));
                }


                return list;

            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null)
                {

                    con.Close();
                    con.Dispose();
                }


                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }

        public DatabaseColumn[] GetColumnInfo(string tableName, string connectionString)
        {

            #region 
            //            NpgsqlConnection con = null;
//            NpgsqlCommand cmd = null;
//            IDataReader reader = null;
//            NpgsqlDataAdapter adapter = null;
//            DataSet ds = null;

//            try
//            {
//                con = new NpgsqlConnection(connectionString);
//                cmd = new NpgsqlCommand();
//                adapter = new NpgsqlDataAdapter();
//                ds = new DataSet();
//                cmd.Connection = con;

//                con.Open();
////                cmd.CommandText = " select a.attname as field, t.typname as type, a.attlen as typesize, a.atttypmod as fieldsize, a.attnotnull as notnull, a.atthasdef as hasdefault, d.adsrc as default  from pg_attribute a 	inner join pg_type t on a.atttypid = t.oid 	inner join pg_class c on a.attrelid = c.oid left join pg_attrdef d on c.oid = d.oid and a.attnum = d.adnum 	where a.attnum > 0 and a.attisdropped = 'f' and c.relname = '" + tableName + "'";


//                cmd.CommandText = "SELECT * FROM " + tableName;

//                DataTable dtg = reader.GetSchemaTable();


//                reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);

//                while (reader.Read())
//                {
//                    DataTable dt = reader.GetSchemaTable();

//                }

                
//                //adapter.SelectCommand = cmd;
//                //adapter.Fill(ds);


//                return null;

//            }
//            catch
//            {
//                throw;
//            }
//            finally
//            {

//                if (con != null)
//                {
//                    con.Close();
//                    con.Dispose();
//                }


            //            }
            #endregion

            OdbcConnection con = null;
            OdbcCommand cmd = null;
            DataTable dt = null;
            DatabaseColumn[] columns = null;

            try
            {

                if (PostgreSqlSchemaLoader.listDataTypes == null)
                {
                    this.LoadDataTypesMapper(connectionString);
                }

                con = new OdbcConnection(connectionString);
                cmd = new OdbcCommand();
                cmd.Connection = con;

                con.Open();

                string[] a = { null, null, tableName };

                dt = con.GetSchema("Columns", a);

                columns = new DatabaseColumn[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    columns[i] = new DatabaseColumn();
                    columns[i].Name = dt.Rows[i][3].ToString();
                    columns[i].isAutoIncremented =  (dt.Rows[i][5].ToString() == "0") ? false : true;
                    columns[i].columnDataType = this.GetExternalType(dt.Rows[i][5].ToString());
                    //columns[i].isAutoIncremented = 
                }

                return columns;

            }
            catch
            {
                throw;
            }
            finally
            {

                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }

                if(dt != null)
                {
                    dt.Dispose();
                }

            }

        }

        #endregion
    }
}
