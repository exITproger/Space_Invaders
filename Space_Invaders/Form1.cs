using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public partial class Form1 : Form
    {
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

        public Form1()
        {
            InitializeComponent();
            playerSpeed = Properties.Settings.Default.PlayerSpeed;
            this.Size = new Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            gameSetup();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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

                // Разные враги для разных уровней
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

        private void gameSetup()
        {
            txtScore.Text = $"Score: {score} | Level: {currentLevel}";
            isGameOver = false;
            enemyBulletTimer = 300;
            enemySpeed = 5 + currentLevel; // Увеличиваем скорость с каждым уровнем
            shooting = false;
            makeInvaders();
            gameTimer.Start();
        }

        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = $"Score: {score} | Level: {currentLevel} - {message}";
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

            // Управление игроком
            int margin = 5; // Небольшой отступ от края
            if (goLeft && player.Left > margin)
            {
                player.Left = Math.Max(margin, player.Left - playerSpeed);
            }
            if (goRight && player.Right < this.ClientSize.Width - margin)
            {
                player.Left = Math.Min(this.ClientSize.Width - player.Width - margin, player.Left + playerSpeed);
            }

            // Выстрелы врагов
            enemyBulletTimer -= 10;
            if (enemyBulletTimer < 1 && sadInvadersArray.Any(x => x != null))
            {
                enemyBulletTimer = 300 - (currentLevel * 10); // Уменьшаем интервал с уровнем

                // Выбираем случайного живого врага для выстрела
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

                    // Удаление пули при выходе за границы
                    if (x.Top < 0)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                        x.Dispose();
                        continue;
                    }

                    // Проверка попадания во врага
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

                            // Проверка завершения уровня
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

                    // Удаление пули при выходе за границы
                    if (x.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                        continue;
                    }

                    // Проверка попадания в игрока
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                        gameOver("Ты был убит вражеской пулей, попробуй еще разок)");
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}