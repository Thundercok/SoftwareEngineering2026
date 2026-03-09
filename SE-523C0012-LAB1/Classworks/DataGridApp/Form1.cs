using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Age";
            string[] row1 = new string[] { "1", "Ky - Trung Pham", "25" };
            string[] row2 = new string[] { "2", "Thai Ky Trung", "30" };
            dataGridView1.Rows.Add(row1);
            dataGridView1.Rows.Add(row2);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string[] row = new string[] { textBoxID.Text, textBoxName.Text, textBoxAge.Text };
            dataGridView1.Rows.Add(row);
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
