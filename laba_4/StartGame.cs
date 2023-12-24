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
    public partial class StartGame : Form
    {
        GameBoard gameBoard; List<Player> players;
        System.Drawing.Image image;

        private Button[][] buttons()
        {
            Button[][] allButtons =
          {
                new Button[]{button1, button2, button3, button4, button5, button6, button7, button8, button9, },
                new Button[]{button10, button11, button12, button13, button14, button15, button16, button17, button18, },
                new Button[]{button19, button20, button21, button22, button23, button24, button25, button26, button27, },
                new Button[]{button28, button29, button30, button31, button32, button33, button34, button35, button36, },
                new Button[]{button37, button38, button39, button40, button41, button42, button43, button44, button45, },
                new Button[]{button46, button47, button48, button49, button50, button51, button52, button53, button54, },
                new Button[]{button55, button56, button57, button58, button59, button60, button61, button62, button63, },
                new Button[]{button64, button65, button66, button67, button68, button69, button70, button71, button72, },
                new Button[]{button73, button74, button75, button76, button77, button78, button79, button80, button81, }
            };
            return allButtons;
        }

        public void ChangeMap()
        {
            for (int h = 0; h < 9; h++)
                for (int j = 0; j < 9; j++)
                {
                    Position position = new Position();
                    position.X = h; position.Y = j;
                    if ((gameBoard.GetUnitAtPosition(position) == null))
                    {
                        buttons()[j][h].BackgroundImage = null;
                    }
                    else
                        foreach (Player player in players)
                            foreach (UnitBase unit in player.Units)
                                if (unit.CurrentPosition != null && unit.CurrentPosition.X == h && unit.CurrentPosition.Y == j)
                                {
                                    image = unit.Image;
                                    System.Drawing.Image part = new Bitmap(50, 50);
                                    Graphics g = Graphics.FromImage(part);
                                    g.DrawImage(image, new Rectangle(0, 0, 58, 62), 0, 0, 85, 90, GraphicsUnit.Pixel);
                                    buttons()[j][h].BackgroundImage = part;
                                }
                }
        }

        // Получение ближайшей цели для атаки
        public UnitBase GetNearestTarget(Player attackerPlayer, UnitBase attacker)
        {
            Player targetPlayer = players.Find(player => player != attackerPlayer);
            UnitBase nearestTarget = null;
            double nearestDistanceSquared = double.MaxValue;
            // Поиск ближайшей цели среди юнитов противника
            foreach (UnitBase target in targetPlayer.Units)
            {
                if (target.State && target.CurrentPosition != null && target.Color != attacker.Color)
                {
                    double distanceSquared = GetDistanceSquared(attacker.CurrentPosition, target.CurrentPosition);
                    // Обновление ближайшей цели, если найдена более близкая
                    if (distanceSquared < nearestDistanceSquared)
                    {
                        nearestDistanceSquared = distanceSquared;
                        nearestTarget = target;
                    }
                }
            }
            return nearestTarget;
        }

        // Игровой цикл для раунда
        private void GameLoop() // раунд
        {
            System.Threading.Thread.Sleep(5);
            ChangeMap();
            bool flag = true;
            while (flag)
            {
                foreach (Player player in players)
                {
                    System.Threading.Thread.Sleep(5);
                    ChangeMap();
                    foreach (UnitBase unit1 in gameBoard.Board.Values.ToList())
                    {
                        buttons()[unit1.CurrentPosition.Y][unit1.CurrentPosition.X].FlatStyle = FlatStyle.Flat;
                        if (unit1.Color == Colors.green)
                            buttons()[unit1.CurrentPosition.Y][unit1.CurrentPosition.X].FlatAppearance.BorderColor = Color.Green;
                        else buttons()[unit1.CurrentPosition.Y][unit1.CurrentPosition.X].FlatAppearance.BorderColor = Color.Red;
                        buttons()[unit1.CurrentPosition.Y][unit1.CurrentPosition.X].FlatAppearance.BorderSize = 1;
                        System.Threading.Thread.Sleep(5);
                        ChangeMap();
                        UnitBase target = GetNearestTarget(player, unit1);
                        // Атака цели
                        if (target != null)
                        {
                            System.Threading.Thread.Sleep(5);
                            ChangeMap();
                            unit1.Attack(target, gameBoard, players);
                            // Удаление юнита из игры, если он уничтожен
                            if (!unit1.State)
                            {
                                gameBoard.RemoveUnit(unit1);
                                player.RemoveUnit(unit1);
                            }
                        }
                    }

                }
                System.Threading.Thread.Sleep(5);
                ChangeMap();

                bool player1Alive = players[0].Units.Any(unit => unit.State);
                bool player2Alive = players[1].Units.Any(unit => unit.State);

                // Если у одного из игроков не осталось живых юнитов, завершаем игру
                if (!player1Alive || !player2Alive)
                {
                    label1.Text = "Игра завершена. Результат:";
                    if (player1Alive && !player2Alive)
                    {
                        flag = false;
                        players[0].Coins += 2;
                        players[1].Coins += 1;
                        System.Threading.Thread.Sleep(5);
                        ChangeMap();
                        label1.Text += $"\nИгрок 1 победил!\nПобедитель получает 2 монеты, проигравший 1 монету\nОсталось юнитов у игрока 1: {gameBoard.Board.Count}.";
                        Data.Value = gameBoard;
                    }
                    else if (!player1Alive && player2Alive)
                    {
                        flag = false;
                        players[1].Coins += 2;
                        players[0].Coins += 1;
                        System.Threading.Thread.Sleep(5);
                        ChangeMap();
                        label1.Text += $"\nИгрок 2 победил!\nПобедитель получает 2 монеты, проигравший 1 монету\nОсталось юнитов у игрока 2: {players[1].Units.Count}.";
                        Data.Value = gameBoard;
                    }
                    else
                    {
                        flag = false;
                        players[1].Coins += 1;
                        players[0].Coins += 1;
                        System.Threading.Thread.Sleep(5);
                        ChangeMap();
                        label1.Text += $"\nНичья! Оба игрока потеряли все юниты.\nОба игрока получили по 1 монете.";
                        Data.Value = gameBoard;
                    }


                }
            }
        }

        // Вычисление квадрата расстояния между двумя позициями
        private double GetDistanceSquared(Position position1, Position position2)
        {
            int dx = position2.X - position1.X;
            int dy = position2.Y - position1.Y;
            return dx * dx + dy * dy;
        }

        public StartGame(GameBoard gameBoard, List<Player> players)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.MaximizeBox = false;
            this.gameBoard = gameBoard;
            this.players = players;
            ChangeMap();
        }

        private void button82_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                GameLoop();
            }
        }
    }
}
