using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowFormLastVersion
{
    public partial class DifficultySelectForm : Form
    {
        
        public CaroGameForm.GameDifficulty SelectedDifficulty { get; private set; }
        public int MaxGames { get; private set; } = 5;

        
        private NumericUpDown _numGames;
        private Button _btnOK;
        private Button _btnCancel;
        private Button _selectedBtn = null;
        private bool _isCompMode;

        public DifficultySelectForm(bool computerMode = true)
        {
            _isCompMode = computerMode;
            InitializeComponent();
            SetupForm();
        }

        
        private void InitializeComponent()
        {
            if (_isCompMode)
            {
                Text = "Select Difficulty and Game Settings";
                ClientSize = new Size(440, 350);
            }
            else
            {
                Text = "Enter Player Names and Game Settings";
                ClientSize = new Size(440, 280);
            }

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
        }

        private void SetupForm()
        {
            if (_isCompMode)
            {
                SetupCompModeForm();    // Chế độ chơi với máy
            }
            else
            {
                SetupPlayerModeForm();  // Chế độ 2 người chơi
            }
        }
        
        private void SetupCompModeForm()
        {
            
            var lblTitle = new Label
            {
                Text = "Select Difficulty",
                Font = new Font("Times New Roman", 20, FontStyle.Bold),
                Location = new Point(0, 15),
                Size = new Size(440, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            
            var btnEasy = new Button
            {
                Text = "Easy",
                Font = new Font("Times New Roman", 14),
                Location = new Point(120, 70),
                Size = new Size(200, 35),
                BackColor = SystemColors.ButtonFace,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                Tag = CaroGameForm.GameDifficulty.Easy
            };
            btnEasy.FlatAppearance.BorderSize = 1;
            btnEasy.FlatAppearance.BorderColor = Color.Gray;
            btnEasy.Click += OnDifficultyBtnClick;

            
            var btnMedium = new Button
            {
                Text = "Medium",
                Font = new Font("Times New Roman", 14),
                Location = new Point(120, 115),
                Size = new Size(200, 35),
                BackColor = SystemColors.ButtonFace,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                Tag = CaroGameForm.GameDifficulty.Medium
            };
            btnMedium.FlatAppearance.BorderSize = 1;
            btnMedium.FlatAppearance.BorderColor = Color.Gray;
            btnMedium.Click += OnDifficultyBtnClick;

            
            var btnHard = new Button
            {
                Text = "Hard",
                Font = new Font("Times New Roman", 14),
                Location = new Point(120, 160),
                Size = new Size(200, 35),
                BackColor = SystemColors.ButtonFace,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false,
                Tag = CaroGameForm.GameDifficulty.Hard
            };
            btnHard.FlatAppearance.BorderSize = 1;
            btnHard.FlatAppearance.BorderColor = Color.Gray;
            btnHard.Click += OnDifficultyBtnClick;

            
            var lblGames = new Label
            {
                Text = "Number of",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 210),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White
            };

            _numGames = new NumericUpDown
            {
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 210),
                Size = new Size(220, 30),
                Minimum = 1,
                Maximum = 10,
                Value = 5
            };

            // Nút OK
            _btnOK = new Button
            {
                Text = "OK",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(90, 260),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            _btnOK.FlatAppearance.BorderSize = 2;
            _btnOK.FlatAppearance.BorderColor = Color.SteelBlue;
            _btnOK.Click += OnBtnOKClick;

            // Nút Cancel
            _btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(230, 260),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            _btnCancel.FlatAppearance.BorderSize = 2;
            _btnCancel.FlatAppearance.BorderColor = Color.SteelBlue;

            // Thêm các control vào form
            Controls.Add(lblTitle);
            Controls.Add(btnEasy);
            Controls.Add(btnMedium);
            Controls.Add(btnHard);
            Controls.Add(lblGames);
            Controls.Add(_numGames);
            Controls.Add(_btnOK);
            Controls.Add(_btnCancel);

            this.AcceptButton = _btnOK;
            this.CancelButton = _btnCancel;
        }

        private void SetupPlayerModeForm()
        {
            var lblTitle = new Label
            {
                Text = "Player Settings",
                Font = new Font("Times New Roman", 20, FontStyle.Bold),
                Location = new Point(0, 15),
                Size = new Size(440, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                ForeColor = Color.Black
            };

           
            var lblX = new Label
            {
                Text = "Player X",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 70),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White
            };

            var txtX = new TextBox
            {
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 70),
                Size = new Size(220, 30),
                Text = "Player X"
            };

            // Label và TextBox cho Player O
            var lblO = new Label
            {
                Text = "Player O",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 110),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White
            };

            var txtO = new TextBox
            {
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 110),
                Size = new Size(220, 30),
                Text = "Player O"
            };

            
            var lblGames = new Label
            {
                Text = "Number of",
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Location = new Point(40, 150),
                Size = new Size(120, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.White
            };

            _numGames = new NumericUpDown
            {
                Font = new Font("Times New Roman", 14),
                Location = new Point(170, 150),
                Size = new Size(220, 30),
                Minimum = 1,
                Maximum = 10,
                Value = 5
            };

            // Nút OK
            _btnOK = new Button
            {
                Text = "OK",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(90, 200),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            _btnOK.FlatAppearance.BorderSize = 2;
            _btnOK.FlatAppearance.BorderColor = Color.SteelBlue;
            _btnOK.Click += OnBtnOKClick;

            // Nút Cancel
            _btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Times New Roman", 14),
                Size = new Size(120, 40),
                Location = new Point(230, 200),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            _btnCancel.FlatAppearance.BorderSize = 2;
            _btnCancel.FlatAppearance.BorderColor = Color.SteelBlue;

            // Thêm các control vào form
            Controls.Add(lblTitle);
            Controls.Add(lblX);
            Controls.Add(txtX);
            Controls.Add(lblO);
            Controls.Add(txtO);
            Controls.Add(lblGames);
            Controls.Add(_numGames);
            Controls.Add(_btnOK);
            Controls.Add(_btnCancel);

            this.AcceptButton = _btnOK;
            this.CancelButton = _btnCancel;
        }

        
        private void OnDifficultyBtnClick(object sender, EventArgs e)
        {
            var clickedBtn = sender as Button;

            // Reset tất cả nút về trạng thái mặc định
            foreach (Control control in Controls)
            {
                if (control is Button btn && btn.Tag != null)
                {
                    btn.BackColor = SystemColors.ButtonFace;
                    btn.ForeColor = Color.Black;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.Gray;
                }
            }

          
            clickedBtn.BackColor = Color.SteelBlue;
            clickedBtn.ForeColor = Color.White;
            clickedBtn.FlatAppearance.BorderSize = 2;
            clickedBtn.FlatAppearance.BorderColor = Color.DarkBlue;

            _selectedBtn = clickedBtn;
            SelectedDifficulty = (CaroGameForm.GameDifficulty)clickedBtn.Tag;
        }

       
        private void OnBtnOKClick(object sender, EventArgs e)
        {
            if (_isCompMode)
            {
                
                if (_selectedBtn == null)
                {
                    MessageBox.Show("Please select a difficulty level.", "Selection Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }

            MaxGames = (int)_numGames.Value;
        }
    }
}