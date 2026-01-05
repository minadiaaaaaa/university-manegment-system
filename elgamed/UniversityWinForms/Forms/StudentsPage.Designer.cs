namespace UniversitySystem.Forms
{
    partial class StudentsPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblCourse = new Label();
            cmbCourse = new ComboBox();
            dgvStudents = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colSeat = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(170, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "👥 My Students";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.ForeColor = Color.White;
            lblCourse.Location = new Point(20, 70);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(95, 20);
            lblCourse.TabIndex = 1;
            lblCourse.Text = "Select Course:";
            // 
            // cmbCourse
            // 
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Location = new Point(130, 67);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(250, 28);
            cmbCourse.TabIndex = 2;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvStudents.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvStudents.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colEmail, colSeat });
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.Font = new Font("Segoe UI", 10F);
            dgvStudents.Location = new Point(20, 110);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(800, 380);
            dgvStudents.TabIndex = 3;
            // 
            // colID
            // 
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.Width = 60;
            // 
            // colName
            // 
            colName.HeaderText = "Student Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colEmail
            // 
            colEmail.HeaderText = "Email";
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            // 
            // colSeat
            // 
            colSeat.HeaderText = "Seat No";
            colSeat.Name = "colSeat";
            colSeat.ReadOnly = true;
            colSeat.Width = 80;
            // 
            // StudentsPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvStudents);
            Controls.Add(cmbCourse);
            Controls.Add(lblCourse);
            Controls.Add(lblTitle);
            Name = "StudentsPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCourse;
        private ComboBox cmbCourse;
        private DataGridView dgvStudents;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colSeat;
    }
}
