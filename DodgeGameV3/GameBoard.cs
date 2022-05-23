using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DodgeGameV3.Units;

namespace DodgeGameV3
{
    public class GameBoard
    {
        public PlayerUnit player;
        public EnemyUnit[] enemy;

        public double _boardWidth, _boardHeight;
        Random random = new Random();

        public GameBoard(double boardWidth, double boardHeight)
        {
            this._boardWidth = boardWidth;
            this._boardHeight = boardHeight;

            player = new PlayerUnit((int)_boardWidth / 2, (int)_boardHeight / 2);

            enemy = new EnemyUnit[10];

            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i] = new EnemyUnit(random.Next(30, (int)_boardWidth - 30), random.Next(30, (int)_boardHeight - 30));
            }
        }
    }
}
