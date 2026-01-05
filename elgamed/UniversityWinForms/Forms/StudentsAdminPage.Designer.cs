namespace UniversitySystem.Forms
{
    partial class StudentsAdminPage
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
            dgvStudents = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colSeat = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colLevel = new DataGridViewTextBoxColumn();
            colDept = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "👥 Students Management";
            
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvStudents.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvStudents.Columns.AddRange(new DataGridViewColumn[] { colID, colSeat, colName, colEmail, colLevel, colDept });
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.Font = new Font("Segoe UI", 10F);
            dgvStudents.Location = new Point(20, 70);
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(800, 350);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colSeat.HeaderText = "Seat"; colSeat.Name = "colSeat"; colSeat.Width = 70;
            colName.HeaderText = "Name"; colName.Name = "colName";
            colEmail.HeaderText = "Email"; colEmail.Name = "colEmail";
            colLevel.HeaderText = "Year"; colLevel.Name = "colLevel"; colLevel.Width = 50;
            colDept.HeaderText = "Department"; colDept.Name = "colDept";
            
            btnAdd.BackColor = Color.FromArgb(0, 150, 100);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 430);
            btnAdd.Size = new Size(120, 40);
            btnAdd.Text = "➕ Add Student";
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            btnDelete.BackColor = Color.FromArgb(180, 50, 50);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10F);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(150, 430);
            btnDelete.Size = new Size(120, 40);
            btnDelete.Text = "🗑️ Delete";
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(dgvStudents);
            Controls.Add(lblTitle);
            Name = "StudentsAdminPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvStudents;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colSeat;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colLevel;
        private DataGridViewTextBoxColumn colDept;
        private Button btnAdd;
        private Button btnDelete;
    }
}
