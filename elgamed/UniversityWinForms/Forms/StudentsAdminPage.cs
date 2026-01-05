using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class StudentsAdminPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        public event EventHandler? AddStudentRequested;
        public event EventHandler<int>? DeleteStudentRequested;

        public StudentsAdminPage()
        {
            InitializeComponent();
            btnAdd.Click += (s, e) => AddStudentRequested?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += BtnDelete_Click;
            LoadData();
        }

        public void LoadData()
        {
            dgvStudents.Rows.Clear();
            var students = _db.GetAllStudentsWithFaculty();
            foreach (var s in students)
            {
                dgvStudents.Rows.Add(s.Id, s.SeatNumber, s.Name, s.Email, s.Level, s.Department);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["colID"].Value);
            if (MessageBox.Show("Are you sure you want to delete this student?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _db.DeleteStudent(studentId);
                LoadData();
            }
        }
    }
}
