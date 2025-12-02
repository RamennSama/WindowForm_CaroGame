using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowFormLastVersion
{
    public partial class WinnerDialog : Form
    {
       
        public WinnerDialog(string winnerName, bool finalWinner = false, string scoreInfo = "")
        {
            InitializeComponent(winnerName, finalWinner, scoreInfo);
        }

       
        private void InitializeComponent(string winnerName, bool finalWinner, string scoreInfo)
        {
           
            this.Text = finalWinner ? "Game Over!" : "Winner!";
            this.ClientSize = new System.Drawing.Size(400, finalWinner ? 280 : 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            
            var lblTitle = new Label
            {
                Text = finalWinner ? " CHAMPION! " : " WINNER! ",
                Font = new Font("Times New Roman", 24, FontStyle.Bold),
                Location = new Point(0, 30),
                Size = new Size(400, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = SystemColors.ControlLight,
                ForeColor = Color.Black
            };

            
            var lblWinner = new Label
            {
                Text = winnerName,
                Font = new Font("Times New Roman", 20, FontStyle.Bold),
                Location = new Point(20, 100),
                Size = new Size(360, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkBlue
            };

            
            Label lblScore = null;
            if (!string.IsNullOrEmpty(scoreInfo))
            {
                lblScore = new Label
                {
                    Text = scoreInfo,
                    Font = new Font("Times New Roman", 14, FontStyle.Regular),
                    Location = new Point(20, 150),
                    Size = new Size(360, 30),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black
                };
            }

            
            var btnOK = new Button
            {
                Text = "OK",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(140, finalWinner ? 220 : 160),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnOK.FlatAppearance.BorderSize = 2;
            btnOK.FlatAppearance.BorderColor = Color.DarkBlue;

            
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblWinner);
            if (lblScore != null)
                this.Controls.Add(lblScore);
            this.Controls.Add(btnOK);

            
            this.AcceptButton = btnOK;
        }
    }
}