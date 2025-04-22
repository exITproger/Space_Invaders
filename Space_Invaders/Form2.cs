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
    public partial class MobileGame : Form
    {
        private DateTime _lastShootTime = DateTime.MinValue;
        private bool _canShoot = true;
        private int _shootCooldown = 200;
        private bool _goLeft;
        private bool _goRight;
        private int _playerSpeed = 12;
        private int _enemySpeed = 5;
        private int _score = 0;
        private int _enemyBulletTimer = 300;
        private PictureBox[] _sadInvadersArray;
        private bool _shooting;
        private bool _isGameOver;
        private int _currentLevel = 1;
        private int _enemiesPerLevel = 20;
        private int _enemiesDestroyed = 0;
        private readonly Random _random = new Random();

        // Элементы управления
        private Button _btnLeft;
        private Button _btnRight;
        private Button _btnShoot;
        private Panel _deathPanel;
        private Button _btnRestart;
        private Button _btnExit;

        public MobileGame()
        {
            InitializeComponent();
            _playerSpeed = Properties.Settings.Default.PlayerSpeed;
            Size = new Size(1920, 1080);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            KeyDown += Form2_KeyDown;
            FormClosed += Form2_FormClosed;

            CreateControlButtons();
            CreateDeathScreen();
            GameSetup();
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
            _btnLeft = new Button
            {
                Text = "←",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Size = new Size(120, 120),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60))
            };
            _btnLeft.Location = new Point(ClientSize.Width - 360, ClientSize.Height - 140);
            _btnLeft.FlatAppearance.BorderSize = 0;
            _btnLeft.MouseDown += (s, e) => _goLeft = true;
            _btnLeft.MouseUp += (s, e) => _goLeft = false;
            _btnLeft.MouseEnter += (s, e) => _btnLeft.BackColor = Color.LightSlateGray;
            _btnLeft.MouseLeave += (s, e) => _btnLeft.BackColor = Color.DarkSlateGray;

            // Кнопка стрельбы
            _btnShoot = new Button
            {
                Text = "⚡",
                Font = new Font("Arial", 32, FontStyle.Bold),
                Size = new Size(120, 120),
                BackColor = Color.Gold,
                ForeColor = Color.DarkRed,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60))
            };
            _btnShoot.Location = new Point(ClientSize.Width - 240, ClientSize.Height - 140);
            _btnShoot.FlatAppearance.BorderSize = 0;
            _btnShoot.Click += (s, e) =>
            {
                if ((DateTime.Now - _lastShootTime).TotalMilliseconds >= 900 && !_shooting)
                {
                    _lastShootTime = DateTime.Now;
                    _shooting = true;
                    MakeBullet("bullet");
                }
            };
            _btnShoot.MouseEnter += (s, e) => _btnShoot.BackColor = Color.Goldenrod;
            _btnShoot.MouseLeave += (s, e) => _btnShoot.BackColor = Color.Gold;

            // Кнопка вправо
            _btnRight = new Button
            {
                Text = "→",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Size = new Size(120, 120),
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 120, 120, 60, 60))
            };
            _btnRight.Location = new Point(ClientSize.Width - 120, ClientSize.Height - 140);
            _btnRight.FlatAppearance.BorderSize = 0;
            _btnRight.MouseDown += (s, e) => _goRight = true;
            _btnRight.MouseUp += (s, e) => _goRight = false;
            _btnRight.MouseEnter += (s, e) => _btnRight.BackColor = Color.LightSlateGray;
            _btnRight.MouseLeave += (s, e) => _btnRight.BackColor = Color.DarkSlateGray;

            Controls.Add(_btnLeft);
            Controls.Add(_btnShoot);
            Controls.Add(_btnRight);

            CreatePlayer();
        }

        private void CreatePlayer()
        {
            player.Size = new Size(150, 130);
            player.Image = Properties.Resources.HERO1_128;
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.Location = new Point(
                ClientSize.Width / 2 - player.Width / 2,
                ClientSize.Height - player.Height - 10);
            Controls.Add(player);
        }

        private void CreateDeathScreen()
        {
            _deathPanel = new Panel
            {
                Size = new Size(400, 200),
                BackColor = Color.FromArgb(150, 0, 0, 0),
                Visible = false
            };
            _deathPanel.Location = new Point(
                (ClientSize.Width - _deathPanel.Width) / 2,
                (ClientSize.Height - _deathPanel.Height) / 2);

            _btnRestart = new Button
            {
                Text = "Заново",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 150, 50, 25, 25))
            };
            _btnRestart.Location = new Point(50, 50);
            _btnRestart.Click += (s, e) =>
            {
                RemoveAll();
                GameSetup();
                _deathPanel.Visible = false;
            };

            _btnExit = new Button
            {
                Text = "Выйти",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(150, 50),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 150, 50, 25, 25))
            };
            _btnExit.Location = new Point(200, 50);
            _btnExit.Click += (s, e) => Close();

            _deathPanel.Controls.Add(_btnRestart);
            _deathPanel.Controls.Add(_btnExit);
            Controls.Add(_deathPanel);
            _deathPanel.BringToFront();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            var mainMenu = new MainMenuForm();
            mainMenu.Show();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void GameSetup()
        {
            txtScore.Text = $"Score: {_score} | Level: {_currentLevel}";
            _isGameOver = false;
            _enemyBulletTimer = 300;
            _enemySpeed = 5 + _currentLevel;
            _shooting = false;
            _deathPanel.Visible = false;
            _btnLeft.Visible = true;
            _btnRight.Visible = true;
            _btnShoot.Visible = true;
            MakeInvaders();
            gameTimer.Start();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                _goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                _goRight = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                _goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                _goRight = false;
            }
            if (e.KeyCode == Keys.Space && !_shooting)
            {
                _shooting = true;
                MakeBullet("bullet");
            }
            if (e.KeyCode == Keys.Enter && _isGameOver)
            {
                RemoveAll();
                GameSetup();
            }
        }

        private void MakeInvaders()
        {
            Image[] enemyImages = new Image[]
           {
                Properties.Resources.Enemy1_64,
                Properties.Resources.Enemy2_64,
                Properties.Resources.vr_px
           };

            _sadInvadersArray = new PictureBox[_enemiesPerLevel];
            int left = 100;
            int top = 50;
            int enemiesInRow = 10;
            int horizontalSpacing = 150;
            int verticalSpacing = 120;

            for (int i = 0; i < _sadInvadersArray.Length; i++)
            {
                _sadInvadersArray[i] = new PictureBox();
                _sadInvadersArray[i].Size = new Size(100, 100);

                if (_currentLevel % 3 == 0)
                {
                    _sadInvadersArray[i].Image = Properties.Resources.Enemy1_64;
                }
                else if (_currentLevel % 2 == 0)
                {
                    _sadInvadersArray[i].Image = Properties.Resources.Enemy2_64;
                }
                else
                {
                    _sadInvadersArray[i].Image = Properties.Resources.vr_px;
                }

                _sadInvadersArray[i].Top = top + (i / enemiesInRow) * verticalSpacing;
                _sadInvadersArray[i].Left = left + (i % enemiesInRow) * horizontalSpacing;
                _sadInvadersArray[i].Tag = "sadInvaders";
                _sadInvadersArray[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(_sadInvadersArray[i]);
            }
        }

        private void GameOver(string message)
        {
            _isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = $"Score: {_score} | Level: {_currentLevel} - {message}";
            _deathPanel.Visible = true;
            _btnLeft.Visible = false;
            _btnRight.Visible = false;
            _btnShoot.Visible = false;
            _goLeft = false;
            _goRight = false;
            player.Left = ClientSize.Width / 2 - player.Width / 2;
            player.Top = ClientSize.Height - player.Height - 10;
        }

        private void RemoveAll()
        {
            foreach (var x in Controls.OfType<PictureBox>().ToList())
            {
                if (x.Tag != null && ((string)x.Tag == "sadInvaders" ||
                                     (string)x.Tag == "bullet" ||
                                     (string)x.Tag == "sadBullet"))
                {
                    Controls.Remove(x);
                    x.Dispose();
                }
            }
        }

        private void MakeBullet(string bulletTag, int? enemyLeft = null)
        {
            var bullet = new PictureBox
            {
                Size = new Size(10, 40),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            if (bulletTag == "bullet")
            {
                bullet.Image = Properties.Resources.HERO1_BULLET;
                bullet.Tag = "bullet";
                bullet.Left = player.Left + player.Width / 2;
                bullet.Top = player.Top - 20;
            }
            else if (bulletTag == "sadBullet")
            {
                bullet.Image = _currentLevel % 2 == 0
                    ? Properties.Resources.Enemy2Bullet14na38
                    : Properties.Resources.EnemyBullet14na38;
                bullet.Tag = "sadBullet";
                bullet.Left = enemyLeft ?? _random.Next(0, ClientSize.Width);
                bullet.Top = (enemyLeft.HasValue ? GetEnemyBottom(enemyLeft.Value) : 0) + 20;
            }

            Controls.Add(bullet);
            bullet.BringToFront();
        }

        private int GetEnemyBottom(int xPos)
        {
            foreach (var enemy in _sadInvadersArray)
            {
                if (enemy != null && enemy.Left <= xPos && enemy.Right >= xPos)
                {
                    return enemy.Bottom;
                }
            }
            return 0;
        }

        private void NextLevel()
        {
            _currentLevel++;
            _enemiesDestroyed = 0;
            RemoveAll();
            GameSetup();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = $"Score: {_score} | Level: {_currentLevel}";

            // Управление игроком с ограничением границ
            const int margin = 5;
            if (_goLeft)
            {
                player.Left = Math.Max(margin, player.Left - _playerSpeed);
            }
            if (_goRight)
            {
                player.Left = Math.Min(ClientSize.Width - player.Width - margin, player.Left + _playerSpeed);
            }

            // Выстрелы врагов
            _enemyBulletTimer -= 10;
            if (_enemyBulletTimer < 1 && _sadInvadersArray.Any(x => x != null))
            {
                _enemyBulletTimer = 300 - (_currentLevel * 10);

                var activeEnemies = _sadInvadersArray.Where(x => x != null).ToList();
                if (activeEnemies.Any())
                {
                    var shooter = activeEnemies[_random.Next(activeEnemies.Count)];
                    MakeBullet("sadBullet", shooter.Left + shooter.Width / 2);
                }
            }

            // Движение врагов
            bool changeDirection = false;
            foreach (var x in _sadInvadersArray)
            {
                if (x != null)
                {
                    x.Left += _enemySpeed;

                    // Проверка достижения границы
                    if (x.Right >= ClientSize.Width || x.Left <= 0)
                    {
                        changeDirection = true;
                    }

                    // Проверка столкновения с игроком
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        GameOver("Тебя взяли в плен. Не расстраивайся! Попробуй еще раз");
                    }
                }
            }

            // Изменение направления движения врагов
            if (changeDirection)
            {
                _enemySpeed = -_enemySpeed;
                foreach (var x in _sadInvadersArray)
                {
                    if (x != null)
                    {
                        x.Top += 30;

                        // Проверка достижения нижней границы
                        if (x.Bottom >= player.Top)
                        {
                            GameOver("Враги прорвали оборону! Попробуй еще раз");
                        }
                    }
                }
            }

            // Обработка пуль
            foreach (var x in Controls.OfType<PictureBox>().ToList())
            {
                // Пуля игрока
                if ((string)x.Tag == "bullet")
                {
                    x.Top -= 20;

                    if (x.Top < 0)
                    {
                        Controls.Remove(x);
                        _shooting = false;
                        x.Dispose();
                        continue;
                    }

                    foreach (var enemy in _sadInvadersArray)
                    {
                        if (enemy != null && x.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            Controls.Remove(x);
                            Controls.Remove(enemy);
                            _shooting = false;
                            x.Dispose();
                            enemy.Dispose();
                            _sadInvadersArray[Array.IndexOf(_sadInvadersArray, enemy)] = null;
                            _score += 10;
                            _enemiesDestroyed++;

                            if (_enemiesDestroyed >= _enemiesPerLevel)
                            {
                                NextLevel();
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

                    if (x.Top > ClientSize.Height)
                    {
                        Controls.Remove(x);
                        x.Dispose();
                        continue;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        Controls.Remove(x);
                        x.Dispose();
                        GameOver("Ты был убит вражеской пулей, попробуй еще разок)");
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
    }
}