using System;

namespace MTPP
{
    // описание задачи, которую после будем помещать в портфель задач
    class Problem
    {
        public double Left { get; set; } // левая граница отрезка (или нижний предел
        public double Right { get; set; } // правая граница отрезка (или правый предел)
        public Func<double, double> F { get; set; } // достаточно хорошая подынтегральная функция

        public Problem(Func<double,double> function, double left, double right)
        {
            if (left >= right) // неверно указан интервал - бросаемся исключением
                throw new ArgumentException("left должно быть меньше right");
            
            F = function;
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
