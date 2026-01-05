using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class FacultiesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public FacultiesPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            dgvFaculties.Rows.Clear();
            var faculties = _db.GetAllFaculties();
            foreach (var f in faculties)
            {
                dgvFaculties.Rows.Add(f.Id, f.Name);
            }
        }
    }
}
