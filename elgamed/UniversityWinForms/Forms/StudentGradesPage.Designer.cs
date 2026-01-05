namespace UniversitySystem.Forms
{
    partial class StudentGradesPage
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
            dgvGrades = new DataGridView();
            colCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colS1 = new DataGridViewTextBoxColumn();
            colS2 = new DataGridViewTextBoxColumn();
            colFinal = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colGrade = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvGrades).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "📊 My Grades";
            
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGrades.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvGrades.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvGrades.Columns.AddRange(new DataGridViewColumn[] { colCode, colName, colS1, colS2, colFinal, colTotal, colGrade });
            dgvGrades.EnableHeadersVisualStyles = false;
            dgvGrades.Font = new Font("Segoe UI", 10F);
            dgvGrades.Location = new Point(20, 70);
            dgvGrades.RowHeadersVisible = false;
            dgvGrades.ReadOnly = true;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.Size = new Size(800, 400);
            
            colCode.HeaderText = "Code"; colCode.Name = "colCode"; colCode.Width = 80;
            colName.HeaderText = "Course Name"; colName.Name = "colName";
            colS1.HeaderText = "S1"; colS1.Name = "colS1"; colS1.Width = 50;
            colS2.HeaderText = "S2"; colS2.Name = "colS2"; colS2.Width = 50;
            colFinal.HeaderText = "Final"; colFinal.Name = "colFinal"; colFinal.Width = 60;
            colTotal.HeaderText = "Total"; colTotal.Name = "colTotal"; colTotal.Width = 60;
            colGrade.HeaderText = "Grade"; colGrade.Name = "colGrade"; colGrade.Width = 60;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvGrades);
            Controls.Add(lblTitle);
            Name = "StudentGradesPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvGrades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvGrades;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colS1;
        private DataGridViewTextBoxColumn colS2;
        private DataGridViewTextBoxColumn colFinal;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colGrade;
    }
}
