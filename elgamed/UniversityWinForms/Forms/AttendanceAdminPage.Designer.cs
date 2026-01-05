namespace UniversitySystem.Forms
{
    partial class AttendanceAdminPage
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
            lblCourse = new Label();
            cmbCourse = new ComboBox();
            dgvAttendance = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colStudent = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "📊 Attendance Records";
            
            lblCourse.AutoSize = true;
            lblCourse.ForeColor = Color.White;
            lblCourse.Location = new Point(20, 70);
            lblCourse.Text = "Course:";
            
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Location = new Point(90, 67);
            cmbCourse.Size = new Size(250, 28);
            
            dgvAttendance.AllowUserToAddRows = false;
            dgvAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAttendance.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvAttendance.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvAttendance.Columns.AddRange(new DataGridViewColumn[] { colID, colStudent, colDate, colStatus });
            dgvAttendance.EnableHeadersVisualStyles = false;
            dgvAttendance.Font = new Font("Segoe UI", 10F);
            dgvAttendance.Location = new Point(20, 110);
            dgvAttendance.RowHeadersVisible = false;
            dgvAttendance.Size = new Size(800, 380);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colStudent.HeaderText = "Student"; colStudent.Name = "colStudent";
            colDate.HeaderText = "Date"; colDate.Name = "colDate"; colDate.Width = 100;
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus"; colStatus.Width = 80;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvAttendance);
            Controls.Add(cmbCourse);
            Controls.Add(lblCourse);
            Controls.Add(lblTitle);
            Name = "AttendanceAdminPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblCourse;
        private ComboBox cmbCourse;
        private DataGridView dgvAttendance;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colStudent;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colStatus;
    }
}
