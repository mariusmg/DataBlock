
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NUnit.Framework;
using voidsoft.DataBlock;
using System.Collections;
using System.Collections.Specialized;



namespace tests
{

    [TestFixture()]
    public class ExecutionEngineInstanceTest
    {
        public ExecutionEngineInstanceTest()
        {
        }

        private ExecutionEngine dal = null;


        [SetUp]
        public void SetThingsUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
            dal = ExecutionEngine.CreateNewExecutionEngine(DatabaseServer.SqlServer, SharedData.sqlserver);
        }


        #region execute non query
        [Test]
        public void TestExecuteNonQueryStringQuery()
        {
            ExecutionQuery exec = new ExecutionQuery("INSERT INTO First VALUES('bwhahahhahaha')", null);

            int x = dal.ExecuteNonQuery(exec);
            Assert.IsTrue(x > 0);
        }

        [Test]
        public void TestExecuteNonQueryStringCollection()
        {
            List<ExecutionQuery> sc = new List<ExecutionQuery>();

            sc.Add(new ExecutionQuery("INSERT INTO First VALUES('bwhahahhahaha')", null));
            sc.Add(new ExecutionQuery( "INSERT INTO First VALUES('x')", null));
            sc.Add(new ExecutionQuery( "INSERT INTO First VALUES('a')", null));
            sc.Add(new ExecutionQuery( "INSERT INTO First VALUES('b')", null));

            int x = dal.ExecuteNonQuery(sc, IsolationLevel.Serializable);
            Assert.IsTrue(x == 4);
        }


        [Test]
        public void TestExecuteNonQueryStoredProcedureWithoutParameters()
        {
            int x = dal.ExecuteNonQuery("CreateDefaultCustomer");
            Assert.IsTrue(x > 0);
        }


        [Test]
        public void TestExecuteNonQueryStoredProcedureWithParameters()
        {
            IDataParameter paramName = null;
            IDataParameter paramAge = null;
            IDataParameter paramBirthDate = null;
            IDataParameter paramMale = null;

            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref  paramName);
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref  paramAge);
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref  paramBirthDate);
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref  paramMale);

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

            int x = dal.ExecuteNonQuery("CreateCustomer", par);
            Assert.IsTrue(x > 0);
        }

        [Test]
        public void TestExecuteNonQueryGetLastGeneratedId()
        {
            object x = null;

            ExecutionQuery exec = new ExecutionQuery("INSERT INTO First values ('buhahhaha')",null);

            int z = dal.ExecuteNonQuery(exec,"First", "Id", ref x);
            Assert.IsTrue(z > 0 && x != null);
        }


//        [Test]
//        public void TestExecuteNonQueryGeneratedGetLastGeenratedId()
//        {
//            //build the sql queries
//            StringCollection sc = new StringCollection();
//
//            sc.Add("GetPrimaryKey//First-Id;INSERT INTO First values('muhahahhahah')");
//            sc.Add("GetPrimaryKey//First-Id;INSERT INTO First values('muci')");
//
//            object[] results = null;
//            dal.ExecuteNonQueryGenerated(sc, IsolationLevel.Serializable, out results);
//
//            for (int i = 0; i < results.Length; i++)
//            {
//                Console.WriteLine(results[i].ToString());
//            }
//
//            Assert.IsTrue(results.Length > 0);
//        }
//
//
//        [Test]
//        public void TestExecuteNonQueryGeneratedGetPrimaryKeyConstraint()
//        {
//            StringCollection scData = new StringCollection();
//
//            IDbConnection icon = null;
//            IDbCommand icmd = null;
//
//            DataFactory.InitializeConnection(DatabaseServer.SqlServer, ref icon);
//            DataFactory.InitializeCommand(DatabaseServer.SqlServer, ref icmd);
//
//            icmd.Connection = icon;
//            icon.ConnectionString = SharedData.sqlserver;
//            icon.Open();
//
//            scData.Add("PrimaryKeyConstraint//2//Id-First");
//            scData.Add("INSERT INTO FIRST VALUES('testttttttt')");
//            scData.Add("INSERT INTO Second values(???,456,566)");
//            scData.Add("INSERT INTO Second values(???,22,5)");
//
//            int x = dal.ExecuteNonQueryGenerated(ref icon, ref  icmd, scData);
//
//            Assert.IsTrue(x > 0);
//
//
//        }
        #endregion
       
        #region Data Reader
        [Test]
        public void TestExecuteDataReaderStringQuery()
        {
            ExecutionQuery exec = new ExecutionQuery("SELECT * FROM Categories",null);

            IDataReader iread = dal.ExecuteReader(exec, CommandBehavior.Default);
            Assert.IsTrue(iread != null);
        }

        [Test]
        public void TestExecuteDataReaderStoredProcedureWithoutParameters()
        {
            IDataReader iread = dal.ExecuteReader( "GetAllCategories", CommandBehavior.Default);
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

            IDataReader iread = dal.ExecuteReader("GetCategoriesWithIdBiggerThan", CommandBehavior.Default, paramId);

            while (iread.Read())
            {
                Console.WriteLine(iread.GetValue(1));
            }

            Assert.IsTrue(iread != null);
        }

       #endregion
        
        #region ExecuteScalar
        [Test]
        public void TestExecuteScalarStringQuery()
        {

            ExecutionQuery exc = new ExecutionQuery("SELECT Count(CategoryID) FROM Categories", null);


            object x = dal.ExecuteScalar(exc);
            Assert.IsTrue(x != null);
        }


        [Test]
        public void TestExecuteScalarStoredProcedureWithoutParameters()
        {
            object x = dal.ExecuteScalar("GetCountCategories");
            Console.WriteLine(x.ToString());
            Assert.IsTrue(x != null);
        }


        [Test]
        public void TestExecuteScalarStoredProcedureWithParameters()
        {

            object value = dal.ExecuteScalar( new ExecutionQuery("SELECT MAX(CategoryID) FROM Categories", null));


            IDataParameter paramId = null;
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref paramId);
            paramId.Value = value;
            paramId.ParameterName = "@Id";


            object result = ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver, "GetCategoryById", paramId);

            Assert.IsTrue(result != null);
        }
        #endregion
        
        #region DataSet

        [Test]
        public void TestExecuteDataSetStringQuery()
        {
            ExecutionQuery exec = new ExecutionQuery("SELECT * FROM Customer", null);

            DataSet ds = dal.ExecuteDataSet(exec);
            Console.WriteLine("cust " + ds.Tables[0].Rows.Count);
            Assert.IsTrue(ds != null);
        }


        public void TestExecuteDataSetStringQueryExistingDataSet()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("buhahahhaha");

            ExecutionQuery exec = new ExecutionQuery("SELECT * FROM Customer", null);

            dal.ExecuteDataSet(exec, ref ds);
            Console.WriteLine("cust " + ds.Tables[0].Rows.Count);
            Assert.IsTrue(ds.Tables.Count == 2);
        }


        [Test]
        public void TestExecuteDataSetStoredProcedureWithoutParameters()
        {
            DataSet ds = dal.ExecuteDataSet("GetAllCategories");
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestExecuteDataSetStoredProcedureWithParameters()
        {
            IDataParameter paramId = null;
            voidsoft.DataBlock.DataFactory.InitializeDataParameter(DatabaseServer.SqlServer, ref paramId);
            paramId.Value = 1;
            paramId.ParameterName = "@Id";

            DataSet ds = dal.ExecuteDataSet("GetCategoriesWithIdBiggerThan", paramId);
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }
        #endregion
        
    }
}
