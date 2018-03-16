using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class SelectionSort : SortingAlgorithm {

        public override string GetName() {
            return "Selection Sort";
        }

        protected override void SortElements(Element[] elements) {
            int start = 0;
            int end = elements.Length - 1;
            for(int i = 0; i < elements.Length - 1; i++) {
                int min = FindMin(elements, start, end);
                SwapElements(elements, start, min);
                start++;
            }
        }

        private int FindMin(Element[] elements, int start, int end) {
            int index = start;
            Element minElement = elements[start];
            for(int i = start + 1; i <= end; i++) {
                if(elements[i].value <= minElement.value) {
                    if (!(elements[i].value == minElement.value & elements[i].index > minElement.index)) {
                        minElement = elements[i];
                        index = i;
                    }
                }
                Thread.Sleep(delay);
            }
            return index;
        }
    }
}
