/*
 * نظام إدارة الجامعة - صفحة المواد
 * UserControl قابل للتصميم في Designer
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class CoursesPage : UserControl
    {
        private readonly DatabaseHelper _db = new();
        private int _professorId;

        public CoursesPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// تعيين ID الدكتور لجلب الكورسات الخاصة به
        /// </summary>
        public void SetProfessorId(int professorId)
        {
            _professorId = professorId;
            LoadCourses();
        }

        private void CoursesPage_Load(object sender, EventArgs e)
        {
            // سيتم تحميل الكورسات عند استدعاء SetProfessorId
        }

        private void LoadCourses()
        {
            dgvCourses.Rows.Clear();
            var courses = _db.GetProfessorCourses(_professorId);
            foreach (var c in courses)
            {
                dgvCourses.Rows.Add(c.Code, c.Name, c.CreditHours, c.Level, c.Semester);
            }
        }

        /// <summary>
        /// تحديث البيانات
        /// </summary>
        public void RefreshData()
        {
            LoadCourses();
        }
    }
}
