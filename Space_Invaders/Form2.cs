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
        int currentLevel = 1;
        int enemiesPerLevel = 20;
        int enemiesDestroyed = 0;
        Random random = new Random();

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
                Size = new Size(120, 120),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60))
            };
            btnLeft.Location = new Point(this.ClientSize.Width - 360, this.ClientSize.Height - 140);
            btnLeft.FlatAppearance.BorderSize = 0;
            btnLeft.MouseDown += (s, e) => goLeft = true;
            btnLeft.MouseUp += (s, e) => goLeft = false;
            btnLeft.MouseEnter += (s, e) => btnLeft.BackColor = Color.LightSlateGray;
            btnLeft.MouseLeave += (s, e) => btnLeft.BackColor = Color.DarkSlateGray;

            // Кнопка стрельбы
            btnShoot = new Button
            {
                Text = "⚡",
                Font = new Font("Arial", 32, FontStyle.Bold),
                Size = new Size(120, 120),
                BackColor = Color.Gold,
                ForeColor = Color.DarkRed,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60))
            };
            btnShoot.Location = new Point(this.ClientSize.Width - 240, this.ClientSize.Height - 140);
            btnShoot.FlatAppearance.BorderSize = 0;
            btnShoot.Click += (s, e) =>
            {
                if ((DateTime.Now - lastShootTime).TotalMilliseconds >= 900 && !shooting)
                {
                    lastShootTime = DateTime.Now;
                    shooting = true;
                    makeBullet("bullet");
                }
            };
            btnShoot.MouseEnter += (s, e) => btnShoot.BackColor = Color.Goldenrod;
            btnShoot.MouseLeave += (s, e) => btnShoot.BackColor = Color.Gold;

            // Кнопка вправо
            btnRight = new Button
            {
                Text = "→",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Size = new Size(120, 120),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60))
            };
            btnRight.Location = new Point(this.ClientSize.Width - 120, this.ClientSize.Height - 140);
            btnRight.FlatAppearance.BorderSize = 0;
            btnRight.MouseDown += (s, e) => goRight = true;
            btnRight.MouseUp += (s, e) => goRight = false;
            btnRight.MouseEnter += (s, e) => btnRight.BackColor = Color.LightSlateGray;
            btnRight.MouseLeave += (s, e) => btnRight.BackColor = Color.DarkSlateGray;

            this.Controls.Add(btnLeft);
            this.Controls.Add(btnShoot);
            this.Controls.Add(btnRight);

            CreatePlayer();
        }
        private void CreatePlayer()
        {
            player.Size = new Size(150, 130); // Установите размер игрока
            player.Image = Properties.Resources.HERO1_128; // Замените на изображение вашего героя
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.Location = new Point(this.ClientSize.Width / 2 - player.Width / 2,
                                        this.ClientSize.Height - player.Height - 10);
            this.Controls.Add(player);
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
            txtScore.Text = $"Score: {score} | Level: {currentLevel}";
            isGameOver = false;
            enemyBulletTimer = 300;
            enemySpeed = 5 + currentLevel;
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

            sadInvadersArray = new PictureBox[enemiesPerLevel];
            int left = 100;
            int top = 50;
            int enemiesInRow = 10;
            int horizontalSpacing = 150;
            int verticalSpacing = 120;

            for (int i = 0; i < sadInvadersArray.Length; i++)
            {
                sadInvadersArray[i] = new PictureBox();
                sadInvadersArray[i].Size = new Size(100, 100);

                if (currentLevel % 2 == 0)
                {
                    sadInvadersArray[i].Image = Properties.Resources.Enemy2_64;
                }
                else
                {
                    sadInvadersArray[i].Image = Properties.Resources.Enemy1_64;
                }

                sadInvadersArray[i].Top = top + (i / enemiesInRow) * verticalSpacing;
                sadInvadersArray[i].Left = left + (i % enemiesInRow) * horizontalSpacing;
                sadInvadersArray[i].Tag = "sadInvaders";
                sadInvadersArray[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(sadInvadersArray[i]);
            }
        }

        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = $"Score: {score} | Level: {currentLevel} - {message}";
            deathPanel.Visible = true;
            btnLeft.Visible = false;
            btnRight.Visible = false;
            btnShoot.Visible = false;
            goLeft = false;
            goRight = false;
            player.Left = this.ClientSize.Width / 2 - player.Width / 2;
            player.Top = this.ClientSize.Height - player.Height - 10;
        }

        private void removeAll()
        {
            foreach (Control x in this.Controls.OfType<PictureBox>().ToList())
            {
                if (x.Tag != null && ((string)x.Tag == "sadInvaders" || (string)x.Tag == "bullet" || (string)x.Tag == "sadBullet"))
                {
                    this.Controls.Remove(x);
                    x.Dispose();
                }
            }
        }

        private void makeBullet(string bulletTag, int? enemyLeft = null)
        {
            PictureBox bullet = new PictureBox();
            bullet.Size = new Size(10, 40);

            if (bulletTag == "bullet")
            {
                bullet.Image = Properties.Resources.HERO1_BULLET;
                bullet.Tag = "bullet";
                bullet.Left = player.Left + player.Width / 2;
                bullet.Top = player.Top - 20;
            }
            else if (bulletTag == "sadBullet")
            {
                bullet.Image = currentLevel % 2 == 0 ? Properties.Resources.Enemy2Bullet14na38 : Properties.Resources.EnemyBullet14na38;
                bullet.Tag = "sadBullet";
                bullet.Left = enemyLeft ?? random.Next(0, this.ClientSize.Width);
                bullet.Top = (enemyLeft.HasValue ? GetEnemyBottom(enemyLeft.Value) : 0) + 20;
            }

            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private int GetEnemyBottom(int xPos)
        {
            foreach (PictureBox enemy in sadInvadersArray)
            {
                if (enemy != null && enemy.Left <= xPos && enemy.Right >= xPos)
                {
                    return enemy.Bottom;
                }
            }
            return 0;
        }

        private void nextLevel()
        {
            currentLevel++;
            enemiesDestroyed = 0;
            removeAll();
            gameSetup();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = $"Score: {score} | Level: {currentLevel}";

            // Управление игроком с ограничением границ
            int margin = 5;
            if (goLeft)
            {
                player.Left = Math.Max(margin, player.Left - playerSpeed);
            }
            if (goRight)
            {
                player.Left = Math.Min(this.ClientSize.Width - player.Width - margin, player.Left + playerSpeed);
            }

            // Выстрелы врагов
            enemyBulletTimer -= 10;
            if (enemyBulletTimer < 1 && sadInvadersArray.Any(x => x != null))
            {
                enemyBulletTimer = 300 - (currentLevel * 10);

                var activeEnemies = sadInvadersArray.Where(x => x != null).ToList();
                if (activeEnemies.Any())
                {
                    var shooter = activeEnemies[random.Next(activeEnemies.Count)];
                    makeBullet("sadBullet", shooter.Left + shooter.Width / 2);
                }
            }

            // Движение врагов
            bool changeDirection = false;
            foreach (PictureBox x in sadInvadersArray)
            {
                if (x != null)
                {
                    x.Left += enemySpeed;

                    // Проверка достижения границы
                    if (x.Right >= this.ClientSize.Width || x.Left <= 0)
                    {
                        changeDirection = true;
                    }

                    // Проверка столкновения с игроком
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameOver("Тебя взяли в плен. Не расстраивайся! Попробуй еще раз");
                    }
                }
            }

            // Изменение направления движения врагов
            if (changeDirection)
            {
                enemySpeed = -enemySpeed;
                foreach (PictureBox x in sadInvadersArray)
                {
                    if (x != null)
                    {
                        x.Top += 30; // Опускаем врагов вниз

                        // Проверка достижения нижней границы
                        if (x.Bottom >= player.Top)
                        {
                            gameOver("Враги прорвали оборону! Попробуй еще раз");
                        }
                    }
                }
            }

            // Обработка пуль
            foreach (Control x in this.Controls.OfType<PictureBox>().ToList())
            {
                // Пуля игрока
                if ((string)x.Tag == "bullet")
                {
                    x.Top -= 20;

                    if (x.Top < 0)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                        x.Dispose();
                        continue;
                    }

                    foreach (PictureBox enemy in sadInvadersArray)
                    {
                        if (enemy != null && x.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            this.Controls.Remove(x);
                            this.Controls.Remove(enemy);
                            shooting = false;
                            x.Dispose();
                            enemy.Dispose();
                            sadInvadersArray[Array.IndexOf(sadInvadersArray, enemy)] = null;
                            score += 10;
                            enemiesDestroyed++;

                            if (enemiesDestroyed >= enemiesPerLevel)
                            {
                                nextLevel();
                                return;
                            }
                            break;
                        }
                    }
                }

                // Пуля врага
                if ((string)x.Tag == "sadBullet")
                {
                    x.Top += 15;

                    if (x.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                        continue;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                        gameOver("Ты был убит вражеской пулей, попробуй еще разок)");
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
    }
}