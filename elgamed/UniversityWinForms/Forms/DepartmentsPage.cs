using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class DepartmentsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public DepartmentsPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            dgvDepartments.Rows.Clear();
            var departments = _db.GetAllDepartments();
            foreach (var d in departments)
            {
                dgvDepartments.Rows.Add(d.Id, d.Name, d.FacultyName);
            }
        }
    }
}
