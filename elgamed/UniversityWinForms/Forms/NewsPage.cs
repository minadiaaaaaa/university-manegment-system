using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;

namespace UniversitySystem.Forms
{
    public partial class NewsPage : UserControl
    {
        private readonly DatabaseHelper _db = new();

        public NewsPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            dgvNews.Rows.Clear();
            var news = _db.GetActiveNews();
            foreach (var n in news)
            {
                dgvNews.Rows.Add(n.Id, n.Title, n.Content, n.PublishDate);
            }
        }
    }
}


