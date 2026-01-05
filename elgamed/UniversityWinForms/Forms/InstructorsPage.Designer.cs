namespace UniversitySystem.Forms
{
    partial class InstructorsPage
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
            dgvInstructors = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colDept = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvInstructors).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "👨‍🏫 Instructors Management";
            
            dgvInstructors.AllowUserToAddRows = false;
            dgvInstructors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvInstructors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInstructors.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvInstructors.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dgvInstructors.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colEmail, colTitle, colDept });
            dgvInstructors.EnableHeadersVisualStyles = false;
            dgvInstructors.Font = new Font("Segoe UI", 10F);
            dgvInstructors.Location = new Point(20, 70);
            dgvInstructors.RowHeadersVisible = false;
            dgvInstructors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInstructors.Size = new Size(800, 350);
            
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 50;
            colName.HeaderText = "Name"; colName.Name = "colName";
            colEmail.HeaderText = "Email"; colEmail.Name = "colEmail";
            colTitle.HeaderText = "Title"; colTitle.Name = "colTitle"; colTitle.Width = 80;
            colDept.HeaderText = "Department"; colDept.Name = "colDept";
            
            btnAdd.BackColor = Color.FromArgb(0, 150, 100);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 430);
            btnAdd.Size = new Size(140, 40);
            btnAdd.Text = "➕ Add Instructor";
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            btnDelete.BackColor = Color.FromArgb(180, 50, 50);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10F);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(170, 430);
            btnDelete.Size = new Size(100, 40);
            btnDelete.Text = "🗑️ Delete";
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(dgvInstructors);
            Controls.Add(lblTitle);
            Name = "InstructorsPage";
            Size = new Size(850, 500);
            ((System.ComponentModel.ISupportInitialize)dgvInstructors).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvInstructors;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colDept;
        private Button btnAdd;
        private Button btnDelete;
    }
}
