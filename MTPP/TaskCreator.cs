using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTPP
{
    class TaskCreator
    {
        private List<Task> tasks;
        private Solver Solver;

        public double N { get; private set; }
        public Func<double,double> Function { get; private set; }
        public double Left { get; private set; }
        public double Right { get; private set; }
        public double Eps { get; private set; }

        public TaskCreator(Func<double, double> f, double left, double right, double eps = 0.001, int n = 3)
        {
            N = n;
            Function = f;
            Left = left;
            Right = right;
            Eps = eps;
            Solver = new Solver(f, left, right, eps);
            tasks = new List<Task>(n);
        }

        public double Solving()
        {
            for (int i = 0; i < N; i++)
            {
                tasks.Add(Task.Factory.StartNew(Solver.Solve));
            }

            int indexOfTask = Task.WaitAny(tasks.ToArray());

            while (!Solver.stack.IsEmpty)
            {
                tasks.RemoveAt(indexOfTask);
                tasks.Add(Task.Factory.StartNew(Solver.Solve));
                indexOfTask = Task.WaitAny(tasks.ToArray());
            }

            return Solver.Solving;
        }

        public void ChangeCslculatePrecision(double eps)
        {
            Eps = eps;
            tasks.Clear();
            Solver = new Solver(Function, Left, Right, eps);
            Console.WriteLine($"Changing calculate precision: {eps}\n");
        }

        public void CgangeNumberOfTasks(int n)
        {
            N = n;
            tasks.Clear();
            tasks = new List<Task>(n);
            Solver = new Solver(Function, Left, Right, Eps);
            Console.WriteLine($"Changing number of tasks: {n}\n");
        }

        public void ChangeProblem(Func<double,double> f, double left, double right)
        {
            Function = f;
            Left = left;
            Right = right;
            tasks.Clear();
            Solver = new Solver(f, left, right, Eps);
            Console.WriteLine($"Changing problem (new function & new interval)\n");
        }

        public void ChangeProblem(Func<double, double> f)
        {
            Function = f;
            tasks.Clear();
            Solver = new Solver(f, Left, Right, Eps);
            Console.WriteLine($"Changing problem (new function)\n");
        }

        public void ChangeProblem(double left, double right)
        {
            Left = left;
            Right = right;
            tasks.Clear();
            Solver = new Solver(Function, left, right, Eps);
            Console.WriteLine($"Changing problem (new interval)\n");
        }
    }
}
