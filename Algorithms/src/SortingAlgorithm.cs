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
            deltaTime = stopwatch.ElapsedMilliseconds;
        }

        //in ms
        public long GetElapsedTime() {
            return deltaTime;
        }

        public int GetIterationCount() {
            return iterationCount;
        }

        protected abstract void SortElements(Element[] elements);
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
