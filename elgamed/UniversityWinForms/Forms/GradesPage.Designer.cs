namespace UniversitySystem.Forms
{
    partial class GradesPage
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
            dgvGrades = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colS1 = new DataGridViewTextBoxColumn();
            colS2 = new DataGridViewTextBoxColumn();
            colFinal = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGrades).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(190, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📝 Enter Grades";
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
            cmbCourse.Size = new Size(250, 28);
            cmbCourse.TabIndex = 2;
            // 
            // dgvGrades
            // 
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGrades.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvGrades.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvGrades.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colS1, colS2, colFinal, colTotal });
            dgvGrades.EnableHeadersVisualStyles = false;
            dgvGrades.Font = new Font("Segoe UI", 10F);
            dgvGrades.Location = new Point(20, 110);
            dgvGrades.Name = "dgvGrades";
            dgvGrades.RowHeadersVisible = false;
            dgvGrades.Size = new Size(800, 330);
            dgvGrades.TabIndex = 3;
            // 
            // colID
            // 
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;
            colID.Width = 50;
            // 
            // colName
            // 
            colName.HeaderText = "Student Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colS1
            // 
            colS1.HeaderText = "S1 (10)";
            colS1.Name = "colS1";
            colS1.Width = 80;
            // 
            // colS2
            // 
            colS2.HeaderText = "S2 (10)";
            colS2.Name = "colS2";
            colS2.Width = 80;
            // 
            // colFinal
            // 
            colFinal.HeaderText = "Final (60)";
            colFinal.Name = "colFinal";
            colFinal.Width = 90;
            // 
            // colTotal
            // 
            colTotal.HeaderText = "Total";
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            colTotal.Width = 80;
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
            btnSave.TabIndex = 4;
            btnSave.Text = "💾 Save Grades";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // GradesPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnSave);
            Controls.Add(dgvGrades);
            Controls.Add(cmbCourse);
            Controls.Add(lblCourse);
            Controls.Add(lblTitle);
            Name = "GradesPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvGrades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCourse;
        private ComboBox cmbCourse;
        private DataGridView dgvGrades;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colS1;
        private DataGridViewTextBoxColumn colS2;
        private DataGridViewTextBoxColumn colFinal;
        private DataGridViewTextBoxColumn colTotal;
        private Button btnSave;
    }
}
