/*
       file: PersistentObjectGenerator.cs
description: C# data access object generator
     author: Marius Gheorghe

*/

using System.IO;

namespace voidsoft.DataBlockModeler
{
    public class CSharpDataAccessObjectGenerator : IDataAccessObjectGenerator
    {
        #region IDataAccessObjectGenerator Members

        /// <summary>
        /// Generates the persistent object.
        /// </summary>
        /// <param name="entityName">Name of the object.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="fileName">Name of the file.</param>
        public void GenerateDataAccessObjects(string entityName,  string namespaceName, string fileName)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                entityName = Utilies.RemoveEmptySpaces(entityName);
                namespaceName = Utilies.RemoveEmptySpaces(namespaceName);

                if (!fileName.EndsWith(".cs"))
                {
                    fileName = fileName + ".cs";
                }

                if (File.Exists(fileName))
                {
                    fs = new FileStream(fileName, FileMode.Truncate, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                }

                sw = new StreamWriter(fs);

                sw.WriteLine("using System;");
                sw.WriteLine("using System.Data;");
                sw.WriteLine("using voidsoft.DataBlock;");
                sw.WriteLine("using System.Collections;");
                sw.WriteLine("using " + namespaceName + ";");

                sw.WriteLine("namespace DataAccess"); //use BusinessObjects as the namespace name
                sw.WriteLine("{");
                sw.WriteLine("public class " + entityName + "DataAccess");
                sw.WriteLine("{ ");
                sw.WriteLine("");
                sw.WriteLine("       private " + entityName + " domainObject = new " + entityName + "();");
                sw.WriteLine("       private PersistentObject persistent = null; ");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("      #region Constructors");
                sw.WriteLine("      public " + entityName + "DataAccess(DatabaseServer database, string connectionString) ");
                sw.WriteLine("      {");
                sw.WriteLine("           persistent = new PersistentObject(database, connectionString, domainObject);");
                sw.WriteLine("      }");
                sw.WriteLine("");
                sw.WriteLine("      ");
                sw.WriteLine("      public " + entityName + "DataAccess(Session session)");
                sw.WriteLine("      {");
                sw.WriteLine("           persistent = new PersistentObject(session, domainObject);");
                sw.WriteLine("      }");
                sw.WriteLine("");
                sw.WriteLine("      public " + entityName + "DataAccess()");
                sw.WriteLine("      {");
                sw.WriteLine("           persistent = new PersistentObject(domainObject);");
                sw.WriteLine("      }");
                sw.WriteLine("		#endregion");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("       #region generated implementation");

                #region generate Read data

                //generate GetDataTable && GetDataSet
                sw.WriteLine("   public DataTable GetDataTable()");
                sw.WriteLine("	 {");
                sw.WriteLine("        return persistent.GetDataTable();");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("   public DataTable GetDataTable(QueryCriteria qc)");
                sw.WriteLine("	 {");
                sw.WriteLine("        return persistent.GetDataTable(qc);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("   public DataTable GetDataTable(params DatabaseField[] fields)");
                sw.WriteLine("	 {");
                sw.WriteLine("        return persistent.GetDataTable(fields);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                //generate GetTableMetadata
                sw.WriteLine("   public " + entityName + "[] Get" + entityName + "()");
                sw.WriteLine("	 {");
                sw.WriteLine("        	 return (" + entityName + "[])" + "persistent.GetTableMetadata();");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("   public " + entityName + " Get" + entityName + "(object primaryKeyValue)");
                sw.WriteLine("   {");
                sw.WriteLine("      	return(" + entityName + ")" + "persistent.GetTableMetadata(primaryKeyValue);");
                sw.WriteLine("   }");
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("   public " + entityName + "[] Get" + entityName + "(QueryCriteria qc)");
                sw.WriteLine("   {");
                sw.WriteLine("      	return(" + entityName + "[])" + "persistent.GetTableMetadata(qc);");
                sw.WriteLine("   }");
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("   public  Array" + " Get" + entityName + "(string relatedTableName, Type classType, object foreignKeyValue)");
                sw.WriteLine("   {");
                sw.WriteLine("     		return persistent.GetTableMetadata(relatedTableName, classType, foreignKeyValue);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                //Generate ArrayList
                sw.WriteLine("   public ArrayList GetFieldList(QueryCriteria criteria)");
                sw.WriteLine("	 {");
                sw.WriteLine("         return persistent.GetFieldList(criteria);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("   public ArrayList GetFieldList(DatabaseField field)");
                sw.WriteLine("	 {");
                sw.WriteLine("        	 return persistent.GetFieldList(field);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");


                //generate GetSingleValue
                sw.WriteLine("   public object GetValue (QueryCriteria criteria)");
                sw.WriteLine("	 {");
                sw.WriteLine("         return persistent.GetValue(criteria);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");


                //generate IsUnique
                sw.WriteLine("   public bool IsUnique (DatabaseField field, object value)");
                sw.WriteLine("	 {");
                sw.WriteLine("        	 return persistent.IsUnique(field, value);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                //generate intrinsec functions
                sw.WriteLine("	public object GetMax(DatabaseField field)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.GetMax(field);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("	public object GetMin(DatabaseField field)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.GetMin(field);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("	public object GetCount()");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.GetCount();");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                #region generate Create

                sw.WriteLine("	public int Create(" + entityName + " domainObjectObject)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Create(domainObjectObject);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                #region generate Update

                sw.WriteLine("	public int Update(" + entityName + " domainObjectObject)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Update(domainObjectObject);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("	public int Update(QueryCriteria criteria)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Update(criteria);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                #region generate Delete

                sw.WriteLine("	public int Delete(" + entityName + " domainObjectObject)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Delete(domainObjectObject);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("	public int Delete(QueryCriteria criteria)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Delete(criteria);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("	public int Delete(object id)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Delete(id);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                sw.WriteLine("       #endregion");

                sw.WriteLine(" ");


                sw.WriteLine("   }"); //end class
                sw.WriteLine("}"); //end namespace.


                sw.Flush();
                sw.Close();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion

       
    }
}