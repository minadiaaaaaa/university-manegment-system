namespace UniversitySystem.Forms
{
    partial class ProfessorForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            btnLogout = new Button();
            btnGrades = new Button();
            btnAttendance = new Button();
            btnStudents = new Button();
            btnCourses = new Button();
            lblProfessorName = new Label();
            panelContent = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(45, 45, 50);
            panelSidebar.Controls.Add(btnCourses);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Controls.Add(btnGrades);
            panelSidebar.Controls.Add(btnAttendance);
            panelSidebar.Controls.Add(btnStudents);
            panelSidebar.Controls.Add(lblProfessorName);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(180, 600);
            panelSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogout.BackColor = Color.FromArgb(180, 50, 50);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(10, 550);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(160, 40);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "🚪 Logout";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnGrades
            // 
            btnGrades.BackColor = Color.FromArgb(55, 55, 60);
            btnGrades.Cursor = Cursors.Hand;
            btnGrades.FlatAppearance.BorderSize = 0;
            btnGrades.FlatStyle = FlatStyle.Flat;
            btnGrades.Font = new Font("Segoe UI", 10F);
            btnGrades.ForeColor = Color.White;
            btnGrades.Location = new Point(10, 250);
            btnGrades.Name = "btnGrades";
            btnGrades.Size = new Size(160, 40);
            btnGrades.TabIndex = 4;
            btnGrades.Text = "📝 Grades";
            btnGrades.TextAlign = ContentAlignment.MiddleLeft;
            btnGrades.UseVisualStyleBackColor = false;
            // 
            // btnAttendance
            // 
            btnAttendance.BackColor = Color.FromArgb(55, 55, 60);
            btnAttendance.Cursor = Cursors.Hand;
            btnAttendance.FlatAppearance.BorderSize = 0;
            btnAttendance.FlatStyle = FlatStyle.Flat;
            btnAttendance.Font = new Font("Segoe UI", 10F);
            btnAttendance.ForeColor = Color.White;
            btnAttendance.Location = new Point(10, 200);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(160, 40);
            btnAttendance.TabIndex = 3;
            btnAttendance.Text = "📋 Attendance";
            btnAttendance.TextAlign = ContentAlignment.MiddleLeft;
            btnAttendance.UseVisualStyleBackColor = false;
            // 
            // btnStudents
            // 
            btnStudents.BackColor = Color.FromArgb(55, 55, 60);
            btnStudents.Cursor = Cursors.Hand;
            btnStudents.FlatAppearance.BorderSize = 0;
            btnStudents.FlatStyle = FlatStyle.Flat;
            btnStudents.Font = new Font("Segoe UI", 10F);
            btnStudents.ForeColor = Color.White;
            btnStudents.Location = new Point(10, 150);
            btnStudents.Name = "btnStudents";
            btnStudents.Size = new Size(160, 40);
            btnStudents.TabIndex = 2;
            btnStudents.Text = "👥 Students";
            btnStudents.TextAlign = ContentAlignment.MiddleLeft;
            btnStudents.UseVisualStyleBackColor = false;
            // 
            // btnCourses
            // 
            btnCourses.BackColor = Color.FromArgb(55, 55, 60);
            btnCourses.Cursor = Cursors.Hand;
            btnCourses.FlatAppearance.BorderSize = 0;
            btnCourses.FlatStyle = FlatStyle.Flat;
            btnCourses.Font = new Font("Segoe UI", 10F);
            btnCourses.ForeColor = Color.White;
            btnCourses.Location = new Point(10, 100);
            btnCourses.Name = "btnCourses";
            btnCourses.Size = new Size(160, 40);
            btnCourses.TabIndex = 1;
            btnCourses.Text = "📚 My Courses";
            btnCourses.TextAlign = ContentAlignment.MiddleLeft;
            btnCourses.UseVisualStyleBackColor = false;
            // 
            // lblProfessorName
            // 
            lblProfessorName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProfessorName.ForeColor = Color.White;
            lblProfessorName.Location = new Point(0, 20);
            lblProfessorName.Name = "lblProfessorName";
            lblProfessorName.Size = new Size(180, 60);
            lblProfessorName.TabIndex = 0;
            lblProfessorName.Text = "👨‍🏫 Professor";
            lblProfessorName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(35, 35, 40);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(180, 0);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1020, 600);
            panelContent.TabIndex = 1;
            // 
            // ProfessorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 35);
            ClientSize = new Size(1200, 600);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1000, 600);
            Name = "ProfessorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Professor Portal - بوابة الأستاذ";
            Load += ProfessorForm_Load;
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Panel panelContent;
        private Label lblProfessorName;
        private Button btnCourses;
        private Button btnStudents;
        private Button btnAttendance;
        private Button btnGrades;
        private Button btnLogout;
    }
}
