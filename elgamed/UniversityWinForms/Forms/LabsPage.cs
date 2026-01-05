using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class LabsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public LabsPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            dgvLabs.Rows.Clear();
            var labs = _db.GetLabsOnly();
            foreach (var l in labs)
            {
                dgvLabs.Rows.Add(l.Id, l.Code, l.Capacity, l.Building);
            }
        }
    }
}

