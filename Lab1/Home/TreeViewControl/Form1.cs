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
            TreeNode rootNode = new TreeNode("Root");
            TreeNode childNode1 = new TreeNode("Child 1");
            TreeNode childNode2 = new TreeNode("Child 2");
            rootNode.Nodes.Add(childNode1);
            rootNode.Nodes.Add(childNode2);
            treeView1.Nodes.Add(rootNode);
        }
    }
}
