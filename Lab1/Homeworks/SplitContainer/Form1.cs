using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitContainer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Label label1 = new Label() { Text = "Panel 1", Dock = DockStyle.Fill };
            Label label2 = new Label() { Text = "Panel 2", Dock = DockStyle.Fill };

            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(label2);
        }
    }
}
