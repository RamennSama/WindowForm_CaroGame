using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace WindowFormLastVersion
{
    public partial class CaroGameForm : Form
    {
        private bool _isXTurn = true; // X luôn đi trước
        private bool _vsComputer;     // Chơi với máy
        private GameDifficulty _diff; // Độ khó

        private const int BoardSize = 15;  // Kích thước bàn cờ
        private const int CellSize = 45;   // Kích thước ô
        private const int Margin = 30;     // Lề
        private int _maxGames = 5;         // Số ván tối đa

        private Button[,] _board;      // Bàn cờ
        private string _xName = "Player X"; // Tên người chơi X
        private string _oName = "Player O"; // Tên người chơi O
        private int _xScore = 0;       // Điểm X
        private int _oScore = 0;       // Điểm O
        private int _gamesCount = 0;   // Số ván đã chơi
        private Label _scoreLbl;       // Label hiển thị điểm

        public enum GameDifficulty
        {
            Easy,   
            Medium, 
            Hard   
        }

       
        public CaroGameForm(bool playWithComputer = false, GameDifficulty difficulty = GameDifficulty.Easy, int maxGames = 5)
        {
            _vsComputer = playWithComputer;
            _diff = difficulty;
            _maxGames = maxGames;

           
            if (!playWithComputer)
            {
                using var nameDialog = new NameInputDialog();
                if (nameDialog.ShowDialog() == DialogResult.OK)
                {
                    _xName = nameDialog.PlayerXName;
                    _oName = nameDialog.PlayerOName;
                    _maxGames = nameDialog.MaxGames;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }
            }

            InitComponents(); 
            SetupGame();     
            this.FormClosing += OnFormClosing; 
        }

       
        private void InitComponents()
        {
            Text = _vsComputer ? $"Caro Game - Computer ({_diff})" : $"Caro Game - {_xName} vs {_oName}";
            ClientSize = new Size(BoardSize * CellSize + 2 * Margin, BoardSize * CellSize + 2 * Margin + 100);
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

           
            _scoreLbl = new Label
            {
                Text = GetScoreText(),
                Location = new Point(Margin, BoardSize * CellSize + Margin + 10),
                Size = new Size(ClientSize.Width - 2 * Margin, 40),
                Font = new Font(Font.FontFamily, 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(_scoreLbl);
        }

        
        private string GetScoreText()
        {
            return _vsComputer
                ? $"{_xName}: {_xScore} - Computer: {_oScore} (Game {_gamesCount + 1}/{_maxGames})"
                : $"{_xName}: {_xScore} - {_oName}: {_oScore} (Game {_gamesCount + 1}/{_maxGames})";
        }

       
        private void SetupGame()
        {
            _board = new Button[BoardSize, BoardSize];

           
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    var btn = new Button
                    {
                        Location = new Point(Margin + j * CellSize, Margin + i * CellSize),
                        Size = new Size(CellSize, CellSize),
                        BackColor = SystemColors.ButtonFace,
                        Font = new Font(Font.FontFamily, 16, FontStyle.Bold),
                        Tag = new Point(i, j), // Lưu vị trí
                        UseVisualStyleBackColor = true
                    };

                    btn.Click += OnCellClick; // Gán sự kiện click
                    Controls.Add(btn);
                    _board[i, j] = btn;
                }
            }

           
            _scoreLbl.BackColor = SystemColors.ActiveCaption;
            _scoreLbl.ForeColor = Color.Black;
            GraphicsPath path = new GraphicsPath();
            int radius = 10;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(_scoreLbl.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(_scoreLbl.Width - radius, _scoreLbl.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, _scoreLbl.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            _scoreLbl.Region = new Region(path);
        }
        private void OnCellClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (!string.IsNullOrEmpty(btn.Text)) return; 

            
            btn.Text = _isXTurn ? "X" : "O";
            btn.ForeColor = _isXTurn ? Color.Blue : Color.Red;

            
            if (CheckWin((Point)btn.Tag))
            {
                string winner = _isXTurn ? _xName : _oName;

                using (var winDialog = new WinnerDialog(winner))
                {
                    winDialog.ShowDialog(this);
                }

                ProcessWinResult(); 
                return;
            }

            _isXTurn = !_isXTurn; 

            
            if (_vsComputer && !_isXTurn)
            {
                ComputerMove();
            }
        }

      
        private void ProcessWinResult()
        {
            if (!_vsComputer)
            {
                
                if (_isXTurn)
                    _xScore++;
                else
                    _oScore++;

                _gamesCount++;

                if (_gamesCount >= _maxGames)
                {
                    ShowFinalResult();
                    return;
                }
                else
                {
                    ResetBoard();
                    _scoreLbl.Text = GetScoreText();
                    return;
                }
            }
            else
            {
               
                _xScore++;
                _gamesCount++;

                if (_gamesCount >= _maxGames)
                {
                    ShowFinalResult();
                    return;
                }
                else
                {
                    ResetBoard();
                    _scoreLbl.Text = GetScoreText();
                    return;
                }
            }
        }
        private void ShowFinalResult()
        {
            string finalWinner, scoreInfo;

            if (!_vsComputer)
            {
                finalWinner = _xScore > _oScore ? _xName : _oScore > _xScore ? _oName : "Draw";
                scoreInfo = $"{_xName}: {_xScore} - {_oName}: {_oScore}";
            }
            else
            {
                finalWinner = _xScore > _oScore ? _xName : _oScore > _xScore ? "Computer" : "Draw";
                scoreInfo = $"{_xName}: {_xScore} - Computer: {_oScore}";
            }

            using (var finalDialog = new WinnerDialog(finalWinner, true, scoreInfo))
            {
                finalDialog.ShowDialog(this);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
        private void ComputerMove()
        {
            Point move = GetCompMove();
            var btn = _board[move.X, move.Y];
            btn.Text = "O";
            btn.ForeColor = Color.Red;

            
            if (CheckWin(move))
            {
                using (var winDialog = new WinnerDialog("Computer"))
                {
                    winDialog.ShowDialog(this);
                }

                _oScore++;
                _gamesCount++;

                if (_gamesCount >= _maxGames)
                {
                    ShowFinalResult();
                    return;
                }
                else
                {
                    ResetBoard();
                    _scoreLbl.Text = GetScoreText();
                    return;
                }
            }

            _isXTurn = true; 
        }

        private Point GetCompMove()
        {
            switch (_diff)
            {
                case GameDifficulty.Easy:
                    return GetEasyMove();   
                case GameDifficulty.Medium:
                    return GetMediumMove(); 
                case GameDifficulty.Hard:
                    return GetHardMove();  
                default:
                    return GetEasyMove();
            }
        }

        private Point GetEasyMove()
        {
           
            var winMove = FindWinningMove("O");
            if (winMove.HasValue) return winMove.Value;

            
            var blockMove = FindWinningMove("X");
            if (blockMove.HasValue) return blockMove.Value;

            var moves = new List<MoveScore>();

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (string.IsNullOrEmpty(_board[i, j].Text))
                    {
                        var score = EvalMove(i, j);
                        moves.Add(new MoveScore { Move = new Point(i, j), Score = score });
                    }
                }
            }

            if (moves.Any())
            {
                var topMoves = moves
                    .OrderByDescending(m => m.Score)
                    .Take(8)
                    .ToList();

                
                if (Random.Shared.Next(0, 10) < 7)
                {
                    return topMoves[Random.Shared.Next(topMoves.Count)].Move;
                }
                // 30% chọn nước tốt nhất
                return topMoves[0].Move;
            }

            return GetRandomMove();
        }

        private Point GetRandomMove()
        {
            var empty = new List<Point>();
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (string.IsNullOrEmpty(_board[i, j].Text))
                    {
                        empty.Add(new Point(i, j));
                    }
                }
            }
            int idx = Random.Shared.Next(empty.Count);
            return empty[idx];
        }

       
        private Point GetHardMove()
        {
           
            var winMove = FindWinningMove("O");
            if (winMove.HasValue) return winMove.Value;

            
            var blockMove = FindWinningMove("X");
            if (blockMove.HasValue) return blockMove.Value;

            var moves = new List<MoveScore>();

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (string.IsNullOrEmpty(_board[i, j].Text))
                    {
                        var score = EvalMove(i, j); 
                        moves.Add(new MoveScore { Move = new Point(i, j), Score = score });
                    }
                }
            }

            if (moves.Any())
            {
                var max = moves.Max(m => m.Score);
                var best = moves.Where(m => m.Score == max).ToList();
                return best[Random.Shared.Next(best.Count)].Move;
            }

            return GetRandomMove();
        }

        private Point GetMediumMove()
        {
           
            var winMove = FindWinningMove("O");
            if (winMove.HasValue) return winMove.Value;

            
            var blockMove = FindWinningMove("X");
            if (blockMove.HasValue) return blockMove.Value;

            var moves = new List<MoveScore>();

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (string.IsNullOrEmpty(_board[i, j].Text))
                    {
                        var score = EvalMove(i, j);
                        moves.Add(new MoveScore { Move = new Point(i, j), Score = score });
                    }
                }
            }

            if (moves.Any())
            {
                var top = moves
                    .OrderByDescending(m => m.Score)
                    .Take(3)
                    .ToList();

               
                if (top.Count > 1 && Random.Shared.Next(0, 100) < 15)
                {
                    return top[Random.Shared.Next(1, top.Count)].Move;
                }
                return top[0].Move;
            }

            return GetRandomMove();
        }

        
        private Point? FindWinningMove(string symbol)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (string.IsNullOrEmpty(_board[i, j].Text))
                    {
                        _board[i, j].Text = symbol;
                        bool wins = CheckWin(new Point(i, j));
                        _board[i, j].Text = "";

                        if (wins)
                            return new Point(i, j);
                    }
                }
            }
            return null;
        }

       
        private bool IsBlocking(int x, int y)
        {
            _board[x, y].Text = "X"; 

            foreach (var dir in new[] {
                (dx: 1, dy: 0),  // Dọc 
                (dx: 0, dy: 1),  // Ngang
                (dx: 1, dy: 1),  // Chéo xuống
                (dx: 1, dy: -1)  // Chéo lên
            })
            {
                int cnt = CountDir(x, y, dir.dx, dir.dy, "X") +
                    CountDir(x, y, -dir.dx, -dir.dy, "X") - 1;

                if (cnt >= 3) 
                {
                    _board[x, y].Text = "";
                    return true;
                }
            }

            _board[x, y].Text = "";
            return false;
        }

       
        private int EvalMove(int x, int y)
        {
            int score = 0;

          
            foreach (var sym in new[] { "O", "X" })
            {
                bool isComp = sym == "O";
                int mult = isComp ? 2 : 3; 

                _board[x, y].Text = sym; 

                foreach (var dir in new[] {
                    (dx: 1, dy: 0),  // Dọc
                    (dx: 0, dy: 1),  // Ngang
                    (dx: 1, dy: 1),  // Chéo xuống
                    (dx: 1, dy: -1)  // Chéo lên
                })
                {
                    int cnt = CountDir(x, y, dir.dx, dir.dy, sym) +
                        CountDir(x, y, -dir.dx, -dir.dy, sym) - 1;

                    score += mult * GetSeqScore(cnt); 

                    int ends = CountEnds(x, y, dir.dx, dir.dy, sym);
                    score += mult * ends * 10; 

                    // Điểm đặc biệt
                    if (cnt >= 4) score += mult * 5000; 
                    if (cnt == 3 && ends == 2) score += mult * 2000; 
                    if (cnt == 3 && ends == 1) score += mult * 500; 
                }

                _board[x, y].Text = ""; 
            }

            score += EvalPos(x, y);

            return score;
        }

        // tính điểm theo độ dài chuỗi
        private int GetSeqScore(int cnt)
        {
            return cnt switch
            {
                5 => 100000, // Thắng - tăng mạnh
                4 => 10000,  // 4 con - rất nguy hiểm
                3 => 1000,   // 3 con - quan trọng
                2 => 100,    // 2 con
                _ => 10      // 1 con
            };
        }

        
        private int CountEnds(int x, int y, int dx, int dy, string sym)
        {
            int ends = 0;

            // Kiểm tra đầu này
            int nextX = x + dx * (CountDir(x, y, dx, dy, sym));
            int nextY = y + dy * (CountDir(x, y, dx, dy, sym));
            if (IsValid(nextX, nextY) && string.IsNullOrEmpty(_board[nextX, nextY].Text))
                ends++;

            // Kiểm tra đầu kia
            nextX = x - dx * (CountDir(x, y, -dx, -dy, sym));
            nextY = y - dy * (CountDir(x, y, -dx, -dy, sym));
            if (IsValid(nextX, nextY) && string.IsNullOrEmpty(_board[nextX, nextY].Text))
                ends++;

            return ends;
        }


        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
        }


        private int EvalPos(int x, int y)
        {
            int centerX = BoardSize / 2;
            int centerY = BoardSize / 2;
            int dist = Math.Abs(x - centerX) + Math.Abs(y - centerY);
            int score = (BoardSize - dist) * 2;

            int nearby = CountNearby(x, y); // Đếm quân xung quanh
            score += nearby * 5;

            return score;
        }

       
        private int CountNearby(int x, int y)
        {
            int cnt = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int newX = x + i;
                    int newY = y + j;
                    if (IsValid(newX, newY) && !string.IsNullOrEmpty(_board[newX, newY].Text))
                    {
                        cnt++;
                    }
                }
            }
            return cnt;
        }


        private void ResetBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _board[i, j].Text = "";
                    _board[i, j].ForeColor = Color.Black;
                }
            }
            _isXTurn = true; // X đi trước
            _scoreLbl.Text = GetScoreText();
        }

        private bool CheckWin(Point move)
        {
            string sym = _board[move.X, move.Y].Text;
            foreach (var dir in new[] {
                (dx: 1, dy: 0),  // Dọc
                (dx: 0, dy: 1),  // Ngang
                (dx: 1, dy: 1),  // Chéo xuống
                (dx: 1, dy: -1)  // Chéo lên
            })
            {
               
                if (CountDir(move.X, move.Y, dir.dx, dir.dy, sym) +
                    CountDir(move.X, move.Y, -dir.dx, -dir.dy, sym) - 1 >= 5)
                {
                    return true;
                }
            }
            return false;
        }

       
        private int CountDir(int x, int y, int dx, int dy, string sym)
        {
            int cnt = 0;
            while (IsValid(x, y) && _board[x, y].Text == sym)
            {
                cnt++;
                x += dx;
                y += dy;
            }
            return cnt;
        }
       
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

       
        private class MoveScore
        {
            public Point Move { get; set; }
            public int Score { get; set; }
        }
    }
}