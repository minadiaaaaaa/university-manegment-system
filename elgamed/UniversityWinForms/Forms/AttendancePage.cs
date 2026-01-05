using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class AttendancePage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private int _professorId;

        public AttendancePage()
        {
            InitializeComponent();
            cmbCourse.SelectedIndexChanged += (s, e) => LoadStudents();
            dtpDate.ValueChanged += (s, e) => LoadStudents();
            btnSave.Click += BtnSave_Click;
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

        private void LoadStudents()
        {
            dgvAttendance.Rows.Clear();
            if (cmbCourse.SelectedItem == null) return;
            string courseCode = cmbCourse.SelectedItem.ToString()!.Split('|')[0];
            var students = _db.GetStudentsInCourse(courseCode);
            foreach (var s in students)
            {
                int rowIndex = dgvAttendance.Rows.Add(s.Id, s.Name);
                dgvAttendance.Rows[rowIndex].Cells["colStatus"].Value = "Present";
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cmbCourse.SelectedItem == null) return;
            string courseCode = cmbCourse.SelectedItem.ToString()!.Split('|')[0];
            string date = dtpDate.Value.ToString("yyyy-MM-dd");

            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (row.Cells["colID"].Value == null) continue;
                int studentId = Convert.ToInt32(row.Cells["colID"].Value);
                string status = row.Cells["colStatus"].Value?.ToString() ?? "Present";
                _db.RecordAttendance(studentId, courseCode, date, status);
            }
            MessageBox.Show("Attendance saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
