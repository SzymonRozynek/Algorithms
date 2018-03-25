using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class CountingSort : SortingAlgorithm {

        public override string GetName() {
            return "Counting Sort";
        }

        public override bool IsInPlace()
        {
            return false;
        }

        protected override void SortElements(Element[] elements) {
            int max = 0;
            foreach(Element e in elements) {
                if (e.value > max)
                    max = e.value;
            }
            int[] c = new int[max + 1];
            foreach(Element e in elements) {
                c[e.value]++;
            }
            for(int i = 1; i <= max; i++) {
                c[i] += c[i - 1];
            }
            Element[] b = new Element[elements.Length];
            for(int i = elements.Length - 1; i >= 0; i--) {
                b[c[elements[i].value] - 1] = elements[i];
                c[elements[i].value]--;
            }
            for(int i = 0; i < elements.Length; i++) {
                elements[i] = b[i];
            }
        }

    }
}
