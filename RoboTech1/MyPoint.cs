using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboTech1
{
    class MyPoint
    {
        public double X { get;  set; }
        public double Y { get;  set; }
        public int Index { get; private set; }
        public int Angle { get; set; }
        public int Angle0 { get; set; }


        public MyPoint(double x, double y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
        }
    }

    
}
