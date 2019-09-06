using System;
using System.Drawing;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        bool goup = false;
        bool godown = false;
        bool goleft = false;
        bool goright = false;

        Point rightEndPoint = new Point(872, 301);

        int ghostSpeed1 = 1;
        int ghostSpeed2 = 1;
        int ghostSpeed3 = 1;
        int countSpeedGhost = 0;

        int speed = 5;
        int score = 0;
        int winScore = 40;

        public Form1()
        {
            InitializeComponent();
            label2.Visible = false;
            timer1.Start();
        }
        //character control unit
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goleft = true;
                pacman.Image = Properties.Resources.Left;
            }

            if(e.KeyCode == Keys.Right)
            {
                goright = true;
                pacman.Image = Properties.Resources.Right;
            }

            if(e.KeyCode == Keys.Up)
            {
                goup = true;
                pacman.Image = Properties.Resources.Up;
            }

            if(e.KeyCode == Keys.Down)
            {
                godown = true;
                pacman.Image = Properties.Resources.down;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Score: " + score;

            //moving pacman block
            if (goleft)
            {
                pacman.Left -= speed;
                //moving pacman left
            }

            if(goright)
            {
                pacman.Left += speed;
                //moving pacman right
            }

            if(goup)
            {
                pacman.Top -= speed;
                // moving pacman up
            }

            if(godown)
            {
                pacman.Top += speed;
                // moving pacman down
            }
            //moving pacman block ends

            foreach(Control c in this.Controls)
            {
                //ghosts moving block
                countSpeedGhost++;
                if (countSpeedGhost % 10 == 1)
                {
                    if (redGhost1.Bounds.IntersectsWith(pictureBoxWall5.Bounds))
                        ghostSpeed1 = -ghostSpeed1;
                    else if (redGhost1.Bounds.IntersectsWith(pictureBoxWall2.Bounds))
                        ghostSpeed1 = -ghostSpeed1;

                    if (redGhost2.Bounds.IntersectsWith(pictureBoxWall5.Bounds))
                        ghostSpeed2 = -ghostSpeed2;
                    else if (redGhost2.Bounds.IntersectsWith(pictureBoxWall2.Bounds))
                        ghostSpeed2 = -ghostSpeed2;

                    if (redGhost3.Bounds.IntersectsWith(pictureBoxWall5.Bounds))
                        ghostSpeed3 = -ghostSpeed3;
                    else if (redGhost3.Bounds.IntersectsWith(pictureBox15.Bounds))
                        ghostSpeed3 = -ghostSpeed3;

                    redGhost1.Top += ghostSpeed1;
                    redGhost2.Top += ghostSpeed2;
                    redGhost3.Top += ghostSpeed3;

                   
                }
                //ghosts moving block ends

                if (c is PictureBox && c.Tag == "ghost")
                {
                    if (((PictureBox)c).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        label2.Text = "Game Over!";
                        label2.Visible = true;
                        timer1.Stop();

                        if(MessageBox.Show("If u wanna play again click retry, else cancel.","", buttons: MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                        {
                            pacman.Location = new Point(1, 250);
                            label2.Visible = false;
                            goup = false;
                            godown = false;
                            goleft = false;
                            goright = false;
                            timer1.Start();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }

                if (score == winScore)
                {
                    timer1.Stop();
                    MessageBox.Show("Winner!");
                    this.Close();
                }

                if (c is PictureBox && c.Tag == "wall")
                {
                    if(((PictureBox)c).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        //stop moving after intersects with wall and animation hit
                        speed = 0;
                       
                        if(goup)
                        {
                            pacman.Top += 5;
                        }
                        if(godown)
                        {
                            pacman.Top -= 5;
                        }
                        if(goright)
                        {
                            pacman.Left -= 5;
                        }
                        if(goleft)
                        {
                            pacman.Left += 5;
                        }
                    }
                    else
                    {
                        speed = 5;
                    }
                }

                if(c is PictureBox && c.Tag == "coin")
                {
                    if(((PictureBox)c).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        //score upper after take coin
                        this.Controls.Remove(c);
                        score++;
                    }
                }
                //right entrance
                if (c is PictureBox && c.Tag == "rightEndPoint")
                {
                    if(((PictureBox)c).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        pacman.Location = new Point(1, 250);
                    }
                }
                //left entrance
                if (pacman.Location.X < -60)
                {
                    pacman.Location = new Point(656, 250);
                }
            }
        }
    }
}
