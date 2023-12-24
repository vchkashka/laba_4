using laba_3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace laba_4
{
    public partial class Shop : Form
    {
        Player player;
        public Shop(Player player)
        {
            InitializeComponent();
            this.CenterToScreen();
            label4.Text = $"{player.Coins}";
            this.player = player;
            //Загружаем картинки в зависимости от цвета игрока
            if (player.Color == Colors.green)
            {
                System.Drawing.Image im = new Bitmap("C:\\Users\\User\\Desktop\\воин.png");
                System.Drawing.Image part = new Bitmap(116, 124);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(im, new Rectangle(0, 0, 116, 124), 0, 0, 85,85, GraphicsUnit.Pixel);
                pictureBox1.BackgroundImage = part;
                System.Drawing.Image im2 = new Bitmap("C:\\Users\\User\\Desktop\\лучник.png");
                part = new Bitmap(116, 124);
                g = Graphics.FromImage(part);
                g.DrawImage(im2, new Rectangle(0, 0, 116, 124), 0, 0, 85, 85, GraphicsUnit.Pixel);
                pictureBox2.Image = part;
            }
            if (player.Color == Colors.red)
            {
                System.Drawing.Image im = new Bitmap("C:\\Users\\User\\Desktop\\воин2.png");
                System.Drawing.Image part = new Bitmap(116, 124);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(im, new Rectangle(0, 0, 116, 124), 0, 0, 85, 85, GraphicsUnit.Pixel);
                pictureBox1.BackgroundImage = part;
                System.Drawing.Image im2 = new Bitmap("C:\\Users\\User\\Desktop\\лучник2.png");
                part = new Bitmap(116, 124);
                g = Graphics.FromImage(part);
                g.DrawImage(im2, new Rectangle(0, 0, 116, 124), 0, 0, 85, 85, GraphicsUnit.Pixel);
                pictureBox2.Image = part;
            }
        }
        //Покупка воина
        private void button1_Click(object sender, EventArgs e)
        {         
            // Проверка, достаточно ли монет для покупки
            if (player.Coins < new Warrior(player.Color).Cost)
            {
                MessageBox.Show("Не хватает монет, чтобы купить юнита.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);               
            }
            else
            {
                player.Coins -= new Warrior(player.Color).Cost; // Уменьшаем количество монет на стоимость юнита
                player.Units.Add(new Warrior(player.Color));
                MessageBox.Show($"Воин добавлен в армию игрока. У вас осталось {player.Coins} монет.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label4.Text = $"{player.Coins}";
                if (player.Color == Colors.green)
                {
                    Data.Player1 = player;
                }
                else if (player.Color == Colors.red)
                {
                    Data.Player2 = player;
                }
            }
        }
        //Покупка лучника
        private void button2_Click(object sender, EventArgs e)
        {
            // Проверка, достаточно ли монет для покупки
            if (player.Coins < new Archer(player.Color).Cost)
            {
                MessageBox.Show("Не хватает монет, чтобы купить юнита.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                player.Coins -= new Archer(player.Color).Cost; // Уменьшаем количество монет на стоимость юнита
                player.Units.Add(new Archer(player.Color));
                MessageBox.Show($"Лучник добавлен в армию игрока. У вас осталось {player.Coins} монет.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label4.Text = $"{player.Coins}";
                if (player.Color == Colors.green)
                {
                    Data.Player1 = player;
                }
                else if (player.Color == Colors.red)
                {
                    Data.Player2 = player;
                }
            }
        }
    }
}
