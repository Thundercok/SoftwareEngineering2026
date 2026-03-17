using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using DAL;
namespace WinformUI
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        public CustomerForm()
        {
            InitializeComponent();
            _customerService = new CustomerService();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                Name = txtName.Text,
                Email = txtEmail.Text
            };
            _customerService.AddCustomer(customer);
            MessageBox.Show("Customer added successfully!");
        }

        private void btnGetCustomer_Click(object sender, EventArgs e)
        {
            int customerId = int.Parse(txtCustomerId.Text);
            var customer = _customerService.GetCustomer(customerId);
            if (customer != null)
            {
                txtName.Text = customer.Name;
                txtEmail.Text = customer.Email;
            }
            else
            {
                MessageBox.Show("Customer not found!");
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            dataGridView1.DataSource=customers;
        }
    }
}
