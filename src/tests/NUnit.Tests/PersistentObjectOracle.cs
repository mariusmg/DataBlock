using System;
using System.Collections;
using System.Data;
using System.IO;
using Extender;
using NUnit.Framework;
using voidsoft.DataBlock;

namespace tests
{
    [TestFixture]
    public class PersistentObjectOracle
    {
        private CategoryTableMetadata ctm = null;
        private CategoryPersistentObject cpo = null;


        private First firstTable = null;
        private FirstPersistentObject firstPersistent = null;


        [SetUp]
        public void Init()
        {
            Configuration.ReadConfigurationFromConfigFile();

            ctm = new CategoryTableMetadata();
            cpo = new CategoryPersistentObject(DatabaseServer.Oracle, SharedData.sqlserver, ctm);

            firstTable = new First();
            firstPersistent = new FirstPersistentObject(DatabaseServer.Oracle, SharedData.sqlserver, firstTable);

            //prepare the database
        }


        public void InitDatabase()
        {
        }

        #region GetDataTable

        [Test]
        public void GetDataTable()
        {
            DataTable dt = cpo.GetDataTable();
            Assert.IsTrue(dt != null);
        }

        [Test]
        public void GetDataTableFields()
        {
            DataTable dt = cpo.GetDataTable(ctm[CategoryTableMetadata.CategoriesFields.CategoryID]);
            Assert.IsTrue(dt != null);
        }

        [Test]
        public void GetDataTableQueryCriteria()
        {
            QueryCriteria qc =
                new QueryCriteria(ctm.TableName, ctm[CategoryTableMetadata.CategoriesFields.CategoryID],
                                  ctm[CategoryTableMetadata.CategoriesFields.Description]);
            qc.Add(CriteriaOperator.Higher, ctm[CategoryTableMetadata.CategoriesFields.CategoryID], 2);
            DataTable dt = cpo.GetDataTable(qc);

            Assert.IsTrue(dt != null);
        }

        #endregion

        #region GetDataSet

        [Test]
        public void TestGetDataSet()
        {
            DataSet ds = cpo.GetDataSet();
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestGetDataSetByFields()
        {
            DataSet ds = cpo.GetDataSet(ctm.TableFields[0], ctm.TableFields[1]);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestGetDataSetByQueryCriteria()
        {
            QueryCriteria qc = new QueryCriteria(ctm);
            qc.Add(CriteriaOperator.Equality, qc.Fields[0], 1);
            DataSet ds = cpo.GetDataSet(qc);
            Assert.IsTrue(ds != null);
        }

        [Test]
        public void TestGetDataSetByRelation()
        {
            DataSet ds = firstPersistent.GetDataSet("Second", 57);
            Assert.IsTrue(ds != null);
        }

        #endregion

        #region GetFieldList

        [Test]
        public void GetStringCollectionFieldList()
        {
            ArrayList sc = cpo.GetFieldList(this.ctm.TableFields[0]);
            Assert.IsTrue(sc != null);
        }


        [Test]
        public void GetSortedListFieldList()
        {
            SortedList sc = cpo.GetFieldList(this.ctm.TableFields[0], this.ctm.TableFields[1]);
            Assert.IsTrue(sc != null);
        }


        [Test]
        public void GetFieldListByQueryCriteria()
        {
            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Equality, qc.Fields[0], 1);
            ArrayList sc = cpo.GetFieldList(qc);

            Assert.IsTrue(sc != null);
        }

        #endregion

        #region GetValue

        [Test]
        public void TestGetValueByDatabaseFields()
        {
            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Max, qc.Fields[0]);
            object result = cpo.GetValue(qc);
            Assert.IsTrue(result != null);
        }

        #endregion

        #region  GetTableMetadata

        [Test]
        public void GetTableMetadataList()
        {
            //ListTableMetadata<CategoryTableMetadata> l = cpo.GetTableMetadataList<CategoryTableMetadata>();

            //foreach (CategoryTableMetadata var in l)
            //{
            //    Console.WriteLine(var.CategoryName);
            //}

            //Assert.IsTrue(l.Count > 0);
        }


        [Test]
        public void GetTableMetadata()
        {
            CategoryTableMetadata[] ctm = (CategoryTableMetadata[]) cpo.GetTableMetadata();
            Assert.IsTrue(ctm != null);
        }

        [Test]
        public void GetTableMetadataByQueryCriteria()
        {
            QueryCriteria qc = new QueryCriteria(ctm);
            qc.Add(CriteriaOperator.Higher, ctm.TableFields[0], 0);

            CategoryTableMetadata[] ctg = (CategoryTableMetadata[]) cpo.GetTableMetadata(qc);
            Assert.IsTrue(ctg != null);
        }


        [Test]
        public void TestGetTableMetadataByPrimaryKey()
        {
            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Max, ctm.TableFields[0]);
            object val = cpo.GetValue(qc);

            CategoryTableMetadata ctmm = (CategoryTableMetadata) this.cpo.GetTableMetadata(val);
            Assert.IsTrue(ctmm != null);
        }


        [Test]
        public void GetTableMetadataByRelation()
        {
            Second[] ctm = (Second[]) this.firstPersistent.GetTableMetadata("Second", typeof (Second), 57);
            Assert.IsTrue(ctm != null);
        }

        #endregion

        #region IsUnique

        [Test]
        public void TestIsUnique()
        {
            bool unique = false;

            //check if we have an ID with the specified value. We shoudn't
            unique = cpo.IsUnique(ctm.TableFields[0], 256756);

            Console.WriteLine(unique.ToString());
            Assert.IsTrue(unique == true);
        }

        #endregion

        #region Create

        [Test]
        public void TestCreateSimple()
        {
            CategoryTableMetadata c = new CategoryTableMetadata();

            c.CategoryName = "muhahaha";
            c.Description = "buhahahah";

            byte[] file = File.ReadAllBytes(SharedData.LARGE_BLOB_FILE_PATH);

            c.Picture = file;

            cpo.Create(c);
        }


        [Test]
        public void TestCreateMultiple()
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

            firstPersistent.Create(first);
        }

        #endregion

        #region Delete

        [Test]
        public void DeleteByPrimaryKey()
        {
            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Min, ctm.TableFields[0]);
            object val = cpo.GetValue(qc);

            this.cpo.Delete(val);
        }


        [Test]
        public void TestDelete()
        {
            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Max, ctm.TableFields[0]);
            object val = cpo.GetValue(qc);

            CategoryTableMetadata c = (CategoryTableMetadata) this.cpo.GetTableMetadata(val);
            this.cpo.Delete(c);
        }

        [Test]
        public void TestDeleteWithQueryCriteria()
        {
            QueryCriteria qc = new QueryCriteria(firstTable.TableName, firstTable.TableFields[0]);
            qc.Add(CriteriaOperator.Max, firstTable.TableFields[0]);
            object value = firstPersistent.GetValue(qc);

            First f = (First) firstPersistent.GetTableMetadata(value);
            firstPersistent.Delete(f);
        }

        #endregion

        #region update

        [Test]
        public void TestUpdate()
        {
            QueryCriteria qc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qc.Add(CriteriaOperator.Max, ctm.TableFields[0]);
            object val = cpo.GetValue(qc);

            CategoryTableMetadata c = (CategoryTableMetadata) this.cpo.GetTableMetadata(val);

            c.CategoryName = "spanac";
            c.Description = "muhahhahahahah";
            c.Picture = null;

            this.cpo.Update(c);
        }

        [Test]
        public void TestUpdateWithQueryCriteria()
        {
            QueryCriteria qcc = new QueryCriteria(ctm.TableName, ctm.TableFields[0]);
            qcc.Add(CriteriaOperator.Max, ctm.TableFields[0]);
            object val = cpo.GetValue(qcc);

            CategoryTableMetadata c = (CategoryTableMetadata) this.cpo.GetTableMetadata(val);

            QueryCriteria qc = new QueryCriteria(c.TableName, c.TableFields[2]);
            qc.Add(CriteriaOperator.Like, ctm.TableFields[1], "a");

            int x = this.cpo.Update(qc);

            Assert.IsTrue(x > 0);
        }

        #endregion
    }
}