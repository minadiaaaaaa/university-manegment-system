namespace UniversitySystem.Forms
{
    partial class AssignCoursesPage
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
            lblInstructor = new Label();
            cmbInstructor = new ComboBox();
            lblCourse = new Label();
            cmbCourse = new ComboBox();
            btnAssign = new Button();
            dgvAssignments = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colInstructor = new DataGridViewTextBoxColumn();
            colCourse = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "📝 Assign Courses to Instructors";
            
            lblInstructor.AutoSize = true;
            lblInstructor.ForeColor = Color.White;
            lblInstructor.Location = new Point(20, 70);
            lblInstructor.Text = "Instructor:";
            
            cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInstructor.Location = new Point(100, 67);
            cmbInstructor.Size = new Size(200, 28);
            
            lblCourse.AutoSize = true;
            lblCourse.ForeColor = Color.White;
            lblCourse.Location = new Point(320, 70);
            lblCourse.Text = "Course:";
            
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Location = new Point(390, 67);
            cmbCourse.Size = new Size(200, 28);
            
            btnAssign.BackColor = Color.FromArgb(0, 150, 100);
            btnAssign.FlatStyle = FlatStyle.Flat;
            btnAssign.Font = new Font("Segoe UI", 10F);
            btnAssign.ForeColor = Color.White;
            btnAssign.Location = new Point(610, 64);
            btnAssign.Size = new Size(100, 35);
            btnAssign.Text = "➕ Assign";
            
            dgvAssignments.AllowUserToAddRows = false;
            dgvAssignments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAssignments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAssignments.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvAssignments.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvAssignments.Columns.AddRange(new DataGridViewColumn[] { colID, colInstructor, colCourse });
            dgvAssignments.EnableHeadersVisualStyles = false;
            dgvAssignments.Font = new Font("Segoe UI", 10F);
            dgvAssignments.Location = new Point(20, 110);
            dgvAssignments.RowHeadersVisible = false;
            dgvAssignments.Size = new Size(800, 380);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colInstructor.HeaderText = "Instructor"; colInstructor.Name = "colInstructor";
            colCourse.HeaderText = "Course"; colCourse.Name = "colCourse";
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvAssignments);
            Controls.Add(btnAssign);
            Controls.Add(cmbCourse);
            Controls.Add(lblCourse);
            Controls.Add(cmbInstructor);
            Controls.Add(lblInstructor);
            Controls.Add(lblTitle);
            Name = "AssignCoursesPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblInstructor;
        private ComboBox cmbInstructor;
        private Label lblCourse;
        private ComboBox cmbCourse;
        private Button btnAssign;
        private DataGridView dgvAssignments;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colInstructor;
        private DataGridViewTextBoxColumn colCourse;
    }
}
