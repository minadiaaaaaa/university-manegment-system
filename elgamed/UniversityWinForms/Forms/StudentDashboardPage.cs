using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class StudentDashboardPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private Student? _student;

        public StudentDashboardPage()
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
            
            lblWelcome.Text = $"👋 Welcome, {_student.Name}!";
            lblIdValue.Text = _student.SeatNumber.ToString();
            lblLevelValue.Text = $"Year {_student.Level}";
            lblDeptValue.Text = _student.Department;
            
            // Calculate GPA from grades
            var grades = _db.GetGradesByStudent(_student.Id);
            double totalPoints = 0;
            int count = 0;
            foreach (var g in grades)
            {
                if (g.TotalScore > 0)
                {
                    double gradePoint = GetGradePoint(g.LetterGrade);
                    totalPoints += gradePoint;
                    count++;
                }
            }
            double gpa = count > 0 ? totalPoints / count : 0;
            lblGPAValue.Text = gpa.ToString("F2");
        }

        private double GetGradePoint(string letterGrade)
        {
            return letterGrade switch
            {
                "A+" => 4.0,
                "A" => 4.0,
                "A-" => 3.7,
                "B+" => 3.3,
                "B" => 3.0,
                "B-" => 2.7,
                "C+" => 2.3,
                "C" => 2.0,
                "C-" => 1.7,
                "D+" => 1.3,
                "D" => 1.0,
                _ => 0.0
            };
        }
    }
}

