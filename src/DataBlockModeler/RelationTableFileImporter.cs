using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace voidsoft.DataBlockModeler
{
    public static class RelationTableFileImporter
    {
        private const string CARDINALITY = "cardinality";
        private const string TYPE = "type";
        private const string FOREIGN_KEY_FIELD_NAME = "fkfieldname";
        private const string PRIMARY_KEY_FIELD_NAME = "pkfieldname";
        private const string RELATED_TABLE_NAME = "relatedtablename";
        private const string PARENT_CHILD = "ParentChild";
        private const string CHILD_PARENT = "ChildParent";
        private const string MANY_TO_MANY = "ManyToMany";
        private const string TABLE = "table";
        private const string CASCADE_DELETE_CHILDREN = "cascadedeletechildren";
        private const string INTERMEDIARY_TABLE_NAME = "intermediarytablename";

        private const string INTERMEDIARY_KEY_FIELD_FROM_PARENT_TABLE = "intermediarykeyfieldfromparenttable";
        private const string INTERMEDIARY_KEY_FIELD_FROM_CHILD_TABLE = "intermediarykeyfieldfromchildtable";



        /// <summary>
        /// Imports the relations.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<ImportedRelation> ImportRelations(string filePath)
        {
            List<ImportedRelation> listRelations = null;


            listRelations = new List<ImportedRelation>();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                try
                {
                    //skip comments
                    if (line.StartsWith("//") || line == string.Empty)
                    {
                        continue;
                    }

                    //get all the parts
                    string[] parts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    TableRelation relation = null;
                    string tableName = string.Empty;
                    ParseRelation(parts, ref relation, ref tableName);

                    if (relation != null && (!string.IsNullOrEmpty(tableName)))
                    {
                        listRelations.Add(new ImportedRelation(tableName, relation));
                    }

                }
                catch
                {
                    continue;
                }
            }

            return listRelations;
        }


        internal static void ParseRelation(string[] parts, ref TableRelation relation, ref string tableName)
        {
            try
            {
                Dictionary<string, string> entries = new Dictionary<string, string>();

                foreach (string part in parts)
                {
                    try
                    {
                        string[] internalPieces = part.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (internalPieces.Length != 2)
                        {
                            continue;
                        }

                        entries.Add(internalPieces[0].Trim().ToLower(), internalPieces[1].Trim());
                    }
                    catch
                    {
                        continue;
                    }
                }

                //check the type
                string entry;
                entries.TryGetValue(TYPE, out entry);

                if (string.IsNullOrEmpty(entry))
                {
                    return;
                }

                if (entry == CHILD_PARENT)
                {
                    ChildTableRelation childTableRelation = new ChildTableRelation();
                    childTableRelation.ForeignKeyName = entries[FOREIGN_KEY_FIELD_NAME];
                    childTableRelation.RelatedTableName = entries[RELATED_TABLE_NAME];
                    childTableRelation.RelationCardinality = (RelationCardinality) Enum.Parse(typeof(RelationCardinality), entries[CARDINALITY], true);
                    childTableRelation.RelatedTableKeyName = entries[PRIMARY_KEY_FIELD_NAME];

                    tableName = entries[TABLE];

                    relation = childTableRelation;
                }
                else if (entry == PARENT_CHILD)
                {
                    ParentTableRelation parentTableRelation = new ParentTableRelation();
                    parentTableRelation.RelatedTableName = entries[RELATED_TABLE_NAME];
                    parentTableRelation.ForeignKeyName = entries[FOREIGN_KEY_FIELD_NAME];
                    parentTableRelation.RelationCardinality = (RelationCardinality) Enum.Parse(typeof(RelationCardinality), entries[CARDINALITY], true);
                    tableName = entries[TABLE];
                    parentTableRelation.CascadeDeleteChildren = Convert.ToBoolean(entries[CASCADE_DELETE_CHILDREN]);

                    relation = parentTableRelation;
                }
                else if (entry == MANY_TO_MANY)
                {
                    ManyToManyTableRelation manyTableRelation = new ManyToManyTableRelation();
                    manyTableRelation.RelatedTableName = entries[RELATED_TABLE_NAME];
                    manyTableRelation.IntermediaryTableName = entries[INTERMEDIARY_TABLE_NAME];
                    manyTableRelation.IntermediaryKeyFieldFromParentTable = entries[INTERMEDIARY_KEY_FIELD_FROM_PARENT_TABLE];
                    manyTableRelation.IntermediaryKeyFieldFromChildTable = entries[INTERMEDIARY_KEY_FIELD_FROM_CHILD_TABLE];

                    manyTableRelation.RelationCardinality = RelationCardinality.ManyToMany;

                    tableName = entries[TABLE];

                    relation = manyTableRelation;

                }
            }
            catch
            {
                relation = null;
            }
        }
    }


    public struct ImportedRelation
    {
        public string tableName;

        public TableRelation relation;


        public ImportedRelation(string tableName, TableRelation relation)
        {
            this.tableName = tableName;
            this.relation = relation;
        }
    }



    public enum TableRelationImportType
    {
        Database,
        File,
        UserAdded
    }

}