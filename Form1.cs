using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace brakewall
{
    public partial class Form1 : Form
    {
        bool goLeft;
        bool goRight;
        bool isGameover;

        PictureBox[] blockArray;

        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            setupGame();
        }
        private void setupGame()
        {
            timer1.Enabled = true;
            label1.Visible = false;
            label2.Visible = false;
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            lblScore.Text = "Score : " + score;
            pboxball.Left = 200;
            pboxball.Top = 200;
            pictureBox1.Left = 350;
            PlaceBlocks();

            // 컨트롤 컴포넌트 들을 전부 불러오고, 그게 블럭이면 랜덤값을 이용해 다양한 색깔을 입힘.
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                }
            }

        }
        private void removeBlocks()
        {
            foreach(PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }
        }
        private void PlaceBlocks()
        {
            blockArray = new PictureBox[12];
            int a = 0;
            int top = 50;
            int left = 20;

            for (int i = 0; i < blockArray.Length; i++) 
            {
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 32;
                blockArray[i].Width = 100;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;
                if(a==6)
                {
                    top = top + 50;
                    left = 20;
                    a = 0;
                }
                if (a < 6)
                {
                    a++;
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]);
                    left = left + 130;
                }
            }
        }
        private void GameOver(string masseage)
        {
            isGameover= true;
            timer1.Stop();
            lblScore.Text = "Score : " + score;
            label1.Visible= true; 
            label2.Visible= true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblScore.Text = "Score : " + score;
            if (goLeft == true && pictureBox1.Left > 0)
            {
                pictureBox1.Left -= playerSpeed;
            }
            if (goRight == true && pictureBox1.Right < 790)
            {
                pictureBox1.Left += playerSpeed;
            }
            pboxball.Left += ballx;
            pboxball.Top += bally;
            if (pboxball.Left < 0 || pboxball.Left > 770)
            {
                ballx = -ballx;
            }
            if (pboxball.Top < 0)
            {
                bally = -bally;
            }
            if (pboxball.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                bally = rand.Next(5, 12) * -1;
                if (ballx < 0)
                {
                    ballx = rand.Next(5, 12) - 1;
                }
                else
                {
                    ballx = rand.Next(5, 12);
                }
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if(pboxball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;
                        this.Controls.Remove(x);
                    }
                }
            }
            if (score >= 12)
            {
                label1.Text = "You Win!!";
                label2.Text = "enter : new game\r\nq : exit game";
                GameOver("You Win!");
            }
            if (pboxball.Top > 390)
            {
                label1.Text = "    GameOver";
                label2.Text = "enter : new game\r\nq : exit game";
                GameOver("Lose :: ball out");
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;

            }
            else if(e.KeyCode ==Keys.Right)
            {
                goRight = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGameover == true)
            {
                removeBlocks();
                setupGame();
            }
            if (e.KeyCode == Keys.Q && isGameover == true)
            {
                Application.Exit();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
