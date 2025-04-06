using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public partial class Form2 : Form
    {
        private DateTime lastShootTime = DateTime.MinValue;
        bool canShoot = true;
        int shootCooldown = 200;

        bool goLeft, goRight;
        int playerSpeed = 12;
        int enemySpeed = 5;
        int score = 0;
        int enemyBulletTimer = 300;
        PictureBox[] sadInvadersArray;
        bool shooting;
        bool isGameOver;

        // Элементы управления
        private Button btnLeft, btnRight, btnShoot;
        private Panel deathPanel;
        private Button btnRestart, btnExit;

        public Form2()
        {
            InitializeComponent();
            playerSpeed = Properties.Settings.Default.PlayerSpeed;
            this.Size = new Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.KeyDown += new KeyEventHandler(Form2_KeyDown);
            this.FormClosed += new FormClosedEventHandler(Form2_FormClosed);

            CreateControlButtons();
            CreateDeathScreen();
            gameSetup();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        private void CreateControlButtons()
        {
            // Кнопка влево
            btnLeft = new Button
            {
                Text = "←",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Size = new Size(120, 120), // Новый размер кнопки
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60)) // Новый радиус скругления
            };
            btnLeft.Location = new Point(this.ClientSize.Width - 360, this.ClientSize.Height - 140); // Скорректированное положение
            btnLeft.FlatAppearance.BorderSize = 0;
            btnLeft.MouseDown += (s, e) => goLeft = true;
            btnLeft.MouseUp += (s, e) => goLeft = false;
            btnLeft.MouseEnter += (s, e) => btnLeft.BackColor = Color.LightSlateGray;
            btnLeft.MouseLeave += (s, e) => btnLeft.BackColor = Color.DarkSlateGray;

            // Кнопка стрельбы


            btnShoot = new Button
            {
                Text = "⚡",
                Font = new Font("Arial", 32, FontStyle.Bold), // Увеличен размер шрифта
                Size = new Size(120, 120), // Новый размер кнопки
                BackColor = Color.Gold,
                ForeColor = Color.DarkRed,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60)) // Новый радиус скругления
            };
            btnShoot.Location = new Point(this.ClientSize.Width - 240, this.ClientSize.Height - 140); // Скорректированное положение
            btnShoot.FlatAppearance.BorderSize = 0;
            btnShoot.Click += (s, e) =>
            {
                // Проверяем, прошло ли достаточно времени с последнего выстрела
                if ((DateTime.Now - lastShootTime).TotalMilliseconds >= 900)
                {
                    // Обновляем время последнего выстрела
                    lastShootTime = DateTime.Now;
                    // Выполняем выстрел
                    if (!shooting) makeBullet("bullet");
                }
            }; ;
            btnShoot.MouseEnter += (s, e) => btnShoot.BackColor = Color.Goldenrod;
            btnShoot.MouseLeave += (s, e) => btnShoot.BackColor = Color.Gold;

            // Кнопка вправо
            btnRight = new Button
            {
                Text = "→",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Size = new Size(120, 120), // Новый размер кнопки
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60)) // Новый радиус скругления
            };
            btnRight.Location = new Point(this.ClientSize.Width - 120, this.ClientSize.Height - 140); // Скорректированное положение
            btnRight.FlatAppearance.BorderSize = 0;
            btnRight.MouseDown += (s, e) => goRight = true;
            btnRight.MouseUp += (s, e) => goRight = false;
            btnRight.MouseEnter += (s, e) => btnRight.BackColor = Color.LightSlateGray;
            btnRight.MouseLeave += (s, e) => btnRight.BackColor = Color.DarkSlateGray;

            this.Controls.Add(btnLeft);
            this.Controls.Add(btnShoot);
            this.Controls.Add(btnRight);
        }

        private void CreateDeathScreen()
        {
            deathPanel = new Panel
            {
                Size = new Size(400, 200),
                BackColor = Color.FromArgb(150, 0, 0, 0),
                Visible = false
            };
            deathPanel.Location = new Point(
                (this.ClientSize.Width - deathPanel.Width) / 2,
                (this.ClientSize.Height - deathPanel.Height) / 2
            );

            btnRestart = new Button
            {
                Text = "Заново",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 150, 50, 25, 25))
            };
            btnRestart.Location = new Point(50, 50);
            btnRestart.Click += (s, e) =>
            {
                removeAll();
                gameSetup();
                deathPanel.Visible = false;
            };

            btnExit = new Button
            {
                Text = "Выйти",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 150, 50, 25, 25))
            };
            btnExit.Location = new Point(200, 50);
            btnExit.Click += (s, e) => this.Close();

            deathPanel.Controls.Add(btnRestart);
            deathPanel.Controls.Add(btnExit);
            this.Controls.Add(deathPanel);
            deathPanel.BringToFront();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gameSetup()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            isGameOver = false;
            enemyBulletTimer = 300;
            enemySpeed = 10;
            shooting = false;
            deathPanel.Visible = false;
            btnLeft.Visible = true;
            btnRight.Visible = true;
            btnShoot.Visible = true;
            makeInvaders();
            gameTimer.Start();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Space && shooting == false)
            {
                shooting = true;
                makeBullet("bullet");
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeAll();
                gameSetup();
            }
        }

        private void makeInvaders()
        {
            Image[] enemyImages = new Image[]
            {
                Properties.Resources.Enemy1_64,
                Properties.Resources.Enemy2_64,
            };

            Random random = new Random();
            sadInvadersArray = new PictureBox[50];
            int left = 0;

            for (int i = 0; i < sadInvadersArray.Length; i++)
            {
                sadInvadersArray[i] = new PictureBox();
                sadInvadersArray[i].Size = new Size(150, 150);
                int randomIndex = random.Next(enemyImages.Length);
                sadInvadersArray[i].Image = enemyImages[randomIndex];
                sadInvadersArray[i].Top = 5;
                sadInvadersArray[i].Tag = "sadInvaders";
                sadInvadersArray[i].Left = left;
                sadInvadersArray[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(sadInvadersArray[i]);
                left = left - 180;
            }
        }



        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
            deathPanel.Visible = true;
            btnLeft.Visible = false;
            btnRight.Visible = false;
            btnShoot.Visible = false;
            goLeft = false;
            goRight = false;
            player.Left = this.ClientSize.Width / 2 - player.Width / 2;
            player.Top = this.ClientSize.Height - player.Height - 10; // Например, на 10 пикселей выше нижней границы экрана

        }

        private void removeAll()
        {
            foreach (PictureBox i in sadInvadersArray)
            {
                this.Controls.Remove(i);
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "bullet" || (string)x.Tag == "sadBullet")
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }

        private void makeBullet(string bulletTag)
        {
            Image[] enemyBulletImages = new Image[]
            {
                Properties.Resources.EnemyBullet14na38,
                Properties.Resources.Enemy2Bullet14na38,
            };

            Random random = new Random();
            PictureBox bullet = new PictureBox();

            if (bulletTag == "bullet")
            {
                bullet.Image = Properties.Resources.HERO1_BULLET;
            }
            else if (bulletTag == "sadBullet")
            {
                int randomIndex = random.Next(enemyBulletImages.Length);
                bullet.Image = enemyBulletImages[randomIndex];
            }

            bullet.Size = new Size(10, 40);
            bullet.Tag = bulletTag;
            bullet.Left = player.Left + player.Width / 2;

            if ((string)bullet.Tag == "bullet")
            {
                bullet.Top = player.Top - 20;
            }
            else if ((string)bullet.Tag == "sadBullet")
            {
                bullet.Top = -100;
            }

            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            if (goLeft)
            {
                player.Left -= playerSpeed;
            }
            if (goRight)
            {
                player.Left += playerSpeed;
            }
            enemyBulletTimer -= 10;
            if (enemyBulletTimer < 1)
            {
                enemyBulletTimer = 300;
                makeBullet("sadBullet");
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "sadInvaders")
                {
                    x.Left += enemySpeed;

                    if (x.Left > this.ClientSize.Width)
                    {
                        x.Top += 120;
                        x.Left = -x.Width;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameOver("Тебя взяли в плен. Не расстраивайся! Попробуй еще раз");
                    }

                    foreach (Control y in this.Controls)
                    {
                        if (y is PictureBox && (string)y.Tag == "bullet")
                        {
                            if (y.Bounds.IntersectsWith(x.Bounds))
                            {
                                this.Controls.Remove(x);
                                this.Controls.Remove(y);
                                score += 1;
                                shooting = false;
                            }
                        }
                    }
                }

                if (x is PictureBox && (string)x.Tag == "bullet")
                {
                    x.Top -= 20;
                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "sadBullet")
                {
                    x.Top += 20;
                    if (x.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(x);
                    }
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(x);
                        gameOver("Ты был убит вражеской пулей, попробуй еще разок)");
                    }
                }
            }

            if (score > 15)
            {
                enemySpeed = 15;
            }

            if (score == sadInvadersArray.Length)
            {
                gameOver("Воу, ты смог победить!!!");
            }
        }
    }
}