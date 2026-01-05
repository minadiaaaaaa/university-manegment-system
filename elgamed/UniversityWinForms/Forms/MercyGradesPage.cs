using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class MercyGradesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public MercyGradesPage()
        {
            InitializeComponent();
            numPoints.ValueChanged += (s, e) => LoadCandidates();
            btnApply.Click += BtnApply_Click;
            LoadCandidates();
        }

        private void LoadCandidates()
        {
            dgvCandidates.Rows.Clear();
            double maxPoints = (double)numPoints.Value;
            
            var candidates = _db.GetMercyCandidates(maxPoints, 60);
            foreach (var c in candidates)
            {
                if (!c.Applied)
                {
                    dgvCandidates.Rows.Add(false, c.StudentId, c.StudentName, c.CurrentTotal.ToString("F1") + "%", c.Difference.ToString("F1"));
                }
            }
        }

        private void BtnApply_Click(object? sender, EventArgs e)
        {
            int count = 0;
            
            foreach (DataGridViewRow row in dgvCandidates.Rows)
            {
                bool selected = Convert.ToBoolean(row.Cells["colSelect"].Value ?? false);
                if (selected)
                {
                    // Need to get gradeId - for now we'll use ApplyMercyToAll
                    count++;
                }
            }
            
            if (count > 0)
            {
                double maxPoints = (double)numPoints.Value;
                int applied = _db.ApplyMercyToAll(maxPoints, 60);
                MessageBox.Show($"Mercy applied to {applied} students!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCandidates();
            }
            else
            {
                MessageBox.Show("Please select students first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

