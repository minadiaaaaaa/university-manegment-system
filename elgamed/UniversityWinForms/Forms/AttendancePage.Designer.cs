namespace UniversitySystem.Forms
{
    partial class AttendancePage
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
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            dgvAttendance = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewComboBoxColumn();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(180, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📋 Attendance";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.ForeColor = Color.White;
            lblCourse.Location = new Point(20, 70);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(56, 20);
            lblCourse.TabIndex = 1;
            lblCourse.Text = "Course:";
            // 
            // cmbCourse
            // 
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Location = new Point(90, 67);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(200, 28);
            cmbCourse.TabIndex = 2;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(310, 70);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(41, 20);
            lblDate.TabIndex = 3;
            lblDate.Text = "Date:";
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(360, 67);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(150, 27);
            dtpDate.TabIndex = 4;
            // 
            // dgvAttendance
            // 
            dgvAttendance.AllowUserToAddRows = false;
            dgvAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAttendance.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvAttendance.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvAttendance.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colStatus });
            dgvAttendance.EnableHeadersVisualStyles = false;
            dgvAttendance.Font = new Font("Segoe UI", 10F);
            dgvAttendance.Location = new Point(20, 110);
            dgvAttendance.Name = "dgvAttendance";
            dgvAttendance.RowHeadersVisible = false;
            dgvAttendance.Size = new Size(800, 330);
            dgvAttendance.TabIndex = 5;
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
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.Items.AddRange(new object[] { "Present", "Absent", "Late", "Excused" });
            colStatus.Name = "colStatus";
            colStatus.Width = 120;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.BackColor = Color.FromArgb(0, 150, 100);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 450);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 40);
            btnSave.TabIndex = 6;
            btnSave.Text = "💾 Save Attendance";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // AttendancePage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnSave);
            Controls.Add(dgvAttendance);
            Controls.Add(dtpDate);
            Controls.Add(lblDate);
            Controls.Add(cmbCourse);
            Controls.Add(lblCourse);
            Controls.Add(lblTitle);
            Name = "AttendancePage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCourse;
        private ComboBox cmbCourse;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private DataGridView dgvAttendance;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewComboBoxColumn colStatus;
        private Button btnSave;
    }
}
