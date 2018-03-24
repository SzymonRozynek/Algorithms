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
                Element e = elements[start];
                for(int k = 0; k < start; k++) {
                    if(e.value < elements[k].value) {
                        for(int j = start; j > k; j--) {
                            elements[j] = elements[j - 1];
                        }
                        elements[k] = e;
                        break;
                    }
                    IterationTick();
                }
                start++;
            }
        }
    }
}
