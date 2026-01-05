namespace UniversitySystem.Forms
{
    partial class StudentCoursesPage
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
            lblRegistered = new Label();
            dgvRegistered = new DataGridView();
            colCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colHours = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            lblAvailable = new Label();
            dgvAvailable = new DataGridView();
            colCode2 = new DataGridViewTextBoxColumn();
            colName2 = new DataGridViewTextBoxColumn();
            colHours2 = new DataGridViewTextBoxColumn();
            btnRegister = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRegistered).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAvailable).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "📚 My Courses";
            
            lblRegistered.AutoSize = true;
            lblRegistered.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRegistered.ForeColor = Color.LightGreen;
            lblRegistered.Location = new Point(20, 55);
            lblRegistered.Text = "✅ Registered Courses";
            
            dgvRegistered.AllowUserToAddRows = false;
            dgvRegistered.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvRegistered.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRegistered.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvRegistered.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dgvRegistered.Columns.AddRange(new DataGridViewColumn[] { colCode, colName, colHours, colStatus });
            dgvRegistered.EnableHeadersVisualStyles = false;
            dgvRegistered.Font = new Font("Segoe UI", 9F);
            dgvRegistered.Location = new Point(20, 80);
            dgvRegistered.RowHeadersVisible = false;
            dgvRegistered.ReadOnly = true;
            dgvRegistered.Size = new Size(800, 150);
            
            colCode.HeaderText = "Code"; colCode.Name = "colCode"; colCode.Width = 80;
            colName.HeaderText = "Course Name"; colName.Name = "colName";
            colHours.HeaderText = "Hours"; colHours.Name = "colHours"; colHours.Width = 60;
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus"; colStatus.Width = 80;
            
            lblAvailable.AutoSize = true;
            lblAvailable.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAvailable.ForeColor = Color.Orange;
            lblAvailable.Location = new Point(20, 240);
            lblAvailable.Text = "📋 Available Courses";
            
            dgvAvailable.AllowUserToAddRows = false;
            dgvAvailable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAvailable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAvailable.BackgroundColor = Color.FromArgb(50, 50, 55);
            dgvAvailable.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 140, 0), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dgvAvailable.Columns.AddRange(new DataGridViewColumn[] { colCode2, colName2, colHours2 });
            dgvAvailable.EnableHeadersVisualStyles = false;
            dgvAvailable.Font = new Font("Segoe UI", 9F);
            dgvAvailable.Location = new Point(20, 265);
            dgvAvailable.RowHeadersVisible = false;
            dgvAvailable.ReadOnly = true;
            dgvAvailable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAvailable.Size = new Size(800, 150);
            
            colCode2.HeaderText = "Code"; colCode2.Name = "colCode2"; colCode2.Width = 80;
            colName2.HeaderText = "Course Name"; colName2.Name = "colName2";
            colHours2.HeaderText = "Hours"; colHours2.Name = "colHours2"; colHours2.Width = 60;
            
            btnRegister.BackColor = Color.FromArgb(0, 150, 100);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10F);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(20, 425);
            btnRegister.Size = new Size(180, 40);
            btnRegister.Text = "✅ Register Selected";
            btnRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(btnRegister);
            Controls.Add(dgvAvailable);
            Controls.Add(lblAvailable);
            Controls.Add(dgvRegistered);
            Controls.Add(lblRegistered);
            Controls.Add(lblTitle);
            Name = "StudentCoursesPage";
            Size = new Size(850, 480);
            ((System.ComponentModel.ISupportInitialize)dgvRegistered).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAvailable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblRegistered;
        private DataGridView dgvRegistered;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colHours;
        private DataGridViewTextBoxColumn colStatus;
        private Label lblAvailable;
        private DataGridView dgvAvailable;
        private DataGridViewTextBoxColumn colCode2;
        private DataGridViewTextBoxColumn colName2;
        private DataGridViewTextBoxColumn colHours2;
        private Button btnRegister;
    }
}
