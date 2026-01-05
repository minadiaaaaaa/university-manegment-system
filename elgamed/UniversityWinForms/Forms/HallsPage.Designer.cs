namespace UniversitySystem.Forms
{
    partial class HallsPage
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
            dgvHalls = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCapacity = new DataGridViewTextBoxColumn();
            colBuilding = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHalls).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "🏛️ Halls Management";
            
            dgvHalls.AllowUserToAddRows = false;
            dgvHalls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHalls.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHalls.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvHalls.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvHalls.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colCapacity, colBuilding });
            dgvHalls.EnableHeadersVisualStyles = false;
            dgvHalls.Font = new Font("Segoe UI", 10F);
            dgvHalls.Location = new Point(20, 70);
            dgvHalls.RowHeadersVisible = false;
            dgvHalls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHalls.Size = new Size(800, 350);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colName.HeaderText = "Hall Name"; colName.Name = "colName";
            colCapacity.HeaderText = "Capacity"; colCapacity.Name = "colCapacity"; colCapacity.Width = 80;
            colBuilding.HeaderText = "Building"; colBuilding.Name = "colBuilding";
            
            btnAdd.BackColor = Color.FromArgb(0, 150, 100);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 430);
            btnAdd.Size = new Size(120, 40);
            btnAdd.Text = "➕ Add Hall";
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnAdd);
            Controls.Add(dgvHalls);
            Controls.Add(lblTitle);
            Name = "HallsPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvHalls).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvHalls;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCapacity;
        private DataGridViewTextBoxColumn colBuilding;
        private Button btnAdd;
    }
}
