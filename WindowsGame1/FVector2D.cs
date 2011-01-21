using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CobaltAlchemy
{
    class FVector2D
    {
        public double x, y;

        public FVector2D(double _x, double _y)
        {
            x = _x; 
            y = _y;
        }

        public double getAngle()
        {
            return Math.Atan2(y, x);
        }

        public double getAngleFrom(FVector2D v)
        {
            return Math.Atan2(this.y-v.y, this.x-v.x);
        }

        public double getMagnitude()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public void add(FVector2D v, FVector2D n)
        {
            if (v != null && n != null)
            {
                n.x = v.x + this.x;
                n.y = v.y + this.y;
            }
        }

        public void subtract(FVector2D v, FVector2D n)
        {
            if (v != null && n != null)
            {
                n.x = this.x - v.x;
                n.y = this.y - v.y;
            }
        }

        public void translate(double _x, double _y, FVector2D n)
        {
            n.x = x + _x;
            n.y = y + _y;
        }

        public void scale(double scalar, FVector2D n)
        {
            n.x = x*scalar;
            n.y = y * scalar;
        }

        public void rotate(double angle, FVector2D n)
        {
            double newX = x*Math.Cos(angle) - y*Math.Sin(angle);
            double newY = y * Math.Cos(angle) + x * Math.Sin(angle);
            n.x = newX;
            n.y = newY;
        }
    }
}
