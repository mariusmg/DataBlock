using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using voidsoft.DataBlock;

namespace tests
{

    [TestFixture]
    public class LoggingTest
    {
        private bool isOk;

        [SetUp]
        public void SetThingsUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
            Configuration.OnQueryLogging += new Configuration.QueryLoggingEventHandler(Configuration_OnQueryLogging);
        }

        void Configuration_OnQueryLogging(string dataToLog)
        {
            string g = dataToLog;

            isOk = (g.Length > 0);
        }


        [Test]
        public void TestExecuteScalarStringQuery()
        {
            object x = ExecutionEngine.ExecuteScalar(DatabaseServer.SqlServer, SharedData.sqlserver2005, new ExecutionQuery("SELECT Count(CategoryID) FROM Categories", null));
            Assert.IsTrue(isOk);
        }




    }
}
