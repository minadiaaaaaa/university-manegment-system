/*
 * نظام إدارة الجامعة - لوحة تحكم الأستاذ
 * تم عمله بواسطة: مينا ضياء  
 * التاريخ: ديسمبر 2024
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class ProfessorForm : Form
    {
        private readonly DatabaseHelper _db = new();
        private readonly Instructor _professor;

        public ProfessorForm(Instructor professor)
        {
            _professor = professor;
            InitializeComponent();
            WireUpEvents();
        }

        private void WireUpEvents()
        {
            // Update professor name label
            lblProfessorName.Text = $"{_professor.Title} {_professor.Name}";

            // Connect Designer buttons to event handlers
            btnCourses.Click += (s, e) => ShowCourses();
            btnStudents.Click += (s, e) => ShowStudents();
            btnAttendance.Click += (s, e) => ShowAttendance();
            btnGrades.Click += (s, e) => ShowGrades();
            btnLogout.Click += (s, e) => this.Close();
        }

        private void ProfessorForm_Load(object? sender, EventArgs e)
        {
            ShowCourses(); // Default view
        }

        private void ClearContent() => panelContent.Controls.Clear();

        private Label CreateTitle(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };
        }

        // ==================== MY COURSES ====================
        private void ShowCourses()
        {
            ClearContent();
            var coursesPage = new CoursesPage();
            coursesPage.Dock = DockStyle.Fill;
            coursesPage.SetProfessorId(_professor.Id);
            panelContent.Controls.Add(coursesPage);
        }

        // ==================== STUDENTS ====================
        private void ShowStudents()
        {
            ClearContent();
            var studentsPage = new StudentsPage();
            studentsPage.Dock = DockStyle.Fill;
            studentsPage.SetProfessorId(_professor.Id);
            panelContent.Controls.Add(studentsPage);
        }

        // ==================== ATTENDANCE ====================
        private void ShowAttendance()
        {
            ClearContent();
            var attendancePage = new AttendancePage();
            attendancePage.Dock = DockStyle.Fill;
            attendancePage.SetProfessorId(_professor.Id);
            panelContent.Controls.Add(attendancePage);
        }

        // ==================== GRADES ====================
        private void ShowGrades()
        {
            ClearContent();
            var gradesPage = new GradesPage();
            gradesPage.Dock = DockStyle.Fill;
            gradesPage.SetProfessorId(_professor.Id);
            panelContent.Controls.Add(gradesPage);
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            ShowCourses();
        }
    }
}

