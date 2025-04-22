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
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            _playerSpeed = Properties.Settings.Default.PlayerSpeed;
            this.Size = new Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            GameSetup();
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
            if (e.KeyCode == Keys.Space && _shooting == false)
            {
                _shooting = true;
                MakeBullet("bullet");
            }
            if (e.KeyCode == Keys.Enter && _isGameOver == true)
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

                if (_currentLevel % 2 == 0)
                {
                    _sadInvadersArray[i].Image = Properties.Resources.Enemy2_64;
                }
                else
                {
                    _sadInvadersArray[i].Image = Properties.Resources.Enemy1_64;
                }

                _sadInvadersArray[i].Top = top + (i / enemiesInRow) * verticalSpacing;
                _sadInvadersArray[i].Left = left + (i % enemiesInRow) * horizontalSpacing;
                _sadInvadersArray[i].Tag = "sadInvaders";
                _sadInvadersArray[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(_sadInvadersArray[i]);
            }
        }

        private void GameSetup()
        {
            txtScore.Text = $"Score: {_score} | Level: {_currentLevel}";
            _isGameOver = false;
            _enemyBulletTimer = 300;
            _enemySpeed = 5 + _currentLevel;
            _shooting = false;
            MakeInvaders();
            gameTimer.Start();
        }

        private void GameOver(string message)
        {
            _isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = $"Score: {_score} | Level: {_currentLevel} - {message}";
        }

        private void RemoveAll()
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

        private void MakeBullet(string bulletTag, int? enemyLeft = null)
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
                bullet.Image = _currentLevel % 2 == 0 ? Properties.Resources.Enemy2Bullet14na38 : Properties.Resources.EnemyBullet14na38;
                bullet.Tag = "sadBullet";
                bullet.Left = enemyLeft ?? _random.Next(0, this.ClientSize.Width);
                bullet.Top = (enemyLeft.HasValue ? GetEnemyBottom(enemyLeft.Value) : 0) + 20;
            }

            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }
        private void Keyisdown(object sender, KeyEventArgs e)
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
        private void Keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                _goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                _goRight = false;
            }
            if (e.KeyCode == Keys.Space && _shooting == false)
            {
                _shooting = true;
                MakeBullet("bullet");
            }
            if (e.KeyCode == Keys.Enter && _isGameOver == true)
            {
                RemoveAll();
                GameSetup();
            }
        }

        private int GetEnemyBottom(int xPos)
        {
            foreach (PictureBox enemy in _sadInvadersArray)
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

            int margin = 5;
            if (_goLeft && player.Left > margin)
            {
                player.Left = Math.Max(margin, player.Left - _playerSpeed);
            }
            if (_goRight && player.Right < this.ClientSize.Width - margin)
            {
                player.Left = Math.Min(this.ClientSize.Width - player.Width - margin, player.Left + _playerSpeed);
            }

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

            bool changeDirection = false;
            foreach (PictureBox x in _sadInvadersArray)
            {
                if (x != null)
                {
                    x.Left += _enemySpeed;

                    if (x.Right >= this.ClientSize.Width || x.Left <= 0)
                    {
                        changeDirection = true;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        GameOver("Тебя взяли в плен. Не расстраивайся! Попробуй еще раз");
                    }
                }
            }

            if (changeDirection)
            {
                _enemySpeed = -_enemySpeed;
                foreach (PictureBox x in _sadInvadersArray)
                {
                    if (x != null)
                    {
                        x.Top += 30;

                        if (x.Bottom >= player.Top)
                        {
                            GameOver("Враги прорвали оборону! Попробуй еще раз");
                        }
                    }
                }
            }

            foreach (Control x in this.Controls.OfType<PictureBox>().ToList())
            {
                if ((string)x.Tag == "bullet")
                {
                    x.Top -= 20;

                    if (x.Top < 0)
                    {
                        this.Controls.Remove(x);
                        _shooting = false;
                        x.Dispose();
                        continue;
                    }

                    foreach (PictureBox enemy in _sadInvadersArray)
                    {
                        if (enemy != null && x.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            this.Controls.Remove(x);
                            this.Controls.Remove(enemy);
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
                        GameOver("Ты был убит вражеской пулей, попробуй еще разок)");
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}