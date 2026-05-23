using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogic;

namespace UIWinForms
{
    public partial class frmItem : Form
    {
        private readonly ItemBLL _bll = new ItemBLL();
        private int _selectedID = -1;

        public frmItem() { InitializeComponent(); }

        private void frmItem_Load(object sender, EventArgs e)
        {
            LoadData();
            dgvItems.ReadOnly = true;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;
        }

        private void LoadData()
        {
            dgvItems.DataSource = _bll.GetAllItems();
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvItems.Rows[e.RowIndex];
            _selectedID = Convert.ToInt32(row.Cells["ItemID"].Value);
            txtItemID.Text = _selectedID.ToString();
            txtItemName.Text = row.Cells["ItemName"].Value.ToString();
            txtSize.Text = row.Cells["Size"].Value?.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                decimal price = decimal.Parse(txtPrice.Text);
                if (_bll.AddItem(txtItemName.Text, txtSize.Text, price))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedID == -1) { MessageBox.Show("Chọn sản phẩm cần sửa!"); return; }
            try
            {
                decimal price = decimal.Parse(txtPrice.Text);
                if (_bll.UpdateItem(_selectedID, txtItemName.Text, txtSize.Text, price))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedID == -1) { MessageBox.Show("Chọn sản phẩm cần xóa!"); return; }
            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_bll.DeleteItem(_selectedID))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData(); ClearForm();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            txtItemID.Text = txtItemName.Text = txtSize.Text = txtPrice.Text = "";
            _selectedID = -1;
            dgvItems.ClearSelection();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {

        }
    }
}