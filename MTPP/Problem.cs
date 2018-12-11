using System;

namespace MTPP
{
    class Problem
    {
        public double Left { get; private set; }
        public double Right { get; private set; }
        public Func<double, double> Function { get; private set; } // Ok, it's programming, not mathematics.

        public Problem(Func<double,double> function, double left, double right)
        {
            if (left >= right)
                throw new ArgumentException("Left must be less then right.");
            
            Function = function;
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
            get { return ((Function(Left) + Function(Right)) / 2) * Length; }
        }

        public double SquareSum
        {
            get { return ((Function(Left) + 2 * Function(Middle) + Function(Right)) / 2) * Length / 2; }
        }
       
    }
}
