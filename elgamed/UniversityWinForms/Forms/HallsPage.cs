using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class HallsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public HallsPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            dgvHalls.Rows.Clear();
            var halls = _db.GetAllClassrooms();
            foreach (var h in halls)
            {
                if (h.RoomType == "Hall")
                    dgvHalls.Rows.Add(h.Id, h.Code, h.Capacity, h.Building);
            }
        }
    }
}

