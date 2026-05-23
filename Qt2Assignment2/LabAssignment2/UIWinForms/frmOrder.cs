using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess;

namespace UIWinForms
{
    public partial class frmOrder : Form
    {
        private readonly OrderBLL _bll = new OrderBLL();
        private readonly AgentDAO _agentDAO = new AgentDAO();
        private readonly ItemDAO _itemDAO = new ItemDAO();
        private int _currentOrderID = -1;

        public frmOrder() { InitializeComponent(); }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;
            dgvDetails.ReadOnly = true;
            dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            LoadOrders();
            LoadAgentCombo();
            LoadItemCombo();
        }

        private void LoadOrders()
        {
            dgvOrders.DataSource = _bll.GetAllOrders();
        }

        private void LoadAgentCombo()
        {
            var dt = _agentDAO.GetAll();
            cboAgent.DataSource = dt;
            cboAgent.DisplayMember = "AgentName";
            cboAgent.ValueMember = "AgentID";
        }

        private void LoadItemCombo()
        {
            var dt = _itemDAO.GetAll();
            cboItem.DataSource = dt;
            cboItem.DisplayMember = "ItemName";
            cboItem.ValueMember = "ItemID";
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            _currentOrderID = Convert.ToInt32(
                dgvOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            lblCurrentOrder.Text = "Đơn hàng #" + _currentOrderID;
            dgvDetails.DataSource = _bll.GetOrderDetails(_currentOrderID);
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int agentId = Convert.ToDouble(cboAgent.SelectedValue) != 0 ? Convert.ToInt32(cboAgent.SelectedValue) : 0;
                int newId = _bll.CreateOrder(dtpOrderDate.Value, agentId);
                _currentOrderID = newId;
                lblCurrentOrder.Text = "Đơn hàng #" + newId + " (vừa tạo)";
                MessageBox.Show("Tạo đơn hàng #" + newId + " thành công!\nBây giờ thêm sản phẩm vào đơn.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrders();
                dgvDetails.DataSource = _bll.GetOrderDetails(newId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1)
            {
                MessageBox.Show("Vui lòng tạo hoặc chọn đơn hàng trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int itemId = Convert.ToInt32(cboItem.SelectedValue);
                int qty = int.Parse(txtQty.Text);
                decimal unit = decimal.Parse(txtUnit.Text);

                if (_bll.AddDetail(_currentOrderID, itemId, qty, unit))
                {
                    MessageBox.Show("Đã thêm sản phẩm vào đơn!");
                    dgvDetails.DataSource = _bll.GetOrderDetails(_currentOrderID);
                    txtQty.Text = txtUnit.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1)
            {
                MessageBox.Show("Chọn đơn hàng cần xóa!");
                return;
            }
            if (MessageBox.Show("Xóa đơn hàng #" + _currentOrderID + " và toàn bộ chi tiết?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_bll.DeleteOrder(_currentOrderID))
                {
                    MessageBox.Show("Xóa thành công!");
                    _currentOrderID = -1;
                    lblCurrentOrder.Text = "Chưa chọn đơn";
                    LoadOrders();
                    dgvDetails.DataSource = null;
                }
            }
        }

        private void btnCreateOrder_Click_1(object sender, EventArgs e) { }
        private void btnAddDetail_Click_1(object sender, EventArgs e) { }
        private void btnDeleteOrder_Click_1(object sender, EventArgs e) { }

        

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_currentOrderID == -1)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần in!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            printPreview.Document = printDoc;
            printPreview.ShowDialog();
        }

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var fontTitle = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            var fontHeader = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            var fontNormal = new System.Drawing.Font("Arial", 10);
            var brush = System.Drawing.Brushes.Black;

            int y = 40;
            int left = 50;

            g.DrawString("HÓA ĐƠN BÁN HÀNG", fontTitle, brush, 200, y);
            y += 40;

            g.DrawString("Mã đơn: #" + _currentOrderID, fontHeader, brush, left, y);
            y += 25;
            g.DrawString("Ngày:    " + dtpOrderDate.Value.ToString("dd/MM/yyyy"), fontNormal, brush, left, y);
            y += 25;
            g.DrawString("Đại lý: " + cboAgent.Text, fontNormal, brush, left, y);
            y += 35;

            g.DrawLine(System.Drawing.Pens.Black, left, y, 750, y);
            y += 10;

            g.DrawString("Tên sản phẩm", fontHeader, brush, left, y);
            g.DrawString("SL", fontHeader, brush, 380, y);
            g.DrawString("Đơn giá", fontHeader, brush, 430, y);
            g.DrawString("Thành tiền", fontHeader, brush, 570, y);
            y += 20;
            g.DrawLine(System.Drawing.Pens.Black, left, y, 750, y);
            y += 10;

            decimal total = 0;
            var dt = _bll.GetOrderDetails(_currentOrderID);
            foreach (System.Data.DataRow row in dt.Rows)
            {
                g.DrawString(row["ItemName"].ToString(), fontNormal, brush, left, y);
                g.DrawString(row["Quantity"].ToString(), fontNormal, brush, 380, y);
                g.DrawString(string.Format("{0:N0}", row["UnitAmount"]), fontNormal, brush, 430, y);
                decimal tt = Convert.ToDecimal(row["ThanhTien"]);
                g.DrawString(string.Format("{0:N0}", tt), fontNormal, brush, 570, y);
                total += tt;
                y += 22;
            }

            y += 10;
            g.DrawLine(System.Drawing.Pens.Black, left, y, 750, y);
            y += 10;
            g.DrawString("TỔNG CỘNG:", fontHeader, brush, 430, y);
            g.DrawString(string.Format("{0:N0} đ", total), fontHeader, brush, 570, y);
            y += 40;

            g.DrawString("Người lập phiếu", fontNormal, brush, 580, y);
        }
    }
}
