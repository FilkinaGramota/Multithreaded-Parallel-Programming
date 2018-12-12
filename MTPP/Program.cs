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

            TaskCreator taskCreator = new TaskCreator(x => Math.Sin(x), left, right1);
            Console.WriteLine($"Answer is {taskCreator.Solving()}\n");
            Console.ReadKey();

            taskCreator.Eps = eps;
            Console.WriteLine($"Answer is {taskCreator.Solving()}\n");
            Console.ReadKey();

            taskCreator.NumberOfTasks = n;
            Console.WriteLine($"Answer is {taskCreator.Solving()}\n");
            Console.ReadKey();

            taskCreator.Function = x => Math.Cos(x);
            Console.WriteLine($"Answer is {taskCreator.Solving()}\n");
            Console.ReadKey();

            taskCreator.Right = right2;
            Console.WriteLine($"Answer is {taskCreator.Solving()}\n");
            Console.ReadKey();

            taskCreator.Left = 0;
            taskCreator.Right = 3;
            taskCreator.Function = x => (x*x);
            Console.WriteLine($"Answer is {taskCreator.Solving()}\n");
            Console.ReadKey();
        }
    }
}
