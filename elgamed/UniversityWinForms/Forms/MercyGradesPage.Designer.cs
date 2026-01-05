namespace UniversitySystem.Forms
{
    partial class MercyGradesPage
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
            lblPoints = new Label();
            numPoints = new NumericUpDown();
            dgvCandidates = new DataGridView();
            colSelect = new DataGridViewCheckBoxColumn();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colDiff = new DataGridViewTextBoxColumn();
            btnApply = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCandidates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPoints).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "❤️ Mercy Grading";
            
            lblCourse.AutoSize = true;
            lblCourse.ForeColor = Color.White;
            lblCourse.Location = new Point(20, 70);
            lblCourse.Text = "Course:";
            
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Location = new Point(90, 67);
            cmbCourse.Size = new Size(200, 28);
            
            lblPoints.AutoSize = true;
            lblPoints.ForeColor = Color.White;
            lblPoints.Location = new Point(310, 70);
            lblPoints.Text = "Max Points:";
            
            numPoints.Location = new Point(400, 67);
            numPoints.Maximum = 10;
            numPoints.Minimum = 1;
            numPoints.Value = 3;
            numPoints.Size = new Size(60, 27);
            
            dgvCandidates.AllowUserToAddRows = false;
            dgvCandidates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCandidates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCandidates.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvCandidates.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvCandidates.Columns.AddRange(new DataGridViewColumn[] { colSelect, colID, colName, colTotal, colDiff });
            dgvCandidates.EnableHeadersVisualStyles = false;
            dgvCandidates.Font = new Font("Segoe UI", 10F);
            dgvCandidates.Location = new Point(20, 110);
            dgvCandidates.RowHeadersVisible = false;
            dgvCandidates.Size = new Size(800, 320);
            
            colSelect.HeaderText = "Select"; colSelect.Name = "colSelect"; colSelect.Width = 60;
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colName.HeaderText = "Student Name"; colName.Name = "colName";
            colTotal.HeaderText = "Total"; colTotal.Name = "colTotal"; colTotal.Width = 70;
            colDiff.HeaderText = "Needed"; colDiff.Name = "colDiff"; colDiff.Width = 70;
            
            btnApply.BackColor = Color.FromArgb(0, 150, 100);
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(20, 440);
            btnApply.Size = new Size(150, 40);
            btnApply.Text = "❤️ Apply Mercy";
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnApply);
            Controls.Add(dgvCandidates);
            Controls.Add(numPoints);
            Controls.Add(lblPoints);
            Controls.Add(cmbCourse);
            Controls.Add(lblCourse);
            Controls.Add(lblTitle);
            Name = "MercyGradesPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvCandidates).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPoints).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblCourse;
        private ComboBox cmbCourse;
        private Label lblPoints;
        private NumericUpDown numPoints;
        private DataGridView dgvCandidates;
        private DataGridViewCheckBoxColumn colSelect;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colDiff;
        private Button btnApply;
    }
}
