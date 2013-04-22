using System;
using Extender;
using NUnit.Framework;

namespace tests
{

    [TestFixture]
    public class RelationshipsTEST
    {

        private Book bk = null;
        private BookPersistentObject bkPerst = null;

        private Author at = null;
        private AuthorPersistentObject atPerst = null;

        [SetUp]
        public void SetThingsUp()
        {
            global::voidsoft.DataBlock.Configuration.ReadConfigurationFromConfigFile();

            bk = new Book();
            bkPerst = new BookPersistentObject(bk);

            at = new Author();
            atPerst = new AuthorPersistentObject(at);

        }


        [Test]
        public void TestOneToMany()
        {

            object y = bkPerst.GetMin(bk[Book.BookFields.BookId]);


            if (y == null)
            {
                Console.WriteLine("invalid value");
            }


            Book bkk = (Book)bkPerst.GetTableMetadata(y);

            Author[] authors = (Author[])bkk.GetAuthor();

            Assert.IsTrue(authors != null);
        }



    }
}
