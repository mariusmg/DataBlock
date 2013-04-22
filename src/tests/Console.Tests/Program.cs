
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using voidsoft.DataBlock;
using System.Collections;
using System.IO;
using Extender;



using System.Drawing;

namespace Console.Tests
{
    class Program
    {


		public static Bitmap bmp;
		public static byte[] b;

		public  static string sqlserver = "Server=marius;user=sa; password=1234;Database=test";


        public static void TestSqlServerOr()
        {
           
        }

        static void Main(string[] args)
        {
            CategoryTableMetadata ctm = new CategoryTableMetadata();
            CategoryPersistentObject cpo = new CategoryPersistentObject(EDatabase.SqlServer, sqlserver, ctm);
            cpo.BeforeExecutingQueries += new PersistentObject.SqlGeneratorEventHandler(cpo_BeforeExecutingQueries);

            ctm.CategoryName = "Zmeura";
            ctm.Description = "Zmeura d-aia buna";

            bool b = ctm.IsNull(0);

            cpo.Create(ctm);


            bool bn = ctm.IsNull(0);


            int s = ctm.CategoryID;


//           
//            OrderDetailsTableMetadata orderDetails = new OrderDetailsTableMetadata();
//            OrdersTableMetadata order = new OrdersTableMetadata();

//            CustomerTableMetadata c = new CustomerTableMetadata();
//            QueryCriteria qc = new QueryCriteria(c);
//            qc.Add(CriteriaOperator.Distinct, c.TableFields[0]);
//            IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(EDatabase.SqlServer);
//            string s = ss.Generate(qc);
//
//            
//            
//            
//             FirstTableMetadata first = new FirstTableMetadata();
//             SecondTableMetadata second = new SecondTableMetadata();
//             ThirdTableMetadata third = new ThirdTableMetadata();
//
//             FirstPersistentObject psqlserver= new FirstPersistentObject(EDatabase.SqlServer,Program.sqlserver, first);
//             FirstPersistentObject msqlserver = new FirstPersistentObject(EDatabase.MySQL, "FGHFH", first);
//             FirstPersistentObject asqlserver = new FirstPersistentObject(EDatabase.Access, "dfdg", first);
//
//
//             QueryCriteria qcFirst = new QueryCriteria(first);
//             QueryCriteria qcSecond = new QueryCriteria(second);
//             QueryCriteria qcThird = new QueryCriteria(third);
//
//             qcThird.Add(CriteriaOperator.Equality, third.TableFields[1], "tt");
//
//
//             qcFirst.AddJoin(JoinType.Inner, first.TableFields[0], second.TableFields[1], qcSecond);
//             qcFirst.AddJoin(JoinType.Left, second.TableFields[0], third.TableFields[2], qcThird);
//
//
//
//            DataSet ds = psqlserver.GetDataSet(qcFirst);



//            QueryCriteria qc = new QueryCriteria(order);
//            qc.Add(CriteriaOperator.Equality, qc.Fields[0], 10257);
//           
//            QueryCriteria qcOrderDetails = new QueryCriteria(orderDetails);
//            qcOrderDetails.Add(CriteriaOperator.Equality, orderDetails.GetField("Discount"), 0);
//
//            qc.AddJoin(JoinType.Inner, order.TableFields[0], orderDetails.TableFields[0], qcOrderDetails);
//
//           
//
//            DataSet ds = cpo.GetDataSet(qc);



           // Console.WriteLine("X");




            // TestSqlServerOr();

//			FileStream fs = null;
//
//			try
//			{
//				fs = new FileStream(@"d:\snap.jpg", FileMode.Open, FileAccess.Read);
//				byte[] b = new byte[fs.Length];
//				fs.Read(b, 0, b.Length);
//				CategoriesTableMetadata ab = new CategoriesTableMetadata();
//				//ab.CategoryID = 1;
//				ab.CategoryName = "Seafood";
//				ab.Description = "MARius";
//				ab.Picture = b;
//				string s = SqlGenerator.GenerateInsertQuery(EDatabase.SqlServer, ab.TableFields, ab.TableName);
//				DataAccessLayer.ExecuteNonQuery(EDatabase.SqlServer, Program.sqlserver, s);
//
//			}
//			finally
//			{
//				if (fs != null)
//				{
//					fs.Close();
//				}
//			}


//            CategoriesTableMetadata aom = new CategoriesTableMetadata();
//
//            QueryCriteria q = new QueryCriteria(aom.TableName, aom.TableFields[0], aom.TableFields[2]);
//            q.Add(CriteriaOperator.OrderBy, aom.TableFields[0], "asc");
//
//            DataSet dsmh = ctg.GetDataSet(q);
//
//            CategoriesTableMetadata[] c = (CategoriesTableMetadata[]) ctg.GetTableMetadata(q);
//



//			ExtenderTerritories.TerritoriesTableMetadata etp = new ExtenderTerritories.TerritoriesTableMetadata();
////			ExtenderTerritories.TerritoriesPersistentObject top = new ExtenderTerritories.TerritoriesPersistentObject(EDatabase.SqlServer, sqlserver, etp);
////			
////			CategoriesTableMetadata[] tp = ctg.MapDataReaderToTableMetadata();
////
////			Program.b = tp[0].Picture;
//
//
//			CategoriesTableMetadata[] ct = (CategoriesTableMetadata[])ctg.GetTableMetadata();
//
//            DataSet ds = ctg.GetDataSet();

            //Form1 f = new Form1();
			//System.Windows.Forms.Application.Run(f);


//
//
//
//			try
//			{
//
//
//
//				CategoriesTableMetadata[] categ = ctg.MapDataReaderToTableMetadata();
//
//				foreach (CategoriesTableMetadata c in categ)
//				{
//					System.Console.WriteLine(c.Description);
//				}
//
//			}
//			catch (Exception ex)
//			{
//				System.Console.WriteLine(ex.Message);
//			}

//			DataSet ds = ctg.GetDataSet();
//
//
//			CategoriesTableMetadata cn = (CategoriesTableMetadata)ctg.MapDataReaderToTableMetadata(3);
//
//		






//            AbonatTableMetadata aom = new AbonatTableMetadata();
//            AbonatCrapTableMetadata aa = new AbonatCrapTableMetadata();
//            AbonatPersistentObject aop = new AbonatPersistentObject(EDatabase.SqlServer, Program.sqlserver, aom);
//
//           // AbonatTableMetadata abonat = (AbonatTableMetadata)aop.MapDataReaderToTableMetadata(26);
//
//
//            aom.CartiImprumutate = 345;
//            aom.CodAbonament = "3242345";
//            aom.DataNasterii = DateTime.Now;
//            aom.Nume = "al_doileeeeeeeeeeeeeeeea";
//            aom.Nota = "dark";
//            aom.Prenume = "spider";
//
//
//			//test sql generator
//
//			string s = SqlGenerator.GenerateSelectQuery(EDatabase.SqlServer, aom.TableName);
//			System.Console.WriteLine(s);
//
//			string ss = SqlGenerator.GenerateSelectQuery(EDatabase.SqlServer, aom.TableName, aom.TableFields[1], aom.TableFields[2]);
//			System.Console.WriteLine(ss);
//
//			string sss = SqlGenerator.GenerateSelectQuery(EDatabase.SqlServer, aom, true);
//			System.Console.WriteLine(sss);
////
//			string m = SqlGenerator.GenerateSelectQuery(EDatabase.SqlServer, aom, false);
//			System.Console.WriteLine(m);


//
//			string ns = SqlGenerator.GenerateDeleteQuery(EDatabase.Access, aom, true);
//
//			//test insert sql generator
//
//
//			System.Console.WriteLine("end");
//


//
//            try
//            {
//
//                aop.BeginTransaction();
//
//                aop.Create(aom, aa);
//                aop.Delete(abonat);
//
//                aop.Commit();
//
//            }
//            catch (Exception ex)
//            {
//                aop.Rollback();
//            }


            //
//            aom = (AbonatTableMetadata) aop.MapDataReaderToTableMetadata(8);
//
//            aop.Delete(aom,true);

//
//            aom.CartiImprumutate = 4353;
//            aom.CodAbonament = "2342342";
//            aom.DataNasterii = DateTime.Now;
//            aom.Nota = "pussy";
//            aom.Nume = "gogu";
//            aom.Prenume = "kkko cu lapter";
//
//
//
//            aa.Blah = "spanac";
//
//            aop.Create(aom, aa);



//            aom.CartiImprumutate = 3453;
//            aom.CodAbonament = "345435345";
//            aom.DataNasterii = DateTime.Now;
//            aom.Nota = "blaj";
//            aom.Nume = "blahhhhhhhhhhhhhhh";
//            aom.Prenume = "sticks";
//
//
//
//            aop.Create(aom);


           // DataSet ds = aop.GetRelationData("FK_AbonatAbonatCrap", 12);

        }

        static void cpo_BeforeExecutingQueries(Operation currentOperation, ref System.Collections.Specialized.StringCollection currentValues, params object[] args)
        {

            System.Windows.Forms.MessageBox.Show(currentOperation.ToString());

            foreach (string s in currentValues)
            {
                System.Windows.Forms.MessageBox.Show(s);
            }

        }
    }
}
