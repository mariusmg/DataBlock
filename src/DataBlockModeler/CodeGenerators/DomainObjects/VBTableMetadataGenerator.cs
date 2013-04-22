/*

        file : VBTableMetadataGenerator.cs
  description: VB.NET TableMetadataGenerator implementation
       author: Marius Gheorghe
  
  
 */
using System;
using System.Collections;
using System.IO;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// VB.NET Table Metadata Generator
    /// </summary>
    internal class VBTableMetadataGenerator : ITableMetadataGenerator
    {
        #region ITableMetadataGenerator Members

        /// <summary>
        /// </summary>
        /// <param name="tableNames">String array with table names</param>
        /// <param name="alColumns">ArrayList with columns for each table name</param>
        /// <param name="alTableRelations"></param>
        /// <param name="fileName">The file path</param>
        /// <param name="namespaceName">Namespace name</param>
        public void GenerateTableMetadatata(string[] entityNames, string[] tableNames, ArrayList alColumns, ArrayList alTableRelations, string fileName, string namespaceName)
        {
            if (entityNames.Length != alColumns.Count)
            {
                throw new ArgumentException();
            }

            if (!fileName.EndsWith(".vb"))
            {
                fileName = fileName + ".vb";
            }


            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                if (File.Exists(fileName))
                {
                    fs = new FileStream(fileName, FileMode.Truncate, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                }

                sw = new StreamWriter(fs);

                //write header;
                sw.WriteLine("Imports System");
                sw.WriteLine("Imports System.Data");
                sw.WriteLine("Imports voidsoft.DataBlock");


                sw.WriteLine("");
                sw.WriteLine("Namespace " + namespaceName);
                sw.WriteLine(" ");

                for (int i = 0; i < entityNames.Length; i++)
                {
                    entityNames[i] = entityNames[i].Trim();

                    DatabaseColumn[] column = (DatabaseColumn[]) alColumns[i];

                    sw.WriteLine("");
                    sw.WriteLine("  <Serializable()> Public Class " + entityNames[i]);
                    sw.WriteLine("		             Inherits TableMetadata");

                    #region add enum field

                    sw.WriteLine("       Public Enum " + entityNames[i] + "Fields");

                    for (int k = 0; k < column.Length; k++)
                    {
                        sw.WriteLine("      " + column[k].Name + " = " + (k + 1).ToString());
                    }

                    sw.WriteLine("       End enum");

                    #endregion

                    sw.WriteLine("			Private _fields() as DatabaseField");
                    sw.WriteLine("");

                    #region generate ctor

                    sw.WriteLine("			Sub New() ");
                    sw.WriteLine("			");
                    sw.WriteLine("					_fields = new DatabaseField(" + (column.Length - 1) + ") {}");

                    string isPrimaryKey;
                    string isAutoIncremented;

                    //generate ctor 
                    for (int x = 0; x < column.Length; x++)
                    {
                        string dataType = column[x].columnDataType; // ds.Tables[i].Columns[x].DataType.ToString().Substring(ds.Tables[i].Columns[x].DataType.ToString().IndexOf(".") + 1);

                        if (dataType.Trim() == "System.Byte[]")
                        {
                            dataType = "System.Binary";
                        }

                        isPrimaryKey = column[x].isPrimaryKey.ToString();
                        isAutoIncremented = column[x].isAutoIncremented.ToString();


                        sw.WriteLine(" _fields(" + x + ") = new DatabaseField(DbType." + dataType.Substring(dataType.IndexOf(".") + 1) + ",\"" + column[x].Name + "\"," + isPrimaryKey + "," + isAutoIncremented + "," + "Nothing)");
                    }

                    sw.WriteLine(" ");
                    sw.WriteLine("me.currentTableName = \"" + tableNames[i] + "\"");
                    sw.WriteLine("");

                    TableRelation[] relations = (TableRelation[]) alTableRelations[i];

                    #region generate relations

                    for (int j = 0; j < relations.Length; j++)
                    {
                        //check the realtion type

                        if (relations[j] is ParentTableRelation)
                        {
                            ParentTableRelation pp = (ParentTableRelation) relations[j];

                            sw.WriteLine("Me.listRelations.Add(new ParentTableRelation(\"" + pp.RelatedTableName + "\", \"" + pp.ForeignKeyName + "\"," + "TableRelationCardinality." + pp.RelationCardinality.ToString() + "," + pp.CascadeDeleteChildren.ToString() + "))");
                        }
                        else if (relations[j] is ChildTableRelation)
                        {
                            ChildTableRelation ch = (ChildTableRelation) relations[j];
                            sw.WriteLine("Me.listRelations.Add(new ChildTableRelation(\"" + ch.RelatedTableName + "\",TableRelationCardinality.OneToOne,\"" + ch.RelatedTableKeyName + "\",\"" + ch.ForeignKeyName + "\"))");
                        }
                        else
                        {
                            //many to many
                            ManyToManyTableRelation mm = (ManyToManyTableRelation) relations[j];
                            sw.WriteLine("Me.listRelations.Add(new ManyToManyTableRelation(\"" + mm.RelatedTableName + "\",\"" + mm.IntermediaryTableName + "\",\"" + mm.IntermediaryKeyFieldFromParentTable + "\",\"" + mm.IntermediaryKeyFieldFromChildTable + "\"))");
                        }
                    }

                    #endregion

                    sw.WriteLine("");
                    sw.WriteLine("End Sub");

                    #endregion

                    //generate TableName and DatabaseFields

                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("");

                    sw.WriteLine("			Public Overrides Property TableFields() as DatabaseField() ");
                    sw.WriteLine("		   	   Get");
                    sw.WriteLine("				  Return _fields");
                    sw.WriteLine("			   End Get");
                    sw.WriteLine("			   Set (ByVal Value as DatabaseField())");
                    sw.WriteLine("			      _fields = value");
                    sw.WriteLine("			   End Set");
                    sw.WriteLine("			End Property");

                    #region generate Clone

                    sw.WriteLine("         Public Function Clone() As " + entityNames[i]);
                    sw.WriteLine("                  Return Me.Clone(Of " + entityNames[i] + ")");
                    sw.WriteLine("         End Function");

                    #endregion

                    #region generate properties

                    for (int x = 0; x < column.Length; x++)
                    {
                        if (column[x].columnDataType == "System.Byte[]")
                        {
                            string dataType = "System.Byte()";

                            sw.WriteLine("");
                            sw.WriteLine("     Public Property " + column[x].Name + "() As " + dataType);
                            sw.WriteLine("        Get");
                            sw.WriteLine("           Dim result As Object = Me.GetField(\"" + column[x].Name + "\").fieldValue");
                            sw.WriteLine("           If result = Nothing Then");
                            sw.WriteLine("               Dim data(0) As Byte");
                            sw.WriteLine("               Return data");
                            sw.WriteLine("           End If");
                            sw.WriteLine("           Return  CType((Me.GetField(\"" + column[x].Name + "\")).fieldValue," + dataType + ")");
                            sw.WriteLine("        End Get ");

                            dataType = "System.Byte";

                            sw.WriteLine("        Set(ByVal Value() As " + dataType + ")");
                            sw.WriteLine("          Me.SetFieldValue(\"" + column[x].Name + "\", Value)");
                            sw.WriteLine("        End Set");
                            sw.WriteLine("      End Property");
                        }
                        else if (column[x].columnDataType == "System.String")
                        {
                            sw.WriteLine("");
                            sw.WriteLine("     Public Property " + column[x].Name + " As " + column[x].columnDataType);
                            sw.WriteLine("        Get");
                            sw.WriteLine("           Return  CType((Me.GetField(\"" + column[x].Name + "\")).fieldValue," + column[x].columnDataType + ")");
                            sw.WriteLine("        End Get ");
                            sw.WriteLine("        Set(ByVal Value As " + column[x].columnDataType + ")");
                            sw.WriteLine("          Me.SetFieldValue(\"" + column[x].Name + "\", Value)");
                            sw.WriteLine("        End Set");
                            sw.WriteLine("      End Property");
                        }
                        else
                        {
                            //nullable types

                            sw.WriteLine("");
                            sw.WriteLine("     Public Property " + column[x].Name + " As Nullable(Of " + column[x].columnDataType + ")");
                            sw.WriteLine("        Get");
                            sw.WriteLine("           Return  CType((Me.GetField(\"" + column[x].Name + "\")).fieldValue," + "Nullable(Of " + column[x].columnDataType + "))");
                            sw.WriteLine("        End Get ");
                            sw.WriteLine("        Set(ByVal Value As Nullable(Of " + column[x].columnDataType + "))");
                            sw.WriteLine("          Me.SetFieldValue(\"" + column[x].Name + "\", Value)");
                            sw.WriteLine("        End Set");
                            sw.WriteLine("      End Property");
                        }
                    }

                    #endregion

                    #region generate "Get" methods implementation;

                    for (int j = 0; j < relations.Length; j++)
                    {
                        //check the realtion type

                        if (relations[j] is ParentTableRelation)
                        {
                            ParentTableRelation pp = (ParentTableRelation) relations[j];

                            if (pp.RelationCardinality == RelationCardinality.OneToOne)
                            {
                                sw.WriteLine("public Function Get" + pp.RelatedTableName + "() As TableMetadata");
                                sw.WriteLine("    Dim relatedType As Type = Type.GetType(\"" + namespaceName + "." + pp.RelatedTableName + "\")");
                                sw.WriteLine("    Dim result() As TableMetadata() = Me.GetRelatedTableData(relatedType)");
                                sw.WriteLine("   If result.Length > 0 Then");
                                sw.WriteLine("      Return result(0);");
                                sw.WriteLine("   Else");
                                sw.WriteLine("      Return Nothing");
                                sw.WriteLine("   End If");
                                sw.WriteLine("End Function");
                            }
                            else if (pp.RelationCardinality == RelationCardinality.OneToMany)
                            {
                                sw.WriteLine("Public Function Get" + pp.RelatedTableName + "() As Array");
                                sw.WriteLine("    Dim relatedType As Type = Type.GetType(\"" + namespaceName + "." + pp.RelatedTableName + "\")");
                                sw.WriteLine("    Return Me.GetRelatedTableData(relatedType)");
                                sw.WriteLine("End Function");
                            }
                        }
                        else if (relations[j] is ChildTableRelation)
                        {
                            ChildTableRelation pp = (ChildTableRelation) relations[j];
                            sw.WriteLine("Public Function Get" + pp.RelatedTableName + "() As TableMetadata");
                            sw.WriteLine("    Dim relatedType As Type = Type.GetType(\"" + namespaceName + "." + pp.RelatedTableName + "\")");
                            sw.WriteLine("    Dim result() As TableMetadata = Me.GetRelatedTableData(relatedType)");
                            sw.WriteLine("   If result.Length > 0 Then");
                            sw.WriteLine("      Return result(0)");
                            sw.WriteLine("   Else");
                            sw.WriteLine("      Return Nothing");
                            sw.WriteLine("   End If");
                            sw.WriteLine("End Function");
                        }
                        else
                        {
                            ManyToManyTableRelation pp = (ManyToManyTableRelation) relations[j];
                            sw.WriteLine("Public Function Get" + pp.RelatedTableName + "() As Array");
                            sw.WriteLine("    Dim relatedType As Type = Type.GetType(\"" + namespaceName + "." + pp.RelatedTableName + "\")");
                            sw.WriteLine("    Dim intermediaryType As Type = Type.GetType(\"" + namespaceName + "." + pp.IntermediaryTableName + "\")");
                            sw.WriteLine("    Return Me.GetRelatedTableData(relatedType, intermediaryType)");
                            sw.WriteLine("End Function");
                        }
                    }

                    #endregion
                }

                //end namespace brace
                sw.WriteLine("End Class");
                sw.WriteLine("End Namespace");

                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch
            {
                throw;
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