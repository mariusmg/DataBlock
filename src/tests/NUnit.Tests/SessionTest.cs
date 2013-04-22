
using System;
using System.Collections.Generic;
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
    public class SessionTest
    {
        public SessionTest()
        {
        }


        [SetUp]
        public void SetThingsUp()
        {
            Configuration.ReadConfigurationFromConfigFile();
        }

         [Test]
        public void CreateSessionTest()
        {
            Session s = Session.CreateNewSession(DatabaseServer.SqlServer, SharedData.sqlserver);
            Assert.IsNotNull(s);
        }

        [Test]
        public void TestMultipleReadExecuteOperations()
        {
            Session s = Session.CreateNewSession(DatabaseServer.SqlServer, SharedData.sqlserver);
            CategoryTableMetadata ctm = new CategoryTableMetadata();
            CategoryPersistentObject cpo = new CategoryPersistentObject(s, ctm);
            DataSet ds = cpo.GetDataSet();
            s.Close();
            Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
        }


        [Test]
        public void ExecuteMultipleOperationsInTransaction()
        {
            Session s = Session.CreateNewSession(DatabaseServer.SqlServer, SharedData.sqlserver);

            CategoryTableMetadata ctm = new CategoryTableMetadata();
            CategoryPersistentObject persistent = new CategoryPersistentObject(s, ctm);

            First first = new First();
            FirstPersistentObject fp = new FirstPersistentObject(s, first); 

            Third third = new Third();
            third.ThirdStuff = "lalala";

            Second sp= new Second();
            sp.Age = 456;
            sp.Quantity = 43;

            sp.AttachTableMetadata(third);

            
            first.Name = "Crocodilu";

            first.AttachTableMetadata(sp);

          

            s.BeginTransaction();

            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Max, ctm.TableFields[0]);

            //get the last one
            CategoryTableMetadata[] ct = (CategoryTableMetadata[]) persistent.GetTableMetadata(qc);
            persistent.Delete(ct[0]);




             CategoryTableMetadata cc = new CategoryTableMetadata();
            cc.CategoryName = "fluffy";
            cc.Description = "tingling";

            persistent.Create(cc);

            fp.Create(first);


            List<ExecutionQuery> scc = s.Queries;


            for (int i = 0; i < scc.Count; i++)
            {
                Console.WriteLine(scc[i]);
            }


            s.Commit();

        }

    }
}
