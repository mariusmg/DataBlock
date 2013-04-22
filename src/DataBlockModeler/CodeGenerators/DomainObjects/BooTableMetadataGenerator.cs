using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace voidsoft.DataBlockModeler
{
    public class BooTableMetadataGenerator : ITableMetadataGenerator
    {

        private bool generateValidationAttributes = false;

        #region ITableMetadataGenerator Members

        /// <summary>
        /// Generates the mapping files
        /// </summary>
        /// <param name="entityNames"></param>
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

            if (!fileName.EndsWith(".boo"))
            {
                fileName = fileName + ".boo";
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

                namespaceName = Utilies.RemoveEmptySpaces(namespaceName);


                sw.WriteLine("namespace " + namespaceName);

                //write header;
                sw.WriteLine("import System;");
                sw.WriteLine("import System.Data;");
                sw.WriteLine("import voidsoft.DataBlock;");


                sw.WriteLine("");



                for (int i = 0; i < entityNames.Length; i++)
                {
                    entityNames[i] = Utilies.RemoveEmptySpaces(entityNames[i].Trim());

                    DatabaseColumn[] column = (DatabaseColumn[]) alColumns[i];

                    sw.WriteLine("");
                    sw.WriteLine("      [Serializable()]");
                    sw.WriteLine("		public class " + entityNames[i] + "(TableMetadata):");
                    sw.WriteLine("		{");
                    sw.WriteLine("");

                    #region generate the internal enum with myFields

                    sw.WriteLine("                   public enum " + entityNames[i] + "Fields:");

                    for (int ij = 0; ij < column.Length; ij++)
                    {
                        if (ij == column.Length - 1)
                        {
                            sw.WriteLine("                      " + column[ij].Name);
                        }
                        else
                        {
                            sw.WriteLine("                      " + column[ij].Name);
                        }
                    }


                    #endregion

                    sw.WriteLine("");
                    sw.WriteLine("");


                    sw.WriteLine("			    private myFields as (DatabaseField);");
                    sw.WriteLine("");

                    #region generate ctor

                    sw.WriteLine("		    	public def " + entityNames[i] + "():");
                    sw.WriteLine("					    myFields = array (DatabaseField," + column.Length + ")");

                    string isPrimaryKey;
                    string isAutoIncremented;


                    for (int x = 0; x < column.Length; x++)
                    {
                        string dataType = column[x].columnDataType;

                        //check the type. If it's a byte[] "rename" it to 

                        if (dataType.Trim() == "System.Byte[]")
                        {
                            dataType = "System.Binary";
                        }

                        isPrimaryKey = column[x].isPrimaryKey.ToString();
                        isAutoIncremented = column[x].isAutoIncremented.ToString();

                        //make it lower case for C#
                        isPrimaryKey = isPrimaryKey.ToLower();
                        isAutoIncremented = isAutoIncremented.ToLower();

                        sw.WriteLine("                    myFields[" + x + "] = DatabaseField(DbType." + dataType.Substring(dataType.IndexOf(".") + 1) + ",\"" + column[x].Name + "\"," + isPrimaryKey + "," + isAutoIncremented + "," + "null)");
                    }

                    sw.WriteLine(" ");
                    sw.WriteLine("                        self.currentTableName = \"" + tableNames[i] + "\"");
                    sw.WriteLine("");

                    List<TableRelation> relations = (List<TableRelation>) alTableRelations[i];

                    #region generate relations

                    for (int j = 0; j < relations.Count; j++)
                    {
                        //check the realtion type

                        if (relations[j] is ParentTableRelation)
                        {
                            ParentTableRelation pp = (ParentTableRelation) relations[j];

                            sw.WriteLine("self.listRelations.Add(new ParentTableRelation(\"" + pp.RelatedTableName + "\", \"" + pp.ForeignKeyName + "\"," + "TableRelationCardinality." + pp.RelationCardinality.ToString() + "," + pp.CascadeDeleteChildren.ToString().ToLower() + "))");
                        }
                        else if (relations[j] is ChildTableRelation)
                        {
                            ChildTableRelation ch = (ChildTableRelation) relations[j];
                            sw.WriteLine("self.listRelations.Add(new ChildTableRelation(\"" + ch.RelatedTableName + "\",TableRelationCardinality.OneToOne,\"" + ch.RelatedTableKeyName + "\",\"" + ch.ForeignKeyName + "\"))");
                        }
                        else
                        {
                            //many to many
                            ManyToManyTableRelation mm = (ManyToManyTableRelation) relations[j];
                            sw.WriteLine("self.listRelations.Add(new ManyToManyTableRelation(\"" + mm.RelatedTableName + "\",\"" + mm.IntermediaryTableName + "\",\"" + mm.IntermediaryKeyFieldFromParentTable + "\",\"" + mm.IntermediaryKeyFieldFromChildTable + "\"))");
                        }
                    }


                    sw.WriteLine("");

                    #endregion

                    #endregion

                    //generate DatabasemyFields

                    sw.WriteLine("");
                    sw.WriteLine("");

                    sw.WriteLine("			public override TableFields as (DatabaseField): ");
                    sw.WriteLine("			{");
                    sw.WriteLine("				get :{ return myFields;}");
                    sw.WriteLine("				set{myFields = value;}");
                    sw.WriteLine("			}");

                    #region Clone

                    sw.WriteLine("          public " + entityNames[i] + " Clone()");
                    sw.WriteLine("          {");
                    sw.WriteLine("                 return this.Clone<" + entityNames[i] + ">();");
                    sw.WriteLine("          }");

                    #endregion

                    #region generate properties with nullable data types

                    for (int x = 0; x < column.Length; x++)
                    {
                        //generate a empty byte[] 
                        if (column[x].columnDataType == "System.Byte[]")
                        {
                            sw.WriteLine("");
                            sw.WriteLine("public " + column[x].columnDataType + " " + column[x].Name);
                            sw.WriteLine("{");
                            sw.WriteLine("    get");
                            sw.WriteLine("    {");
                            sw.WriteLine("         object result = (this.GetField(\"" + column[x].Name + "\")).fieldValue;");
                            sw.WriteLine("         if(result == null)");
                            sw.WriteLine("         {");
                            sw.WriteLine("              return new System.Byte[0];");
                            sw.WriteLine("         }");
                            sw.WriteLine("");
                            sw.WriteLine("          return (" + column[x].columnDataType + ") result;"); // + " (this.GetField(\"" + column[x].Name + "\")).fieldValue;");
                            sw.WriteLine("    }");
                            sw.WriteLine("");
                            sw.WriteLine("    set");
                            sw.WriteLine("    {");
                            sw.WriteLine("          this.SetFieldValue(\"" + column[x].Name + "\", value);");
                            sw.WriteLine("    }");
                            sw.WriteLine("}");
                            sw.WriteLine("");
                        }
                        else if (column[x].columnDataType == "System.String")
                        {
                            sw.WriteLine("");

                            //generate validation attributes
                            if (generateValidationAttributes)
                            {
                                //not null
                                if (!column[x].allowsNull)
                                {
                                    //sw.WriteLine("[EvilAttributes.ValidateRequired(" + column[x].Name + "Null" + ")]");
                                }

                                //also add size check
                                //sw.WriteLine("[EvilAttributes.ValidateMaxLength(" + column[x].fieldLength + ", " + column[x].Name + "MaxLength" + ")]");
                            }

                            sw.WriteLine("public " + column[x].columnDataType + " " + column[x].Name);
                            sw.WriteLine("{");
                            sw.WriteLine("    get");
                            sw.WriteLine("    {");
                            sw.WriteLine("         object result = this.GetField(\"" + column[x].Name + "\").fieldValue; ");
                            sw.WriteLine("         return (result != null) ? result.ToString() : null;");
                            sw.WriteLine("    }");
                            sw.WriteLine("");
                            sw.WriteLine("    set");
                            sw.WriteLine("    {");
                            sw.WriteLine("          this.SetFieldValue(\"" + column[x].Name + "\", value);");
                            sw.WriteLine("    }");
                            sw.WriteLine("}");
                            sw.WriteLine("");
                        }
                        else
                        {
                            //generate nullable types for the rest of the columns

                            if (generateValidationAttributes)
                            {
                                if (!column[x].allowsNull)
                                {
                                    // sw.WriteLine("[EvilAttributes.ValidateRequired(" + column[x].Name + "Null" + ")]");
                                }
                            }


                            if (column[x].allowsNull)
                            {
                                sw.WriteLine("");
                                sw.WriteLine("public " + column[x].columnDataType + "? " + column[x].Name);
                                sw.WriteLine("{");
                                sw.WriteLine("    get");
                                sw.WriteLine("    {");
                                sw.WriteLine("         object result = this.GetField(\"" + column[x].Name + "\").fieldValue; ");

                                sw.WriteLine(" return (result == null || result == DBNull.Value) ? null : (" + column[x].columnDataType + "?) result;");
                                sw.WriteLine("    }");
                                sw.WriteLine("");
                                sw.WriteLine("    set");
                                sw.WriteLine("    {");
                                sw.WriteLine("          this.SetFieldValue(\"" + column[x].Name + "\", value);");
                                sw.WriteLine("    }");
                                sw.WriteLine("}");
                                sw.WriteLine("");
                            }
                            else
                            {
                                sw.WriteLine("");
                                sw.WriteLine("public " + column[x].columnDataType + " " + column[x].Name);
                                sw.WriteLine("{");
                                sw.WriteLine("    get");
                                sw.WriteLine("    {");
                                sw.WriteLine("        return (" + column[x].columnDataType + ") this.GetField(\"" + column[x].Name + "\").fieldValue; ");
                                sw.WriteLine("    }");
                                sw.WriteLine("");
                                sw.WriteLine("    set");
                                sw.WriteLine("    {");
                                sw.WriteLine("          this.SetFieldValue(\"" + column[x].Name + "\", value);");
                                sw.WriteLine("    }");
                                sw.WriteLine("}");
                                sw.WriteLine("");
                            }
                        }
                    }

                    #endregion

                    #region Generate Get methods

                    //generate "Get" methods implementation;

                    for (int j = 0; j < relations.Count; j++)
                    {
                        //check the realtion type

                        if (relations[j] is ParentTableRelation)
                        {
                            ParentTableRelation pp = (ParentTableRelation) relations[j];

                            if (pp.RelationCardinality == RelationCardinality.OneToOne)
                            {
                                sw.WriteLine("public TableMetadata Get" + Utilies.GetEntityName(pp.RelatedTableName) + "()");
                                sw.WriteLine("{");
                                sw.WriteLine("    Type relatedType = Type.GetType(\"" + namespaceName + "." + Utilies.GetEntityName(pp.RelatedTableName) + "\");");
                                sw.WriteLine("    TableMetadata[] result = this.GetRelatedTableData(relatedType);");
                                sw.WriteLine("    return result.Length > 0 ? result[0] : null; ");
                                sw.WriteLine("}");
                            }
                            else if (pp.RelationCardinality == RelationCardinality.OneToMany)
                            {
                                sw.WriteLine("public Array Get" + Utilies.GetEntityName(pp.RelatedTableName) + "()");
                                sw.WriteLine("{");
                                sw.WriteLine("    Type relatedType = Type.GetType(\"" + namespaceName + "." + Utilies.GetEntityName(pp.RelatedTableName) + "\");");
                                sw.WriteLine("    return this.GetRelatedTableData(relatedType);");
                                sw.WriteLine("}");
                            }
                        }
                        else if (relations[j] is ChildTableRelation)
                        {
                            ChildTableRelation pp = (ChildTableRelation) relations[j];
                            sw.WriteLine("public TableMetadata Get" + Utilies.GetEntityName(pp.RelatedTableName) + "()");
                            sw.WriteLine("{");
                            sw.WriteLine("    Type relatedType = Type.GetType(\"" + namespaceName + "." + Utilies.GetEntityName(pp.RelatedTableName) + "\");");
                            sw.WriteLine("    TableMetadata[] result = this.GetRelatedTableData(relatedType);");
                            sw.WriteLine("    return result.Length > 0 ? result[0] : null; ");
                            sw.WriteLine("}");
                        }
                        else
                        {
                            ManyToManyTableRelation pp = (ManyToManyTableRelation) relations[j];
                            sw.WriteLine("public Array Get" + Utilies.GetEntityName(pp.RelatedTableName) + "()");
                            sw.WriteLine("{");
                            sw.WriteLine("    Type relatedType = Type.GetType(\"" + namespaceName + "." + Utilies.GetEntityName(pp.RelatedTableName) + "\");");
                            sw.WriteLine("    Type intermediaryType = Type.GetType(\"" + namespaceName + "." + Utilies.GetEntityName(pp.IntermediaryTableName) + "\");");
                            sw.WriteLine("    return this.GetRelatedTableData(relatedType, intermediaryType);");
                            sw.WriteLine("}");
                        }
                    }

                    #endregion

                    sw.WriteLine("}"); //end class brace
                }


                //end namespace brace
                sw.WriteLine("}");

                sw.Flush();
                sw.Close();
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