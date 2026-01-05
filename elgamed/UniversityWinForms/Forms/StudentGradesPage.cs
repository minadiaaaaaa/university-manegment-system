using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class StudentGradesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private Student? _student;

        public StudentGradesPage()
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
            
            dgvGrades.Rows.Clear();
            var grades = _db.GetGradesByStudent(_student.Id);
            foreach (var g in grades)
            {
                dgvGrades.Rows.Add(g.CourseCode, g.CourseCode, g.S1Score, g.S2Score, g.FinalExamScore, g.TotalScore, g.LetterGrade);
            }
        }
    }
}

