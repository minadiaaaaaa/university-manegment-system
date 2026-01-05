/*
 * نظام إدارة الجامعة - الصفحة الرئيسية للطالب
 * تم عمله بواسطة: Mina Diaa
 * التاريخ: ديسمبر 2024
 * الوصف: عرض بيانات الطالب، الدرجات، المواد، المدفوعات
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class MainForm : Form
    {
        // بيانات الطالب الحالي
        private readonly Student _currentStudent;
        // اتصال قاعدة البيانات
        private readonly DatabaseHelper _db = new();

        // ازرار القائمة الجانبية
        private Button btnDashboard = new();  // الصفحة الرئيسية
        private Button btnGrades = new();     // الدرجات
        private Button btnCourses = new();    // المواد
        private Button btnPayments = new();   // المدفوعات 
        private Button btnProfile = new();    // الملف الشخصي
        private Button btnLogout = new();     // تسجيل الخروج

        public MainForm(Student student)
        {
            _currentStudent = student;
            InitializeComponent();
            this.Text = $"University System - Welcome {_currentStudent.Name}";
            SetupSidebar();
            ShowDashboard();
        }

        private void SetupSidebar()
        {
            // Logo PictureBox
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "assets", "logo.jpg");
            var picLogo = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(120, 120),
                Location = new Point((panelSidebar.Width - 120) / 2, 20)
            };
            if (File.Exists(logoPath))
            {
                picLogo.Image = Image.FromFile(logoPath);
            }
            panelSidebar.Controls.Add(picLogo);

            // Logo Text under picture
            var lblLogoText = new Label
            {
                Text = "Student Panel",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            lblLogoText.Location = new Point((panelSidebar.Width - lblLogoText.PreferredWidth) / 2, picLogo.Bottom + 10);
            panelSidebar.Controls.Add(lblLogoText);

            // Create sidebar buttons
            int buttonY = lblLogoText.Bottom + 20;
            int buttonSpacing = 50;

            btnDashboard = CreateSidebarButton("📊 Dashboard", buttonY);
            btnDashboard.Click += (s, e) => ShowDashboard();
            panelSidebar.Controls.Add(btnDashboard);
            buttonY += buttonSpacing;

            btnGrades = CreateSidebarButton("📝 My Grades", buttonY);
            btnGrades.Click += (s, e) => ShowGrades();
            panelSidebar.Controls.Add(btnGrades);
            buttonY += buttonSpacing;

            btnCourses = CreateSidebarButton("📚 Courses", buttonY);
            btnCourses.Click += (s, e) => ShowCourses();
            panelSidebar.Controls.Add(btnCourses);
            buttonY += buttonSpacing;

            btnPayments = CreateSidebarButton("💳 Payments", buttonY);
            btnPayments.Click += (s, e) => ShowPayments();
            panelSidebar.Controls.Add(btnPayments);
            buttonY += buttonSpacing;

            btnProfile = CreateSidebarButton("👤 Profile", buttonY);
            btnProfile.Click += (s, e) => ShowProfile();
            panelSidebar.Controls.Add(btnProfile);

            // Logout button at bottom
            btnLogout = CreateSidebarButton("🚪 Logout", 0); // Y is handled by Anchor
            btnLogout.BackColor = Color.FromArgb(180, 50, 50);
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogout.Location = new Point(10, panelSidebar.Height - 60);
            btnLogout.Click += (s, e) => this.Close();
            panelSidebar.Controls.Add(btnLogout);
        }

        private Button CreateSidebarButton(string text, int y)
        {
            return new Button
            {
                Text = text,
                Size = new Size(180, 40),
                Location = new Point(10, y),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 55),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand
            };
        }

        private void ClearContent()
        {
            panelContent.Controls.Clear();
        }

        // ==================== DASHBOARD ====================
        private void ShowDashboard()
        {
            ClearContent();

            // Welcome label
            var lblWelcome = new Label
            {
                Text = $"Welcome, {_currentStudent.Name}!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30),
                AutoSize = true
            };
            panelContent.Controls.Add(lblWelcome);

            // Student info cards
            int cardY = 100;
            int cardWidth = 200;
            int cardHeight = 100;
            int cardSpacing = 220;

            AddInfoCard("Student ID", _currentStudent.Id.ToString(), 30, cardY, cardWidth, cardHeight, Color.FromArgb(0, 122, 204));
            AddInfoCard("Department", _currentStudent.Department, 30 + cardSpacing, cardY, cardWidth, cardHeight, Color.FromArgb(104, 33, 122));
            AddInfoCard("Level", $"Level {_currentStudent.Level}", 30 + cardSpacing * 2, cardY, cardWidth, cardHeight, Color.FromArgb(0, 150, 100));


            // Quick Actions
            var lblActions = new Label
            {
                Text = "Quick Actions",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 340),
                AutoSize = true
            };
            panelContent.Controls.Add(lblActions);

            var btnViewGrades = new Button
            {
                Text = "📝 View Grades",
                Size = new Size(150, 50),
                Location = new Point(30, 380),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnViewGrades.Click += (s, e) => ShowGrades();
            panelContent.Controls.Add(btnViewGrades);
        }

        private void AddInfoCard(string title, string value, int x, int y, int width, int height, Color color)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                Location = new Point(x, y),
                BackColor = color
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 45),
                AutoSize = true
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblValue);
            panelContent.Controls.Add(panel);
        }

        // ==================== GRADES ====================
        private void ShowGrades()
        {
            ClearContent();

            var lblTitle = new Label
            {
                Text = "📝 My Grades",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30),
                AutoSize = true
            };
            panelContent.Controls.Add(lblTitle);

            var dgvGrades = new DataGridView
            {
                Location = new Point(30, 80),
                Size = new Size(panelContent.Width - 60, 400),
                BackgroundColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(0, 122, 204),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvGrades.Columns.Add("Course", "Course");
            dgvGrades.Columns.Add("S1", "S1 (10)");
            dgvGrades.Columns.Add("S2", "S2 (10)");
            dgvGrades.Columns.Add("YW", "Year Work (20)");
            dgvGrades.Columns.Add("Final", "Final (60)");
            dgvGrades.Columns.Add("Total", "Total");
            dgvGrades.Columns.Add("Grade", "Grade");
            dgvGrades.Columns.Add("Pearson", "Pearson (P/M/D)");

            var grades = _db.GetGradesByStudent(_currentStudent.Id);
            foreach (var g in grades)
            {
                // PearsonGrade is already calculated correctly in GetGradesByStudent using percentage
                dgvGrades.Rows.Add(g.CourseCode, g.S1Score, g.S2Score, g.YearWorkScore,
                    g.FinalExamScore, g.TotalScore, g.LetterGrade, g.PearsonGrade);
            }

            if (grades.Count == 0)
            {
                dgvGrades.Rows.Add("No grades available", "-", "-", "-", "-", "-", "-", "-");
            }

            panelContent.Controls.Add(dgvGrades);
        }

        // ==================== COURSES ====================
        private void ShowCourses()
        {
            ClearContent();

            // Title with student info
            var lblTitle = new Label
            {
                Text = $"📚 مواد السنة {_currentStudent.Level} - الترم {_currentStudent.Semester}",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 20),
                AutoSize = true
            };
            panelContent.Controls.Add(lblTitle);

            var lblTrack = new Label
            {
                Text = $"Track: {_currentStudent.Track}",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.LightGray,
                Location = new Point(30, 60),
                AutoSize = true
            };
            panelContent.Controls.Add(lblTrack);

            // Register All Button
            var btnRegisterAll = new Button
            {
                Text = "📝 تسجيل كل المواد",
                Size = new Size(180, 40),
                Location = new Point(panelContent.Width - 220, 20),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 150, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegisterAll.FlatAppearance.BorderSize = 0;
            btnRegisterAll.Click += (s, e) => RegisterAllCourses();
            panelContent.Controls.Add(btnRegisterAll);

            // Available Courses Section
            var lblAvailable = new Label
            {
                Text = "📖 المواد المتاحة للتسجيل:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 200, 150),
                Location = new Point(30, 100),
                AutoSize = true
            };
            panelContent.Controls.Add(lblAvailable);

            var dgvCourses = new DataGridView
            {
                Name = "dgvAvailableCourses",
                Location = new Point(30, 135),
                Size = new Size(panelContent.Width - 60, 180),
                BackgroundColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(0, 122, 204),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvCourses.Columns.Add("Code", "الكود");
            dgvCourses.Columns.Add("Name", "اسم المادة");
            dgvCourses.Columns.Add("Hours", "الساعات");
            dgvCourses.Columns.Add("Status", "الحالة");

            // Get courses for student's level and term
            var courses = _db.GetCoursesByLevelAndTerm(_currentStudent.Level, _currentStudent.Semester, _currentStudent.Track);
            foreach (var c in courses)
            {
                bool isRegistered = _db.IsRegisteredForCourse(_currentStudent.Id, c.Code);
                int rowIndex = dgvCourses.Rows.Add(c.Code, c.Name, c.CreditHours, isRegistered ? "✅ مسجل" : "⏳ غير مسجل");
            }

            // Get carryover courses (from previous terms)
            var carryoverCourses = _db.GetCarryoverCourses(_currentStudent.Id, _currentStudent.Level, _currentStudent.Semester, _currentStudent.Track);
            foreach (var c in carryoverCourses)
            {
                int rowIndex = dgvCourses.Rows.Add(c.Code, $"🔴 {c.Name} (مرحّل من سنة {c.Level} ترم {c.Semester})", c.CreditHours, "⚠️ مرحّل");
                dgvCourses.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(80, 40, 40);
                dgvCourses.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.White;
            }

            if (courses.Count == 0 && carryoverCourses.Count == 0)
            {
                dgvCourses.Rows.Add("", "لا توجد مواد متاحة لهذه السنة والترم", "", "");
            }

            panelContent.Controls.Add(dgvCourses);

            // Register Selected Button
            var btnRegister = new Button
            {
                Text = "📝 تسجيل المادة المحددة",
                Size = new Size(180, 35),
                Location = new Point(30, 325),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += (s, e) => RegisterSelectedCourse(dgvCourses);
            panelContent.Controls.Add(btnRegister);

            // Registered Courses Section
            var lblRegistered = new Label
            {
                Text = "✅ المواد المسجلة:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 200, 100),
                Location = new Point(30, 375),
                AutoSize = true
            };
            panelContent.Controls.Add(lblRegistered);

            var dgvRegistered = new DataGridView
            {
                Name = "dgvRegisteredCourses",
                Location = new Point(30, 410),
                Size = new Size(panelContent.Width - 60, 150),
                BackgroundColor = Color.FromArgb(50, 70, 50),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(0, 150, 100),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            dgvRegistered.Columns.Add("Code", "الكود");
            dgvRegistered.Columns.Add("Name", "اسم المادة");
            dgvRegistered.Columns.Add("Hours", "الساعات");

            var registeredCourses = _db.GetRegisteredCourses(_currentStudent.Id);
            foreach (var c in registeredCourses)
            {
                dgvRegistered.Rows.Add(c.Code, c.Name, c.CreditHours);
            }

            if (registeredCourses.Count == 0)
            {
                dgvRegistered.Rows.Add("", "لم تسجل أي مواد بعد", "");
            }

            panelContent.Controls.Add(dgvRegistered);

            // Total Credit Hours
            int totalHours = registeredCourses.Sum(c => c.CreditHours);
            var lblTotal = new Label
            {
                Text = $"إجمالي الساعات المسجلة: {totalHours} ساعة",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.LightGreen,
                Location = new Point(250, 325),
                AutoSize = true
            };
            panelContent.Controls.Add(lblTotal);
        }

        private void RegisterSelectedCourse(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("اختر مادة للتسجيل!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var code = dgv.SelectedRows[0].Cells["Code"].Value?.ToString();
            if (string.IsNullOrEmpty(code)) return;

            if (_db.IsRegisteredForCourse(_currentStudent.Id, code))
            {
                MessageBox.Show("أنت مسجل في هذه المادة بالفعل!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_db.RegisterForCourse(_currentStudent.Id, code))
            {
                MessageBox.Show("تم تسجيل المادة بنجاح! ✅", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowCourses(); // Refresh
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء التسجيل!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterAllCourses()
        {
            var courses = _db.GetCoursesByLevelAndTerm(_currentStudent.Level, _currentStudent.Semester, _currentStudent.Track);
            int registered = 0;

            foreach (var c in courses)
            {
                if (!_db.IsRegisteredForCourse(_currentStudent.Id, c.Code))
                {
                    if (_db.RegisterForCourse(_currentStudent.Id, c.Code))
                    {
                        registered++;
                    }
                }
            }

            if (registered > 0)
            {
                MessageBox.Show($"تم تسجيل {registered} مادة بنجاح! ✅", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowCourses(); // Refresh
            }
            else
            {
                MessageBox.Show("جميع المواد مسجلة بالفعل!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ==================== PAYMENTS ====================
        private void ShowPayments()
        {
            ClearContent();

            var lblTitle = new Label
            {
                Text = "💳 المدفوعات والمصاريف",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 20),
                AutoSize = true
            };
            panelContent.Controls.Add(lblTitle);

            // Get payment summary
            var summary = _db.GetPaymentSummary(_currentStudent.Id, _currentStudent.Level);

            // Fee cards
            int cardY = 70;
            int cardWidth = 180;
            int cardHeight = 90;

            // Total Fee Card
            var panelFee = new Panel
            {
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(30, cardY),
                BackColor = Color.FromArgb(0, 122, 204)
            };
            panelFee.Controls.Add(new Label { Text = "إجمالي المصاريف", Font = new Font("Segoe UI", 9), ForeColor = Color.LightGray, Location = new Point(10, 10), AutoSize = true });
            panelFee.Controls.Add(new Label { Text = $"{summary.TotalFee:N0} ج.م", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 40), AutoSize = true });
            panelContent.Controls.Add(panelFee);

            // Paid Card
            var panelPaid = new Panel
            {
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(230, cardY),
                BackColor = Color.FromArgb(0, 150, 100)
            };
            panelPaid.Controls.Add(new Label { Text = "المدفوع", Font = new Font("Segoe UI", 9), ForeColor = Color.LightGray, Location = new Point(10, 10), AutoSize = true });
            panelPaid.Controls.Add(new Label { Text = $"{summary.TotalPaid:N0} ج.م", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 40), AutoSize = true });
            panelContent.Controls.Add(panelPaid);

            // Remaining Card
            var panelRemaining = new Panel
            {
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(430, cardY),
                BackColor = summary.Remaining > 0 ? Color.FromArgb(180, 50, 50) : Color.FromArgb(0, 150, 100)
            };
            panelRemaining.Controls.Add(new Label { Text = "المتبقي", Font = new Font("Segoe UI", 9), ForeColor = Color.LightGray, Location = new Point(10, 10), AutoSize = true });
            panelRemaining.Controls.Add(new Label { Text = $"{summary.Remaining:N0} ج.م", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 40), AutoSize = true });
            panelContent.Controls.Add(panelRemaining);

            // Level info
            var lblLevel = new Label
            {
                Text = $"📚 السنة الدراسية: {_currentStudent.Level} - المصاريف: {(summary.TotalFee == 15000 ? "15,000" : "20,000")} جنيه",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.LightGray,
                Location = new Point(30, cardY + cardHeight + 15),
                AutoSize = true
            };
            panelContent.Controls.Add(lblLevel);

            // Payment status
            string status = summary.Remaining <= 0 ? "✅ تم سداد جميع المصاريف" : $"⚠️ متبقي {summary.Remaining:N0} جنيه للسداد";
            var lblStatus = new Label
            {
                Text = status,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = summary.Remaining <= 0 ? Color.LightGreen : Color.Orange,
                Location = new Point(30, cardY + cardHeight + 45),
                AutoSize = true
            };
            panelContent.Controls.Add(lblStatus);

            // Payment History
            var lblHistory = new Label
            {
                Text = "📜 سجل المدفوعات:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, cardY + cardHeight + 90),
                AutoSize = true
            };
            panelContent.Controls.Add(lblHistory);

            var dgv = new DataGridView
            {
                Location = new Point(30, cardY + cardHeight + 125),
                Size = new Size(panelContent.Width - 60, 200),
                BackgroundColor = Color.FromArgb(50, 50, 55),
                GridColor = Color.FromArgb(70, 70, 75),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 35
            };
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 65);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv.Columns.Add("Date", "التاريخ");
            dgv.Columns.Add("Amount", "المبلغ");
            dgv.Columns.Add("Type", "النوع");
            dgv.Columns.Add("Status", "الحالة");
            dgv.Columns.Add("Description", "الوصف");

            var payments = _db.GetStudentPayments(_currentStudent.Id);
            foreach (var p in payments)
            {
                dgv.Rows.Add(p.PaymentDate, $"{p.Amount:N0} ج.م", p.PaymentType, p.Status == "paid" ? "✅ مدفوع" : "⏳ معلق", p.Description);
            }

            if (payments.Count == 0)
            {
                dgv.Rows.Add("-", "-", "لا توجد مدفوعات مسجلة", "-", "-");
            }

            panelContent.Controls.Add(dgv);
        }

        // ==================== PROFILE ====================
        private void ShowProfile()
        {
            ClearContent();

            var lblTitle = new Label
            {
                Text = "👤 My Profile",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30),
                AutoSize = true
            };
            panelContent.Controls.Add(lblTitle);

            int y = 90;
            int spacing = 40;

            AddProfileField("Name:", _currentStudent.Name, y); y += spacing;
            AddProfileField("Email:", _currentStudent.Email, y); y += spacing;
            AddProfileField("Department:", _currentStudent.Department, y); y += spacing;
            AddProfileField("Level:", $"Level {_currentStudent.Level}", y); y += spacing;

        }

        private void AddProfileField(string label, string value, int y)
        {
            var lblLabel = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(30, y),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(150, y),
                AutoSize = true
            };

            panelContent.Controls.Add(lblLabel);
            panelContent.Controls.Add(lblValue);
        }
    }
}
