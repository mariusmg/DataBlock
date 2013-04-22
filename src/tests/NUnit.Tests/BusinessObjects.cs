using System.Data;
using BusinessObjects;
using Extender;
using NUnit.Framework;
using voidsoft.DataBlock;
using System.Collections;

namespace tests
{

    //tests business objects with sqlserver2005

    [TestFixture]
    public class BusinessObjects
    {
        private AuthorBusinessObject auth = null;
        private Author a = null;
        
        
        [SetUp]
        public void SetThingsUp()
        {
            auth = new AuthorBusinessObject(DatabaseServer.SqlServer, SharedData.sqlserver2005);
            a = new Author();
        }
        
        
        [Test]
        public void GetDataTable()
        {
            DataTable dt =   auth.GetDataTable();
            Assert.IsTrue(dt != null);
        }

        
        [Test]
        public void GetDataTableByFields()
        {
            DataTable dt = auth.GetDataTable(a[Author.AuthorFields.Age], a[Author.AuthorFields.AuthorId]);
            Assert.IsTrue(dt != null);
        }
        
        
        [Test]
        public void GetDataTableByQueryCriteria()
        {
            QueryCriteria qc = new QueryCriteria(a.TableName, a[Author.AuthorFields.AuthorId]);
            qc.Add(CriteriaOperator.Different, a[Author.AuthorFields.Age], 34);
            DataTable dtt = auth.GetDataTable(qc);
            Assert.IsTrue(dtt != null);
        }


        [Test]
        public void GetAllAuthors()
        {
            Author[] aa = this.auth.GetAuthor();
            Assert.IsTrue(aa != null);
        }


        [Test]
        public void GetAuthorByPKValue()
        {
            object x = auth.GetMin(this.a[Author.AuthorFields.AuthorId]);
            
            if(x == null)
            {
                return;
            }
            
            Author aa = this.auth.GetAuthor(x);
            Assert.IsTrue(aa != null);
        }



        [Test]
        public void GetCount()
        {
            Assert.IsTrue( ((int)auth.GetCount()) > -1);
        }

        [Test]
        public void GetMax()
        {
            Assert.IsTrue( this.auth.GetMax(this.a[Author.AuthorFields.AuthorId]) != null );
        }


        [Test]
        public void GetMin()
        {
            Assert.IsTrue(this.auth.GetMin(this.a[Author.AuthorFields.AuthorId]) != null);
        }



        [Test]
        public void GetFieldsBySingleDatabaseField()
        {
            ArrayList al =  auth.GetFieldList(a[Author.AuthorFields.Age]);
            
            Assert.IsTrue(al != null);
        }


        [Test]
        public void GetFieldsByQueryCriteria()
        {
            QueryCriteria qc = new QueryCriteria(a.TableName, a[Author.AuthorFields.AuthorId]);
            qc.Add(CriteriaOperator.Different, a[Author.AuthorFields.Age], 56 );
 
            ArrayList al = auth.GetFieldList(qc);

            Assert.IsTrue(al != null);
        }



        [Test]
        public void GetValue()
        {

            object x = auth.GetMin(this.a[Author.AuthorFields.AuthorId]);

            if (x == null)
            {
                return;
            }

            QueryCriteria qc = new QueryCriteria(a.TableName, a[Author.AuthorFields.AuthorId]);
            qc.Add(CriteriaOperator.Different, a[Author.AuthorFields.Age], x);
            
            Assert.IsTrue(this.auth.GetValue(qc) != null);
        }




        [Test]
        public void IsUniqueTrue()
        {

            //check if the value 8888889 is unique in the table

            Assert.IsTrue(this.auth.IsUnique(a[Author.AuthorFields.AuthorId], 8888889) == true);
        }

        [Test]
        public void Create()
        {

            Author a = new Author();
            a.Age = 34;
            a.Location = "Vienna";
            a.Name = "Wicked";

            this.auth.Create(a);
        }

        [Test]
        public void Delete()
        {
            object x = auth.GetMin(this.a[Author.AuthorFields.AuthorId]);

            if (x == null)
            {
                return;
            }
            
            Author k = this.auth.GetAuthor(x);
          
            this.auth.Create(k);
        }


        [Test]
        public void Update()
        {
            object x = auth.GetMin(this.a[Author.AuthorFields.AuthorId]);

            if (x == null)
            {
                return;
            }

            Author k = this.auth.GetAuthor(x);
            k.Age = 34;
            k.Location = "UpdatedLocation";
            k.Name = "UpdatedName";

            int res = this.auth.Update(k);


            Assert.IsTrue(res == 1);

        }


        [Test]
        public void UpdateWithQueryCriteria()
        {
            object x = auth.GetMin(this.a[Author.AuthorFields.AuthorId]);

            if (x == null)
            {
                return;
            }

            a.Name = "cotcodacescu";
            a.Location= "Whoooop";


            QueryCriteria qc = new QueryCriteria(a.TableName, a[Author.AuthorFields.Name], a[Author.AuthorFields.Location]);
            qc.Add(CriteriaOperator.Different, a[Author.AuthorFields.AuthorId], x);
           
            int res = this.auth.Update(qc);


            Assert.IsTrue(res > -1);

        }

    }
}
