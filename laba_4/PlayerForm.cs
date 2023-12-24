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

namespace laba_4
{
    public partial class PlayerForm : Form
    {
        Player player;
        GameBoard gameBoard;

        public PlayerForm(Player player, GameBoard gameBoard)
        {
            InitializeComponent();
            this.CenterToScreen();
            label2.Text = $"{player.Coins}";
            this.player = player;
            this.gameBoard = gameBoard;
            //В зависимости от цвета игрока загружаем на форму картинку
            if (player.Color == Colors.green)
                pictureBox1.Image = Image.FromFile("C:\\Users\\User\\Desktop\\игрок.png");
            if (player.Color == Colors.red)
                pictureBox1.Image = Image.FromFile("C:\\Users\\User\\Desktop\\игрок2.png");
        }

        //Покупка юнитов
        private void button1_Click(object sender, EventArgs e)
        {
            using (Shop form = new Shop(player))
            {
                form.ShowDialog();
            }
            label2.Text = $"{player.Coins}";
            if (Data.Player1 != null && player.Color == Data.Player1.Color)
            {
                player = Data.Player1;
            }
            else if (Data.Player2 != null && player.Color == Data.Player2.Color)
            {
                player = Data.Player2;
            }
            if (Data.Value != null)
                gameBoard = Data.Value;
        }
        //Продажа юнитов
        private void button2_Click(object sender, EventArgs e)
        {
            if (player.Units.Count > 0)
                using (Sell form = new Sell(player, gameBoard))
                {
                    form.ShowDialog();
                }
            else
                MessageBox.Show("У вас пока нет ни одного юнита.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            label2.Text = $"{player.Coins}";
            if (Data.Player1 != null && player.Color == Data.Player1.Color)
            {
                player = Data.Player1;
            }
            else if (Data.Player2 != null && player.Color == Data.Player2.Color)
            {
                player = Data.Player2;
            }
            if (Data.Value != null)
                gameBoard = Data.Value;
        }

        //Расстановка юнитов
        private void button3_Click(object sender, EventArgs e)
        {
            if (player.Units.Count > 0)
                using (Place form = new Place(player, gameBoard))
                {
                    form.ShowDialog();
                }
            else
                MessageBox.Show("У вас пока нет ни одного юнита.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (Data.Player1 != null && player.Color == Data.Player1.Color)
            {
                player = Data.Player1;
            }
            else if (Data.Player2 != null && player.Color == Data.Player2.Color)
            {
                player = Data.Player2;
            }
            if (Data.Value != null)
                gameBoard = Data.Value;
        }
    }
}
