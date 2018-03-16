using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class Program {

        private Element[] elements;

        public void Start() {
            Console.WriteLine("Insert number of elements");
            int n = InputParameter();
            Console.WriteLine("Insert maximum value of elements");
            int max = InputParameter();
            Console.WriteLine("Insert a delay time between each iteration (0 = no delay)");
            int delay = InputParameter();

            GenerateElements(n, max);

            SortingAlgorithm s = InputAlgorithm();
            Thread graphThread = new Thread(new ThreadStart(DrawGraph));
            graphThread.Start();
            s.Sort(elements, delay);

            PrintElements();

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
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
                } catch (FormatException e) {
                    Console.WriteLine("Invalid format! Please try again.");
                } catch (OverflowException e) {
                    Console.WriteLine("Number is too large!");
                }
            }
            return n;
        }

        private void DrawGraph() {
            Graph g = new Graph();
            g.Draw(elements);
        }

        private void GenerateElements(int count, int maxValue) {
            elements = new Element[count];
            Random rand = new Random();
            for (int i = 0; i < count; i++) {
                elements[i] = new Element() {
                    value = rand.Next(maxValue) + 1,
                    index = i + 1
                };
            }
        }

        private void PrintElements() {
            foreach (Element e in elements)
                Console.WriteLine("#" + e.index + ": " + e.value);
        }
    }

    public class Element {
        public int value;
        public int index;
    }
}
