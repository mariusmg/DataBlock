using System.IO;

namespace voidsoft.DataBlockModeler
{
    public class CSharpBusinessObjectGenerator : IBusinessObjectGenerator
    {
        public void GenerateBusinessObjects(string objectName, string namespaceName, string fileName)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                objectName = Utilies.RemoveEmptySpaces(objectName);
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

                sw.WriteLine("namespace " + namespaceName); 
                sw.WriteLine("{");
                sw.WriteLine("public partial class " + objectName + "BusinessObject");
                sw.WriteLine("{ ");
                sw.WriteLine("");
                sw.WriteLine("       private " + objectName + " domainObject = new " + objectName + "();");
                sw.WriteLine("       private PersistentObject persistent = null; ");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("      #region Constructors");
                sw.WriteLine("      public " + objectName + "BusinessObject(DatabaseServer database, string connectionString) ");
                sw.WriteLine("      {");
                sw.WriteLine("           persistent = new PersistentObject(database, connectionString, domainObject);");
                sw.WriteLine("      }");
                sw.WriteLine("");
                sw.WriteLine("      ");
                sw.WriteLine("      public " + objectName + "BusinessObject(Session session)");
                sw.WriteLine("      {");
                sw.WriteLine("           persistent = new PersistentObject(session, domainObject);");
                sw.WriteLine("      }");
                sw.WriteLine("");
                sw.WriteLine("      public " + objectName + "BusinessObject()");
                sw.WriteLine("      {");
                sw.WriteLine("           persistent = new PersistentObject(domainObject);");
                sw.WriteLine("      }");
                sw.WriteLine("		#endregion");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("       #region generated implementation");

                #region generate Read data

                sw.WriteLine("   public DataTable GetDataTable()");
                sw.WriteLine("	 {");
                sw.WriteLine("        return persistent.GetDataTable();");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

/*
                sw.WriteLine("   public DataTable GetDataTable(QueryCriteria qc)");
                sw.WriteLine("	 {");
                sw.WriteLine("        return persistent.GetDataTable(qc);");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");
*/
                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("   public " + objectName + "[] Get" + objectName + "()");
                sw.WriteLine("	 {");
                sw.WriteLine("        	 return (" + objectName + "[])" + "persistent.GetTableMetadata();");
                sw.WriteLine("	 }");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("   public " + objectName + " Get" + objectName + "(object primaryKeyValue)");
                sw.WriteLine("   {");
                sw.WriteLine("      	return(" + objectName + ")" + "persistent.GetTableMetadata(primaryKeyValue);");
                sw.WriteLine("   }");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("");
                sw.WriteLine("");


                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                #region generate Create

                sw.WriteLine("	public int Create(" + objectName + " entity)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Create(entity);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");

                #endregion

                #region generate Update

                sw.WriteLine("	public int Update(" + objectName + " entity)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Update(entity);");
                sw.WriteLine("	}");
                sw.WriteLine("");
                sw.WriteLine("");


                #endregion

                #region generate Delete

                sw.WriteLine("	public int Delete(" + objectName + " entity)");
                sw.WriteLine("	{");
                sw.WriteLine("		return persistent.Delete(entity);");
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
    }
}