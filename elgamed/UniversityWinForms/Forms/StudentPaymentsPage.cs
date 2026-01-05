using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class StudentPaymentsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private Student? _student;

        public StudentPaymentsPage()
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
            
            // Load payment summary
            var summary = _db.GetPaymentSummary(_student.Id, _student.Level);
            lblTotalValue.Text = $"{summary.TotalFee:N0} EGP";
            lblPaidValue.Text = $"{summary.TotalPaid:N0} EGP";
            lblRemainingValue.Text = $"{summary.Remaining:N0} EGP";
            
            // Load payment history
            dgvPayments.Rows.Clear();
            var payments = _db.GetStudentPayments(_student.Id);
            foreach (var p in payments)
            {
                dgvPayments.Rows.Add(p.PaymentDate, $"{p.Amount:N0} EGP", p.PaymentType, p.Description);
            }
        }
    }
}

