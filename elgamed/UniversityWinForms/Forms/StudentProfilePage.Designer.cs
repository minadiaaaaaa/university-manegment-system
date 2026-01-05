namespace UniversitySystem.Forms
{
    partial class StudentProfilePage
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            picAvatar = new PictureBox();
            pnlInfo = new Panel();
            lblName = new Label();
            lblNameValue = new Label();
            lblEmail = new Label();
            lblEmailValue = new Label();
            lblId = new Label();
            lblIdValue = new Label();
            lblLevel = new Label();
            lblLevelValue = new Label();
            lblDept = new Label();
            lblDeptValue = new Label();
            lblTrack = new Label();
            lblTrackValue = new Label();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Text = "👤 My Profile";
            
            picAvatar.Size = new Size(100, 100);
            picAvatar.Location = new Point(20, 65);
            picAvatar.BackColor = Color.FromArgb(0, 122, 204);
            picAvatar.SizeMode = PictureBoxSizeMode.CenterImage;
            
            pnlInfo.Location = new Point(140, 65);
            pnlInfo.Size = new Size(680, 380);
            pnlInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            pnlInfo.BackColor = Color.FromArgb(45, 45, 50);
            pnlInfo.Padding = new Padding(20);
            
            int y = 20;
            int spacing = 55;
            
            AddFieldPair(lblName, lblNameValue, "Full Name:", "Loading...", y); y += spacing;
            AddFieldPair(lblEmail, lblEmailValue, "Email:", "Loading...", y); y += spacing;
            AddFieldPair(lblId, lblIdValue, "Student ID:", "Loading...", y); y += spacing;
            AddFieldPair(lblLevel, lblLevelValue, "Academic Year:", "Loading...", y); y += spacing;
            AddFieldPair(lblDept, lblDeptValue, "Department:", "Loading...", y); y += spacing;
            AddFieldPair(lblTrack, lblTrackValue, "Track:", "Loading...", y);
            
            pnlInfo.Controls.AddRange(new Control[] { 
                lblName, lblNameValue, lblEmail, lblEmailValue, 
                lblId, lblIdValue, lblLevel, lblLevelValue,
                lblDept, lblDeptValue, lblTrack, lblTrackValue
            });
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 35, 40);
            Controls.Add(pnlInfo);
            Controls.Add(picAvatar);
            Controls.Add(lblTitle);
            Name = "StudentProfilePage";
            Size = new Size(850, 480);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void AddFieldPair(Label title, Label value, string titleText, string valueText, int y)
        {
            title.AutoSize = true;
            title.ForeColor = Color.LightGray;
            title.Font = new Font("Segoe UI", 10F);
            title.Location = new Point(20, y);
            title.Text = titleText;
            
            value.AutoSize = true;
            value.ForeColor = Color.White;
            value.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            value.Location = new Point(20, y + 22);
            value.Text = valueText;
        }

        private Label lblTitle;
        private PictureBox picAvatar;
        private Panel pnlInfo;
        private Label lblName;
        private Label lblNameValue;
        private Label lblEmail;
        private Label lblEmailValue;
        private Label lblId;
        private Label lblIdValue;
        private Label lblLevel;
        private Label lblLevelValue;
        private Label lblDept;
        private Label lblDeptValue;
        private Label lblTrack;
        private Label lblTrackValue;
    }
}
