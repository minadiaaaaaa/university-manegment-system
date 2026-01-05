using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class AttendanceAdminPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public AttendanceAdminPage()
        {
            InitializeComponent();
            cmbCourse.SelectedIndexChanged += (s, e) => LoadAttendance();
            LoadCourses();
        }

        private void LoadCourses()
        {
            cmbCourse.Items.Clear();
            var courses = _db.GetAllCourses();
            foreach (var c in courses)
            {
                cmbCourse.Items.Add($"{c.Code}|{c.Name}");
            }
            if (cmbCourse.Items.Count > 0) cmbCourse.SelectedIndex = 0;
        }

        private void LoadAttendance()
        {
            dgvAttendance.Rows.Clear();
            if (cmbCourse.SelectedItem == null) return;
            string courseCode = cmbCourse.SelectedItem.ToString()!.Split('|')[0];
            
            var records = _db.GetCourseAttendance(courseCode);
            foreach (var r in records)
            {
                dgvAttendance.Rows.Add(r.StudentId, r.Name, r.Date, r.Status);
            }
        }
    }
}

