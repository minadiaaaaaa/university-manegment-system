using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class AssignCoursesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public AssignCoursesPage()
        {
            InitializeComponent();
            btnAssign.Click += BtnAssign_Click;
            LoadData();
        }

        private void LoadData()
        {
            // Load instructors
            cmbInstructor.Items.Clear();
            var instructors = _db.GetAllInstructors();
            foreach (var i in instructors)
            {
                cmbInstructor.Items.Add($"{i.Id}|{i.Name}");
            }
            if (cmbInstructor.Items.Count > 0) cmbInstructor.SelectedIndex = 0;
            
            // Load courses (use simple list with course_id)
            cmbCourse.Items.Clear();
            var courses = _db.GetAllCoursesSimple();
            foreach (var c in courses)
            {
                cmbCourse.Items.Add($"{c.Id}|{c.Name}");
            }
            if (cmbCourse.Items.Count > 0) cmbCourse.SelectedIndex = 0;
            
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            dgvAssignments.Rows.Clear();
            var offerings = _db.GetAllCourseOfferings();
            foreach (var o in offerings)
            {
                dgvAssignments.Rows.Add(o.OfferingId, o.InstructorName, o.CourseName);
            }
        }

        private void BtnAssign_Click(object? sender, EventArgs e)
        {
            if (cmbInstructor.SelectedItem == null || cmbCourse.SelectedItem == null) return;
            int instructorId = int.Parse(cmbInstructor.SelectedItem.ToString()!.Split('|')[0]);
            int courseId = int.Parse(cmbCourse.SelectedItem.ToString()!.Split('|')[0]);
            
            _db.AssignCourseToInstructor(instructorId, courseId, "2024-2025", 1);
            MessageBox.Show("Course assigned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAssignments();
        }
    }
}


