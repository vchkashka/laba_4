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
    public partial class ViewBoard : Form
    {
        GameBoard gameBoard;
        Image image;

        public ViewBoard(GameBoard gameBoard)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.gameBoard = gameBoard;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Вывод игрового поля
            CreateMap();
            this.Size = new Size(465, 490);
        }

        public void CreateMap()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    Position position = new Position();
                    position.X = i; position.Y = j;
                    //Для каждой клетки игрового поля создаем кнопку
                    Button butt = new Button();
                    butt.Size = new Size(50, 50);
                    butt.Location = new Point(i * 50, j * 50);
                    butt.BackColor = Color.White;
                    //В зависимости от значения, хранящегося в map, выводим изображение юнита
                    if (gameBoard.GetUnitAtPosition(position) == null)
                    {
                        this.Controls.Add(butt);
                    }
                    else
                            if (gameBoard.Board[position].Color == Colors.green)
                    {

                        if (gameBoard.Board[position].GetType().Name == "Archer")
                            image = new Bitmap("лучник.png");
                        if (gameBoard.Board[position].GetType().Name == "Warrior")
                            image = new Bitmap("воин.png");
                        Image part = new Bitmap(50, 50);
                        Graphics g = Graphics.FromImage(part);
                        g.DrawImage(image, new Rectangle(0, 0, 58, 62), 0, 0, 85, 90, GraphicsUnit.Pixel);
                        butt.BackgroundImage = part;
                        this.Controls.Add(butt);
                    }
                    else
                            if (gameBoard.Board[position].Color == Colors.red)
                    {
                        if (gameBoard.Board[position].GetType().Name == "Archer")
                            image = new Bitmap("лучник2.png");
                        if (gameBoard.Board[position].GetType().Name == "Warrior")
                            image = new Bitmap("воин2.png");
                        Image part = new Bitmap(50, 50);
                        Graphics g = Graphics.FromImage(part);
                        g.DrawImage(image, new Rectangle(0, 0, 58, 62), 0, 0, 85, 90, GraphicsUnit.Pixel);
                        butt.BackgroundImage = part;
                        this.Controls.Add(butt);
                    }
                }
        }
    }
}
