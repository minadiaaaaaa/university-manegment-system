using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class StudentsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private int _professorId;

        public StudentsPage()
        {
            InitializeComponent();
            cmbCourse.SelectedIndexChanged += CmbCourse_SelectedIndexChanged;
        }

        public void SetProfessorId(int professorId)
        {
            _professorId = professorId;
            LoadCourses();
        }

        private void LoadCourses()
        {
            cmbCourse.Items.Clear();
            var courses = _db.GetProfessorCourses(_professorId);
            foreach (var c in courses)
            {
                cmbCourse.Items.Add($"{c.Code}|{c.Name}");
            }
            if (cmbCourse.Items.Count > 0) cmbCourse.SelectedIndex = 0;
        }

        private void CmbCourse_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            dgvStudents.Rows.Clear();
            if (cmbCourse.SelectedItem == null) return;
            string courseCode = cmbCourse.SelectedItem.ToString()!.Split('|')[0];
            var students = _db.GetStudentsInCourse(courseCode);
            foreach (var s in students)
            {
                dgvStudents.Rows.Add(s.Id, s.Name, s.Email, s.SeatNumber);
            }
        }
    }
}
