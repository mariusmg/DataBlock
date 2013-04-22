
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using voidsoft.DataBlock;
using Extender;



namespace Console.Tests
{
	partial class Form1 : Form
	{

        public string sqlserver = "Server=VICTOR; user= sa; password=1234;Database=Northwind";


        public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            try
            {

                CustomerTableMetadata c = new CustomerTableMetadata();
                QueryCriteria qc = new QueryCriteria(c);
                qc.Add(CriteriaOperator.Distinct, c.TableFields[0]);
                IQueryCriteriaGenerator ss = DataFactory.InitializeQueryCriteriaGenerator(EDatabase.SqlServer);

               




                //  CategoriesTableMetadata aom = new CategoriesTableMetadata();
              //  CategoriesPersistentObject ctg = new CategoriesPersistentObject(EDatabase.SqlServer, Program.sqlserver, aom);

//                ExtenderTerritories.TerritoriesTableMetadata etp = new ExtenderTerritories.TerritoriesTableMetadata();

              //  CategoriesTableMetadata[] ct = (CategoriesTableMetadata[])ctg.GetTableMetadata();

//               CategoriesTableMetadata c = (CategoriesTableMetadata)ctg.GetTableMetadata(2);
//
//                CategoriesTableMetadata[] cc = new CategoriesTableMetadata[1];
              //  CategoriesTableMetadata[] ctm = (CategoriesTableMetadata[])ctg.GetTableMetadata();


//                cc[0] = c;
//                this.dataGridView1.DataSource = cc;



//                EmployeesTableMetadata eom = new EmployeesTableMetadata();
//                EmployeesPersistentObject emp = new EmployeesPersistentObject(EDatabase.SqlServer, Program.sqlserver, eom);
//                EmployeesTableMetadata[] ex = (EmployeesTableMetadata[]) emp.GetTableMetadata();
//
//                EmployeeTerritoriesTableMetadata[] ao = (EmployeeTerritoriesTableMetadata[]) emp.GetTableMetadata("FK", "ExtenderEmployees.EmployeeTerritoriesTableMetadata", 1);
//
//                this.dataGridView1.DataSource = ao;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                Session ss = Session.CreateNewSession(EDatabase.SqlServer, Program.sqlserver);

                //CategoriesPersistentObject cp = new CategoriesPersistentObject(

            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
	}
}