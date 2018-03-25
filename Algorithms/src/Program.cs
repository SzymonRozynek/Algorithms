using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class Program {

        public void Start() {
            int input = -1;
            while (input != 0) {
                Console.WriteLine("--------------");
                Console.WriteLine("Choose a task");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Implementation");
                Console.WriteLine("2. Diagnostics");
                Console.WriteLine("3. Quick sort and insertion sort for each distribution");
                Console.WriteLine("4. Counting Sort and Quick Sort comparison");
                input = InputParameter();
                switch (input) {
                    case 0:
                        break;
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    default:
                        Console.WriteLine("No task with such index");
                        break;
                }
            }
        }

        private void Task1() {
            Console.WriteLine("Insert number of elements");
            int n = InputParameter();
            Console.WriteLine("Insert maximum value of elements");
            int max = InputParameter();

            Element[] elements = GenerateElements(n, max);

            SortingAlgorithm s = InputAlgorithm();
            if (s.IsInPlace()) {
                Visualization g = new Visualization(elements);
                g.StartWithNewThread();
            }
            s.Sort(elements);
            Console.WriteLine(s.GetName() + " has finished");
            if (s.Stable) {
                Console.WriteLine(s.GetName() + " is stable");
            }
            else {
                Console.WriteLine(s.GetName() + " is not stable");
            }
            Console.WriteLine("Time: " + s.GetElapsedTime());
        }

        private void Task2() {
            Console.WriteLine("Insert the first number of elements (pref 200-500)");
            int nm = InputParameter();
            Table table = new Table(3, 21);
            table.StartWithNewThread();
            Graph g1 = new Graph("Number of elements", "Time[s]");
            Graph g2 = new Graph("Number of elements", "Time[s]");
            T2(new SortingAlgorithm[] { new InsertionSort(), new BoubleSort(), new SelectionSort() }, table, nm, g1);
            T2(new SortingAlgorithm[]{ new QuickSort(), new HeapSort(), new CountingSort()}, table, 100*nm, g2);
            g1.StartWithNewThread();
            g2.StartWithNewThread();
        }


        private void T2(SortingAlgorithm[] ss, Table table, int nm, Graph g) {
            foreach (SortingAlgorithm s in ss) {
                Table.Tab tab = new Table.Tab(s.GetName());
                table.AddTab(tab);
                tab.AddLine(new string[] { "Number of elements", "Time (in seconds)", "Number of iterations" });
                Graph.Data pointData = new Graph.Data(s.GetName());
                for (int i = 0; i < 20; i++) {
                    int n = nm * (i + 1);
                    Element[] elements = GenerateElements(n, n);
                    s.Sort(elements);
                    pointData.AddPoint(n, ((float)s.GetElapsedTime() / 1000));
                    string[] line = { n.ToString(), ((float)s.GetElapsedTime() / 1000).ToString("0.00"), s.GetIterationCount().ToString() };
                    tab.AddLine(line);
                }
                g.AddData(pointData);
            }
        }

        private void Task3() {
            SortingAlgorithm[] algs = { new QuickSort(), new InsertionSort() };
            Console.WriteLine("Insert the first number of elements (pref 400-800)");
            int nm = InputParameter();
            Table table = new Table(3, 21);
            table.StartWithNewThread();
            for (int k = 0; k < 2; k++) {
                string distr = k == 0 ? "(Random)" : "(Ascending)";
                Graph g = new Graph("Number of elements", "Time[s]");
                foreach (SortingAlgorithm s in algs) {
                    Table.Tab tab = new Table.Tab(s.GetName() + " " + distr);
                    table.AddTab(tab);
                    tab.AddLine(new string[] { "Number of elements", "Time (in seconds)", "Number of iterations" });
                    Graph.Data pointData = new Graph.Data(s.GetName() + " " + distr);
                    for (int i = 0; i < 20; i++) {
                        int n = nm * (i + 1);
                        Element[] elements = GenerateElements(n, n, k == 0 ? Distribution.Random : Distribution.Ascending);
                        s.Sort(elements);
                        pointData.AddPoint(n, ((float)s.GetElapsedTime() / 1000));
                        string[] line = { n.ToString(), ((float)s.GetElapsedTime() / 1000).ToString("0.00"), s.GetIterationCount().ToString() };
                        tab.AddLine(line);
                    }
                    g.AddData(pointData);
                }
                g.StartWithNewThread();
            }
        }

        private void Task4() {
            SortingAlgorithm[] algs = { new CountingSort(), new QuickSort() };
            Console.WriteLine("Insert the first number of elements (pref 5000-15000)");
            int nm = InputParameter();
            Table table = new Table(3, 21);
            table.StartWithNewThread();
            Graph[] g = new Graph[2];
            for (int k = 0; k < 2; k++) {
                string type = k == 0 ? "[1, 100*n]" : "[1, 0.01*n]";
                g[k] = new Graph("Number of elements", "Time[s]");
                foreach (SortingAlgorithm s in algs) {
                    Table.Tab tab = new Table.Tab(s.GetName() + " " + type);
                    table.AddTab(tab);
                    tab.AddLine(new string[] { "Number of elements", "Time (in seconds)", "Number of iterations" });
                    Graph.Data pointData = new Graph.Data(s.GetName() + " " + type);
                    for (int i = 0; i < 20; i++) {
                        int n = nm * (i + 1);
                        Element[] elements = GenerateElements(n, k == 0 ? n*100 : (int)(n*0.01));
                        s.Sort(elements);
                        pointData.AddPoint(n, ((float)s.GetElapsedTime() / 1000));
                        string[] line = { n.ToString(), ((float)s.GetElapsedTime() / 1000).ToString("0.00"), s.GetIterationCount().ToString() };
                        tab.AddLine(line);
                    }
                    g[k].AddData(pointData);
                }
            }
            g[0].StartWithNewThread();
            g[1].StartWithNewThread();
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

        private Element[] GenerateElements(int count, int maxValue, Distribution dis = Distribution.Random) {
            Element[] elements = new Element[count];
            if (dis == Distribution.Random) {
                Random rand = new Random();
                for (int i = 0; i < count; i++) {
                    elements[i] = new Element() {
                        value = rand.Next(maxValue) + 1,
                        index = i + 1
                    };
                }
            } else if (dis == Distribution.Ascending) {
                for (int i = 0; i < count; i++) {
                    elements[i] = new Element() {
                        value = i + 1,
                        index = i + 1
                    };
                }
            }
            return elements;
        }
    }

    public class Element {
        public int value;
        public int index;
    }

    public enum Distribution {
        Random, Ascending
    }
}
