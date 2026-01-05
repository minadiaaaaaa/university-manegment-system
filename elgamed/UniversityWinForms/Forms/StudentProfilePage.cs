using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class StudentProfilePage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private Student? _student;

        public StudentProfilePage()
        {
            InitializeComponent();
        }

        public void SetStudent(Student student)
        {
            _student = student;
            LoadData();
        }

        private void LoadData()
        {
            if (_student == null) return;
            
            lblNameValue.Text = _student.Name;
            lblEmailValue.Text = _student.Email;
            lblIdValue.Text = _student.SeatNumber.ToString();
            lblLevelValue.Text = $"Year {_student.Level}";
            lblDeptValue.Text = _student.Department;
            lblTrackValue.Text = _student.Track;
        }
    }
}

