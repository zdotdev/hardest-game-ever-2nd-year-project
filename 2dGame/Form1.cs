using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gameTimer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        bool left, right, up, down;
        int coins = 6;
        int speed = 8;
        int death = 0;

        private void mainTimerEvent(object sender, EventArgs e)
        {
            switch (true)
            {
                case bool leftMove when left && player.Left > 41:
                    player.Left -= speed;
                    break;
                case bool rightMove when right && player.Left < 549:
                    player.Left += speed;
                    break;
                case bool upMove when up && player.Top > 87:
                    player.Top -= speed;
                    break;
                case bool downMove when down && player.Top < 544:
                    player.Top += speed;
                    break;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "wall")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        int distanceToLeft = player.Left - x.Left;
                        int distanceToRight = x.Right - player.Right;
                        int distanceToTop = player.Top - x.Top;
                        int distanceToBottom = x.Bottom - player.Bottom;

                        int closestSide = Math.Min(Math.Min(distanceToLeft, distanceToRight), Math.Min(distanceToTop, distanceToBottom));

                        if (closestSide == distanceToLeft)
                        {
                            player.Left = x.Left - player.Width;
                        }
                        else if (closestSide == distanceToRight)
                        {
                            player.Left = x.Right;
                        }
                        else if (closestSide == distanceToTop)
                        {
                            player.Top = x.Top - player.Height;
                        }
                        else
                        {
                            player.Top = x.Bottom;
                        }
                    }
                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        coins--;
                    }
                }
            }
            foreach (Control o in this.Controls)
            {
                if (o is PictureBox && (string)o.Tag == "obstacle")
                {
                    if (player.Bounds.IntersectsWith(o.Bounds))
                    {
                        player.Top = 113;
                        player.Left = 62;
                        death++;
                        coins = 6;
                        deathCount.Text = "Death: " + death;

                        foreach (Control c in this.Controls)
                        {
                            if (c is PictureBox && (string)c.Tag == "coin")
                            {
                                if (!c.Visible)
                                {
                                    c.Visible = true;
                                }
                            }
                        }

                    }
                }
            }
            if (coins == 0)
            {
                foreach (Control n in this.Controls)
                {
                    if((n is PictureBox && (string)n.Tag == "end"))
                    {
                        if (player.Bounds.IntersectsWith(n.Bounds))
                        {
                            Application.Exit();
                        }
                    }
                }
            }
        }
        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    left = true;
                    break;
                case Keys.D:
                case Keys.Right:
                    right = true;
                    break;
                case Keys.W:
                case Keys.Up:
                    up = true;
                    break;
                case Keys.S:
                case Keys.Down:
                    down = true;
                    break;
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    left = false;
                    break;
                case Keys.D:
                case Keys.Right:
                    right = false;
                    break;
                case Keys.W:
                case Keys.Up:
                    up = false;
                    break;
                case Keys.S:
                case Keys.Down:
                    down = false;
                    break;
            }
        }

        private void gameClose(object sender, FormClosedEventArgs e)
        {
            // Add code here kung maraming forms na 
            // Application.Exit();;
        }
    }
}
