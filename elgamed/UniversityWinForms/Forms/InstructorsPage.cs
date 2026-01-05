using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class InstructorsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public InstructorsPage()
        {
            InitializeComponent();
            btnDelete.Click += BtnDelete_Click;
            LoadData();
        }

        public void LoadData()
        {
            dgvInstructors.Rows.Clear();
            var instructors = _db.GetAllInstructors();
            foreach (var i in instructors)
            {
                dgvInstructors.Rows.Add(i.Id, i.Name, i.Email, i.Title, i.Specialization);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvInstructors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an instructor to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = Convert.ToInt32(dgvInstructors.SelectedRows[0].Cells["colID"].Value);
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _db.DeleteInstructor(id);
                LoadData();
            }
        }
    }
}

