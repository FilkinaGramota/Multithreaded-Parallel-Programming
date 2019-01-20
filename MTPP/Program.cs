using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MTPP
{
    class Program
    {
        static int n = 4; // количество Task

        public static void ProblemSolving(Func<double, double> f, double left, double right, double eps)
        {
            List<Task> tasks = new List<Task>(n);
            Solver solver = new Solver(f, left, right, eps);

            // создаем и запускаем Tasks, которым указываем метод Solve для работы
            for (int i = 0; i < n; i++)
            {
                tasks.Add(Task.Factory.StartNew(solver.Solve));
            }

            // берем номер Task, которая уже свое отработала
            int indexOfTask = Task.WaitAny(tasks.ToArray());

            // пока стек (портфель задач) не опустеет
            while (!solver.Stack.IsEmpty)
            {
                tasks.RemoveAt(indexOfTask); // удаляем из списка tasks отработавшую Task
                if (tasks.Count == 0) // если нет больше рабочих потоков, а задача еще не решена
                    tasks.Add(Task.Factory.StartNew(solver.Solve)); // добавим еще
                indexOfTask = Task.WaitAny(tasks.ToArray()); // вновь берем номер Task, которая отработала
            }
            Console.WriteLine($"Ответ = {solver.Solving}");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            double left = 0;
            double right1 = Math.PI;
            double right2 = Math.PI / 2;
            double eps = 0.001;

            ProblemSolving(x => Math.Sin(x), left, right1, eps);

            ProblemSolving(x => Math.Cos(x), left, right2, eps);

            ProblemSolving(x => Math.Exp(x), 0, 3, eps);
        }
    }
}
