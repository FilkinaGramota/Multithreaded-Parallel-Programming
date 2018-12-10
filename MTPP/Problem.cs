using System;
using System.Collections.Generic;
using System.Text;

namespace MTPP
{
    class Problem
    {
        public double Left { get; set; }
        public double Right { get; set; }
        public Func<double, double> F;

        public Problem(Func<double,double> f, double left, double right)
        {
            F = f;
            Left = left;
            Right = right;
        }

        public double Middle
        {
            get { return (Left + Right) / 2; }
        }

        public double Length
        {
            get { return (Right - Left); }
        }

        public double Square
        {
            get { return ((F(Left) + F(Right)) / 2) * Length; }
        }

        public double SquareSum
        {
            get { return ((F(Left) + 2 * F(Middle) + F(Right)) / 2) * Length / 2; }
        }
       
    }
}
