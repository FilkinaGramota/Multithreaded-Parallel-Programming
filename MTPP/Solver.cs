using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MTPP
{
    class Solver
    {
        private object locker = new object();
        public ConcurrentStack<Problem> stack { get; private set; }
        
        public double Solving { get; private set; }
        public double Eps { get; private set; }

        public Solver(Func<double, double> f, double left, double right, double eps)
        {
            if (eps <= 0)
                throw new ArgumentException("Precision must be greater then zero", nameof(Eps));

            Eps = eps;
            stack = new ConcurrentStack<Problem>();
            stack.Push(new Problem(f, left, right));
        }

        public void Solve()
        { 
            Problem problem;

            lock (locker)
            {
                if (stack.TryPop(out problem))
                {
                    if (Math.Abs(problem.Square - problem.SquareSum) > Eps)
                    {
                        stack.Push(new Problem(problem.Function, problem.Left, problem.Middle));
                        stack.Push(new Problem(problem.Function, problem.Middle, problem.Right));
                    }
                    else
                    {
                        Solving = Solving + problem.SquareSum;
                        Console.WriteLine($"[Task {Task.CurrentId}] Solving = {Solving}");
                    }
                }
            }
        }
    }
}
