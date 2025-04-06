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
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            // Инициализация и воспроизведение музыки из ресурсов

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1();
            gameForm.Show();
            this.Hide();
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            ShopForm shopForm = new ShopForm(); // Создаем экземпляр формы магазина
            shopForm.Show(); // Открываем форму магазина
            this.Hide(); // Скрываем главное меню (опционально)
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 gameForm = new Form2();
            gameForm.Show();
            this.Hide();
        }
    }
}
