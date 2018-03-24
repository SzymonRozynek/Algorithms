using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Algorithms {
    abstract class SortingAlgorithm {

        private long deltaTime = 0;
        protected int iterationCount = 0;

        public void Sort(Element[] elements) {
            iterationCount = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SortElements(elements);
            stopwatch.Stop();
            if(!IsSorted(elements)) {
                Console.WriteLine("Error! The algorithm failed to sort all the elements!");
            }
            if (!IsStable(elements)) {
                Console.WriteLine("The algorithm is not stable!");
            }
            deltaTime = stopwatch.ElapsedMilliseconds;
        }

        private bool IsSorted(Element[] elements) {
            for(int i = 0; i < elements.Length - 1 ; i++) {
                if (elements[i].value > elements[i + 1].value)
                    return false;
            }
            return true;
        }

        private bool IsStable(Element[] elements) {
            for (int i = 0; i < elements.Length - 1; i++) {
                if (elements[i].value == elements[i + 1].value && elements[i].index > elements[i + 1].index)
                    return false;
            }
            return true;
        }

        //in ms
        public long GetElapsedTime() {
            return deltaTime;
        }

        public int GetIterationCount() {
            return iterationCount;
        }

        protected abstract void SortElements(Element[] elements);
        //Czy algorytm wykonuje sie w miejscu
        public abstract bool IsInPlace();
        public abstract string GetName();

        protected void IterationTick() {
            iterationCount++;
        }

        protected void SwapElements(Element[] elements, int index1, int index2) {
            Element temp = elements[index2];
            elements[index2] = elements[index1];
            elements[index1] = temp;
        }
    }
}
