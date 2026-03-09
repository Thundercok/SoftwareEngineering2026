using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableLayoutPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set up the TableLayoutPanel
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            // Add controls to the TableLayoutPanel
            Button button1 = new Button() { Text = "Button 1" };
            Button button2 = new Button() { Text = "Button 2" };
            Button button3 = new Button() { Text = "Button 3" };
            Button button4 = new Button() { Text = "Button 4" };
            tableLayoutPanel1.Controls.Add(button1, 0, 0); // Add button1 to column 0, row 0
            tableLayoutPanel1.Controls.Add(button2, 1, 0); // Add button2 to column 1, row 0
            tableLayoutPanel1.Controls.Add(button3, 0, 1); // Add button3 to column 0, row 1
            tableLayoutPanel1.Controls.Add(button4, 1, 1); // Add button4 to column 1, row 1
        }
    }
}
