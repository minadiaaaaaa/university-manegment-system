using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class StudentCoursesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private Student? _student;

        public StudentCoursesPage()
        {
            InitializeComponent();
            btnRegister.Click += BtnRegister_Click;
        }

        public void SetStudent(Student student)
        {
            _student = student;
            LoadData();
        }

        private void LoadData()
        {
            if (_student == null) return;
            
            // Load registered courses
            dgvRegistered.Rows.Clear();
            var registered = _db.GetRegisteredCourses(_student.Id);
            foreach (var c in registered)
            {
                dgvRegistered.Rows.Add(c.Code, c.Name, c.CreditHours, "Registered");
            }
            
            // Load available courses
            dgvAvailable.Rows.Clear();
            var available = _db.GetCoursesByLevelAndTerm(_student.Level, 1, _student.Track);
            foreach (var c in available)
            {
                if (!_db.IsRegisteredForCourse(_student.Id, c.Code))
                {
                    dgvAvailable.Rows.Add(c.Code, c.Name, c.CreditHours);
                }
            }
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            if (_student == null || dgvAvailable.SelectedRows.Count == 0) return;
            
            string courseCode = dgvAvailable.SelectedRows[0].Cells["colCode2"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(courseCode)) return;
            
            bool success = _db.RegisterForCourse(_student.Id, courseCode);
            if (success)
            {
                MessageBox.Show("Registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Registration failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
