using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e){

        }

        private void button1_Click(object sender, EventArgs e){
            hello.Text = "Button Clicked!";
            string inputText = hello.Text;
            MessageBox.Show(inputText);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(n1.Text);
            double num2 = Convert.ToDouble(n2.Text);
            double num3 = num1 - num2;
            n3.Text = num3.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Item 1");
            listBox1.Items.Add("Item 2");
            comboBox1.Items.Add("Option A");
            comboBox1.Items.Add("Option B");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(n1.Text);
            double num2 = Convert.ToDouble(n2.Text);
            double num3 = num1 * num2;
            n3.Text = num3.ToString();
        }

        private void plus_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(n1.Text);
            double num2 = Convert.ToDouble(n2.Text);
            double num3 = num1 + num2;
            n3.Text = num3.ToString();
        }

        private void division_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(n1.Text);
            double num2 = Convert.ToDouble(n2.Text);
            double num3 = num1 / num2;
            n3.Text = num3.ToString();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            string selectedOption = comboBox1.SelectedItem.ToString();
            MessageBox.Show($"ListBox: {selectedItem}, ComboBox:{selectedOption}");

        }
    }
}
