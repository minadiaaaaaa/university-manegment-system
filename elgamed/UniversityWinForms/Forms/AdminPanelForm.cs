/*
 * نظام إدارة الجامعة - لوحة تحكم الأدمن
 * تم عمله بواسطة: Mina Diaa  
 * التاريخ: ديسمبر 2024
 * الوصف: شاشة إدارة الطلاب والمواد والدرجات
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class AdminPanelForm : Form
    {
        // Database connection
        private readonly DatabaseHelper _db = new();

        public AdminPanelForm()
        {
            InitializeComponent();
            WireUpEvents();
        }

        private void WireUpEvents()
        {
            // Connect Designer buttons to event handlers
            btnColleges.Click += (s, e) => ShowFaculties();
            btnDepartments.Click += (s, e) => ShowDepartments();
            btnStudents.Click += (s, e) => ShowStudents();
            btnCourses.Click += (s, e) => ShowCourses();
            btnInstructors.Click += (s, e) => ShowInstructors();
            btnAssignCourses.Click += (s, e) => ShowAssignCourses();
            btnHalls.Click += (s, e) => ShowHalls();
            btnLabs.Click += (s, e) => ShowLabs();
            btnMercyGrades.Click += (s, e) => ShowMercyGrades();
            btnAttendance.Click += (s, e) => ShowAttendance();
            btnNews.Click += (s, e) => ShowNews();
            btnLogout.Click += (s, e) => this.Close();
        }

        private void AdminPanelForm_Load(object? sender, EventArgs e)
        {
            // Show default view
            ShowFaculties();
        }

        private Button CreateMenuButton(string text, int y)
        {
            return new Button
            {
                Text = text,
                Size = new Size(180, 42),
                Location = new Point(10, y),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 55),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
        }

        private void ClearContent()
        {
            panelContent.Controls.Clear();
        }

        private Label CreateTitle(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                AutoSize = true
            };
        }

        private DataGridView CreateDataGrid(int y, int height)
        {
            return new DataGridView
            {
                Location = new Point(30, y),
                Size = new Size(panelContent.Width - 60, height),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
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
        }

        // ==================== STUDENTS ====================
        private void ShowStudents()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("👥 Manage Students"));

            var btnAdd = CreateActionButton("➕ Add", 30, 70, Color.FromArgb(0, 150, 100));
            var btnEdit = CreateActionButton("✏️ Edit", 140, 70, Color.FromArgb(0, 122, 204));
            var btnDelete = CreateActionButton("🗑️ Delete", 250, 70, Color.FromArgb(180, 50, 50));

            panelContent.Controls.Add(btnAdd);
            panelContent.Controls.Add(btnEdit);
            panelContent.Controls.Add(btnDelete);

            var dgv = CreateDataGrid(120, 400);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Name", "Name");
            dgv.Columns.Add("Email", "Email");
            dgv.Columns.Add("Faculty", "Faculty");
            dgv.Columns.Add("Department", "Department");
            dgv.Columns.Add("Level", "Year");


            var students = _db.GetAllStudentsWithFaculty();
            foreach (var s in students)
            {
                dgv.Rows.Add(s.Id, s.Name, s.Email, s.FacultyName, s.Department, s.Level);
            }

            btnAdd.Click += (s, e) => AddStudent(dgv);
            btnDelete.Click += (s, e) => DeleteStudent(dgv);

            panelContent.Controls.Add(dgv);
        }

        private void AddStudent(DataGridView dgv)
        {
            using var form = new Form
            {
                Text = "Add New Student",
                Size = new Size(500, 520),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(45, 45, 48),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            int y = 20;
            
            // Basic Info
            var txtName = CreateInputField(form, "Name:", ref y);
            var txtNameAr = CreateInputField(form, "Name (Arabic):", ref y);
            var txtEmail = CreateInputField(form, "Email:", ref y);
            var txtPassword = CreateInputField(form, "Password:", ref y);
            txtPassword.Text = "123456"; // Default password
            var txtPhone = CreateInputField(form, "Phone:", ref y);
            var txtNationalId = CreateInputField(form, "National ID:", ref y);

            // Faculty dropdown
            var lblFaculty = new Label { Text = "Faculty:", Location = new Point(20, y), ForeColor = Color.White, AutoSize = true };
            form.Controls.Add(lblFaculty);
            var cmbFaculty = new ComboBox
            {
                Location = new Point(130, y),
                Size = new Size(320, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            var faculties = _db.GetAllFaculties();
            foreach (var f in faculties) cmbFaculty.Items.Add($"{f.Id}|{f.Name}");
            if (cmbFaculty.Items.Count > 0) cmbFaculty.SelectedIndex = 0;
            form.Controls.Add(cmbFaculty);
            y += 35;

            // Department dropdown
            var lblDept = new Label { Text = "Department:", Location = new Point(20, y), ForeColor = Color.White, AutoSize = true };
            form.Controls.Add(lblDept);
            var cmbDept = new ComboBox
            {
                Location = new Point(130, y),
                Size = new Size(320, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            form.Controls.Add(cmbDept);
            y += 35;

            // Update departments when faculty changes
            cmbFaculty.SelectedIndexChanged += (s, e) =>
            {
                cmbDept.Items.Clear();
                if (cmbFaculty.SelectedItem == null) return;
                int facultyId = int.Parse(cmbFaculty.SelectedItem.ToString()!.Split('|')[0]);
                var departments = _db.GetAllDepartments();
                foreach (var d in departments.Where(d => d.FacultyId == facultyId))
                {
                    cmbDept.Items.Add($"{d.Id}|{d.Name}");
                }
                if (cmbDept.Items.Count > 0) cmbDept.SelectedIndex = 0;
            };
            // Trigger initial load only if faculties exist
            if (cmbFaculty.Items.Count > 0) cmbFaculty.SelectedIndex = 0;

            // Level and Semester
            var lblLevel = new Label { Text = "Year:", Location = new Point(20, y), ForeColor = Color.White, AutoSize = true };
            form.Controls.Add(lblLevel);
            var cmbLevel = new ComboBox
            {
                Location = new Point(130, y),
                Size = new Size(80, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbLevel.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbLevel.SelectedIndex = 0;
            form.Controls.Add(cmbLevel);

            var lblSem = new Label { Text = "Term:", Location = new Point(230, y), ForeColor = Color.White, AutoSize = true };
            form.Controls.Add(lblSem);
            var cmbSem = new ComboBox
            {
                Location = new Point(290, y),
                Size = new Size(80, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSem.Items.AddRange(new object[] { "1", "2" });
            cmbSem.SelectedIndex = 0;
            form.Controls.Add(cmbSem);
            y += 35;

            // Track (only for IT department)
            var lblTrack = new Label { Text = "Track:", Location = new Point(20, y), ForeColor = Color.White, AutoSize = true };
            form.Controls.Add(lblTrack);
            var cmbTrack = new ComboBox
            {
                Location = new Point(130, y),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTrack.Items.AddRange(new object[] { "SW", "Network" });
            cmbTrack.SelectedIndex = 0;
            form.Controls.Add(cmbTrack);
            y += 35;

            // Function to update Track visibility
            void UpdateTrackVisibility()
            {
                bool isITDept = cmbDept.SelectedItem?.ToString()?.Contains("Information Technology") == true;
                int year = int.TryParse(cmbLevel.SelectedItem?.ToString(), out int y) ? y : 1;
                
                // Track only for IT department AND year 3 or 4
                bool showTrack = isITDept && (year >= 3);
                
                lblTrack.Visible = showTrack;
                cmbTrack.Visible = showTrack;
            }

            // Update Track when department or year changes
            cmbDept.SelectedIndexChanged += (s, e) => UpdateTrackVisibility();
            cmbLevel.SelectedIndexChanged += (s, e) => UpdateTrackVisibility();

            // Initially hide track
            lblTrack.Visible = false;
            cmbTrack.Visible = false;

            var btnSave = new Button
            {
                Text = "💾 Save Student",
                Size = new Size(150, 40),
                Location = new Point(170, y + 10),
                BackColor = Color.FromArgb(0, 150, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11)
            };

            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Name and Email are required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int deptId = 1;
                if (cmbDept.SelectedItem != null)
                {
                    deptId = int.Parse(cmbDept.SelectedItem.ToString()!.Split('|')[0]);
                }

                var student = new Student
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Phone = txtPhone.Text,
                    Level = int.Parse(cmbLevel.SelectedItem?.ToString() ?? "1"),
                    Semester = int.Parse(cmbSem.SelectedItem?.ToString() ?? "1"),
                    Track = cmbTrack.Visible ? (cmbTrack.SelectedItem?.ToString() ?? "General") : "General"
                };

                if (_db.AddStudentWithDept(student, deptId, txtNameAr.Text, txtNationalId.Text))
                {
                    MessageBox.Show($"Student added! ID: {_db.GenerateStudentId(student.Level) - 1}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form.Close();
                    ShowStudents();
                }
                else
                {
                    MessageBox.Show("Failed! Email may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.Controls.Add(btnSave);
            form.ShowDialog();
        }

        private TextBox CreateInputField(Form form, string label, ref int y)
        {
            var lbl = new Label
            {
                Text = label,
                Location = new Point(20, y),
                ForeColor = Color.White,
                AutoSize = true
            };
            form.Controls.Add(lbl);

            var txt = new TextBox
            {
                Location = new Point(130, y),
                Size = new Size(230, 25),
                BackColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            form.Controls.Add(txt);
            y += 40;
            return txt;
        }

        private void DeleteStudent(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a student!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cell = dgv.SelectedRows[0].Cells["ID"].Value;
            if (cell == null) return;

            int id = Convert.ToInt32(cell);
            if (MessageBox.Show("Delete this student?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_db.DeleteStudent(id))
                {
                    ShowStudents();
                }
            }
        }

        private Button CreateActionButton(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Size = new Size(100, 35),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
        }

        // ==================== COURSES ====================
        private void ShowCourses()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("📚 Manage Courses"));

            int y = 60;

            // Faculty Filter
            var lblFaculty = new Label { Text = "Faculty:", Location = new Point(30, y + 3), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblFaculty);
            var cmbFaculty = new ComboBox
            {
                Location = new Point(100, y),
                Size = new Size(250, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFaculty.Items.Add("All Faculties");
            foreach (var f in _db.GetAllFaculties())
            {
                cmbFaculty.Items.Add($"{f.Id}|{f.Name}");
            }
            cmbFaculty.SelectedIndex = 0;
            panelContent.Controls.Add(cmbFaculty);

            // Department Filter
            var lblDept = new Label { Text = "Dept:", Location = new Point(370, y + 3), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblDept);
            var cmbDept = new ComboBox
            {
                Location = new Point(420, y),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDept.Items.Add("All Departments");
            cmbDept.SelectedIndex = 0;
            panelContent.Controls.Add(cmbDept);

            y += 40;

            var dgv = CreateDataGrid(y + 10, 380);
            dgv.Columns.Add("Code", "Code");
            dgv.Columns.Add("Name", "Course Name");
            dgv.Columns.Add("Hours", "Hours");
            dgv.Columns.Add("Level", "Year");
            dgv.Columns.Add("Term", "Term");
            dgv.Columns.Add("Track", "Track");

            // Function to load courses
            void LoadCourses()
            {
                dgv.Rows.Clear();
                var courses = _db.GetAllCourses();
                
                // Filter by department if selected
                if (cmbDept.SelectedIndex > 0 && cmbDept.SelectedItem != null)
                {
                    string deptName = cmbDept.SelectedItem.ToString()!.Split('|').Last();
                    courses = courses.Where(c => c.Department == deptName).ToList();
                }
                
                foreach (var c in courses)
                {
                    dgv.Rows.Add(c.Code, c.Name, c.CreditHours, c.Level, c.Semester, c.Track);
                }
            }

            // Faculty change -> Update departments
            cmbFaculty.SelectedIndexChanged += (s, e) =>
            {
                cmbDept.Items.Clear();
                cmbDept.Items.Add("All Departments");
                
                if (cmbFaculty.SelectedIndex > 0 && cmbFaculty.SelectedItem != null)
                {
                    int facultyId = int.Parse(cmbFaculty.SelectedItem.ToString()!.Split('|')[0]);
                    var departments = _db.GetAllDepartments().Where(d => d.FacultyId == facultyId);
                    foreach (var d in departments)
                    {
                        cmbDept.Items.Add($"{d.Id}|{d.Name}");
                    }
                }
                else
                {
                    foreach (var d in _db.GetAllDepartments())
                    {
                        cmbDept.Items.Add($"{d.Id}|{d.Name}");
                    }
                }
                cmbDept.SelectedIndex = 0;
                LoadCourses();
            };

            // Department change -> Filter courses
            cmbDept.SelectedIndexChanged += (s, e) => LoadCourses();

            LoadCourses();
            panelContent.Controls.Add(dgv);
        }

        // ==================== FACULTIES (COLLEGES) ====================
        private void ShowFaculties()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("🏫 Colleges (Faculties)"));

            var dgv = CreateDataGrid(80, 420);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Name", "College Name (English)");
            dgv.Columns.Add("NameAr", "College Name (Arabic)");
            dgv.Columns.Add("Code", "Code");

            var faculties = _db.GetAllFaculties();
            foreach (var f in faculties)
            {
                dgv.Rows.Add(f.Id, f.Name, f.NameAr, f.Code);
            }
            panelContent.Controls.Add(dgv);
        }

        // ==================== DEPARTMENTS ====================
        private void ShowDepartments()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("📂 Departments"));

            var dgv = CreateDataGrid(80, 420);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Name", "Department Name (English)");
            dgv.Columns.Add("NameAr", "Department Name (Arabic)");
            dgv.Columns.Add("Code", "Code");
            dgv.Columns.Add("Faculty", "College");

            var departments = _db.GetAllDepartments();
            foreach (var d in departments)
            {
                dgv.Rows.Add(d.Id, d.Name, d.NameAr, d.Code, d.FacultyName);
            }
            panelContent.Controls.Add(dgv);
        }

        // ==================== INSTRUCTORS ====================
        private void ShowInstructors()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("👨‍🏫 Manage Instructors"));

            var btnAdd = CreateActionButton("➕ Add", 30, 70, Color.FromArgb(0, 150, 100));
            var btnDelete = CreateActionButton("🗑️ Delete", 140, 70, Color.FromArgb(180, 50, 50));
            panelContent.Controls.Add(btnAdd);
            panelContent.Controls.Add(btnDelete);

            var dgv = CreateDataGrid(120, 380);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Name", "Name");
            dgv.Columns.Add("Email", "Email");
            dgv.Columns.Add("Phone", "Phone");
            dgv.Columns.Add("Title", "Title");
            dgv.Columns.Add("Specialization", "Specialization");

            var instructors = _db.GetAllInstructors();
            foreach (var i in instructors)
            {
                dgv.Rows.Add(i.Id, i.Name, i.Email, i.Phone, i.Title, i.Specialization);
            }

            btnAdd.Click += (s, e) => AddInstructor(dgv);
            btnDelete.Click += (s, e) => DeleteInstructor(dgv);
            panelContent.Controls.Add(dgv);
        }

        private void AddInstructor(DataGridView dgv)
        {
            using var form = new Form
            {
                Text = "Add Instructor",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(45, 45, 48),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            int y = 20;
            var txtName = CreateInputField(form, "Name:", ref y);
            var txtEmail = CreateInputField(form, "Email:", ref y);
            var txtPhone = CreateInputField(form, "Phone:", ref y);
            var txtTitle = CreateInputField(form, "Title (Dr./Prof.):", ref y);
            var txtSpec = CreateInputField(form, "Specialization:", ref y);

            var btnSave = new Button
            {
                Text = "Save",
                Size = new Size(100, 35),
                Location = new Point(145, y + 10),
                BackColor = Color.FromArgb(0, 150, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnSave.Click += (s, e) =>
            {
                if (_db.AddInstructor(txtName.Text, txtEmail.Text, txtPhone.Text, txtTitle.Text, txtSpec.Text))
                {
                    MessageBox.Show("Instructor added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form.Close();
                    ShowInstructors();
                }
            };

            form.Controls.Add(btnSave);
            form.ShowDialog();
        }

        private void DeleteInstructor(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0) return;
            var cell = dgv.SelectedRows[0].Cells["ID"].Value;
            if (cell == null) return;
            int id = Convert.ToInt32(cell);
            if (MessageBox.Show("Delete this instructor?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_db.DeleteInstructor(id)) ShowInstructors();
            }
        }

        // ==================== HALLS ====================
        private void ShowHalls()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("🏛️ Manage Lecture Halls"));

            var btnAdd = CreateActionButton("➕ Add", 30, 70, Color.FromArgb(0, 150, 100));
            panelContent.Controls.Add(btnAdd);

            var dgv = CreateDataGrid(120, 380);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Code", "Room Code");
            dgv.Columns.Add("Building", "Building");
            dgv.Columns.Add("Capacity", "Capacity");
            dgv.Columns.Add("Type", "Type");

            var halls = _db.GetAllClassrooms();
            foreach (var h in halls)
            {
                dgv.Rows.Add(h.Id, h.Code, h.Building, h.Capacity, h.RoomType);
            }

            btnAdd.Click += (s, e) => AddHall(dgv);
            panelContent.Controls.Add(dgv);
        }

        private void AddHall(DataGridView dgv)
        {
            using var form = new Form
            {
                Text = "Add Hall/Lab",
                Size = new Size(400, 280),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(45, 45, 48),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            int y = 20;
            var txtCode = CreateInputField(form, "Room Code:", ref y);
            var txtBuilding = CreateInputField(form, "Building:", ref y);
            var txtCapacity = CreateInputField(form, "Capacity:", ref y);
            
            var lblType = new Label { Text = "Type:", Location = new Point(20, y), ForeColor = Color.White, AutoSize = true };
            form.Controls.Add(lblType);
            var cmbType = new ComboBox
            {
                Location = new Point(130, y),
                Size = new Size(230, 25),
                BackColor = Color.FromArgb(60, 60, 65),
                ForeColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbType.Items.AddRange(new[] { "Hall", "Lab", "Lecture Room" });
            cmbType.SelectedIndex = 0;
            form.Controls.Add(cmbType);
            y += 40;

            var btnSave = new Button
            {
                Text = "Save",
                Size = new Size(100, 35),
                Location = new Point(145, y + 10),
                BackColor = Color.FromArgb(0, 150, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnSave.Click += (s, e) =>
            {
                int capacity = int.TryParse(txtCapacity.Text, out int c) ? c : 50;
                if (_db.AddClassroom(txtCode.Text, txtBuilding.Text, capacity, cmbType.SelectedItem?.ToString() ?? "Hall"))
                {
                    MessageBox.Show("Room added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form.Close();
                    ShowHalls();
                }
            };

            form.Controls.Add(btnSave);
            form.ShowDialog();
        }

        // ==================== LABS ====================
        private void ShowLabs()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("🔬 Manage Laboratories"));

            var dgv = CreateDataGrid(80, 420);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Code", "Lab Code");
            dgv.Columns.Add("Building", "Building");
            dgv.Columns.Add("Capacity", "Capacity");

            var labs = _db.GetLabsOnly();
            foreach (var l in labs)
            {
                dgv.Rows.Add(l.Id, l.Code, l.Building, l.Capacity);
            }
            panelContent.Controls.Add(dgv);
        }

        // ==================== GRADES ====================
        private void ShowGrades()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("📝 Enter & Manage Grades"));

            // Student selection
            var lblStudent = new Label { Text = "Select Student:", Location = new Point(30, 70), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblStudent);
            
            var cmbStudent = new ComboBox
            {
                Location = new Point(150, 67),
                Size = new Size(250, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            var students = _db.GetAllStudents();
            foreach (var s in students) cmbStudent.Items.Add($"{s.Id} - {s.Name}");
            panelContent.Controls.Add(cmbStudent);

            // Course selection
            var lblCourse = new Label { Text = "Select Course:", Location = new Point(420, 70), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblCourse);
            
            var cmbCourse = new ComboBox
            {
                Location = new Point(530, 67),
                Size = new Size(250, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            var courses = _db.GetAllCourses();
            foreach (var c in courses) cmbCourse.Items.Add($"{c.Code} - {c.Name}");
            panelContent.Controls.Add(cmbCourse);

            // Grade entry fields
            int y = 120;
            var lblS1 = new Label { Text = "Assignment 1 (20%):", Location = new Point(30, y), ForeColor = Color.White, AutoSize = true };
            var txtS1 = new TextBox { Location = new Point(200, y), Size = new Size(80, 25), BackColor = Color.FromArgb(60, 60, 65), ForeColor = Color.White };
            panelContent.Controls.Add(lblS1);
            panelContent.Controls.Add(txtS1);

            var lblS2 = new Label { Text = "Assignment 2 (30%):", Location = new Point(300, y), ForeColor = Color.White, AutoSize = true };
            var txtS2 = new TextBox { Location = new Point(470, y), Size = new Size(80, 25), BackColor = Color.FromArgb(60, 60, 65), ForeColor = Color.White };
            panelContent.Controls.Add(lblS2);
            panelContent.Controls.Add(txtS2);

            y += 40;
            var lblCW = new Label { Text = "Course Work (20%):", Location = new Point(30, y), ForeColor = Color.White, AutoSize = true };
            var txtCW = new TextBox { Location = new Point(200, y), Size = new Size(80, 25), BackColor = Color.FromArgb(60, 60, 65), ForeColor = Color.White };
            panelContent.Controls.Add(lblCW);
            panelContent.Controls.Add(txtCW);

            var lblFinal = new Label { Text = "Final Exam (30%):", Location = new Point(300, y), ForeColor = Color.White, AutoSize = true };
            var txtFinal = new TextBox { Location = new Point(470, y), Size = new Size(80, 25), BackColor = Color.FromArgb(60, 60, 65), ForeColor = Color.White };
            panelContent.Controls.Add(lblFinal);
            panelContent.Controls.Add(txtFinal);

            y += 50;
            var btnSave = CreateActionButton("💾 Save Grade", 30, y, Color.FromArgb(0, 150, 100));
            btnSave.Size = new Size(150, 40);
            panelContent.Controls.Add(btnSave);

            // Grades DataGrid
            y += 60;
            var dgv = CreateDataGrid(y, 250);
            dgv.Columns.Add("Student", "Student");
            dgv.Columns.Add("Course", "Course");
            dgv.Columns.Add("S1", "S1");
            dgv.Columns.Add("S2", "S2");
            dgv.Columns.Add("Final", "Final");
            dgv.Columns.Add("Total", "Total");
            dgv.Columns.Add("Grade", "Grade");
            panelContent.Controls.Add(dgv);

            void LoadGrades()
            {
                dgv.Rows.Clear();
                var grades = _db.GetAllGradesForDisplay();
                foreach (var g in grades)
                    dgv.Rows.Add(g.StudentName, g.CourseName, g.S1, g.S2, g.Final, g.Total, g.Grade);
            }

            LoadGrades();

            btnSave.Click += (s, e) =>
            {
                if (cmbStudent.SelectedIndex < 0 || cmbCourse.SelectedIndex < 0)
                {
                    MessageBox.Show("Select student and course!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int studentId = int.Parse(cmbStudent.SelectedItem!.ToString()!.Split('-')[0].Trim());
                string courseCode = cmbCourse.SelectedItem!.ToString()!.Split('-')[0].Trim();
                double s1Val = double.TryParse(txtS1.Text, out double v1) ? v1 : 0;
                double s2Val = double.TryParse(txtS2.Text, out double v2) ? v2 : 0;
                double cwVal = double.TryParse(txtCW.Text, out double v3) ? v3 : 0;
                double finalVal = double.TryParse(txtFinal.Text, out double v4) ? v4 : 0;

                if (_db.SaveGrade(studentId, courseCode, s1Val, s2Val, cwVal, finalVal))
                {
                    MessageBox.Show("Grade saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrades();
                }
                else
                {
                    MessageBox.Show("Error saving grade!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }

        // ==================== ATTENDANCE ====================
        private void ShowAttendance()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("📋 Attendance Tracking"));

            // Student selection
            var lblStudent = new Label { Text = "Student:", Location = new Point(30, 70), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblStudent);
            
            var cmbStudent = new ComboBox
            {
                Location = new Point(100, 67),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            var students = _db.GetAllStudents();
            foreach (var s in students) cmbStudent.Items.Add($"{s.Id} - {s.Name}");
            panelContent.Controls.Add(cmbStudent);

            // Course selection
            var lblCourse = new Label { Text = "Course:", Location = new Point(320, 70), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblCourse);
            
            var cmbCourse = new ComboBox
            {
                Location = new Point(390, 67),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            var courses = _db.GetAllCourses();
            foreach (var c in courses) cmbCourse.Items.Add($"{c.Code} - {c.Name}");
            panelContent.Controls.Add(cmbCourse);

            // Date picker
            var lblDate = new Label { Text = "Date:", Location = new Point(610, 70), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblDate);
            var dtpDate = new DateTimePicker
            {
                Location = new Point(660, 67),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Short
            };
            panelContent.Controls.Add(dtpDate);

            // Status selection
            int y = 110;
            var lblStatus = new Label { Text = "Status:", Location = new Point(30, y), ForeColor = Color.White, AutoSize = true };
            panelContent.Controls.Add(lblStatus);
            
            var cmbStatus = new ComboBox
            {
                Location = new Point(100, y),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new[] { "Present", "Absent", "Late", "Excused" });
            cmbStatus.SelectedIndex = 0;
            panelContent.Controls.Add(cmbStatus);

            var btnMark = CreateActionButton("✓ Mark", 270, y - 3, Color.FromArgb(0, 150, 100));
            btnMark.Click += (s, e) =>
            {
                if (cmbStudent.SelectedIndex < 0 || cmbCourse.SelectedIndex < 0)
                {
                    MessageBox.Show("Select student and course!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int studentId = int.Parse(cmbStudent.SelectedItem!.ToString()!.Split('-')[0].Trim());
                string courseCode = cmbCourse.SelectedItem!.ToString()!.Split('-')[0].Trim();
                string status = cmbStatus.SelectedItem?.ToString() ?? "Present";
                string date = dtpDate.Value.ToString("yyyy-MM-dd");

                if (_db.MarkAttendance(studentId, courseCode, date, status))
                {
                    MessageBox.Show("Attendance marked!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            panelContent.Controls.Add(btnMark);

            // Attendance DataGrid
            y += 50;
            var dgv = CreateDataGrid(y, 350);
            dgv.Columns.Add("Student", "Student");
            dgv.Columns.Add("Course", "Course");
            dgv.Columns.Add("Date", "Date");
            dgv.Columns.Add("Status", "Status");
            panelContent.Controls.Add(dgv);
        }

        // ==================== NEWS ====================
        private void ShowNews()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("📢 Manage News"));

            var btnAdd = CreateActionButton("➕ Add", 30, 70, Color.FromArgb(0, 150, 100));
            panelContent.Controls.Add(btnAdd);

            var dgv = CreateDataGrid(120, 380);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Title", "Title");
            dgv.Columns.Add("Author", "Author");
            dgv.Columns.Add("Date", "Date");
            dgv.Columns.Add("Pinned", "Pinned");

            var news = _db.GetActiveNews();
            foreach (var n in news)
            {
                dgv.Rows.Add(n.Id, n.Title, n.Author, n.PublishDate, n.IsPinned ? "📌" : "");
            }

            panelContent.Controls.Add(dgv);
        }

        // ==================== ASSIGN COURSES TO INSTRUCTORS ====================
        private void ShowAssignCourses()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("📋 Assign Courses to Instructors"));

            int y = 70;
            
            // Course dropdown
            var lblCourse = new Label { Text = "Course:", Location = new Point(30, y), ForeColor = Color.White, Font = new Font("Segoe UI", 10), AutoSize = true };
            panelContent.Controls.Add(lblCourse);
            
            var cmbCourse = new ComboBox { Location = new Point(120, y - 3), Size = new Size(250, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            var courses = _db.GetAllCoursesSimple();
            foreach (var c in courses) cmbCourse.Items.Add($"{c.Id}|{c.Name}");
            if (cmbCourse.Items.Count > 0) cmbCourse.SelectedIndex = 0;
            panelContent.Controls.Add(cmbCourse);

            // Instructor dropdown
            var lblInstructor = new Label { Text = "Instructor:", Location = new Point(400, y), ForeColor = Color.White, Font = new Font("Segoe UI", 10), AutoSize = true };
            panelContent.Controls.Add(lblInstructor);
            
            var cmbInstructor = new ComboBox { Location = new Point(490, y - 3), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            var instructors = _db.GetAllInstructorsSimple();
            foreach (var i in instructors) cmbInstructor.Items.Add($"{i.Id}|{i.Name}");
            if (cmbInstructor.Items.Count > 0) cmbInstructor.SelectedIndex = 0;
            panelContent.Controls.Add(cmbInstructor);

            // Assign button
            var btnAssign = CreateActionButton("➕ Assign", 720, y - 5, Color.FromArgb(0, 150, 100));
            panelContent.Controls.Add(btnAssign);

            // Grid
            var dgv = CreateDataGrid(y + 50, 350);
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Course", "Course");
            dgv.Columns.Add("Code", "Code");
            dgv.Columns.Add("Instructor", "Instructor");
            dgv.Columns.Add("Year", "Academic Year");

            void LoadOfferings()
            {
                dgv.Rows.Clear();
                var offerings = _db.GetAllCourseOfferings();
                foreach (var o in offerings)
                    dgv.Rows.Add(o.OfferingId, o.CourseName, o.CourseCode, o.InstructorName, o.AcademicYear);
            }

            LoadOfferings();
            panelContent.Controls.Add(dgv);

            btnAssign.Click += (s, e) =>
            {
                if (cmbCourse.SelectedItem == null || cmbInstructor.SelectedItem == null) return;
                int courseId = int.Parse(cmbCourse.SelectedItem.ToString()!.Split('|')[0]);
                int instructorId = int.Parse(cmbInstructor.SelectedItem.ToString()!.Split('|')[0]);
                
                if (_db.AssignCourseToInstructor(courseId, instructorId, "2024-2025", 1))
                {
                    MessageBox.Show("✅ Course assigned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOfferings();
                }
                else
                {
                    MessageBox.Show("⚠️ Already assigned or error occurred.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Delete button
            var btnDelete = CreateActionButton("🗑️ Remove", 30, dgv.Bottom + 20, Color.FromArgb(180, 50, 50));
            btnDelete.Click += (s, e) =>
            {
                if (dgv.SelectedRows.Count == 0) return;
                int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
                if (MessageBox.Show("Remove this assignment?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _db.RemoveCourseOffering(id);
                    LoadOfferings();
                }
            };
            panelContent.Controls.Add(btnDelete);
        }

        // ==================== MERCY GRADING (نظام الرأفة) ====================
        private void ShowMercyGrades()
        {
            ClearContent();
            panelContent.Controls.Add(CreateTitle("🎁 Mercy Grading - نظام الرأفة (Pearson)"));

            int y = 70;

            // Settings
            var lblRange = new Label { Text = "Mercy Range %:", Location = new Point(30, y), ForeColor = Color.White, Font = new Font("Segoe UI", 10), AutoSize = true };
            panelContent.Controls.Add(lblRange);

            var txtRange = new TextBox { Text = "5", Location = new Point(150, y - 3), Size = new Size(50, 25) };
            panelContent.Controls.Add(txtRange);

            var lblPass = new Label { Text = "Pass % (P):", Location = new Point(220, y), ForeColor = Color.White, Font = new Font("Segoe UI", 10), AutoSize = true };
            panelContent.Controls.Add(lblPass);

            var txtPass = new TextBox { Text = "60", Location = new Point(310, y - 3), Size = new Size(50, 25) };
            panelContent.Controls.Add(txtPass);

            var btnSearch = CreateActionButton("🔍 Find Candidates", 400, y - 5, Color.FromArgb(0, 122, 204));
            btnSearch.Width = 150;
            panelContent.Controls.Add(btnSearch);

            // Grid
            var dgv = CreateDataGrid(y + 50, 320);
            dgv.Columns.Add("GradeID", "ID");
            dgv.Columns.Add("Student", "Student");
            dgv.Columns.Add("Course", "Course");
            dgv.Columns.Add("Current", "Current %");
            dgv.Columns.Add("Pass", "Pass %");
            dgv.Columns.Add("Needed", "% Needed");
            dgv.Columns.Add("Before", "Before");
            dgv.Columns.Add("After", "After Mercy");
            dgv.Columns.Add("Applied", "Applied");
            dgv.Columns["GradeID"].Visible = false;
            panelContent.Controls.Add(dgv);

            void LoadCandidates()
            {
                dgv.Rows.Clear();
                double range = double.TryParse(txtRange.Text, out double r) ? r : 5;
                double pass = double.TryParse(txtPass.Text, out double p) ? p : 60;
                var candidates = _db.GetMercyCandidates(range, pass);
                foreach (var c in candidates)
                {
                    // Calculate Pearson grade before and after
                    string beforeGrade = c.CurrentTotal >= 100 ? "D" : c.CurrentTotal >= 80 ? "M" : c.CurrentTotal >= 60 ? "P" : "F";
                    string afterGrade = pass >= 100 ? "D" : pass >= 80 ? "M" : pass >= 60 ? "P" : "F";
                    dgv.Rows.Add(c.GradeId, c.StudentName, c.CourseName, 
                        $"{c.CurrentTotal:F0}%", $"{c.PassingGrade:F0}%", 
                        $"{c.Difference:F1}%", beforeGrade, afterGrade,
                        c.Applied ? "✅" : "❌");
                }
            }

            btnSearch.Click += (s, e) => LoadCandidates();

            // Apply buttons
            var btnApplySelected = CreateActionButton("✅ Apply to Selected", 30, dgv.Bottom + 20, Color.FromArgb(0, 150, 100));
            btnApplySelected.Width = 160;
            btnApplySelected.Click += (s, e) =>
            {
                if (dgv.SelectedRows.Count == 0) return;
                int gradeId = Convert.ToInt32(dgv.SelectedRows[0].Cells["GradeID"].Value);
                string neededStr = dgv.SelectedRows[0].Cells["Needed"].Value?.ToString()?.Replace("%", "") ?? "0";
                double needed = double.Parse(neededStr);
                if (_db.ApplyMercyGrade(gradeId, needed))
                {
                    MessageBox.Show("✅ Mercy applied! Student now has P grade.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCandidates();
                }
            };
            panelContent.Controls.Add(btnApplySelected);

            var btnApplyAll = CreateActionButton("🎉 Apply to All", 210, dgv.Bottom + 20, Color.FromArgb(180, 100, 0));
            btnApplyAll.Width = 140;
            btnApplyAll.Click += (s, e) =>
            {
                double range = double.TryParse(txtRange.Text, out double r) ? r : 5;
                double pass = double.TryParse(txtPass.Text, out double p) ? p : 60;
                int count = _db.ApplyMercyToAll(range, pass);
                MessageBox.Show($"✅ Applied mercy to {count} students!\nThey now have P grade (Pass).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCandidates();
            };
            panelContent.Controls.Add(btnApplyAll);

            // Info label
            var lblInfo = new Label
            {
                Text = "💡 Pearson Mercy System:\n" +
                       "   • Pass grade (P) = 60%\n" +
                       "   • Students between 55-59% (if range=5) will be raised to 60%\n" +
                       "   • This converts them from F → P (Pass)",
                Location = new Point(30, dgv.Bottom + 70),
                ForeColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            panelContent.Controls.Add(lblInfo);
        }
    }
}
