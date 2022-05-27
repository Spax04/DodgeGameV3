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
using Windows.UI;

namespace DodgeGameV3.Units
{
    public class PlayerUnit : UnitTool
    {
        public bool isDead = false;
        public int _lives;
        public PlayerUnit(int x, int y,Rectangle rectangle,int lives,int point) : base(x, y, 70, 70,10,rectangle)
        {
            this._lives = lives;
        }

        public override Rectangle createNewRectangle(Rectangle rectangle,UnitTool ut,Canvas canvas)
        {
            rectangle = new Rectangle();
            rectangle.Width = ut._width;
            rectangle.Height = ut._height;
            rectangle.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/diverV2-removebg-preview (1).png"))
            };
            Canvas.SetLeft(rectangle, ut._x);
            Canvas.SetTop(rectangle, ut._y);
            canvas.Children.Add(rectangle);
            return rectangle;
        }

        public async override void collisionCheck(Rectangle playerRectangle, UnitTool p1, UnitTool e1, Canvas myCanvas,DispatcherTimer timer)
        {
            if ((p1._x - 20) + p1._width >= e1._x &&
                p1._x + 20 <= e1._x  + e1._width &&
                (p1._y - 20) + p1._height  >= e1._y  &&
                p1._y + 20 <= e1._y + e1._height)
            {
                _lives--;
                if(_lives == -1)
                {
                    p1._speed = 0;
                    isDead = true;
                }
                
            }
        }

    }
}

