using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DodgeGameV3.Units;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;
using System.Windows;
using Windows.Media.Devices;
using System.Threading.Tasks;

namespace DodgeGameV3
{
    public class GameBoard
    {
        Rectangle rectangle;
        public PlayerUnit player;
        public EnemyUnit[] enemy;
        

        public double _boardWidth, _boardHeight;
        Random random = new Random();

        public GameBoard(double boardWidth, double boardHeight)
        {
            this._boardWidth = boardWidth;
            this._boardHeight = boardHeight;

            player = new PlayerUnit((int)_boardWidth / 2, (int)_boardHeight / 2,rectangle);

            enemy = new EnemyUnit[10];

            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i] = new EnemyUnit(random.Next(30, (int)_boardWidth - 30), random.Next(30, (int)_boardHeight - 30),rectangle);
            }
        }
    }
}
