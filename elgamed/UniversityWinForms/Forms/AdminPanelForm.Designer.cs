namespace UniversitySystem.Forms
{
    partial class AdminPanelForm
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
            panelContent = new Panel();
            lblAdminTitle = new Label();
            btnColleges = new Button();
            btnDepartments = new Button();
            btnStudents = new Button();
            btnCourses = new Button();
            btnInstructors = new Button();
            btnAssignCourses = new Button();
            btnHalls = new Button();
            btnLabs = new Button();
            btnMercyGrades = new Button();
            btnAttendance = new Button();
            btnNews = new Button();
            btnLogout = new Button();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(30, 30, 35);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Controls.Add(btnNews);
            panelSidebar.Controls.Add(btnAttendance);
            panelSidebar.Controls.Add(btnMercyGrades);
            panelSidebar.Controls.Add(btnLabs);
            panelSidebar.Controls.Add(btnHalls);
            panelSidebar.Controls.Add(btnAssignCourses);
            panelSidebar.Controls.Add(btnInstructors);
            panelSidebar.Controls.Add(btnCourses);
            panelSidebar.Controls.Add(btnStudents);
            panelSidebar.Controls.Add(btnDepartments);
            panelSidebar.Controls.Add(btnColleges);
            panelSidebar.Controls.Add(lblAdminTitle);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(3, 4, 3, 4);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(220, 700);
            panelSidebar.TabIndex = 0;
            // 
            // lblAdminTitle
            // 
            lblAdminTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAdminTitle.ForeColor = Color.White;
            lblAdminTitle.Location = new Point(10, 20);
            lblAdminTitle.Name = "lblAdminTitle";
            lblAdminTitle.Size = new Size(200, 60);
            lblAdminTitle.TabIndex = 0;
            lblAdminTitle.Text = "🎓 Admin Panel";
            lblAdminTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnColleges
            // 
            btnColleges.BackColor = Color.FromArgb(50, 50, 55);
            btnColleges.Cursor = Cursors.Hand;
            btnColleges.FlatStyle = FlatStyle.Flat;
            btnColleges.Font = new Font("Segoe UI", 10F);
            btnColleges.ForeColor = Color.White;
            btnColleges.Location = new Point(10, 90);
            btnColleges.Name = "btnColleges";
            btnColleges.Size = new Size(200, 40);
            btnColleges.TabIndex = 1;
            btnColleges.Text = "🏫 Colleges";
            btnColleges.TextAlign = ContentAlignment.MiddleLeft;
            btnColleges.UseVisualStyleBackColor = false;
            // 
            // btnDepartments
            // 
            btnDepartments.BackColor = Color.FromArgb(50, 50, 55);
            btnDepartments.Cursor = Cursors.Hand;
            btnDepartments.FlatStyle = FlatStyle.Flat;
            btnDepartments.Font = new Font("Segoe UI", 10F);
            btnDepartments.ForeColor = Color.White;
            btnDepartments.Location = new Point(10, 135);
            btnDepartments.Name = "btnDepartments";
            btnDepartments.Size = new Size(200, 40);
            btnDepartments.TabIndex = 2;
            btnDepartments.Text = "📂 Departments";
            btnDepartments.TextAlign = ContentAlignment.MiddleLeft;
            btnDepartments.UseVisualStyleBackColor = false;
            // 
            // btnStudents
            // 
            btnStudents.BackColor = Color.FromArgb(50, 50, 55);
            btnStudents.Cursor = Cursors.Hand;
            btnStudents.FlatStyle = FlatStyle.Flat;
            btnStudents.Font = new Font("Segoe UI", 10F);
            btnStudents.ForeColor = Color.White;
            btnStudents.Location = new Point(10, 180);
            btnStudents.Name = "btnStudents";
            btnStudents.Size = new Size(200, 40);
            btnStudents.TabIndex = 3;
            btnStudents.Text = "👥 Students";
            btnStudents.TextAlign = ContentAlignment.MiddleLeft;
            btnStudents.UseVisualStyleBackColor = false;
            // 
            // btnCourses
            // 
            btnCourses.BackColor = Color.FromArgb(50, 50, 55);
            btnCourses.Cursor = Cursors.Hand;
            btnCourses.FlatStyle = FlatStyle.Flat;
            btnCourses.Font = new Font("Segoe UI", 10F);
            btnCourses.ForeColor = Color.White;
            btnCourses.Location = new Point(10, 225);
            btnCourses.Name = "btnCourses";
            btnCourses.Size = new Size(200, 40);
            btnCourses.TabIndex = 4;
            btnCourses.Text = "📚 Courses";
            btnCourses.TextAlign = ContentAlignment.MiddleLeft;
            btnCourses.UseVisualStyleBackColor = false;
            // 
            // btnInstructors
            // 
            btnInstructors.BackColor = Color.FromArgb(50, 50, 55);
            btnInstructors.Cursor = Cursors.Hand;
            btnInstructors.FlatStyle = FlatStyle.Flat;
            btnInstructors.Font = new Font("Segoe UI", 10F);
            btnInstructors.ForeColor = Color.White;
            btnInstructors.Location = new Point(10, 270);
            btnInstructors.Name = "btnInstructors";
            btnInstructors.Size = new Size(200, 40);
            btnInstructors.TabIndex = 5;
            btnInstructors.Text = "👨‍🏫 Instructors";
            btnInstructors.TextAlign = ContentAlignment.MiddleLeft;
            btnInstructors.UseVisualStyleBackColor = false;
            // 
            // btnAssignCourses
            // 
            btnAssignCourses.BackColor = Color.FromArgb(50, 50, 55);
            btnAssignCourses.Cursor = Cursors.Hand;
            btnAssignCourses.FlatStyle = FlatStyle.Flat;
            btnAssignCourses.Font = new Font("Segoe UI", 10F);
            btnAssignCourses.ForeColor = Color.White;
            btnAssignCourses.Location = new Point(10, 315);
            btnAssignCourses.Name = "btnAssignCourses";
            btnAssignCourses.Size = new Size(200, 40);
            btnAssignCourses.TabIndex = 6;
            btnAssignCourses.Text = "📋 Assign Courses";
            btnAssignCourses.TextAlign = ContentAlignment.MiddleLeft;
            btnAssignCourses.UseVisualStyleBackColor = false;
            // 
            // btnHalls
            // 
            btnHalls.BackColor = Color.FromArgb(50, 50, 55);
            btnHalls.Cursor = Cursors.Hand;
            btnHalls.FlatStyle = FlatStyle.Flat;
            btnHalls.Font = new Font("Segoe UI", 10F);
            btnHalls.ForeColor = Color.White;
            btnHalls.Location = new Point(10, 360);
            btnHalls.Name = "btnHalls";
            btnHalls.Size = new Size(200, 40);
            btnHalls.TabIndex = 7;
            btnHalls.Text = "🏛️ Halls";
            btnHalls.TextAlign = ContentAlignment.MiddleLeft;
            btnHalls.UseVisualStyleBackColor = false;
            // 
            // btnLabs
            // 
            btnLabs.BackColor = Color.FromArgb(50, 50, 55);
            btnLabs.Cursor = Cursors.Hand;
            btnLabs.FlatStyle = FlatStyle.Flat;
            btnLabs.Font = new Font("Segoe UI", 10F);
            btnLabs.ForeColor = Color.White;
            btnLabs.Location = new Point(10, 405);
            btnLabs.Name = "btnLabs";
            btnLabs.Size = new Size(200, 40);
            btnLabs.TabIndex = 8;
            btnLabs.Text = "🔬 Labs";
            btnLabs.TextAlign = ContentAlignment.MiddleLeft;
            btnLabs.UseVisualStyleBackColor = false;
            // 
            // btnMercyGrades
            // 
            btnMercyGrades.BackColor = Color.FromArgb(50, 50, 55);
            btnMercyGrades.Cursor = Cursors.Hand;
            btnMercyGrades.FlatStyle = FlatStyle.Flat;
            btnMercyGrades.Font = new Font("Segoe UI", 10F);
            btnMercyGrades.ForeColor = Color.White;
            btnMercyGrades.Location = new Point(10, 450);
            btnMercyGrades.Name = "btnMercyGrades";
            btnMercyGrades.Size = new Size(200, 40);
            btnMercyGrades.TabIndex = 9;
            btnMercyGrades.Text = "🎁 Mercy Grades";
            btnMercyGrades.TextAlign = ContentAlignment.MiddleLeft;
            btnMercyGrades.UseVisualStyleBackColor = false;
            // 
            // btnAttendance
            // 
            btnAttendance.BackColor = Color.FromArgb(50, 50, 55);
            btnAttendance.Cursor = Cursors.Hand;
            btnAttendance.FlatStyle = FlatStyle.Flat;
            btnAttendance.Font = new Font("Segoe UI", 10F);
            btnAttendance.ForeColor = Color.White;
            btnAttendance.Location = new Point(10, 495);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(200, 40);
            btnAttendance.TabIndex = 10;
            btnAttendance.Text = "📋 Attendance";
            btnAttendance.TextAlign = ContentAlignment.MiddleLeft;
            btnAttendance.UseVisualStyleBackColor = false;
            // 
            // btnNews
            // 
            btnNews.BackColor = Color.FromArgb(50, 50, 55);
            btnNews.Cursor = Cursors.Hand;
            btnNews.FlatStyle = FlatStyle.Flat;
            btnNews.Font = new Font("Segoe UI", 10F);
            btnNews.ForeColor = Color.White;
            btnNews.Location = new Point(10, 540);
            btnNews.Name = "btnNews";
            btnNews.Size = new Size(200, 40);
            btnNews.TabIndex = 11;
            btnNews.Text = "📢 News";
            btnNews.TextAlign = ContentAlignment.MiddleLeft;
            btnNews.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogout.BackColor = Color.FromArgb(180, 50, 50);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(10, 650);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(200, 40);
            btnLogout.TabIndex = 12;
            btnLogout.Text = "🚪 Logout";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // panelContent
            // 
            panelContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContent.BackColor = Color.FromArgb(45, 45, 48);
            panelContent.Location = new Point(220, 0);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(880, 700);
            panelContent.TabIndex = 1;
            // 
            // AdminPanelForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1100, 700);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1000, 700);
            Name = "AdminPanelForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "University System - Admin Panel";
            Load += AdminPanelForm_Load;
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Button btnColleges;
        private System.Windows.Forms.Button btnDepartments;
        private System.Windows.Forms.Button btnStudents;
        private System.Windows.Forms.Button btnCourses;
        private System.Windows.Forms.Button btnInstructors;
        private System.Windows.Forms.Button btnAssignCourses;
        private System.Windows.Forms.Button btnHalls;
        private System.Windows.Forms.Button btnLabs;
        private System.Windows.Forms.Button btnMercyGrades;
        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.Button btnNews;
        private System.Windows.Forms.Button btnLogout;
    }
}
