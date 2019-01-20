using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MTPP
{
    class Solver
    {
        private object locker = new object(); // объект для установки блокировки (короче - замок)
        public ConcurrentStack<Problem> Stack { get; private set; } // потокобезопасный стек
        
        public double Solving { get; private set; } // решение задачи Problem
        public double Eps { get; private set; } // точность вычислений

        public Solver(Func<double, double> f, double left, double right, double eps)
        {
            Eps = eps;
            Stack = new ConcurrentStack<Problem>(); // создаем стек, который выполняет роль портфеля задач
            Stack.Push(new Problem(f, left, right)); // и кладем в него нашу задачу
        }

        // здесь описываем все действия, которые будут выполнять потоки с несчатным портфелем задач
        public void Solve()
        { 
            Problem problem; // "пустая" задача - для метода TryPop

            lock (locker) // блокировка
            {
                if (Stack.TryPop(out problem)) // если смогли достать из портфеля задачу, то записываем ее в problem
                {
                    // и работаем дальше
                    if (Math.Abs(problem.Square - problem.SquareSum) > Eps)
                    {
                        Stack.Push(new Problem(problem.F, problem.Left, problem.Middle));
                        Stack.Push(new Problem(problem.F, problem.Middle, problem.Right));
                    }
                    else
                    {
                        Solving = Solving + problem.SquareSum;
                        Console.WriteLine($"Промежуточные вычисления = {Solving}");
                    }
                }
            }
        }
    }
}
