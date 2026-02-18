using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatetimePickerAndProgressbar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += (s, args) =>
            {
                if (progressBar1.Value < 100)
                {
                    progressBar1.Value += 10;
                }
                else
                {
                    timer.Stop();
                    MessageBox.Show("Progress complete!");
                }
            };
            timer.Start();
        }
    }
}
