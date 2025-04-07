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
    public partial class ShopForm : Form
    {
        public ShopForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += new FormClosedEventHandler(ShopForm_FormClosed);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm(); // Создаем экземпляр главного меню
            mainMenu.Show(); // Открываем главное меню
            this.Close(); // Закрываем форму магазина
        }
        private void ShopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
        }

        private void ShopForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Улучшение куплено!");
        }

        private void btnBuySpeed_Click(object sender, EventArgs e)
        {
            int newSpeed = Properties.Settings.Default.PlayerSpeed + 1; // Увеличиваем сохранённую скорость
            Properties.Settings.Default.PlayerSpeed = newSpeed;
            Properties.Settings.Default.Save(); // Сохраняем значение
            MessageBox.Show($"Скорость увеличена! Новая скорость: {newSpeed}");
        }

        private void btnResetSpeed_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlayerSpeed = 12; // Устанавливаем значение по умолчанию
            Properties.Settings.Default.Save(); // Сохраняем изменения
            MessageBox.Show("Скорость сброшена до стандартного значения!");
        }

        private void PicPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
