using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MTPP
{
    class TaskCreator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private List<Task> tasks;
        private Solver Solver;

        private int numberOfTasks;
        public int NumberOfTasks
        {
            get { return numberOfTasks; }
            set
            {
                numberOfTasks = value;
                //OnPropertyChanged(nameof(NumberOfTasks));
            }
        }

        private Func<double, double> function;
        public Func<double,double> Function
        {
            get { return function; }
            set
            {
                function = value;
                //OnPropertyChanged(nameof(Function));
            }
        }

        private double left;
        public double Left
        {
            get { return left; }
            set
            {
                left = value;
                //OnPropertyChanged(nameof(Left));
            }
        }

        private double right;
        public double Right
        {
            get { return right; }
            set
            {
                right = value;
                //OnPropertyChanged(nameof(Right));
            }
        }

        private double eps;
        public double Eps
        {
            get { return eps; }
            set
            {
                eps = value;
                //OnPropertyChanged(nameof(Eps));
            }
        }

        public TaskCreator(Func<double, double> function, double left, double right, double eps = 0.001, int numberOfTasks = 3)
        {
            NumberOfTasks = numberOfTasks;
            Function = function;
            Left = left;
            Right = right;
            Eps = eps;
            Solver = new Solver(function, left, right, eps);
            tasks = new List<Task>(numberOfTasks);

            //PropertyChanged += ProblemPropertyChangedHandler;
        }

        public double Solving()
        {
            for (int i = 0; i < NumberOfTasks; i++)
            {
                tasks.Add(Task.Factory.StartNew(Solver.Solve));
            }

            int indexOfTask = Task.WaitAny(tasks.ToArray());
            
            while (!Solver.Stack.IsEmpty)
            {
                tasks.RemoveAt(indexOfTask);
                if (tasks.Count == 0)
                    tasks.Add(Task.Factory.StartNew(Solver.Solve));
                indexOfTask = Task.WaitAny(tasks.ToArray());
            }
            
            return Solver.Solving;
        }

        public void OnNext()
        {
            tasks.Clear();
            tasks = new List<Task>(NumberOfTasks);
            Solver = new Solver(Function, Left, Right, Eps);
        }

        /*private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/

        /*private void ProblemPropertyChangedHandler(object sender, PropertyChangedEventArgs eventArgs)
        {
            Console.WriteLine($"Something changed... {eventArgs.PropertyName}\n");
            tasks.Clear();

            if (eventArgs.PropertyName.Equals(nameof(NumberOfTasks)))
                tasks = new List<Task>(NumberOfTasks);

            Solver = new Solver(Function, Left, Right, Eps);
        }*/
    }
}
