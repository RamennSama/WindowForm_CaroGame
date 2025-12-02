using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowFormLastVersion
{
    public partial class NameInputDialog : Form
    {
        public string PlayerXName { get; private set; } = "Player X";
        public string PlayerOName { get; private set; } = "Player O";
        public int MaxGames { get; private set; } = 5;

        
        private TextBox _txtX;
        private TextBox _txtO;
        private NumericUpDown _numGames;
        private Button _btnOK;
        private Button _btnCancel;

        public NameInputDialog()
        {
            InitializeComponent();
        }

        
        private void InitializeComponent()
        {
            
            this.Text = "Enter Player Names and Game Settings";
            this.ClientSize = new Size(440, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            
            var lblTitle = new Label
            {
                Text = "Player Settings",
                Font = new Font("Times New Roman", 20, FontStyle.Bold),
                Location = new Point(0, 40),
                Size = new Size(440, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = SystemColors.ControlLight,
                ForeColor = Color.Black
            };

            
            var lblX = new Label
            {
                Text = "Player X",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 100),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DarkBlue
            };

            _txtX = new TextBox
            {
                Text = "Player X",
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 100),
                Size = new Size(220, 30),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            _txtX.Enter += (s, e) => { _txtX.BackColor = Color.White; _txtX.BorderStyle = BorderStyle.FixedSingle; };
            _txtX.Leave += (s, e) => { _txtX.BorderStyle = BorderStyle.FixedSingle; };

           
            var lblO = new Label
            {
                Text = "Player O",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 140),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DarkBlue
            };

            _txtO = new TextBox
            {
                Text = "Player O",
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 140),
                Size = new Size(220, 30),
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            _txtO.Enter += (s, e) => { _txtO.BackColor = Color.White; _txtO.BorderStyle = BorderStyle.FixedSingle; };
            _txtO.Leave += (s, e) => { _txtO.BorderStyle = BorderStyle.FixedSingle; };

            
            var lblGames = new Label
            {
                Text = "Number of",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 180),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DarkBlue
            };

            _numGames = new NumericUpDown
            {
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 180),
                Size = new Size(220, 30),
                Minimum = 1,
                Maximum = 10,
                Value = 5,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Nút OK
            _btnOK = new Button
            {
                Text = "OK",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(90, 230),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            _btnOK.FlatAppearance.BorderSize = 2;
            _btnOK.FlatAppearance.BorderColor = Color.DarkBlue;
            _btnOK.Click += OnBtnOKClick;

            // Nút Cancel
            _btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(230, 230),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            _btnCancel.FlatAppearance.BorderSize = 2;
            _btnCancel.FlatAppearance.BorderColor = Color.DarkBlue;

            // Thêm các control vào form
            this.Controls.AddRange(new Control[] { lblTitle, lblX, _txtX, lblO, _txtO, lblGames, _numGames, _btnOK, _btnCancel });
            this.AcceptButton = _btnOK;
            this.CancelButton = _btnCancel;
        }

        private void OnBtnOKClick(object sender, EventArgs e)
        {
            
            PlayerXName = string.IsNullOrWhiteSpace(_txtX.Text) ? "Player X" : _txtX.Text.Trim();
            PlayerOName = string.IsNullOrWhiteSpace(_txtO.Text) ? "Player O" : _txtO.Text.Trim();
            MaxGames = (int)_numGames.Value;
        }
    }
}