using System;
using  System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MTPP
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;

            double left = 0;
            double right1 = Math.PI;
            double right2 = Math.PI/2;

            double eps = 0.0001;

            TaskCreator task = new TaskCreator(x => Math.Sin(x), left, right1);
            Console.WriteLine($"Answer is {task.Solving()}\n");

            task.ChangeCslculatePrecision(eps);
            Console.WriteLine($"Answer is {task.Solving()}\n");

            task.CgangeNumberOfTasks(n);
            Console.WriteLine($"Answer is {task.Solving()}\n");

            task.ChangeProblem(x => Math.Cos(x));
            Console.WriteLine($"Answer is {task.Solving()}\n");

            task.ChangeProblem(left, right2);
            Console.WriteLine($"Answer is {task.Solving()}\n");

            task.ChangeProblem(x => x, 0, 4);
            Console.WriteLine($"Answer is {task.Solving()}\n");

            Console.ReadLine();
        }
    }
}
