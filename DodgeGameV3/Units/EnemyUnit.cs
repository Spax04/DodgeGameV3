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
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGameV3.Units
{
    public class EnemyUnit : UnitTool
    {
        public bool isAlive = true;

        public EnemyUnit(int x, int y,Rectangle rectangle,int points,int scoreboard) : base(x, y, 30, 30,1,rectangle)
        {
            
        }

        public override Rectangle createNewRectangle(Rectangle rectangle, UnitTool ut,Canvas canvas)
        { 
            rectangle = new Rectangle();
            rectangle.Width = ut._width;
            rectangle.Height = ut._height;
            rectangle.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Shark.png"))
            };
            Canvas.SetLeft(rectangle, ut._x);
            Canvas.SetTop(rectangle, ut._y);
            canvas.Children.Add(rectangle);
            return rectangle;
        }

        public void Move(Rectangle enemyRect, UnitTool e1, UnitTool p1)
        {
            if (e1._x > p1._x)
            {
                Canvas.SetLeft(enemyRect, Canvas.GetLeft(enemyRect) - e1._speed);
                e1._x = (int)Canvas.GetLeft(enemyRect) - e1._speed;
            }
            if (e1._x < p1._x)
            {
                Canvas.SetLeft(enemyRect, Canvas.GetLeft(enemyRect) + e1._speed);
                e1._x = (int)Canvas.GetLeft(enemyRect) + e1._speed;
            }
            if (e1._y > p1._y)
            {
                Canvas.SetTop(enemyRect, Canvas.GetTop(enemyRect) - e1._speed);
                e1._y = (int)Canvas.GetTop(enemyRect) - e1._speed;
            }
            if (e1._y < p1._y)
            {
                Canvas.SetTop(enemyRect, Canvas.GetTop(enemyRect) + e1._speed);
                e1._y = (int)Canvas.GetTop(enemyRect) + -e1._speed;
            }
        }

        public async override void collisionCheck(Rectangle enemyRectangle, UnitTool enemyRectOne, UnitTool enemyRectTwo,Canvas myCanvas,DispatcherTimer timer)
        {
            if (enemyRectOne._x + enemyRectOne._width >= enemyRectTwo._x &&
                enemyRectOne._x <= enemyRectTwo._x + enemyRectTwo._width &&
                enemyRectOne._y + enemyRectOne._height >= enemyRectTwo._y &&
                enemyRectOne._y <= enemyRectTwo._y + enemyRectTwo._height)
            {
                
                //style part: 
                enemyRectangle.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/DeadFish2.png"))
                };
                enemyRectangle.Width = 50;
                enemyRectangle.Height = 50;
                enemyRectOne._speed = 0;
              
                isAlive = false;
                await Task.Delay(4000); //async pause 

                myCanvas.Children.Remove(enemyRectangle);
                Canvas.SetLeft(enemyRectangle, 0);
                Canvas.SetTop(enemyRectangle, 0);
            }
        }

    }
}
