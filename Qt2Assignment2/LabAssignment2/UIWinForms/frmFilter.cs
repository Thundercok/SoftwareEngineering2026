using System;
using System.Windows.Forms;
using DataAccess;

namespace UIWinForms
{
    public partial class frmFilter : Form
    {
        private readonly FilterDAO _dao = new FilterDAO();

        public frmFilter() { InitializeComponent(); }

        private void frmFilter_Load(object sender, EventArgs e) => LoadAll();

        private void btnLoad_Click(object sender, EventArgs e) => LoadAll();

        private void LoadAll()
        {
            dgvBest.DataSource = _dao.GetBestItems();
            dgvByAgent.DataSource = _dao.GetItemsByAgent();
            dgvAgentPurchase.DataSource = _dao.GetAgentPurchases();

            dgvBest.ReadOnly = dgvByAgent.ReadOnly = dgvAgentPurchase.ReadOnly = true;
        }

        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            new frmFilter().Show();
        }
    }
}