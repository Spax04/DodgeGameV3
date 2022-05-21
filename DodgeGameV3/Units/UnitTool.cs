using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGameV3.Units
{
    public class UnitTool
    {
        public int _x, _y;
        public int _width , _height;
        public int _speed;

        public UnitTool(int x,int y,int width,int height,int speed)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _speed = speed;

        }
    }
}
