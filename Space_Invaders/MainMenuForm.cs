using SpaceInvaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            // Инициализация и воспроизведение музыки из ресурсов

        }
        private void PlayDesktopButton_Click(object sender, EventArgs e)
        {
            DesktopGame gameForm = new DesktopGame();
            gameForm.Show();
            this.Hide();
        }

        private void OpenShopButton_Click(object sender, EventArgs e)
        {
            ShopForm shopForm = new ShopForm(); // Создаем экземпляр формы магазина
            shopForm.Show(); // Открываем форму магазина
            this.Hide(); // Скрываем главное меню (опционально)
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayMobileButton_Click(object sender, EventArgs e)
        {
            MobileGame gameForm = new MobileGame();
            gameForm.Show();
            this.Hide();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
