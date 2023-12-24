using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace laba_3
{
    [Serializable]
    public class GameBoard
    {
        private Dictionary<Position, UnitBase> board;

        public Dictionary<Position, UnitBase> Board { get { return board; } }

        public GameBoard()
        {
            board = new Dictionary<Position, UnitBase>();
        }

        // Метод для размещения юнита на доске
        public void PlaceUnit(UnitBase unit, Position position)
        {
            if (unit != null)
            {
                // Размещение юнита на доске
                board[position] = unit;
                unit.CurrentPosition = position;
            }
        }

        // Метод для удаления юнита с доски
        public void RemoveUnit(UnitBase unit)
        {
            board.Remove(unit.CurrentPosition);
        }


        // Метод для перемещения юнита на доске
        public void MoveUnit(Position position, UnitBase unit)
        {
            unit.CurrentPosition = position;
        }


        // Метод для получения юнита на указанной позиции
        public UnitBase GetUnitAtPosition(Position position)
        {
            foreach (var kvp in board)
            {
                if (kvp.Key.X == position.X && kvp.Key.Y == position.Y)
                {
                    return kvp.Value;
                }
            }
            return null;
        }
    }
}