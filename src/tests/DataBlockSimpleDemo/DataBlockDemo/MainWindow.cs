using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects;
using voidsoft.DataBlock;
using voidsoft.Mappings;

namespace DataBlockDemo
{
    public partial class MainWindow : Form
    {
        private Customer[] customers = null;
        private CustomerBusinessObject businessObject = null;

        public MainWindow()
        {
            InitializeComponent();
            businessObject = new CustomerBusinessObject();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                customers = businessObject.GetCustomer();
                this.dataGridView1.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLoadByQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Customer c = new Customer();

                QueryCriteria qc = new QueryCriteria(c);

                qc.Add(CriteriaOperator.HigherOrEqual, c[Customer.CustomerFields.Age], 9);
                qc.Add(CriteriaOperator.OrderBy, c[Customer.CustomerFields.Name], "ASC");

                customers = businessObject.GetCustomer(qc);
                this.dataGridView1.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer();
            newCustomer.Name = "Newly created customer";
            newCustomer.LastName = "Jean";
            newCustomer.Age = 9;

            businessObject.Create(newCustomer);

            //reload data. You can also use generic lists to hold the retrieved data.

            this.customers = businessObject.GetCustomer();
            List<Customer> listCustomers = new List<Customer>(this.customers);
            this.dataGridView1.DataSource = listCustomers;



        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Please select a customer");
                return;
            }

            //update the selected customer and reload
            Customer cst = this.customers[this.dataGridView1.SelectedRows[0].Index];

            cst.LastName = DateTime.Now.ToShortTimeString();
            businessObject.Update(cst);

            this.buttonLoad_Click(null, null);
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Please select a customer");
                return;
            }

            //update the selected customer and reload
            Customer cst = this.customers[this.dataGridView1.SelectedRows[0].Index];

            //delete by id
            businessObject.Delete(cst);

            this.buttonLoad_Click(null, null);
        }
    }
}