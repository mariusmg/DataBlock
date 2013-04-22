
using System;
using System.Text;
using Extender;
using voidsoft.DataBlock;
using NUnit.Framework;


namespace tests
{
    [TestFixture]
    public class TableMetadataTEST
    {

        private CategoryTableMetadata ctg = null;

        [SetUp]
        public void SetUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
            ctg = new CategoryTableMetadata();
        }



        [Test]
        public void TestGetFieldByIndex()
        {
            DatabaseField df = ctg.GetField(1);
            Assert.IsTrue(df.fieldName != string.Empty);
        }

        [Test]
        public void TestGetFieldByString()
        {
            DatabaseField df = ctg.GetField("CategoryID");
            Assert.IsTrue(df.fieldName != string.Empty);
        }


        [Test]
        public void TestGetPrimaryKeyField()
        {
            DatabaseField df = ctg.GetPrimaryKeyField();
            Assert.IsTrue(df.fieldName != string.Empty);
        }

        [Test]
        public void TestSetFieldValueOverload1()
        {
            DatabaseField df = ctg.GetField(0);
            ctg.SetFieldValue(df.fieldName, 5);
            Assert.IsTrue(ctg.TableFields[0].fieldValue.ToString() == "5");
        }

        [Test]
        public void TestSetFieldValueOverload2()
        {
            DatabaseField df = ctg.GetField(0);
            ctg.SetFieldValue(0, 5);
            Assert.IsTrue(ctg.TableFields[0].fieldValue.ToString() == "5");
        }


        [Test]
        public void GetFieldByIndexer()
        {
            CustomerTableMetadata customer = new CustomerTableMetadata();
            DatabaseField field =     customer[CustomerTableMetadata.CustomerFields.Age];

            Assert.IsTrue(field.fieldName == "Age");
        }

        [Test]
        public void TestClone()
        {
            CustomerTableMetadata c = new CustomerTableMetadata();
            c.Age = 32;
            c.Birthdate = new DateTime(1000, 1, 3);
            c.Id = 34;
            c.Male = true;
            c.Name = "F";

            CustomerTableMetadata ck = c.Clone<CustomerTableMetadata>();


            for (int i = 0; i < ck.TableFields.Length; i++)
            {
                Console.WriteLine(ck.TableFields[i].fieldName + " = " + ck.TableFields[i].fieldValue.ToString());
            }

            Assert.IsTrue(ck.Name == "F");
        
        }



    }
}
