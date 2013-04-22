using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using NUnit.Framework;
using Extender;
using System.Data;
using voidsoft.DataBlock;



namespace tests
{


    [TestFixture]
    public class DataConvertorsTEST
    {

        [SetUp]
        public void SetThingsup()
        {
            Configuration.ReadConfigurationFromConfigFile();
        }


        [Test]
        public void TestConvertToDataParameter()
        {

            CategoryTableMetadata ctm = new CategoryTableMetadata();
            IDataParameter[] iparams = DataConvertor.ConvertToDataParameter(DatabaseServer.SqlServer, ctm);

            Assert.IsTrue(iparams.Length > 0);
        }

        [Test]
        public void TestConvertToDataTable()
        {

            CategoryTableMetadata c = new CategoryTableMetadata();
            CategoryPersistentObject cto = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, c);
            CategoryTableMetadata[] categs = (CategoryTableMetadata[])cto.GetTableMetadata();

            DataTable dt = DataConvertor.ConvertToDataTable(categs);

            Assert.IsTrue(categs.Length > 0);

        }

        [Test]
        public void TestConvertToDataTableOverload2()
        {

            CategoryTableMetadata c = new CategoryTableMetadata();
            CategoryPersistentObject cto = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, c);
            CategoryTableMetadata[] categs = (CategoryTableMetadata[])cto.GetTableMetadata();

            DataTable dt = DataConvertor.ConvertToDataTable(categs, categs[0].GetField(1), categs[0].GetField(2));

            Assert.IsTrue(categs.Length > 0);
        }

        [Test]
        public void TestConvertToStringCollection()
        {
            CategoryTableMetadata c = new CategoryTableMetadata();
            CategoryPersistentObject cto = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, c);
            CategoryTableMetadata[] categs = (CategoryTableMetadata[])cto.GetTableMetadata();

            StringCollection dt = DataConvertor.ConvertToStringCollection(categs, categs[0].GetField(1));

            Assert.IsTrue(dt.Count > 0);
        }

        [Test]
        public void TestConvertToHashtable()
        {
            CategoryTableMetadata c = new CategoryTableMetadata();
            CategoryPersistentObject cto = new CategoryPersistentObject(DatabaseServer.SqlServer, SharedData.sqlserver, c);
            CategoryTableMetadata[] categs = (CategoryTableMetadata[])cto.GetTableMetadata();

            Hashtable dt = DataConvertor.ConvertToHashtable(categs, categs[0].GetField(0), categs[0].GetField(1));

            Assert.IsTrue(dt.Count > 0);

        }

    }
}
