namespace UniversitySystem.Forms
{
    partial class CoursesAdminPage
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
            dgvCourses = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colHours = new DataGridViewTextBoxColumn();
            colYear = new DataGridViewTextBoxColumn();
            colTerm = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "📚 Courses Management";
            
            dgvCourses.AllowUserToAddRows = false;
            dgvCourses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCourses.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvCourses.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvCourses.Columns.AddRange(new DataGridViewColumn[] { colID, colCode, colName, colHours, colYear, colTerm });
            dgvCourses.EnableHeadersVisualStyles = false;
            dgvCourses.Font = new Font("Segoe UI", 10F);
            dgvCourses.Location = new Point(20, 70);
            dgvCourses.RowHeadersVisible = false;
            dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCourses.Size = new Size(800, 400);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colCode.HeaderText = "Code"; colCode.Name = "colCode"; colCode.Width = 80;
            colName.HeaderText = "Course Name"; colName.Name = "colName";
            colHours.HeaderText = "Hours"; colHours.Name = "colHours"; colHours.Width = 60;
            colYear.HeaderText = "Year"; colYear.Name = "colYear"; colYear.Width = 50;
            colTerm.HeaderText = "Term"; colTerm.Name = "colTerm"; colTerm.Width = 50;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvCourses);
            Controls.Add(lblTitle);
            Name = "CoursesAdminPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvCourses;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colHours;
        private DataGridViewTextBoxColumn colYear;
        private DataGridViewTextBoxColumn colTerm;
    }
}
