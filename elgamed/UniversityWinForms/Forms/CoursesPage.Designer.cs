namespace UniversitySystem.Forms
{
    partial class CoursesPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            dgvCourses = new DataGridView();
            colCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colHours = new DataGridViewTextBoxColumn();
            colYear = new DataGridViewTextBoxColumn();
            colSemester = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvCourses).BeginInit();
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
            lblTitle.Text = "📚 My Courses";
            // 
            // dgvCourses
            // 
            dgvCourses.AllowUserToAddRows = false;
            dgvCourses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCourses.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvCourses.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvCourses.Columns.AddRange(new DataGridViewColumn[] { colCode, colName, colHours, colYear, colSemester });
            dgvCourses.EnableHeadersVisualStyles = false;
            dgvCourses.Font = new Font("Segoe UI", 10F);
            dgvCourses.Location = new Point(20, 70);
            dgvCourses.Name = "dgvCourses";
            dgvCourses.RowHeadersVisible = false;
            dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCourses.Size = new Size(800, 400);
            dgvCourses.TabIndex = 1;
            // 
            // colCode
            // 
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            // 
            // colName
            // 
            colName.HeaderText = "Course Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colHours
            // 
            colHours.HeaderText = "Hours";
            colHours.Name = "colHours";
            colHours.ReadOnly = true;
            // 
            // colYear
            // 
            colYear.HeaderText = "Year";
            colYear.Name = "colYear";
            colYear.ReadOnly = true;
            // 
            // colSemester
            // 
            colSemester.HeaderText = "Semester";
            colSemester.Name = "colSemester";
            colSemester.ReadOnly = true;
            // 
            // CoursesPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvCourses);
            Controls.Add(lblTitle);
            Name = "CoursesPage";
            Size = new Size(850, 500);
            Load += CoursesPage_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCourses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvCourses;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colHours;
        private DataGridViewTextBoxColumn colYear;
        private DataGridViewTextBoxColumn colSemester;
    }
}
