
using System;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using Extender;
using voidsoft.DataBlock;
using NUnit.Framework;
using System.Xml;
using System.Collections.Generic;

namespace tests
{

    [TestFixture]
    public class ExecutionEngineStaticTest
    {
        public ExecutionEngineStaticTest()
        {
        }

        [SetUp]
        public void SetThingsUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
        }

        #region execute scalar

        [Test]
        public void TestExecuteScalarStringQuery()
        {
            object x = ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver, new ExecutionQuery("SELECT Count(CategoryID) FROM Categories", null));
            Assert.IsTrue(x != null);
        }


        [Test]
        public void TestExecuteScalarStoredProcedureWithoutParameters()
        {
           object x = ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver, "GetCountCategories");
           Console.WriteLine(x.ToString());
           Assert.IsTrue(x != null);
        }


        [Test]
        public void TestExecuteScalarStoredProcedureWithParameters()
        {

           object value = ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver, new ExecutionQuery( "SELECT MAX(CategoryID) FROM Categories", null));


            IDataParameter paramId = null;
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref paramId);
            paramId.Value = value;
            paramId.ParameterName = "@Id";


            object result = ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver, "GetCategoryById", paramId);

            Assert.IsTrue(result != null);
        }


        [Test]
        public void TestExecuteScalarMultipleStringQueries()
        {

            List<ExecutionQuery> list = new List<ExecutionQuery>();



            list.Add(new ExecutionQuery("SELECT Count(CategoryID) FROM Categories", null));
            list.Add(new ExecutionQuery("SELECT Count(CategoryName) FROM Categories", null));

            object[] x = null;

            ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver, list, out x);

            Console.WriteLine(x.Length);

            Assert.IsTrue(x != null);
        }
        


        #endregion


        #region execute dataset

        [Test]
        public void TestExecuteDataSetStringQuery()
        {
            DataSet ds = ExecutionEngine.ExecuteDataSet(DatabaseServer.SqlServer, SharedData.sqlserver, new ExecutionQuery("SELECT * FROM Categories", null));
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestExecuteDataSetFillAlreadyExistingDataSet()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(new DataTable("puscasi"));

            ExecutionQuery exec = new ExecutionQuery("SELECT * FROM Categories", null);

            ExecutionEngine.ExecuteDataSet(DatabaseServer.SqlServer, SharedData.sqlserver, exec, ref ds);

            Assert.IsTrue(ds.Tables.Count == 2);
        }


        [Test]
        public void TestExecuteDataSetStoredProcedureWithoutParameters()
        {
            DataSet ds = ExecutionEngine.ExecuteDataSet(DatabaseServer.SqlServer, SharedData.sqlserver, "GetAllCategories");
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestExecuteDataSetStoredProcedureWithParameters()
        {
            IDataParameter paramId = null;
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref paramId);
            paramId.Value = 1;
            paramId.ParameterName = "@Id";

            DataSet ds = ExecutionEngine.ExecuteDataSet(DatabaseServer.SqlServer, SharedData.sqlserver, "GetCategoriesWithIdBiggerThan", paramId);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }



        #endregion


        #region execute DataReader
        [Test]
        public void TestExecuteDataReaderStringQuery()
        {
            IDataReader iread = ExecutionEngine.ExecuteReader(DatabaseServer.SqlServer, SharedData.sqlserver, new ExecutionQuery( "SELECT * FROM Categories", null));
            Assert.IsTrue(iread != null);
        }

        [Test]
        public void TestExecuteDataReaderStoredProcedureWithoutParameters()
        {
            IDataReader iread  = ExecutionEngine.ExecuteReader(DatabaseServer.SqlServer, SharedData.sqlserver, "GetAllCategories");
            while (iread.Read())
            {
                Console.WriteLine(iread.GetValue(1));
            }
            Assert.IsTrue(iread != null);
        }
    
        [Test]
        public void TestExecuteDataReaderStoredProcedureWithParameters()
        {

            IDataParameter paramId = null;
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref paramId);
            paramId.Value = 1;
            paramId.ParameterName = "@Id";

            IDataReader iread = ExecutionEngine.ExecuteReader(DatabaseServer.SqlServer, SharedData.sqlserver, "GetCategoriesWithIdBiggerThan", paramId);

            while (iread.Read())
            {
                Console.WriteLine(iread.GetValue(1));
            }
            
            Assert.IsTrue(iread != null);
        }

        [Test]
        public void TestExecuteDataReaderXmlForSqlServer2000()
        {
            ExecutionQuery exec = new ExecutionQuery("SELECT * FROM First FOR XML Auto", null);

            XmlReader xr = ExecutionEngine.ExecuteXmlReader(SharedData.sqlserver, exec);
            Assert.IsTrue(xr != null);
        }


        #endregion


        #region execute non query
        [Test]  
        public void TestExecuteNonQueryStringQuery()
        {
            ExecutionQuery xc = new ExecutionQuery("INSERT INTO First VALUES('bwhahahhahaha')", null);


            int x = ExecutionEngine.ExecuteNonQuery(DatabaseServer.SqlServer, SharedData.sqlserver, xc);
            Assert.IsTrue(x > 0);
        }

        [Test]
        public void TestExecuteNonQueryStringCollection()
        {
            StringCollection sc = new StringCollection();

            List<ExecutionQuery> listExecution = new List<ExecutionQuery>();

            listExecution.Add( new ExecutionQuery("INSERT INTO First VALUES('bwhahahhahaha')", null));
            listExecution.Add( new ExecutionQuery("INSERT INTO First VALUES('x')", null));
            listExecution.Add(new ExecutionQuery("INSERT INTO First VALUES('a')", null));
            listExecution.Add(new ExecutionQuery("INSERT INTO First VALUES('b')", null));

            int x = ExecutionEngine.ExecuteNonQuery(DatabaseServer.SqlServer, SharedData.sqlserver, listExecution, IsolationLevel.Serializable);
            Assert.IsTrue(x == 4);
        }


        [Test]
        public void TestExecuteNonQueryStoredProcedureWithoutParameters()
        {
            int x = ExecutionEngine.ExecuteNonQuery(DatabaseServer.SqlServer, SharedData.sqlserver, "CreateDefaultCustomer");
            Assert.IsTrue(x > 0);
        }


        [Test]
        public void TestExecuteNonQueryStoredProcedureWithParameters()
        {
            IDataParameter paramName = null;
            IDataParameter paramAge = null;
            IDataParameter paramBirthDate = null;
            IDataParameter paramMale = null;

            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer,ref  paramName);
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref  paramAge);
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer,ref  paramBirthDate);
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer,ref  paramMale);

            paramName.ParameterName = "@Name";
            paramName.Value = "Stanescu";
            paramAge.ParameterName = "@Age";
            paramAge.Value = 99;
            paramMale.ParameterName = "@Male";
            paramMale.Value = true;
            paramBirthDate.ParameterName = "@BirthDate";
            paramBirthDate.Value = DateTime.Now.ToShortDateString();

            IDataParameter[] par = new IDataParameter[4];
            par[0] = paramName;
            par[1] = paramAge;
            par[2] = paramBirthDate;
            par[3] = paramMale;

            int x = ExecutionEngine.ExecuteNonQuery(DatabaseServer.SqlServer, SharedData.sqlserver, "CreateCustomer", par);
            Assert.IsTrue(x > 0);
        }

        //[Test]
        //public void TestExecuteNonQueryGetLastGeneratedId()
        //{
        //    object x = null;
        //    int z = ExecutionEngine.ExecuteNonQuery(DatabaseServer.SqlServer, SharedData.sqlserver, "INSERT INTO First values ('buhahhaha')","First", "Id", ref x);
        //    Assert.IsTrue(z > 0 && x != null);
        //}


        [Test]
        public void TestExecuteNonQueryGeneratedGetLastGeenratedId()
        {
            ////build the sql queries
            //StringCollection sc = new StringCollection();

            //sc.Add("GetPrimaryKey//First-Id;INSERT INTO First values('muhahahhahah')");
            //sc.Add("GetPrimaryKey//First-Id;INSERT INTO First values('muci')");

            //object[] results = null;
            //ExecutionEngine.ExecuteNonQueryGenerated(DatabaseServer.SqlServer, SharedData.sqlserver, sc, IsolationLevel.Serializable, out results);

            //for (int i = 0; i < results.Length; i++)
            //{
            //    Console.WriteLine(results[i].ToString());
            //}

            //Assert.IsTrue(results.Length > 0);
        }


        [Test]
        public void TestExecuteNonQueryGeneratedGetPrimaryKeyConstraint()
        {
            //StringCollection scData = new StringCollection();

            //IDbConnection icon = null;
            //IDbCommand icmd = null;

            //DataFactory.InitializeConnection(DatabaseServer.SqlServer, ref icon);
            //DataFactory.InitializeCommand(DatabaseServer.SqlServer, ref icmd);

            //icmd.Connection = icon;
            //icon.ConnectionString = SharedData.sqlserver;
            //icon.Open();

            //scData.Add("PrimaryKeyConstraint//2//Id-First");
            //scData.Add("INSERT INTO FIRST VALUES('testttttttt')");
            //scData.Add("INSERT INTO Second values(???,456,566)");
            //scData.Add("INSERT INTO Second values(???,22,5)");

            //int x = ExecutionEngine.ExecuteNonQueryGenerated(ref icon, ref  icmd, scData);

            //Assert.IsTrue(x > 0);


        }
        #endregion



    }
}
