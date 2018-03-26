using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    class InsertionSort : SortingAlgorithm {
        public override string GetName() {
            return "Insertion Sort";
        }

        public override bool IsInPlace() {
            return true;
        }

        protected override void SortElements(Element[] elements) {
            int start = 1;
            for(int i = start; i < elements.Length; i++) {
                for(int k = start; k > 0; k--) {
                    if(elements[k].value < elements[k - 1].value) {
                        SwapElements(elements, k, k - 1);
                        IterationTick();
                    }
                    else {
                        break;
                    }
                }
                start++;
            }
        }
    }
}
