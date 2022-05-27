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
   
    public class UnitTool
    {
        public int _x, _y;
        public int _width , _height;
        public int _speed;
        public Rectangle _rectangle;
        public int scroeBoard;

        public UnitTool(int x,int y,int width,int height,int speed,Rectangle rectangle)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _speed = speed;
            this._rectangle = rectangle;
        }

        public virtual Rectangle createNewRectangle(Rectangle rectangle,UnitTool ut,Canvas canvas)
        {
            return rectangle;
        }

        public async virtual void collisionCheck(Rectangle rectangle, UnitTool ut1, UnitTool ut2, Canvas myCanvas,DispatcherTimer timer)
        {

        }
    }
}
