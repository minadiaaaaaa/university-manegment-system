namespace UniversitySystem.Forms
{
    partial class FacultiesPage
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
            dgvFaculties = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvFaculties).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "🏫 Colleges (Faculties)";
            
            dgvFaculties.AllowUserToAddRows = false;
            dgvFaculties.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvFaculties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFaculties.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvFaculties.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvFaculties.Columns.AddRange(new DataGridViewColumn[] { colID, colName });
            dgvFaculties.EnableHeadersVisualStyles = false;
            dgvFaculties.Font = new Font("Segoe UI", 10F);
            dgvFaculties.Location = new Point(20, 70);
            dgvFaculties.RowHeadersVisible = false;
            dgvFaculties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFaculties.Size = new Size(800, 400);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 60;
            colName.HeaderText = "Faculty Name"; colName.Name = "colName";
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvFaculties);
            Controls.Add(lblTitle);
            Name = "FacultiesPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvFaculties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvFaculties;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
    }
}
