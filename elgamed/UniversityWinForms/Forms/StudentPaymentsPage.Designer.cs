namespace UniversitySystem.Forms
{
    partial class StudentPaymentsPage
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlSummary = new Panel();
            lblTotal = new Label();
            lblTotalValue = new Label();
            lblPaid = new Label();
            lblPaidValue = new Label();
            lblRemaining = new Label();
            lblRemainingValue = new Label();
            lblHistory = new Label();
            dgvPayments = new DataGridView();
            colDate = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colMethod = new DataGridViewTextBoxColumn();
            colRef = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "💳 Payments";
            
            // Summary panel
            pnlSummary.Location = new Point(20, 55);
            pnlSummary.Size = new Size(800, 80);
            pnlSummary.BackColor = Color.FromArgb(45, 45, 50);
            
            lblTotal.AutoSize = true;
            lblTotal.ForeColor = Color.LightGray;
            lblTotal.Location = new Point(20, 10);
            lblTotal.Text = "Total Fees:";
            lblTotalValue.AutoSize = true;
            lblTotalValue.ForeColor = Color.White;
            lblTotalValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalValue.Location = new Point(20, 35);
            lblTotalValue.Text = "0 EGP";
            
            lblPaid.AutoSize = true;
            lblPaid.ForeColor = Color.LightGray;
            lblPaid.Location = new Point(280, 10);
            lblPaid.Text = "Total Paid:";
            lblPaidValue.AutoSize = true;
            lblPaidValue.ForeColor = Color.LightGreen;
            lblPaidValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPaidValue.Location = new Point(280, 35);
            lblPaidValue.Text = "0 EGP";
            
            lblRemaining.AutoSize = true;
            lblRemaining.ForeColor = Color.LightGray;
            lblRemaining.Location = new Point(540, 10);
            lblRemaining.Text = "Remaining:";
            lblRemainingValue.AutoSize = true;
            lblRemainingValue.ForeColor = Color.Orange;
            lblRemainingValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblRemainingValue.Location = new Point(540, 35);
            lblRemainingValue.Text = "0 EGP";
            
            pnlSummary.Controls.AddRange(new Control[] { 
                lblTotal, lblTotalValue, lblPaid, lblPaidValue, lblRemaining, lblRemainingValue 
            });
            
            lblHistory.AutoSize = true;
            lblHistory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHistory.ForeColor = Color.White;
            lblHistory.Location = new Point(20, 145);
            lblHistory.Text = "📜 Payment History";
            
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvPayments.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvPayments.Columns.AddRange(new DataGridViewColumn[] { colDate, colAmount, colMethod, colRef });
            dgvPayments.EnableHeadersVisualStyles = false;
            dgvPayments.Font = new Font("Segoe UI", 10F);
            dgvPayments.Location = new Point(20, 175);
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.ReadOnly = true;
            dgvPayments.Size = new Size(800, 290);
            
            colDate.HeaderText = "Date"; colDate.Name = "colDate"; colDate.Width = 120;
            colAmount.HeaderText = "Amount"; colAmount.Name = "colAmount"; colAmount.Width = 100;
            colMethod.HeaderText = "Method"; colMethod.Name = "colMethod";
            colRef.HeaderText = "Reference"; colRef.Name = "colRef";
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvPayments);
            Controls.Add(lblHistory);
            Controls.Add(pnlSummary);
            Controls.Add(lblTitle);
            Name = "StudentPaymentsPage";
            Size = new Size(850, 480);
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Panel pnlSummary;
        private Label lblTotal;
        private Label lblTotalValue;
        private Label lblPaid;
        private Label lblPaidValue;
        private Label lblRemaining;
        private Label lblRemainingValue;
        private Label lblHistory;
        private DataGridView dgvPayments;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colMethod;
        private DataGridViewTextBoxColumn colRef;
    }
}
