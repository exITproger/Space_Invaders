using Space_Invaders;
using System;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>
    /// Класс отвечающий за магазин
    /// </summary>
    public partial class ShopForm : Form
    {
        private const int _DefaultPlayerSpeed = 12;
        private const int _MaxPlayerSpeed = 40;
        /// <summary>
        /// конструктор класса
        /// </summary>
        public ShopForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += ShopForm_FormClosed;
        }
        /// <summary>
        /// вернуться в главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm(); // Создаем экземпляр главного меню
            mainMenu.Show(); // Открываем главное меню
            this.Close(); // Закрываем форму магазина
        }
        /// <summary>
        /// метод закрытия окна магазина
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
        }

        /// <summary>
        /// покупка улучшения(надпись)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Улучшение куплено!");
        }
        /// <summary>
        /// покупка скорости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void BuySpeedButton_Click(object sender, EventArgs e)
        {
            int newSpeed = Space_Invaders.Properties.Settings.Default.PlayerSpeed + 1;

            if (newSpeed > _MaxPlayerSpeed)
            {
                MessageBox.Show("Достигнута максимальная скорость!");
                return;
            }

            Space_Invaders.Properties.Settings.Default.PlayerSpeed = newSpeed;
            Space_Invaders.Properties.Settings.Default.Save();
            MessageBox.Show($"Скорость увеличена! Новая скорость: {newSpeed}");
        }

        /// <summary>
        /// сброс накопленной скорости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetSpeedButton_Click(object sender, EventArgs e)
        {
            Space_Invaders.Properties.Settings.Default.PlayerSpeed = _DefaultPlayerSpeed;
            Space_Invaders.Properties.Settings.Default.Save();
            MessageBox.Show("Скорость сброшена до стандартного значения!");
        }
    }
}