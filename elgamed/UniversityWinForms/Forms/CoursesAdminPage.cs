using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class CoursesAdminPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public CoursesAdminPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            dgvCourses.Rows.Clear();
            var courses = _db.GetAllCourses();
            int id = 1;
            foreach (var c in courses)
            {
                dgvCourses.Rows.Add(id++, c.Code, c.Name, c.CreditHours, c.Level, c.Semester);
            }
        }
    }
}


