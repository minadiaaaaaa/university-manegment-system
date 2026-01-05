namespace UniversitySystem.Forms
{
    partial class StudentDashboardPage
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            lblSubtitle = new Label();
            pnlCards = new FlowLayoutPanel();
            cardId = new Panel();
            lblIdTitle = new Label();
            lblIdValue = new Label();
            cardLevel = new Panel();
            lblLevelTitle = new Label();
            lblLevelValue = new Label();
            cardDept = new Panel();
            lblDeptTitle = new Label();
            lblDeptValue = new Label();
            cardGPA = new Panel();
            lblGPATitle = new Label();
            lblGPAValue = new Label();
            SuspendLayout();
            
            // lblWelcome
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Text = "👋 Welcome!";
            
            // lblSubtitle
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = Color.LightGray;
            lblSubtitle.Location = new Point(20, 65);
            lblSubtitle.Text = "Here's your academic overview";
            
            // pnlCards
            pnlCards.Location = new Point(20, 100);
            pnlCards.Size = new Size(800, 350);
            pnlCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            pnlCards.FlowDirection = FlowDirection.LeftToRight;
            pnlCards.WrapContents = true;
            pnlCards.BackColor = Color.Transparent;
            
            // Create cards
            SetupCard(cardId, lblIdTitle, lblIdValue, "🎓 Student ID", "Loading...", Color.FromArgb(0, 122, 204));
            SetupCard(cardLevel, lblLevelTitle, lblLevelValue, "📚 Level", "Year 1", Color.FromArgb(60, 179, 113));
            SetupCard(cardDept, lblDeptTitle, lblDeptValue, "🏛️ Department", "Loading...", Color.FromArgb(255, 140, 0));
            SetupCard(cardGPA, lblGPATitle, lblGPAValue, "📊 GPA", "0.0", Color.FromArgb(147, 112, 219));
            
            pnlCards.Controls.AddRange(new Control[] { cardId, cardLevel, cardDept, cardGPA });
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(pnlCards);
            Controls.Add(lblSubtitle);
            Controls.Add(lblWelcome);
            Name = "StudentDashboardPage";
            Size = new Size(850, 500);
            ResumeLayout(false);
            PerformLayout();
        }

        private void SetupCard(Panel card, Label title, Label value, string titleText, string valueText, Color color)
        {
            card.Size = new Size(180, 120);
            card.BackColor = color;
            card.Margin = new Padding(10);
            card.Padding = new Padding(15);
            
            title.Text = titleText;
            title.AutoSize = true;
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 10F);
            title.Location = new Point(15, 15);
            
            value.Text = valueText;
            value.AutoSize = true;
            value.ForeColor = Color.White;
            value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            value.Location = new Point(15, 60);
            
            card.Controls.Add(title);
            card.Controls.Add(value);
        }

        private Label lblWelcome;
        private Label lblSubtitle;
        private FlowLayoutPanel pnlCards;
        private Panel cardId;
        private Label lblIdTitle;
        private Label lblIdValue;
        private Panel cardLevel;
        private Label lblLevelTitle;
        private Label lblLevelValue;
        private Panel cardDept;
        private Label lblDeptTitle;
        private Label lblDeptValue;
        private Panel cardGPA;
        private Label lblGPATitle;
        private Label lblGPAValue;
    }
}
