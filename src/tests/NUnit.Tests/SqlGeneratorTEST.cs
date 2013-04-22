using System;
using System.Text;
using NUnit.Framework;
using voidsoft.DataBlock;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Extender;

namespace tests
{

    [TestFixture()]
    public class SqlGeneratorTEST
    {
        private CategoryTableMetadata ctg = null;

        private First first = null;
        private FirstPersistentObject fpo = null;



        [SetUp]
        public void SetThingsUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
        }

        public SqlGeneratorTEST()
        {
            ctg = new CategoryTableMetadata();
            first = new First();
        }


        #region SELECT

        #region SqlServer
        [Test]
        public void TestSelectSqlServerWithQuerySelect()
        {
            CategoryPersistentObject cpo = new  CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            QueryCriteria qc = new QueryCriteria(this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.SqlServer, ctg);
            Console.WriteLine(query);

            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectSqlServerWithTableName()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery( DatabaseServer.SqlServer, ctg.TableName, ctg.TableFields[0], ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectSqlServerByPrimaryKey()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.SqlServer, ctg, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        [Test]
        public void TestSelectSqlServerByAllFields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.SqlServer, ctg, false);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        [Test]
        public void TestSelectSqlServerByFieldsParametersAndTableMetadata()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.SqlServer, ctg, ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        [Test]
        public void TestSelectSqlServerByDatabaseFieldsAndPrimaryKeyCondition()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.SqlServer, ctg.TableName, ff, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectSqlServerByDatabaseFieldsAndWithCondtitionalDatabaseDields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.SqlServer, ctg.TableName, ff, ctg.TableFields[1]);


            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }


        #endregion


        #region MySQL
        [Test]
        public void TestSelectMySQLWithQuerySelect()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            QueryCriteria qc = new QueryCriteria(this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg);
            Console.WriteLine(query);

            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectMySQLWithTableName()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg.TableName, ctg.TableFields[0], ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectMySQLByPrimaryKey()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        [Test]
        public void TestSelectMySQLByAllFields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg, false);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        [Test]
        public void TestSelectMySQLByFieldsParametersAndTableMetadata()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg, ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        [Test]
        public void TestSelectMySQLByDatabaseFieldsAndPrimaryKeyCondition()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg.TableName, ff, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectMySQLByDatabaseFieldsAndWithCondtitionalDatabaseDields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.MySQL, ctg.TableName, ff, ctg.TableFields[1]);


            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }


        #endregion


        #region Access
        [Test]
        public void TestSelectAccessWithQuerySelect()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            QueryCriteria qc = new QueryCriteria(this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg);
            Console.WriteLine(query);

            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectAccessWithTableName()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg.TableName, ctg.TableFields[0], ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectAccessByPrimaryKey()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectAccessByAllFields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg, false);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectAccessByFieldsParametersAndTableMetadata()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg, ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectAccessByDatabaseFieldsAndPrimaryKeyCondition()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg.TableName, ff, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectAccessByDatabaseFieldsAndWithCondtitionalDatabaseDields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, ctg.TableName, ff, ctg.TableFields[1]);


            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        #endregion


        #region PostgreSQL
        [Test]
        public void TestSelectPostgreWithQuerySelect()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql , SharedData.sqlserver, this.ctg);
            QueryCriteria qc = new QueryCriteria(this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg);
            Console.WriteLine(query);

            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectPostgreWithTableName()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg.TableName, ctg.TableFields[0], ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectPostgreByPrimaryKey()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectPostgreByAllFields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg, false);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectPostgreByFieldsParametersAndTableMetadata()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql, SharedData.sqlserver, this.ctg);
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg, ctg.TableFields[1]);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectPostgreByDatabaseFieldsAndPrimaryKeyCondition()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg.TableName, ff, true);
            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }

        [Test]
        public void TestSelectPostgreByDatabaseFieldsAndWithCondtitionalDatabaseDields()
        {
            CategoryPersistentObject cpo = new CategoryPersistentObject(DatabaseServer.PostgreSql, SharedData.sqlserver, this.ctg);
            DatabaseField[] ff = new DatabaseField[] { this.ctg.TableFields[0] };
            ExecutionQuery query = SqlGenerator.GenerateSelectQuery(DatabaseServer.PostgreSql, ctg.TableName, ff, ctg.TableFields[1]);


            Console.WriteLine(query);
            Assert.IsTrue(query.Query != string.Empty);
        }
        #endregion

        #endregion


        #region DELETE



        #region delete sql server


        //multiple deletes

        public void TestDeleteMultipleSqlServer()
        {
            fpo = new FirstPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.first);


            DatabaseField field = this.first.TableFields[0];
            QueryCriteria qc = new QueryCriteria(this.first.TableName, this.first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, this.first.TableFields[0]);

            object pk = fpo.GetValue(qc);
            First ft = (First) fpo.GetTableMetadata(pk);

            List<ExecutionQuery> listQueries = SqlGenerator.GenerateMultipleDeleteQueries(DatabaseServer.SqlServer, ft);

            foreach (ExecutionQuery var in listQueries)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(listQueries.Count > 0);
        }







		public void TestDeleteSqlServerOverload1()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 1;
			ab.CategoryName = "Seafood";
			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.SqlServer, ab.TableName, ab.GetField("CategoryName") );
			Assert.IsTrue(s.Query != string.Empty);
		}

		public void TestDeleteSqlServerOverload2()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 1;
			ab.CategoryName = "Seafood";
			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.SqlServer, ab, true);
			Assert.IsTrue(s.Query != string.Empty);
		}


		public void TestDeleteSqlServerOverload3()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 1;
			ab.CategoryName = "Seafood";
			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.SqlServer, ab, false);
			Assert.IsTrue(s.Query != string.Empty);
		}
		#endregion


		#region delete Access

        //multiple deletes

        public void TestDeleteMultipleAccess()
        {
            fpo = new FirstPersistentObject(DatabaseServer.Access, SharedData.access, this.first);



            PersistentObjectAccessTEST pp = new PersistentObjectAccessTEST();
            pp.TestCreateMultiple();


            DatabaseField field = this.first.TableFields[0];
            QueryCriteria qc = new QueryCriteria(this.first.TableName, this.first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, this.first.TableFields[0]);

            object pk = fpo.GetValue(qc);
            First ft = (First)fpo.GetTableMetadata(pk);

            List<ExecutionQuery> listQueries = SqlGenerator.GenerateMultipleDeleteQueries(DatabaseServer.Access, ft);

            foreach (ExecutionQuery var in listQueries)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(listQueries.Count > 0);
        }


		public void TestDeleteAccessOverload1()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryName = "Seafood";

			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.Access, ab.TableName, ab.GetField("CategoryName"));
			Assert.IsTrue(s.Query != string.Empty);
		}


		public void TestDeleteAccessOverload2()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryName = "Seafood";

			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.Access, ab, true);
			Assert.IsTrue(s.Query != string.Empty);
		}

		public void TestDeleteAccessOverload3()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryName = "Seafood";

			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.Access, ab, false);
			Assert.IsTrue(s.Query != string.Empty);
		}
		#endregion


		#region delete mysql

        public void TestDeleteMultipleMySql()
        {
            fpo = new FirstPersistentObject(DatabaseServer.MySQL, SharedData.mysql, this.first);



            DatabaseField field = this.first.TableFields[0];
            QueryCriteria qc = new QueryCriteria(this.first.TableName, this.first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, this.first.TableFields[0]);

            object pk = fpo.GetValue(qc);
            First ft = (First)fpo.GetTableMetadata(pk);


            List<ExecutionQuery> listQueries = SqlGenerator.GenerateMultipleDeleteQueries(DatabaseServer.MySQL, ft);

            foreach (ExecutionQuery var in listQueries)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(listQueries.Count > 0);

        }


		public void TestDeleteMySqlOverload1()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryName = "Seafood";

			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.MySQL, ab.TableName, ab.GetField("CategoryName"));
			Assert.IsTrue(s.Query != string.Empty);
		}


		public void TestDeleteMySqlOverload2()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryName = "Seafood";

			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.MySQL, ab, true);
			Assert.IsTrue(s.Query != string.Empty);
		}

		public void TestDeleteMySqlOverload3()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryName = "Seafood";

			ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.MySQL, ab, false);
			Assert.IsTrue(s.Query != string.Empty);
		}
		#endregion


        #region delete PostgreSql

        public void TestDeleteMultiplePostgreSql()
        {
            fpo = new FirstPersistentObject(DatabaseServer.PostgreSql, SharedData.postgresql, this.first);



            DatabaseField field = this.first.TableFields[0];
            QueryCriteria qc = new QueryCriteria(this.first.TableName, this.first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, this.first.TableFields[0]);

            object pk = fpo.GetValue(qc);
            First ft = (First)fpo.GetTableMetadata(pk);


            List<ExecutionQuery> listQueries = SqlGenerator.GenerateMultipleDeleteQueries(DatabaseServer.PostgreSql, ft);
            
            foreach (ExecutionQuery var in listQueries)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(listQueries.Count > 0);

        }


        public void TestDeletePostgreSqlOverload1()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryName = "Seafood";

            ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.PostgreSql, ab.TableName, ab.GetField("CategoryName"));
            Assert.IsTrue(s.Query != string.Empty);
        }


        public void TestDeletePostgreSqlOverload2()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryName = "Seafood";

            ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.PostgreSql, ab, true);
            Assert.IsTrue(s.Query != string.Empty);
        }

        public void TestDeletePostgreSqlOverload3()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryName = "Seafood";

            ExecutionQuery s = SqlGenerator.GenerateDeleteQuery(DatabaseServer.PostgreSql, ab, false);
            Assert.IsTrue(s.Query != string.Empty);
        }
        #endregion



		#endregion


		#region INSERT


		#region sql server

        public void TestInsertMultipleSqlServer()
        {
            Third third = new Third();
            third.ThirdStuff = "lalala";

            Second s = new Second();
            s.Age = 456;
            s.Quantity = 43;

            s.AttachTableMetadata(third);

            First first = new First();
            first.Name = "Gogu";

            first.AttachTableMetadata(s);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleInsertQueries(DatabaseServer.SqlServer, first);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(list.Count > 0);
        }

		public void TestInsertSqlServerOverload1()
		{
            this.ctg.CategoryName = "Blossom";
            this.ctg.Description = "muhahahhaha";


            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.SqlServer, this.ctg);

            Assert.IsTrue(s.Query != null);
        }

        public void TestInsertSqlServerWithDataFieldsParameters()
        {
            this.ctg.CategoryName = "TestOverload2";
            this.ctg.Description = "things";

            DatabaseField[] fields = new DatabaseField[] {ctg.TableFields[1], ctg.TableFields[2]};

            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.SqlServer, fields, ctg.TableName);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != null);
        }


		#endregion


		#region access

        public void TestInsertMultipleAccess()
        {
            Third third = new Third();
            third.ThirdStuff = "lalala";

            Second s = new Second();
            s.Age = 456;
            s.Quantity = 43;

            s.AttachTableMetadata(third);

            First first = new First();
            first.Name = "Gogu";

            first.AttachTableMetadata(s);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleInsertQueries(DatabaseServer.Access, first);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(list.Count > 0);

        }
		public void TestInsertAccessOverload1()
		{
            this.ctg.CategoryName = "Blossom";
            this.ctg.Description = "muhahahhaha";


            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.Access, this.ctg);

            Assert.IsTrue(s.Query != null);
        }

        public void TestInsertAccessWithDataFieldsParameters()
        {
            this.ctg.CategoryName = "TestOverload2";
            this.ctg.Description = "things";

            DatabaseField[] fields = new DatabaseField[] { ctg.TableFields[1], ctg.TableFields[2] };

            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.Access, fields, ctg.TableName);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != null);
        }

        #endregion


        #region mysql

        public void TestInsertMultipleMySql()
        {
            Third third = new Third();
            third.ThirdStuff = "lalala";

            Second s = new Second();
            s.Age = 456;
            s.Quantity = 43;

            s.AttachTableMetadata(third);

            First first = new First();
            first.Name = "Gogu";

            first.AttachTableMetadata(s);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleInsertQueries(DatabaseServer.MySQL, first);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(list.Count > 0);
        }


        public void TestInsertMySqlOverload1()
		{
            this.ctg.CategoryName = "Blossom";
            this.ctg.Description = "muhahahhaha";

            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.MySQL, this.ctg);

            Assert.IsTrue(s.Query != null);
		}



        public void TestInsertMySqlWithDataFieldsParameters()
        {
            this.ctg.CategoryName = "TestOverload2";
            this.ctg.Description = "things";

            DatabaseField[] fields = new DatabaseField[] { ctg.TableFields[1], ctg.TableFields[2] };

            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.MySQL, fields, ctg.TableName);

            Console.WriteLine(s);

            Assert.IsTrue(s.Query != null);
        }
		#endregion


        #region postgresql
         public void TestInsertMultiplePostgreSql()
        {
            Third third = new Third();
            third.ThirdStuff = "lalala";

            Second s = new Second();
            s.Age = 456;
            s.Quantity = 43;

            s.AttachTableMetadata(third);

            First first = new First();
            first.Name = "Gogu";

            first.AttachTableMetadata(s);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleInsertQueries(DatabaseServer.PostgreSql, first);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }

            Assert.IsTrue(list.Count > 0);
        }


        public void TestInsertPostgreSqlOverload1()
		{
            this.ctg.CategoryName = "Blossom";
            this.ctg.Description = "muhahahhaha";

            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.PostgreSql, this.ctg);

            Assert.IsTrue(s.Query != null);
		}



        public void TestInsertPostgreSqlWithDataFieldsParameters()
        {
            this.ctg.CategoryName = "TestOverload2";
            this.ctg.Description = "things";

            DatabaseField[] fields = new DatabaseField[] { ctg.TableFields[1], ctg.TableFields[2] };

            ExecutionQuery s = SqlGenerator.GenerateInsertQuery(DatabaseServer.PostgreSql, fields, ctg.TableName);

            Console.WriteLine(s);

            Assert.IsTrue(s.Query != null);
        }

        #endregion

        #endregion


        #region update


        #region sql server

        public void TestUpdateMultipleSqlServer()
        {
            First inst = new First();

            inst.Id = 56;
            inst.Name = "testMultiple";

          //  Second[] second = inst.GetSecond();

            //insert
            Second ss = new Second();
            ss.Age = 99;
            ss.Quantity = 890;
        
            //update
            Second s1 = new Second();
            s1.Id = 909;
            s1.Quantity = 90;
            s1.Age = 90;
            s1.FirstId = 56;



            Second s2 = new Second();
            s2.Id = 112;
            s2.Quantity = 11;
            s2.Age = 11;
            s2.FirstId = 56;

            inst.AttachTableMetadata(s1);
            inst.AttachTableMetadata(s2);
            
            //delete
            inst.RemoveTableMetadata(s2);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleUpdateQueries(DatabaseServer.SqlServer, inst);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }


            Assert.IsTrue(list.Count > 0);
        }

        public void TestUpdateSqlServerByPrimaryKey()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 8;
			ab.CategoryName = "Seafood";
			ab.Description = "MARius";
			//ab.Picture = b;
			ExecutionQuery exec = SqlGenerator.GenerateUpdateQuery(DatabaseServer.SqlServer, ab, true);


            ExecutionEngine.ExecuteNonQuery(DatabaseServer.SqlServer, SharedData.sqlserver, exec); 


            Console.WriteLine(exec.Query);
			Assert.IsTrue(exec.Query != string.Empty);
		}


		public void TestUpdateSqlServerByConditionalFields()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 8;
			ab.CategoryName = "Seafood";
			ab.Description = "MARius";
			//ab.Picture = b;
			ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.SqlServer, ab.TableName, false, ab.GetPrimaryKeyField(), ab.TableFields);
            Console.WriteLine(s.Query);
			Assert.IsTrue(s.Query != string.Empty);
		}

        public void TestUpdateSqlServerByMultipleConditionalFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;

            DatabaseField[] fields = new DatabaseField[] { ab.TableFields[0], ab.TableFields[1] };


            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.SqlServer, ab.TableName, false, fields , ab.TableFields);
            Console.WriteLine(s.Query);
            Assert.IsTrue(s.Query != string.Empty);
        }


		public void TestUpdateSqlServerPrimaryKeyAndDatabaseFields()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 8;
			ab.CategoryName = "Seafood";
			ab.Description = "MARius";
			//ab.Picture = b;
			DatabaseField[] df = new DatabaseField[2];
			df[0] = ab.GetField("Picture");
			df[1] = ab.GetField(0);

			ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.SqlServer, ab.TableName, true, df, ab.TableFields);
            Console.WriteLine(s.Query);
			Assert.IsTrue(s.Query != string.Empty);
		}

		#endregion


		#region access
        public void TestUpdateMultipleAccess()
        {
            First inst = new First();

            inst.Id = 56;
            inst.Name = "testMultiple";

            //  Second[] second = inst.GetSecond();

            //insert
            Second ss = new Second();
            ss.Age = 99;
            ss.Quantity = 890;

            //update
            Second s1 = new Second();
            s1.Id = 909;
            s1.Quantity = 90;
            s1.Age = 90;
            s1.FirstId = 56;



            Second s2 = new Second();
            s2.Id = 112;
            s2.Quantity = 11;
            s2.Age = 11;
            s2.FirstId = 56;

            inst.AttachTableMetadata(s1);
            inst.AttachTableMetadata(s2);

            //delete
            inst.RemoveTableMetadata(s2);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleUpdateQueries(DatabaseServer.Access, inst);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }


            Assert.IsTrue(list.Count > 0);
        }

		public void TestUpdateAccessByPrimaryKey()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 8;
			ab.CategoryName = "Seafood";
			ab.Description = "MARius";
			//ab.Picture = b;
			ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.Access, ab, true);
			Assert.IsTrue(s.Query != string.Empty);
		}


		public void TestUpdateAccessByConditionalFields()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 8;
			ab.CategoryName = "Seafood";
			ab.Description = "MARius";
			//ab.Picture = b;
			ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.Access, ab.TableName, false, ab.GetPrimaryKeyField(), ab.TableFields);
			Assert.IsTrue(s.Query != string.Empty);
		}

        public void TestUpdateAccessByMultipleConditionalFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;

            DatabaseField[] fields = new DatabaseField[] { ab.TableFields[0], ab.TableFields[1] };


            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.Access, ab.TableName, false, fields, ab.TableFields);
            Console.WriteLine(s.Query);
            Assert.IsTrue(s.Query != string.Empty);
        }


        public void TestUpdateAccessPrimaryKeyAndDatabaseFields()
		{
			CategoryTableMetadata ab = new CategoryTableMetadata();
			ab.CategoryID = 8;
			ab.CategoryName = "Seafood";
			ab.Description = "MARius";
			//ab.Picture = b;
			DatabaseField[] df = new DatabaseField[2];
			df[0] = ab.GetField("Picture");
			df[1] = ab.GetField(0);

			ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.Access, ab.TableName, true, df, ab.TableFields);
			Assert.IsTrue(s.Query != string.Empty);
		}

		#endregion


        #region  mysql
        public void TestUpdateMultipleMYSql()
        {
            First inst = new First();

            inst.Id = 56;
            inst.Name = "testMultiple";

            //  Second[] second = inst.GetSecond();

            //insert
            Second ss = new Second();
            ss.Age = 99;
            ss.Quantity = 890;

            //update
            Second s1 = new Second();
            s1.Id = 909;
            s1.Quantity = 90;
            s1.Age = 90;
            s1.FirstId = 56;



            Second s2 = new Second();
            s2.Id = 112;
            s2.Quantity = 11;
            s2.Age = 11;
            s2.FirstId = 56;

            inst.AttachTableMetadata(s1);
            inst.AttachTableMetadata(s2);

            //delete
            inst.RemoveTableMetadata(s2);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleUpdateQueries(DatabaseServer.MySQL, inst);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }


            Assert.IsTrue(list.Count > 0);
        }

        public void TestUpdateMySQLByPrimaryKey()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;
            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.MySQL, ab, true);
            Assert.IsTrue(s.Query != string.Empty);
        }


        public void TestUpdateMySQLByConditionalFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;
            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.MySQL, ab.TableName, false, ab.GetPrimaryKeyField(), ab.TableFields);
            Assert.IsTrue(s.Query != string.Empty);
        }

        public void TestUpdateMySQLByMultipleConditionalFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;

            DatabaseField[] fields = new DatabaseField[] { ab.TableFields[0], ab.TableFields[1] };


            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.MySQL, ab.TableName, false, fields, ab.TableFields);
            Console.WriteLine(s.Query);
            Assert.IsTrue(s.Query != string.Empty);
        }


        public void TestUpdateMySQLPrimaryKeyAndDatabaseFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;
            DatabaseField[] df = new DatabaseField[2];
            df[0] = ab.GetField("Picture");
            df[1] = ab.GetField(0);

            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.MySQL, ab.TableName, true, df, ab.TableFields);
            Assert.IsTrue(s.Query != string.Empty);
        }

        #endregion

        #region  PostgreSql
        public void TestUpdateMultiplePostgreSql()
        {
            First inst = new First();

            inst.Id = 56;
            inst.Name = "testMultiple";

            //  Second[] second = inst.GetSecond();

            //insert
            Second ss = new Second();
            ss.Age = 99;
            ss.Quantity = 890;

            //update
            Second s1 = new Second();
            s1.Id = 909;
            s1.Quantity = 90;
            s1.Age = 90;
            s1.FirstId = 56;



            Second s2 = new Second();
            s2.Id = 112;
            s2.Quantity = 11;
            s2.Age = 11;
            s2.FirstId = 56;

            inst.AttachTableMetadata(s1);
            inst.AttachTableMetadata(s2);

            //delete
            inst.RemoveTableMetadata(s2);

            List<ExecutionQuery> list = SqlGenerator.GenerateMultipleUpdateQueries(DatabaseServer.PostgreSql, inst);

            foreach (ExecutionQuery var in list)
            {
                Console.WriteLine(var.Query);
            }


            Assert.IsTrue(list.Count > 0);
        }

        public void TestUpdatePostgreSqlByPrimaryKey()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;
            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.PostgreSql, ab, true);
            Assert.IsTrue(s.Query != string.Empty);
        }


        public void TestUpdatePostgreSqlByConditionalFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;
            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.PostgreSql, ab.TableName, false, ab.GetPrimaryKeyField(), ab.TableFields);
            Assert.IsTrue(s.Query != string.Empty);
        }

        public void TestUpdatePostgreSqlByMultipleConditionalFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;

            DatabaseField[] fields = new DatabaseField[] { ab.TableFields[0], ab.TableFields[1] };


            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.PostgreSql, ab.TableName, false, fields, ab.TableFields);
            Console.WriteLine(s.Query);
            Assert.IsTrue(s.Query != string.Empty);
        }


        public void TestUpdatePostgreSqlPrimaryKeyAndDatabaseFields()
        {
            CategoryTableMetadata ab = new CategoryTableMetadata();
            ab.CategoryID = 8;
            ab.CategoryName = "Seafood";
            ab.Description = "MARius";
            //ab.Picture = b;
            DatabaseField[] df = new DatabaseField[2];
            df[0] = ab.GetField("Picture");
            df[1] = ab.GetField(0);

            ExecutionQuery s = SqlGenerator.GenerateUpdateQuery(DatabaseServer.PostgreSql, ab.TableName, true, df, ab.TableFields);
            Assert.IsTrue(s.Query != string.Empty);
        }

        #endregion
	

		#endregion



	}
}
