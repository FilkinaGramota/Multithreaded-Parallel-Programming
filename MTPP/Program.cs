using System;

namespace MTPP
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 4;

            double left = 0;
            double right1 = Math.PI;
            double right2 = Math.PI/2;

            double eps = 0.0001;

            TaskCreator task = new TaskCreator(x => Math.Sin(x), left, right1);
            Console.WriteLine($"Answer is {task.Solving()}\n");
            Console.ReadKey();
            
            task.Eps = eps;
            Console.WriteLine($"Answer is {task.Solving()}\n");
            Console.ReadKey();

            task.NumberOfTasks = n;
            Console.WriteLine($"Answer is {task.Solving()}\n");
            Console.ReadKey();

            task.Function = x => Math.Cos(x);
            Console.WriteLine($"Answer is {task.Solving()}\n");

            task.Right = right2;
            Console.WriteLine($"Answer is {task.Solving()}\n");
            Console.ReadKey();

            task.Left = 0;
            task.Right = 3;
            task.Function = x => (x*x);
            Console.WriteLine($"Answer is {task.Solving()}\n");
            Console.ReadKey();
        }
    }
}
