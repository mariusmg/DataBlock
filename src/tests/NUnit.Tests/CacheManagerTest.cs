//
//#region Using directives
//
//using System;
//using System.Text;
//using NUnit.Core;
//using NUnit.Framework;
/////using voidsoft.DataBlock.Cache;
//
//#endregion
//
//namespace tests
//{
//
//    [TestFixture]
//    public class CacheManagerTest
//    {
//
//        [Test]
//        public void TestInsertTemporary()
//        {
//            CacheManager.Clear();
//            CacheManager.Add("blah", "blah", new TimeSpan(0,1,45));
//            Assert.IsTrue (CacheManager.Count > 0);
//        }
//
//        [Test]
//        public void TestInsertPermanent()
//        {
//            CacheManager.Clear();
//            CacheManager.Add("blah", "blah");
//            Assert.IsTrue(CacheManager.Count > 0);
//        }
//
//        [Test]
//        public void TestClear()
//        {
//            CacheManager.Clear();
//            Assert.IsTrue(CacheManager.Count == 0);
//        }
//
//        [Test]
//        public void TestContains()
//        {
//            CacheManager.Clear();
//            CacheManager.Add("a", "b");
//            Assert.IsTrue(CacheManager.Contains("a"));
//        }
//
//
//        [Test]
//        public void TestTemporaryObjects()
//        {
//            CacheManager.Clear();
//            CacheManager.Add("x", "vbvcbcb", new TimeSpan(0,0,1));
//
//            System.Threading.Thread.Sleep(3000);
//            
//            Console.WriteLine(CacheManager.Count == 0);
//
//            Assert.IsTrue(CacheManager.Count == 0);
//        }
//    }
//}
