using System;
using System.Windows.Forms;
using BusinessLogic;

namespace UIWinForms
{
    public partial class frmAgent : Form
    {
        private readonly AgentBLL _bll = new AgentBLL();
        private int _selectedID = -1;

        public frmAgent() { InitializeComponent(); }

        private void frmAgent_Load(object sender, EventArgs e)
        {
            LoadData();
            dgvAgents.ReadOnly = true;
            dgvAgents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAgents.MultiSelect = false;
        }

        private void LoadData() => dgvAgents.DataSource = _bll.GetAllAgents();

        private void dgvAgents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvAgents.Rows[e.RowIndex];
            _selectedID = Convert.ToInt32(row.Cells["AgentID"].Value);
            txtAgentID.Text = _selectedID.ToString();
            txtAgentName.Text = row.Cells["AgentName"].Value.ToString();
            txtAddress.Text = row.Cells["Address"].Value?.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (_bll.AddAgent(txtAgentName.Text, txtAddress.Text))
                { MessageBox.Show("Thêm thành công!"); LoadData(); ClearForm(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedID == -1) { MessageBox.Show("Chọn đại lý cần sửa!"); return; }
            try
            {
                if (_bll.UpdateAgent(_selectedID, txtAgentName.Text, txtAddress.Text))
                { MessageBox.Show("Cập nhật thành công!"); LoadData(); ClearForm(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedID == -1) { MessageBox.Show("Chọn đại lý cần xóa!"); return; }
            if (MessageBox.Show("Xóa đại lý này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_bll.DeleteAgent(_selectedID))
                { MessageBox.Show("Xóa thành công!"); LoadData(); ClearForm(); }
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            txtAgentID.Text = txtAgentName.Text = txtAddress.Text = "";
            _selectedID = -1;
            dgvAgents.ClearSelection();
        }
    }
}