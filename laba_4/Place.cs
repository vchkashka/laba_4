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
    public partial class Place : Form
    {
        Image image; GameBoard gameBoard; Player player;
        Button prevButton; bool isMoving = false;

        //Показать юнитов
        private void Show_Units(Player player)
        {
            //Создаем и добавляем кнопку ОК
            Button buttonOK = new Button();
            buttonOK.Size = new Size(75, 23);
            buttonOK.Location = new Point(650, 420);
            buttonOK.Text = "OK";
            buttonOK.Click += new EventHandler(OK_Click);
            this.Controls.Add(buttonOK);
            for (int i = 0; i < player.Units.Count; i++)
            {
                //Если юнит не стоит на игровом поле, то создаем кнопку с его изображением вне поля
                if (player.Units[i].CurrentPosition == null)
                {
                    //Создаем кнопку
                    image = player.Units[i].Image;//Изображение юнита
                    Button butt = new Button();
                    butt.Size = new Size(50, 50);
                    butt.Location = new Point(470, i * 50);
                    Image part = new Bitmap(50, 50);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(image, new Rectangle(0, 0, 58, 62), 0, 0, 85, 90, GraphicsUnit.Pixel);
                    butt.BackgroundImage = part;
                    butt.BackColor = Color.White;
                    //Привязываем к кнопке юнита
                    butt.Tag = player.Units[i];
                    //Привязываем к кнопке событие по нажатию
                    butt.Click += new EventHandler(OnFigurePress);
                    //Выводим кнопку на экран
                    this.Controls.Add(butt);
                    //Создаем и выводим label для представления данных о юните
                    Label label = new Label();
                    label.Size = new Size(300, 50);
                    label.Location = new Point(525, i * 50 + 20);
                    label.Text = $"Текущее здоровье: {player.Units[i].Health}";
                    this.Controls.Add(label);
                }
                //Если юнит стоит на игровом поле, то выводим его среди клеток-кнопок поля
                else
                {
                    Button butt = new Button();
                    butt.Size = new Size(50, 50);
                    butt.BackColor = Color.White;
                    image = player.Units[i].Image;
                    butt.Location = new Point(player.Units[i].CurrentPosition.X * 50, player.Units[i].CurrentPosition.Y * 50);
                    Image part = new Bitmap(50, 50);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(image, new Rectangle(0, 0, 58, 62), 0, 0, 85, 90, GraphicsUnit.Pixel);
                    butt.BackgroundImage = part;
                    butt.Tag = player.Units[i];
                    butt.Click += new EventHandler(OnFigurePress);
                    this.Controls.Add(butt);
                }
            }
            //Выводим оставшиеся пустые клетки поля
            if (gameBoard != null)
            {
                for (int h = 0; h < 9; h++)
                    for (int j = 0; j < 9; j++)
                    {
                        Position position = new Position();
                        position.X = h;
                        position.Y = j;
                        if (gameBoard.GetUnitAtPosition(position) == null) 
                        {
                            Button butt = new Button();
                            butt.Size = new Size(50, 50);
                            butt.Location = new Point(h * 50, j * 50);
                            butt.BackColor = Color.White;
                            butt.Click += new EventHandler(OnFigurePress);
                            this.Controls.Add(butt);
                        }
                    }
            }
            //Если поле изначательно пустое, то все клетки пусты
            else
                for (int h = 0; h < 9; h++)
                    for (int j = 0; j < 9; j++)
                    {
                        Button butt = new Button();
                        butt.Size = new Size(50, 50);
                        butt.Location = new Point(h * 50, j * 50);
                        butt.BackColor = Color.White;
                        butt.Click += new EventHandler(OnFigurePress);
                        this.Controls.Add(butt);
                    }
        }
        //Обработчик события при нажатии кнопки
        public void OnFigurePress(object sender, EventArgs e)
        {
            UnitBase unit = null;
            //Предыдущая нажатая кнопка
            if (prevButton != null)
            {
                unit = prevButton.Tag as UnitBase;
            }
            //Кнопка, нажатая в данный момент
            Button pressedButton = sender as Button;
            if (pressedButton.BackgroundImage != null)
            {
                //Перемещение юнита
                isMoving = true;
            }
            else
            {
                if (isMoving)
                {
                    if (pressedButton.Location.X / 50 >= 0 && pressedButton.Location.X / 50 <= 8 && pressedButton.Location.Y / 50 >= 0 && pressedButton.Location.Y / 50 <= 8)
                    {
                        Position nowPosition = new Position();
                        nowPosition.X = pressedButton.Location.X / 50;nowPosition.Y= pressedButton.Location.Y / 50;
                        //Если клетка поля пуста
                        if (gameBoard.GetUnitAtPosition(nowPosition) == null)
                        {
                            Position prevPosition = new Position();
                            prevPosition.X = prevButton.Location.X / 50; prevPosition.Y = prevButton.Location.Y / 50;
                            //Кнопка, в которой находился юнит, становится пустой
                            if (prevButton != null && prevButton.Location.X / 50 >= 0 && prevButton.Location.X / 50 <= 8 &&
                                prevButton.Location.Y / 50 >= 0 && prevButton.Location.Y / 50 <= 8)
                                gameBoard.RemoveUnit(unit);
                            //Размещаем юнита на новом месте
                            for (int i = 0; i < player.Units.Count; i++)
                                if (player.Units[i] == unit)
                                    gameBoard.PlaceUnit(player.Units[i], nowPosition);
                            //Привязываем значение к новой кнопке
                            pressedButton.Tag = prevButton.Tag;
                            //Перемещаем изображение юнита
                            pressedButton.BackgroundImage = prevButton.BackgroundImage;
                            //Обнуляем значение прошлой кнопки
                            prevButton.BackgroundImage = null;
                            prevButton.Tag = null;
                        }
                        //Если клетка уже занята, то выводим предупреждающеее сообщение
                        else
                        {
                            MessageBox.Show("Данная клетка уже занята другим игроком.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                }
            }
            prevButton = pressedButton;
        }

        public Place(Player player, GameBoard gameBoard)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.Font = new Font("Blackoak Std", 7, FontStyle.Bold);
            this.gameBoard = gameBoard;
            this.player = player;
            Show_Units(this.player);           
            this.Size = new Size(750, 490);
        }

        private void Place_Load(object sender, EventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {
            Close();
            Data.Value = gameBoard;
            if (player.Color == Colors.green)
                Data.Player1 = player;
            else
                Data.Player2 = player;
        }
    }
}
