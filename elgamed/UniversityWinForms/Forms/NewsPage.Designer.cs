namespace UniversitySystem.Forms
{
    partial class NewsPage
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
            dgvNews = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNews).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "📢 News Management";
            
            dgvNews.AllowUserToAddRows = false;
            dgvNews.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNews.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvNews.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvNews.Columns.AddRange(new DataGridViewColumn[] { colID, colTitle, colContent, colDate });
            dgvNews.EnableHeadersVisualStyles = false;
            dgvNews.Font = new Font("Segoe UI", 10F);
            dgvNews.Location = new Point(20, 70);
            dgvNews.RowHeadersVisible = false;
            dgvNews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNews.Size = new Size(800, 350);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colTitle.HeaderText = "Title"; colTitle.Name = "colTitle";
            colContent.HeaderText = "Content"; colContent.Name = "colContent";
            colDate.HeaderText = "Date"; colDate.Name = "colDate"; colDate.Width = 100;
            
            btnAdd.BackColor = Color.FromArgb(0, 150, 100);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 430);
            btnAdd.Size = new Size(120, 40);
            btnAdd.Text = "➕ Add News";
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnAdd);
            Controls.Add(dgvNews);
            Controls.Add(lblTitle);
            Name = "NewsPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvNews).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvNews;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colContent;
        private DataGridViewTextBoxColumn colDate;
        private Button btnAdd;
    }
}
