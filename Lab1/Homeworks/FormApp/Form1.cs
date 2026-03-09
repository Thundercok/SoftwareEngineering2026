using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode rootNode = new TreeNode("Root");
            TreeNode childNode1 = new TreeNode("Child 1");
            TreeNode childNode2 = new TreeNode("Child 2");
           
            childNode1.Nodes.Add(childNode1);
            childNode2.Nodes.Add(childNode2);
            rootNode.Nodes.Add(rootNode);
        }
    }
}
