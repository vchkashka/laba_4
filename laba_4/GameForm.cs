using laba_3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_4
{
    public partial class GameForm : Form
    {
        [Serializable]
        public class SaveData
        {
            public GameBoard GameBoard { get; set; }
            public List<Player> Players { get; set; }
        }

        static Player player1 = new Player(Colors.green);
        static Player player2 = new Player(Colors.red);
        GameBoard gameBoard = new GameBoard();
        private List<Player> players = new List<Player> { player1, player2 };
        private bool UnitsAreSet(Player player1, Player player2)
        {
            // Проверяем, что у обоих игроков есть юниты
            return player1.Units.Count > 0 && player2.Units.Count > 0;
        }

        public GameForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            pictureBox1.Image = Image.FromFile("C:\\Users\\User\\Desktop\\игроки.png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (PlayerForm form = new PlayerForm(players[0], gameBoard))
            {
                form.ShowDialog();
            }
            if (Data.Player1 != null)
                players[0] = Data.Player1;
            if (Data.Value != null)
                gameBoard = Data.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (PlayerForm form = new PlayerForm(players[1], gameBoard))
            {
                form.ShowDialog();
            }
            if (Data.Player2 != null)
                players[1] = Data.Player2;
            if (Data.Value != null)
                gameBoard = Data.Value;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (UnitsAreSet(players[0], players[1]) && gameBoard.Board.Count > 0)
            {
                using (StartGame form = new StartGame(gameBoard, players))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Расставьте юнитов для обоих игроков перед началом игры.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            players[0] = Data.Player1;
            players[1] = Data.Player2;
            gameBoard = Data.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (ViewBoard form = new ViewBoard(gameBoard))
            {
                form.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveData data = new SaveData()
            {
                GameBoard = gameBoard,
                Players = players
            };
            BinaryFormatter formatter = new BinaryFormatter();
            // Сериализация объекта и сохранение в файл
            using (FileStream fs = new FileStream("game.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);
            }
            MessageBox.Show("Игра сохранена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Десериализация объекта из файла
            using (FileStream fs = new FileStream("game.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SaveData saveData = (SaveData)formatter.Deserialize(fs);
                // Присвоение сохраненных значений текущим объектам
                gameBoard = saveData.GameBoard;
                players = saveData.Players;
            }
            MessageBox.Show("Игра загружена.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
