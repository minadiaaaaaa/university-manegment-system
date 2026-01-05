namespace UniversitySystem.Forms
{
    partial class LabsPage
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
            dgvLabs = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCapacity = new DataGridViewTextBoxColumn();
            colBuilding = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvLabs).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "🔬 Labs Management";
            
            dgvLabs.AllowUserToAddRows = false;
            dgvLabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLabs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLabs.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvLabs.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvLabs.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colCapacity, colBuilding });
            dgvLabs.EnableHeadersVisualStyles = false;
            dgvLabs.Font = new Font("Segoe UI", 10F);
            dgvLabs.Location = new Point(20, 70);
            dgvLabs.RowHeadersVisible = false;
            dgvLabs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLabs.Size = new Size(800, 400);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colName.HeaderText = "Lab Name"; colName.Name = "colName";
            colCapacity.HeaderText = "Capacity"; colCapacity.Name = "colCapacity"; colCapacity.Width = 80;
            colBuilding.HeaderText = "Building"; colBuilding.Name = "colBuilding";
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(dgvLabs);
            Controls.Add(lblTitle);
            Name = "LabsPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvLabs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvLabs;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCapacity;
        private DataGridViewTextBoxColumn colBuilding;
    }
}
