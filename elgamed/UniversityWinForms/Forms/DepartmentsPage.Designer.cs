namespace UniversitySystem.Forms
{
    partial class DepartmentsPage
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
            dgvDepartments = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colFaculty = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvDepartments).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "📂 Departments";
            
            dgvDepartments.AllowUserToAddRows = false;
            dgvDepartments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDepartments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDepartments.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvDepartments.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvDepartments.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colFaculty });
            dgvDepartments.EnableHeadersVisualStyles = false;
            dgvDepartments.Font = new Font("Segoe UI", 10F);
            dgvDepartments.Location = new Point(20, 70);
            dgvDepartments.RowHeadersVisible = false;
            dgvDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartments.Size = new Size(800, 400);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 60;
            colName.HeaderText = "Department Name"; colName.Name = "colName";
            colFaculty.HeaderText = "Faculty"; colFaculty.Name = "colFaculty";
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvDepartments);
            Controls.Add(lblTitle);
            Name = "DepartmentsPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvDepartments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvDepartments;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colFaculty;
    }
}
