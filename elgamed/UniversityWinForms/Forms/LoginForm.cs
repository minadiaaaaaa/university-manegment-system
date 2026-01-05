
using System;
using System.Drawing;
using System.Windows.Forms;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Forms
{
    public partial class LoginForm : Form
    {
        // اتصال بقاعدة البيانات
        private readonly DatabaseHelper _db = new();

        // Constructor - بيتنفذ لما الفورم يفتح
        public LoginForm()
        {
            InitializeComponent();
            LoadLogo(); // تحميل اللوجو
        }

        // دالة لتحميل صورة اللوجو
        private void LoadLogo()
        {
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "assets", "logo.jpg");
            if (File.Exists(logoPath))
            {
                picLogo.Image = Image.FromFile(logoPath);
            }
        }

        private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblStatus.Text = "Please enter email and password!";
                lblStatus.ForeColor = Color.OrangeRed;
                return;
            }

            // Try Student login
            var student = _db.AuthenticateStudent(email, password);
            if (student != null)
            {
                lblStatus.Text = "Login successful!";
                lblStatus.ForeColor = Color.LightGreen;

                this.Hide();
                var mainForm = new MainForm(student);
                mainForm.FormClosed += (s, args) => Application.Exit();
                mainForm.Show();
                return;
            }

            // Try Admin login
            if (_db.AuthenticateAdmin(email, password))
            {
                lblStatus.Text = "Admin login successful!";
                lblStatus.ForeColor = Color.LightGreen;

                this.Hide();
                var adminForm = new AdminPanelForm();
                adminForm.FormClosed += (s, args) => Application.Exit();
                adminForm.Show();
                return;
            }

            // Try Professor login (with password)
            var professor = _db.AuthenticateProfessor(email, password);
            if (professor != null)
            {
                lblStatus.Text = "Professor login successful!";
                lblStatus.ForeColor = Color.LightGreen;

                this.Hide();
                var profForm = new ProfessorForm(professor);
                profForm.FormClosed += (s, args) => Application.Exit();
                profForm.Show();
                return;
            }

            lblStatus.Text = "Invalid email or password!";
            lblStatus.ForeColor = Color.OrangeRed;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
