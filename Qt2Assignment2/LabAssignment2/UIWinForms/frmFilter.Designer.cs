namespace UIWinForms
{
    partial class frmFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabFilter = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvBest = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvByAgent = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvAgentPurchase = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabFilter.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBest)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvByAgent)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgentPurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // tabFilter
            // 
            this.tabFilter.Controls.Add(this.tabPage1);
            this.tabFilter.Controls.Add(this.tabPage2);
            this.tabFilter.Controls.Add(this.tabPage3);
            this.tabFilter.Location = new System.Drawing.Point(0, 12);
            this.tabFilter.Name = "tabFilter";
            this.tabFilter.SelectedIndex = 0;
            this.tabFilter.Size = new System.Drawing.Size(799, 279);
            this.tabFilter.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvBest);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 250);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sản phẩm bán chạy";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvBest
            // 
            this.dgvBest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBest.Location = new System.Drawing.Point(154, 30);
            this.dgvBest.Name = "dgvBest";
            this.dgvBest.RowHeadersWidth = 51;
            this.dgvBest.RowTemplate.Height = 24;
            this.dgvBest.Size = new System.Drawing.Size(240, 150);
            this.dgvBest.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvByAgent);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 250);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SP theo đại lý";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvByAgent
            // 
            this.dgvByAgent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvByAgent.Location = new System.Drawing.Point(153, 83);
            this.dgvByAgent.Name = "dgvByAgent";
            this.dgvByAgent.RowHeadersWidth = 51;
            this.dgvByAgent.RowTemplate.Height = 24;
            this.dgvByAgent.Size = new System.Drawing.Size(240, 150);
            this.dgvByAgent.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvAgentPurchase);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(791, 250);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Đại lý mua hàng";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvAgentPurchase
            // 
            this.dgvAgentPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAgentPurchase.Location = new System.Drawing.Point(494, 33);
            this.dgvAgentPurchase.Name = "dgvAgentPurchase";
            this.dgvAgentPurchase.RowHeadersWidth = 51;
            this.dgvAgentPurchase.RowTemplate.Height = 24;
            this.dgvAgentPurchase.Size = new System.Drawing.Size(240, 150);
            this.dgvAgentPurchase.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(254, 342);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Tải dữ liệu";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click_1);
            // 
            // frmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.tabFilter);
            this.Name = "frmFilter";
            this.Text = "frmFilter";
            this.Load += new System.EventHandler(this.frmFilter_Load);
            this.tabFilter.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBest)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvByAgent)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgentPurchase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabFilter;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvBest;
        private System.Windows.Forms.DataGridView dgvByAgent;
        private System.Windows.Forms.DataGridView dgvAgentPurchase;
        private System.Windows.Forms.Button btnLoad;
    }
}