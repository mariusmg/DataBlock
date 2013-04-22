using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace voidsoft.DataBlockModeler
{
    public delegate void FinishedLoading(string errorMessage);


    public class RootWindowPresenter
    {
        private Form myView = null;
        private FinishedLoading finishedLoadingCallback;
        private FinishedLoading finishedLoadingImportCallback;

        private string importFilePath;


        public RootWindowPresenter(Form view)
        {
            this.myView = view;
        }

        public void LoadDatabaseSchema(FinishedLoading loadingCallback)
        {
            finishedLoadingCallback = loadingCallback;

            Thread thStart = new Thread(OnStartLoadingDatabaseSchema);
            thStart.Priority = ThreadPriority.Highest;
            thStart.Start();
        }


        public void LoadTableRelationsFromFile(string filePath, FinishedLoading loading)
        {
            finishedLoadingImportCallback = loading;
            importFilePath = filePath;
            Thread th = new Thread(OnStartLoadingTableRelationFromFile);
            th.Start();
        }


        private void OnStartLoadingTableRelationFromFile()
        {
            try
            {
                List<ImportedRelation> relations = RelationTableFileImporter.ImportRelations(importFilePath);


                int importRelationsCounter = 0;

                foreach (ImportedRelation relation in relations)
                {
                    foreach (DatabaseTable table in GeneratorContext.CurrentDatabaseTables)
                    {
                        if (relation.tableName == table.TableName)
                        {
                            ++importRelationsCounter;
                            table.Relations.Add(relation.relation);
                            break;
                        }
                    }
                }

                finishedLoadingImportCallback(importRelationsCounter.ToString());
            }
            catch (Exception ex)
            {
                finishedLoadingImportCallback("error=" + ex.Message);
            }
        }


        private void OnStartLoadingDatabaseSchema()
        {
            List<string> listTables = null;

            try
            {
                //load the table names
                listTables = Schema.GetTableList(GeneratorContext.currentDatabaseServer, GeneratorContext.currentConnectionString);

                GeneratorContext.CurrentDatabaseTables.Clear();

                for (int i = 0; i < listTables.Count; i++)
                {
                    try
                    {
                        //load the column info for current table
                        DatabaseColumn[] columns = Schema.GetColumnInfo(GeneratorContext.currentDatabaseServer, listTables[i], GeneratorContext.currentConnectionString);

                        string tableName = Utilies.RemoveEmptySpaces(listTables[i]);

                        GeneratorContext.CurrentDatabaseTables.Add(new DatabaseTable(tableName, columns, Utilies.GetEntityName(tableName)));
                    }
                    catch
                    {
                        continue;
                    }
                }

                //invoke the delegate on the UI thread
                myView.Invoke(finishedLoadingCallback, new object[1] { string.Empty });
            }
            catch (Exception ex)
            {
                myView.Invoke(finishedLoadingCallback, new object[1] { ex.Message });
            }
        }




        public void GenerateDomainObjects(string namespaceName, string path, Language selectedLanguage, List<string> listTables, List<string> listEntities)
        {
            ITableMetadataGenerator generator = null;

            //initialize the generator based on the language

            if (selectedLanguage == Language.CSharp)
            {
                generator = new CSharpTableMetadataGenerator();
            }
            else if (selectedLanguage == Language.VbNet)
            {
                generator = new VBTableMetadataGenerator();
            }


            //check path
            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }


            ArrayList alColumns = new ArrayList();
            ArrayList alTableRelations = new ArrayList();


            string currentFilePath = string.Empty;

            for (int i = 0; i < listEntities.Count; i++)
            {
                alColumns.Clear();
                alTableRelations.Clear();

                foreach (DatabaseTable var in GeneratorContext.CurrentDatabaseTables)
                {
                    if (var.EntityName == listEntities[i])
                    {
                        alColumns.Add(var.Columns);
                        alTableRelations.Add(var.Relations);
                        break;
                    }
                }


                currentFilePath = path + listEntities[i];

                generator.GenerateTableMetadatata(new string[1] { listEntities[i] }, new string[1] { listTables[i] }, alColumns, alTableRelations, currentFilePath, namespaceName);
            }

        }




        public void GenerateDataAccessEntities(string namespaceName, string path, Language selectedLanguage, List<string> listTables, List<string> listEntities)
        {
            IDataAccessObjectGenerator generator = null;

            try
            {
                //initialize the generator based on the language

                if (selectedLanguage == Language.CSharp)
                {
                    generator = new CSharpDataAccessObjectGenerator();
                }
                else if (selectedLanguage == Language.VbNet)
                {
                    generator = new VBDataAccessObjectGenerator();
                }


                if (!path.EndsWith(@"\"))
                {
                    path += @"\";
                }

                //string[] tableNames = new string[listView.CheckedItems.Count];
                ArrayList alColumns = new ArrayList();
                ArrayList alTableRelations = new ArrayList();

                string currentFilePath = string.Empty;

                for (int i = 0; i < listTables.Count; i++)
                {
                    alColumns.Clear();
                    alTableRelations.Clear();

                    alColumns.Add(GeneratorContext.CurrentDatabaseTables[i].Columns);
                    alTableRelations.Add(GeneratorContext.CurrentDatabaseTables[i].Relations);

                    currentFilePath = path + listEntities[i] + "DataAccess";


                    generator.GenerateDataAccessObjects(listEntities[i], namespaceName, currentFilePath);
                }
            }
            catch
            {
                throw;
            }
        }






        public void GenerateSilverChaliceObjects(string namespaceName, string path, bool generateContentPlaceholder, Language selectedLanguage, List<string> listTables, List<string> listEntities)
        {

            AspnetCodeGenerator c = new AspnetCodeGenerator();


            int index = -1;

            foreach (string table in listTables)
            {
                ++index;

                DatabaseTable tbl = GetTableByName(table);

                if(tbl != null)
                {
                    c.Generate(tbl.Columns, namespaceName, listEntities[index], generateContentPlaceholder, Application.StartupPath + @"\Output", tbl.Relations);

                }

            }

           
            


        }



        public void GenerateBusinessObjects(string namespaceName, string path, Language selectedLanguage, List<string> listTables, List<string> listEntities)
        {
            IBusinessObjectGenerator generator = null;


            //initialize the generator based on the language

            //if (selectedLanguage == Language.CSharp)
            //{
            generator = new CSharpBusinessObjectGenerator();
            //}
            //else if (selectedLanguage == Language.VbNet)
            //{
            //    generator = new VBDataAccessObjectGenerator();
            //}

            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }

            ArrayList alColumns = new ArrayList();
            ArrayList alTableRelations = new ArrayList();

            string currentFilePath = string.Empty;

            for (int i = 0; i < listTables.Count; i++)
            {
                alColumns.Clear();
                alTableRelations.Clear();

                alColumns.Add(GeneratorContext.CurrentDatabaseTables[i].Columns);
                alTableRelations.Add(GeneratorContext.CurrentDatabaseTables[i].Relations);

                currentFilePath = path + listEntities[i] + "BusinessObject";


                generator.GenerateBusinessObjects(listEntities[i], namespaceName, currentFilePath);
            }

        }



        private DatabaseTable GetTableByName(string name)
        {
            DatabaseTable find = GeneratorContext.CurrentDatabaseTables.Find(delegate(DatabaseTable table)
                                                                                 {
                                                                                     return (table.TableName == name) ? true : false;

                                                                                 });


            return find;

        }
    }
}