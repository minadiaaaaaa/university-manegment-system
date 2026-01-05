using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class GradesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private int _professorId;

        public GradesPage()
        {
            InitializeComponent();
            cmbCourse.SelectedIndexChanged += (s, e) => LoadStudentGrades();
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

        private void LoadStudentGrades()
        {
            dgvGrades.Rows.Clear();
            if (cmbCourse.SelectedItem == null) return;
            string courseCode = cmbCourse.SelectedItem.ToString()!.Split('|')[0];
            
            var students = _db.GetStudentsInCourse(courseCode);
            foreach (var s in students)
            {
                double total = s.S1 + s.S2 + s.FinalExam;
                dgvGrades.Rows.Add(s.Id, s.Name, s.S1, s.S2, s.FinalExam, total);
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cmbCourse.SelectedItem == null) return;
            string courseCode = cmbCourse.SelectedItem.ToString()!.Split('|')[0];

            foreach (DataGridViewRow row in dgvGrades.Rows)
            {
                if (row.Cells["colID"].Value == null) continue;
                int studentId = Convert.ToInt32(row.Cells["colID"].Value);
                double s1 = Convert.ToDouble(row.Cells["colS1"].Value ?? 0);
                double s2 = Convert.ToDouble(row.Cells["colS2"].Value ?? 0);
                double final = Convert.ToDouble(row.Cells["colFinal"].Value ?? 0);
                
                _db.UpdateStudentGrade(studentId, courseCode, "s1", s1);
                _db.UpdateStudentGrade(studentId, courseCode, "s2", s2);
                _db.UpdateStudentGrade(studentId, courseCode, "final_exam", final);
            }
            MessageBox.Show("Grades saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadStudentGrades();
        }
    }
}
