
using System;
using System.Text;
using NUnit.Framework;
using Extender;
using System.Data;
using voidsoft.DataBlock;


namespace tests
{

    [TestFixture]
    public class QueryCriteriaTest
    {

        private First first = new First();
        private Second second = new Second();
        private Third third = new Third();

        private FirstPersistentObject psqlserver;// new FirstPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.first);
        private FirstPersistentObject msqlserver;// = new FirstPersistentObject(DatabaseServer.MySQL, SharedData.sqlserver, this.first);
        private FirstPersistentObject pAccess;// = new FirstPersistentObject(DatabaseServer.Access, SharedData.sqlserver, this.first);



        [SetUp]
        public void SetThingsUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
        }

        public QueryCriteriaTest()
        {
            psqlserver = new FirstPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, this.first);
            msqlserver = new FirstPersistentObject(DatabaseServer.MySQL, SharedData.mysql, this.first);
            pAccess = new FirstPersistentObject(DatabaseServer.Access, SharedData.access, this.first);

        }

        #region sql server

        [Test]
        public void TestSqlServerMax()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, first.TableFields[0]);
            //IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = iq.GenerateSelect(qc);

            // Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);

        }


        [Test]
        public void TestSqlServerMin()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Min, first.TableFields[0]);
            //IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = iq.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);

        }

        [Test]
        public void TestSqlServerCount()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Count, first.TableFields[0]);
            //IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = iq.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);

        }


        [Test]
        public void TestSqlServerBetween()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 1, 10);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s =  ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerEquality()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 1);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);
            //Console.WriteLine(s);
            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerHigher()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 1);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerHigherOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.HigherOrEqual, c.TableFields[0], 1);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerIsNotNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNotNull, c.TableFields[0]);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerIsNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerLike()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Like, c.TableFields[1], "A");
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestSqlServerNot()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 456);
            qc.Add(CriteriaOperator.Not, c.TableFields[0]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[1], "A");

            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerSmaller()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Smaller, c.TableFields[0], 47);
            qc.AddAlias(c.TableFields[0].fieldName, "kko_cu_lapte");
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestSqlServerSmallerOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.SmallerOrEqual, c.TableFields[0], 47);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);


            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestSqlServerOr()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);
            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }



        [Test]
        public void TestSqlServerDistinct()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Distinct, c.TableFields[0]);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }



        [Test]
        public void TestSqlServerOrderBy()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "desc");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestSqlServerMultiple()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 48, 70);
            qc.Add(CriteriaOperator.Like, c.TableFields[1], "blah");
            qc.Add(CriteriaOperator.Not, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "desc");


            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestSqlServerAliases()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 48, 70);
            qc.Add(CriteriaOperator.Like, c.TableFields[1], "blah");
            qc.Add(CriteriaOperator.Not, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "desc");

            //aliases

            qc.AddAlias("Age", "AAAAAAAg");
            qc.AddAlias("Name", "Numele_bahhhhhhhhhhhhhh");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.SqlServer);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = psqlserver.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }















        #region sqlserver joins

        [Test]
        public void TestJoinSqlServerSingleJoin()
        {
            QueryCriteria qcFirst = new QueryCriteria(first);
            QueryCriteria qcSecond = new QueryCriteria(second);
            QueryCriteria qcThird = new QueryCriteria(third);

            qcFirst.AddJoin(JoinType.Inner, first.TableName,  first.TableFields[0], second.TableName, second.TableFields[1], qcSecond);

            qcFirst.Add(CriteriaOperator.OrderBy, first.TableFields[0], "Asc");

            DataSet ds = psqlserver.GetDataSet(qcFirst);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestJoinSqlServerMultipleJoins()
        {
            QueryCriteria qcFirst = new QueryCriteria(first);
            QueryCriteria qcSecond = new QueryCriteria(second);
            QueryCriteria qcThird = new QueryCriteria(third);
            qcThird.Add(CriteriaOperator.Equality, third.TableFields[1], "tt");

            qcFirst.AddJoin(JoinType.Inner, first.TableName, first.TableFields[0],second.TableName, second.TableFields[1], qcSecond);
            qcFirst.Add(CriteriaOperator.Higher, first.TableFields[0], 1);

            qcFirst.AddJoin(JoinType.Inner, second.TableName,  second.TableFields[0], third.TableName, third.TableFields[2], qcThird);

            DataSet ds = psqlserver.GetDataSet(qcFirst);
            Assert.IsTrue(ds != null);
        }

        #endregion


        #endregion

        
        #region access

        [Test]
        public void TestAccessMax()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[1]);
            qc.Add(CriteriaOperator.Max, first.TableFields[1]);
            //IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = iq.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);

        }


        [Test]
        public void TestAccessMin()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Min, first.TableFields[0]);
            //IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = iq.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);

        }

        [Test]
        public void TestAccessCount()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Count, first.TableFields[0]);
            //IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = iq.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);

        }


        [Test]
        public void TestAccessBetween()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 1, 10);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessEquality()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 1);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = ss.GenerateSelect(qc);
            //Console.WriteLine(s);
            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessHigher()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 1);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessHigherOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.HigherOrEqual, c.TableFields[0], 1);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessIsNotNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNotNull, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessIsNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessLike()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Like, c.TableFields[2], "A");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestAccessNot()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 456);
            qc.Add(CriteriaOperator.Not, c.TableFields[0]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[1], "A");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessSmaller()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Smaller, c.TableFields[0], 47);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestAccessSmallerOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.SmallerOrEqual, c.TableFields[0], 47);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);


            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestAccessOr()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);
            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }



        [Test]
        public void TestAccessDistinct()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Distinct, c.TableFields[0]);
            //IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            //string s = ss.GenerateSelect(qc);

            //Console.WriteLine(s);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }



        [Test]
        public void TestAccessOrderBy()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "desc");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }


        [Test]
        public void TestAccessMultiple()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 48, 70);
            qc.Add(CriteriaOperator.Like, c.TableFields[1], "blah");
            qc.Add(CriteriaOperator.Not, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "desc");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.Access);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            DataSet ds = pAccess.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        #endregion
        

        #region MySQL

        [Test]
        public void TestMySqlMax()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, first.TableFields[0]);
            IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = iq.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != null);

        }

        [Test]
        public void TestMySQLBetween()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 1, 10);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLEquality()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 1);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLHigher()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 1);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLHigherOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.HigherOrEqual, c.TableFields[0], 1);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLIsNotNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNotNull, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLIsNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLLike()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Like, c.TableFields[2], "A");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        [Test]
        public void TestMySQLNot()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 456);
            qc.Add(CriteriaOperator.Not, c.TableFields[0]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[2], "A");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLSmaller()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Smaller, c.TableFields[0], 47);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        [Test]
        public void TestMySQLSmallerOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.SmallerOrEqual, c.TableFields[0], 47);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        [Test]
        public void TestMySQLOr()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySqlDistinct()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Distinct, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }



        [Test]
        public void TestMySqlOrderBy()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "desc");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestMySQLMultiple()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 48, 70);
            qc.Add(CriteriaOperator.Like, c.TableFields[1], "blah");
            qc.Add(CriteriaOperator.Not, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "desc");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.MySQL);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        #endregion


        #region PostgreSql
        [Test]
        public void TestPostgreSqlMax()
        {

            QueryCriteria qc = new QueryCriteria(first.TableName, first.TableFields[0]);
            qc.Add(CriteriaOperator.Max, first.TableFields[0]);
            IQueryCriteriaGenerator iq = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = iq.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != null);

        }

        [Test]
        public void TestPostgreSqlBetween()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 1, 10);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlEquality()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 1);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlHigher()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 1);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlHigherOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.HigherOrEqual, c.TableFields[0], 1);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlIsNotNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNotNull, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlIsNull()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlLike()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Like, c.TableFields[2], "A");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        [Test]
        public void TestPostgreSqlNot()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Higher, c.TableFields[0], 456);
            qc.Add(CriteriaOperator.Not, c.TableFields[0]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[2], "A");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlSmaller()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Smaller, c.TableFields[0], 47);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        [Test]
        public void TestPostgreSqlSmallerOrEqual()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.SmallerOrEqual, c.TableFields[0], 47);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        [Test]
        public void TestPostgreSqlOr()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlDistinct()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Distinct, c.TableFields[0]);
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }



        [Test]
        public void TestPostgreSqlOrderBy()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "desc");
            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }


        [Test]
        public void TestPostgreSqlMultiple()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            QueryCriteria qc = new QueryCriteria(c);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 47);
            qc.Add(CriteriaOperator.Or, c.TableFields[1]);
            qc.Add(CriteriaOperator.Equality, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.Between, c.TableFields[0], 48, 70);
            qc.Add(CriteriaOperator.Like, c.TableFields[1], "blah");
            qc.Add(CriteriaOperator.Not, c.TableFields[0], 48);
            qc.Add(CriteriaOperator.IsNull, c.TableFields[0]);
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[1], "asc");
            qc.Add(CriteriaOperator.OrderBy, c.TableFields[0], "desc");

            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(DatabaseServer.PostgreSql);
            ExecutionQuery s = ss.GenerateSelect(qc);

            Console.WriteLine(s.Query);

            Assert.IsTrue(s.Query != string.Empty);
        }

        #endregion
        

        #region Joins




        #endregion
    }
}
