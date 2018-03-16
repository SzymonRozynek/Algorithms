using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    abstract class SortingAlgorithm {

        private int deltaTime = 0;
        protected int delay = 0;

        public void Sort(Element[] elements) {
            int t = DateTime.Now.Millisecond;
            SortElements(elements);
            deltaTime = DateTime.Now.Millisecond - t;
        }

        public void Sort(Element[] elements, int delay) {
            this.delay = delay;
            Sort(elements);
        }

            //in ms
            public int GetElapsedTime() {
            return deltaTime;
        }

        protected abstract void SortElements(Element[] elements);
        public abstract string GetName();

        protected void SwapElements(Element[] elements, int index1, int index2) {
            Element temp = elements[index2];
            elements[index2] = elements[index1];
            elements[index1] = temp;
        }
    }
}
