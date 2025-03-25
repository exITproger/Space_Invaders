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
        int shootCooldown = 200; // время в миллисекундах между выстрелами

        bool goLeft, goRight;
        int playerSpeed = 12;
        int enemySpeed = 5;
        int score = 0;
        int enemyBulletTimer = 300;
        PictureBox[] sadInvadersArray;
        bool shooting;
        bool isGameOver;

        public Form1()
        {
            InitializeComponent();
            playerSpeed = Properties.Settings.Default.PlayerSpeed ; // Загружаем скорость из настроек
            // Устанавливаем размеры формы на 1920x1080
            this.Size = new Size(1920, 1080);

            // Убираем рамки окна
            this.FormBorderStyle = FormBorderStyle.None;

            // Запускаем в полноэкранном режиме
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
            // реализовать покупку скоростельности в магазине
            /*
            if (e.KeyCode == Keys.Space && canShoot)
            {
                makeBullet("bullet");
                canShoot = false;
                // Таймер для восстановления возможности выстрела через shootCooldown мс
                Timer shootTimer = new Timer();
                shootTimer.Interval = shootCooldown;
                shootTimer.Tick += (s, args) =>
                {
                    canShoot = true;
                    shootTimer.Stop();
                    shootTimer.Dispose();
                };
                shootTimer.Start();
            }*/ 
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeAll();
                gameSetup();
            }
        }

        private void makeInvaders()
        {
            // Создаем массив доступных изображений врагов
            Image[] enemyImages = new Image[]
            {
                Properties.Resources.Enemy1_64,
                Properties.Resources.Enemy2_64,
                // Добавьте другие изображения врагов по мере необходимости
            };

            Random random = new Random();
            sadInvadersArray = new PictureBox[50];
            int left = 0;

            for (int i = 0; i < sadInvadersArray.Length; i++)
            {
                sadInvadersArray[i] = new PictureBox();
                sadInvadersArray[i].Size = new Size(150, 150);

                // Случайный выбор изображения из массива
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

        private void gameSetup()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            isGameOver = false;
            enemyBulletTimer = 300;
            enemySpeed = 10;
            shooting = false;
            makeInvaders();
            gameTimer.Start();
        }

        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
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
            // Создаем массив доступных изображений пуль врагов
            Image[] enemyBulletImages = new Image[]
            {
        Properties.Resources.EnemyBullet14na38,
        Properties.Resources.Enemy2Bullet14na38,
                // Добавьте другие изображения пуль врагов по мере необходимости
            };

            Random random = new Random();
            PictureBox bullet = new PictureBox();

            if (bulletTag == "bullet")
            {
                // Пуля игрока - всегда одинаковая
                bullet.Image = Properties.Resources.HERO1_BULLET;
            }
            else if (bulletTag == "sadBullet")
            {
                // Случайный выбор изображения для пули врага
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
                    // Двигаем врага вправо
                    x.Left += enemySpeed;

                    // Если враг дошел до правого края экрана
                    if (x.Left > this.ClientSize.Width)
                    {
                        // Перемещаем врага на новый уровень
                        x.Top += 120; // Смещаем вниз
                        x.Left = -x.Width; // Начинаем с левого края
                    }

                    // Проверка столкновения врага с игроком
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameOver("Тебя взяли в плен. Не расстраивайся! Попробуй еще раз");
                    }

                    // Проверка столкновения пули игрока с врагом
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

                // Движение пули игрока
                if (x is PictureBox && (string)x.Tag == "bullet")
                {
                    x.Top -= 20;
                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                    }
                }

                // Движение пули врага
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

            // Увеличение скорости врагов при наборе очков
            if (score > 15)
            {
                enemySpeed = 15;
            }

            // Проверка на победу
            if (score == sadInvadersArray.Length)
            {
                gameOver("Воу, ты смог победить!!!");
            }
        }
    }
}