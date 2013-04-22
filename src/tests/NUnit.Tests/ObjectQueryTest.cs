using System;
using System.Text;

using NUnit.Framework;
using System.Collections;
using System.Collections.Specialized;
using voidsoft.DataBlock;
using Extender;

using System.Data;

namespace tests
{
    [TestFixture]
    public class ObjectQueryTest
    {
        Author[] aut = null;
        CustomerTableMetadata[] cc = null;

        public ObjectQueryTest()
        {
            aut = new Author[5];
            cc = new CustomerTableMetadata[4];

            aut[0] = new Author();
            aut[0].Age = 56;
            aut[0].AuthorId = 13;
            aut[0].Location = "vermont";
            aut[0].Name = "gogu";

            aut[1] = new Author();
            aut[1].Age = 22;
            aut[1].AuthorId = 87;
            aut[1].Location = "overture";
            aut[1].Name = "trickster";


            aut[2] = new Author();
            aut[2].Age = 44;
            aut[2].AuthorId = 2;
            aut[2].Location = "snickers";
            aut[2].Name = "nikel";


            aut[3] = new Author();
            aut[3].Age = 67;
            aut[3].AuthorId = 60;
            aut[3].Location = "johnnie";
            aut[3].Name = "anvelope";


            aut[4] = new Author();
            aut[4].Age = 39;
            aut[4].AuthorId = 137;
            aut[4].Location = "vicks";
            aut[4].Name = "gun";



            cc[0] = new CustomerTableMetadata();
            cc[0].Age = 56;
            cc[0].Birthdate = DateTime.Parse("4/3/1954");
            cc[0].Male = true;
            cc[0].Name = "Gogosel";


            cc[1] = new CustomerTableMetadata();
            cc[1].Age = 67;
            cc[1].Birthdate = DateTime.Parse("2/1/1976");
            cc[1].Male = false;
            cc[1].Name = "Nae";

            cc[2] = new CustomerTableMetadata();
            cc[2].Age = 11;
            cc[2].Birthdate = DateTime.Parse("2/9/1988");
            cc[2].Male = true;
            cc[2].Name = "Bucalaie";

            cc[3] = new CustomerTableMetadata();
            cc[3].Age = 33;
            cc[3].Birthdate = DateTime.Parse("4/5/1990");
            cc[3].Male = false;
            cc[3].Name = "Slugarnicu";

        }


        [Test]
        public void TestMinDateTime()
        {
            ObjectQuery<CustomerTableMetadata> ctp = new ObjectQuery<CustomerTableMetadata>();
            DateTime dt = (DateTime) ctp.Min(cc, cc[0].GetField("Birthdate"));

            Assert.IsTrue(dt == new DateTime(1954,4,3));
        }

        [Test]
        public void TestMaxDateTime()
        {
            ObjectQuery<CustomerTableMetadata> ctp = new ObjectQuery<CustomerTableMetadata>();
            DateTime dt = (DateTime)ctp.Max(cc, cc[0].GetField("Birthdate"));
            Assert.IsTrue(dt == new DateTime(1990, 4, 5));
        }

        [Test]
        public void TestMinInteger()
        {
            ObjectQuery<Author> oo = new ObjectQuery<Author>();
            int m = Convert.ToInt32( oo.Min(this.aut, aut[0].GetField("AuthorId")));

            Assert.IsTrue(m == 2);
        }
        
        [Test]
        public void TestMaxInteger()
        {
            ObjectQuery<Author> oo = new ObjectQuery<Author>();
            int m = Convert.ToInt32( oo.Max(this.aut, aut[0].GetField("AuthorId")));

            Assert.IsTrue(m == 137);
        }
        

        [Test]
        public void TestSum()
        {
            ObjectQuery<Author> oo = new ObjectQuery<Author>();
            decimal m = Convert.ToDecimal( oo.Sum(this.aut, aut[0].GetField("AuthorId")));

            Assert.IsTrue(m != 0);
        }        
        
        [Test]
        public void TestSumConditional()
        {
            ObjectQuery<Author> oo = new ObjectQuery<Author>();
            decimal m = Convert.ToDecimal( oo.Sum(this.aut, aut[0].GetField("AuthorId")));
            decimal j = Convert.ToDecimal( oo.Sum(this.aut, aut[0].GetField("AuthorId"), " AuthorId > 60"));

            Assert.IsTrue( m != j);
        }

        [Test]
        public void TestSelect()
        {
            ObjectQuery<Author> oo = new ObjectQuery<Author>();
            Author[] aa =  oo.Select(this.aut, "AuthorId >10 and Name like go*" );

            Assert.IsTrue(aa.Length > 0);
        }
        

        [Test]
        public void TestComplexSelect()
        {
            ObjectQuery<CustomerTableMetadata> oo = new ObjectQuery<CustomerTableMetadata>();
            CustomerTableMetadata[] aa =  oo.Select(this.cc, "Age = 56 and Male = true and Birthdate >= 1/1/1800 and Name like Gog*" );

            Assert.IsTrue(aa.Length > 0);
        }

        [Test]
        public void TestSelectBoolean()
        {
            ObjectQuery<CustomerTableMetadata> ctp = new ObjectQuery<CustomerTableMetadata>();
            CustomerTableMetadata[] ctm = ctp.Select(this.cc, "Male =true");
            Assert.IsTrue(ctm.Length == 2);

        }
        
    }
}
