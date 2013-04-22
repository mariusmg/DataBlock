using System;
using System.IO;

namespace voidsoft.DataBlockModeler
{
    public class VBDataAccessObjectGenerator : IDataAccessObjectGenerator
    {
        /// <summary>
        /// Generates the persistent object.
        /// </summary>
        /// <param name="entityName">Name of the object.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="fileName">Name of the file.</param>
        public void GenerateDataAccessObjects(string entityName,
                                             string namespaceName,
                                             string fileName)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                entityName = Utilies.RemoveEmptySpaces(entityName);
                namespaceName = Utilies.RemoveEmptySpaces(namespaceName);

                if (!fileName.EndsWith(".vb"))
                {
                    fileName = fileName + ".vb";
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

                sw.WriteLine("Imports System");
                sw.WriteLine("Imports System.Data");
                sw.WriteLine("Imports voidsoft.DataBlock");
                sw.WriteLine("Imports System.Collections");
                sw.WriteLine("Imports " + namespaceName);


                sw.WriteLine("Namespace BusinessObjects"); //use BusinessObjects as the namespace name
                sw.WriteLine(" ");
                sw.WriteLine("Public Class " + entityName + "BusinessObject");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("		Private mapped As New " + entityName + " ()");
                sw.WriteLine("		Private persistent As PersistentObject = Nothing");
                sw.WriteLine("");
                sw.WriteLine("      Public Sub New (database as DatabaseServer,connectionString as String)");
                sw.WriteLine("             persistent = new PersistentObject(database, connectionString, mapped)");
                sw.WriteLine("      End Sub");
                sw.WriteLine("	");
                sw.WriteLine("      Public Sub New (s as Session)");
                sw.WriteLine("             persistent = new PersistentObject(s, mapped)");
                sw.WriteLine("      End Sub");

                sw.WriteLine("      Public Sub New ()");
                sw.WriteLine("              persistent = new PersistentObject(database, mapped)");
                sw.WriteLine("      End Sub");

                #region generate Read Data

                sw.WriteLine("		Public Function GetDataTable() As DataTable");
                sw.WriteLine("			Return persistent.GetDataTable()");
                sw.WriteLine("     End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("		Public Function GetDataTable(criteria as QueryCriteria) As DataTable");
                sw.WriteLine("			Return persistent.GetDataTable(criteria)");
                sw.WriteLine("     End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("		Public Function GetDataTable(fields() as DatabaseField) As DataTable");
                sw.WriteLine("			Return persistent.GetDataTable(criteria)");
                sw.WriteLine("     End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                //generate GetTableMetadata

                sw.WriteLine("		Public Function Get" + entityName + "() As " + entityName + "()");
                sw.WriteLine("			Return CType(persistent.GetTableMetadata(), " + entityName + "()");
                sw.WriteLine("      End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("		Public Function Get" + entityName + "(primaryKeyValue as Object) As " + entityName);
                sw.WriteLine("			Return persistent.GetTableMetadata(primaryKeyValue)");
                sw.WriteLine("      End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("		Public Function Get" + entityName + "(relatedTableName As String, classType As Type, foreignKeyValue As Object) As Array");
                sw.WriteLine("			Return persistent.GetTableMetadata(relatedTableName, classType, foreignKeyValue)");
                sw.WriteLine("      End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                //generate GetFieldList
                sw.WriteLine("		Public Function GetFieldList (criteria As QueryCriteria) As ArrayList");
                sw.WriteLine("			Return persistent.GetFieldList(criteria)");
                sw.WriteLine("      End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("		Public Function GetFieldList (field As DatabaseField) As ArrayList");
                sw.WriteLine("			Return persistent.GetFieldList(field)");
                sw.WriteLine("      End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                //generate GetSingleValue
                sw.WriteLine("		Public Function GetValue (criteria As QueryCriteria) As object");
                sw.WriteLine("			Return persistent.GetValue(criteria)");
                sw.WriteLine("      End Function");
                sw.WriteLine("");
                sw.WriteLine("");

                //generate IsUnique
                sw.WriteLine("   Public IsUnique (DatabaseField field, value As Object) As Boolean");
                sw.WriteLine("        	 Return persistent.IsUnique(field, value)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("");
                sw.WriteLine("");


                //generate intrinsec functions
                sw.WriteLine("   Public GetMax (DatabaseField field) As Object");
                sw.WriteLine("        	 Return persistent.GetMax(field)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("");
                sw.WriteLine("");

                sw.WriteLine("   Public GetMin (DatabaseField field) As Object");
                sw.WriteLine("        	 Return persistent.GetMin(field)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("");

                sw.WriteLine("   Public GetCount () As Object");
                sw.WriteLine("        	 Return persistent.GetCount()");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("");

                #endregion

                //generate Create
                sw.WriteLine("   Public Create (" + entityName + " mappedObject) As Integer");
                sw.WriteLine("        	 Return persistent.Create(mappedObject)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");


                //generate Update
                sw.WriteLine("   Public Update (" + entityName + " mappedObject) As Integer");
                sw.WriteLine("        	 Return persistent.Update(mappedObject)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("	 ");

                //generate Delete
                sw.WriteLine("   Public Delete (" + entityName + " mappedObject) As Integer");
                sw.WriteLine("        	 Return persistent.Delete(mappedObject)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("	 ");

                sw.WriteLine("   Public Delete (criteria As QueryCriteria) As Integer");
                sw.WriteLine("        	 Return persistent.Delete(criteria)");
                sw.WriteLine("	 End Function");
                sw.WriteLine("	 ");
                sw.WriteLine("	 ");


                sw.WriteLine("	");


                sw.WriteLine("End Class");

                sw.WriteLine("End Namespace"); //end namespace

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
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