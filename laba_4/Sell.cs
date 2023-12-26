using laba_3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_4
{
    public partial class Sell : Form
    {

        public Image image; Button prevButton; GameBoard gameBoard; Player player; UnitBase unit; bool select;
        //Показать юнитов
        private void Show_Unins(Player player)
        {
            int k = 0;
            //Создаем надпись 
            Label label1 = new Label();
            label1.Size = new Size(226, 13);
            label1.Location = new Point(2, 6);
            label1.Text = "Выберите юнита, которого хотите продать:";
            this.Controls.Add(label1);
            //Выводим всех юнитов(создаем кнопки с их изображения и label c информацией об их здоровье и размещении на поле)
            for (int i = 0; i < player.Units.Count; i++)
            {
                //Кнопки
                switch (player.Units[i].Color)
                {
                    case Colors.red: image = new Bitmap("лучник2.png"); break;
                    case Colors.green: image = new Bitmap("лучник.png"); break;
                }
                Button butt = new Button();
                butt.Size = new Size(50, 50);
                butt.Location = new Point(0, i * 50 + 30);
                Image part = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(image, new Rectangle(0, 0, 58, 62), 0, 0, 85, 90, GraphicsUnit.Pixel);
                butt.BackgroundImage = part;
                butt.Tag = player.Units[i];
                //Label
                Label label = new Label();
                label.Size = new Size(200, 50);
                label.Location = new Point(60, i * 50 + 30);
                label.Text = $"{i + 1}. {player.Units[i].GetType().Name}\n (Текущее здоровье - {player.Units[i].Health})\n";
                if (player.Units[i].CurrentPosition == null)
                {
                    label.Text += "Юнит не стоит на поле";
                }
                else
                {
                    label.Text += $"Позиция: ({player.Units[i].CurrentPosition.X},{player.Units[i].CurrentPosition.Y})\n";
                }
                k++;
                butt.Click += new EventHandler(OnFigurePress);
                this.Controls.Add(butt);
                this.Controls.Add(label);
            }
            //Создаем кнопку ОК
            Button buttonOK = new Button();
            buttonOK.Size = new Size(75, 23);
            buttonOK.Location = new Point(200, k * 30 + k * 50);
            buttonOK.Text = "OK";
            buttonOK.Click += new EventHandler(OnFigurePress);
            this.Controls.Add(buttonOK);
            this.Size = new Size(300, k * 50 + k * 30 + 70);
        }
        //Обработчик события при нажатии кнопки
        public void OnFigurePress(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            //Предыдущая нажатая кнопка
            if (prevButton != null)
            {
                unit = prevButton.Tag as UnitBase;
            }
            //Кнопка, нажатая в данный момент
            if (pressedButton.BackgroundImage != null)
            {
                //Выбор юнита
                select = true;
            }
            else
            {
                if (select)
                {
                    for (int i = 0; i < player.Units.Count; i++)
                        if (player.Units[i] == unit)
                        {
                            //Если юнит на поле, то удаляем его с поля
                            if (unit.CurrentPosition != null)
                                gameBoard.RemoveUnit(player.Units[i]);
                            //Удаляем унита из списка юнитов игрока
                            player.RemoveUnit(player.Units[i]);
                            //Очищаем форму
                            this.Controls.Clear();
                            //Если юниты еще остались, то выводим их заново
                            if (player.Units.Count == 0)
                            {
                                if (player.Color == Colors.green)
                                {
                                    Data.Player1 = player;
                                }
                                else if (player.Color == Colors.red)
                                {
                                    Data.Player2 = player;
                                }
                                Data.Value = gameBoard;
                                Close();
                            }
                            else
                                Show_Unins(player);
                        }
                }
            }
            prevButton = pressedButton;
            if (player.Units.Count == 0)
            {
                MessageBox.Show("Все юниты проданы.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Data.Value = gameBoard;
                if (player.Color == Colors.green)
                {
                    Data.Player1 = player;
                }
                else if (player.Color == Colors.red)
                {
                    Data.Player2 = player;
                }
                Close();
            }
        }

        public Sell(Player player, GameBoard gameBoard)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.Font = new Font("Blackoak Std", 7, FontStyle.Bold);
            this.gameBoard = gameBoard;
            this.player = player;
            //Выводим юнитов игрока для продажи
            Show_Unins(player);
        }

        private void Sellcs_Load(object sender, EventArgs e)
        {

        }

        
    }
}
