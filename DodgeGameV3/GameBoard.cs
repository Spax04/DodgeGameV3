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
        public int lvlCounter = 1;
        public int scoreCounter = 0;
        public int point = 10;
        public bool isLost = false;
        

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

        public void lostCheck(DispatcherTimer timer,UnitTool p1)
        {
            if (player.isDead == true)
            {
                isLost = true;
                timer.Stop();
            }

            /*if (player.heartCounter == 0)
            {
                p1._speed = 0;
                isLost = true;
                timer.Stop();
            }*/

        }
        public void winCheck(DispatcherTimer timer,UnitTool p1)
        {
            int count = 0;
            

            for(int i = 0; i < enemy.Length; i++)
            {
                if(enemy[i].isAlive != true)
                {
                    int a = count;
                    count++;
                    if(count > a)
                    {
                        scoreCounter += point;
                    }
                }
            }

            if(count == enemy.Length -1 )
            {
                p1._speed = 0;
               lvlCounter++;
                timer.Stop();
            }
            
        }

        public void lvlGrowing()
        {
            if(lvlCounter >= 2)
            for(int i = 0; i < enemy.Length; i++)
            {
                enemy[i]._speed = lvlCounter / 2;
            }
        }

        public void scoreCheck(TextBlock text)
        {
            for(int i = 0; i < enemy.Length; i++)
            {
                text.Text = enemy[i].scroeBoard.ToString();
            }
        }

        public void boarderCollisionCheck(Rectangle rect,UnitTool ut,Canvas myCanvas)
        {
            if(ut._x  < 0)
            {
                ut._x = (int)_boardWidth;  
                Canvas.SetLeft(rect, ut._x);
            }
            else if (ut._x  > _boardWidth)
            {
                ut._x = 0;
                Canvas.SetLeft(rect, ut._x);
            }else if(ut._y < 0)
            {
                ut._y = (int)_boardHeight;
                Canvas.SetTop(rect, ut._y);
            }else if(ut._y > _boardHeight)
            {
                ut._y = 0;
                Canvas.SetTop(rect,ut._y);
            }
        }
    }
}
