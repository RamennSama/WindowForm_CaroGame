using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowFormLastVersion
{
    public partial class Form1 : Form
    {
        private bool _closingFromButton = false; 

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;

            
            SetupRoundedLabel(label1, 20);   
            SetupRoundedLabel(label2, 15);
        }

       
        private void SetupRoundedLabel(Label label, int radius)
        {
            label.Paint += (sender, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                   
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(label.Width - radius - 1, 0, radius, radius, 270, 90);
                    path.AddArc(label.Width - radius - 1, label.Height - radius - 1, radius, radius, 0, 90);
                    path.AddArc(0, label.Height - radius - 1, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    label.Region = new Region(path); 

                   
                    using (SolidBrush brush = new SolidBrush(label.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                   
                    using (SolidBrush brush = new SolidBrush(label.ForeColor))
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;      
                        sf.LineAlignment = StringAlignment.Center;  
                        e.Graphics.DrawString(label.Text, label.Font, brush, label.ClientRectangle, sf);
                    }
                }
            };

            label.Padding = new Padding(radius / 2);
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Caro Game"; 
        }

       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            if (_closingFromButton)
            {
                if (MessageBox.Show("Are you sure you want to exit the application?", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true; 
                    _closingFromButton = false; 
                }
            }
           
        }

       
        private void button3_Click_1(object sender, EventArgs e)
        {
            _closingFromButton = true;
            this.Close(); 
        }

       
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            using var gameForm = new CaroGameForm(playWithComputer: false);
            if (gameForm.DialogResult != DialogResult.Cancel)
            {
                gameForm.ShowDialog(); 
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
           
            using var difficultyForm = new DifficultySelectForm();
            if (difficultyForm.ShowDialog() == DialogResult.OK)
            {
                
                var gameForm = new CaroGameForm(
                    playWithComputer: true,
                    difficulty: difficultyForm.SelectedDifficulty,
                    maxGames: difficultyForm.MaxGames);
                gameForm.ShowDialog();
            }
        }
    }
}