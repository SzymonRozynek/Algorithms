using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class Program {

        public void Start() {
            Console.WriteLine("Choose a task");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            int input = InputParameter();
            switch(input) {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                default:
                    Console.WriteLine("No task with such index");
                    break;
            }
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        private void Task1() {
            Console.WriteLine("Insert number of elements");
            int n = InputParameter();
            Console.WriteLine("Insert maximum value of elements");
            int max = InputParameter();

            Element[] elements = GenerateElements(n, max);

            SortingAlgorithm s = InputAlgorithm();
            Visualization g = new Visualization(elements);
            g.StartWithNewThread();
            s.Sort(elements);
            Console.WriteLine("Time: " + s.GetElapsedTime());
        }

        private void Task2() {
            Console.WriteLine("Insert the first number of elements");
            int nm = InputParameter();
            SortingAlgorithm s = InputAlgorithm();
            Table table = new Table(3, 21);
            table.StartWithNewThread();
            table.AddLine(new string[] {"Number of elements", "Time (in seconds)", "Number of iterations"});
            for (int i = 0; i < 20; i++) {
                int n = nm * (i + 1);
                Element[] elements = GenerateElements(n, n);
                s.Sort(elements);
                string[] line = {n.ToString(), ((float)s.GetElapsedTime()/1000).ToString(), s.GetIterationCount().ToString() };
                table.AddLine(line);
            }

        }

        private SortingAlgorithm InputAlgorithm() {
            SortingAlgorithm s = null;
            Console.WriteLine("Choose a sorting algorithm");
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(SortingAlgorithm));
            int i = 1;
            foreach (var x in types) {
                SortingAlgorithm temp = (SortingAlgorithm)Activator.CreateInstance(x);
                Console.WriteLine(i + ": " + temp.GetName());
                i++;
            }
            while (s == null) {
                int input = InputParameter();
                i = 1;
                foreach (var x in types) {
                    if (i == input)
                        s = (SortingAlgorithm)Activator.CreateInstance(x);
                    i++;
                }
                if (s == null)
                    Console.WriteLine("There is no algorithm with such index");
            }
            return s;
            
        }

        private int InputParameter() {
            int n = -1;
            while (n < 0) {
                try {
                    n = int.Parse(Console.ReadLine());
                    if(n < 0) {
                        Console.WriteLine("Parameter must be a non-negative integer! Please try again.");
                    }
                } catch (FormatException) {
                    Console.WriteLine("Invalid format! Please try again.");
                } catch (OverflowException) {
                    Console.WriteLine("Number is too large!");
                }
            }
            return n;
        }

        private Element[] GenerateElements(int count, int maxValue) {
            Element[] elements = new Element[count];
            Random rand = new Random();
            for (int i = 0; i < count; i++) {
                elements[i] = new Element() {
                    value = rand.Next(maxValue) + 1,
                    index = i + 1
                };
            }
            return elements;
        }
    }

    public class Element {
        public int value;
        public int index;
    }
}
