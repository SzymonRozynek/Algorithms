using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class QuickSort2 : SortingAlgorithm {

        public override string GetName() {
            return "Quick Sort [Utmost]";
        }

        public override bool IsInPlace() {
            return true;
        }

        void QS(Element[] elements, int s, int e) {
            //if (e <= s) return;
            int p, l, r;
            l = s - 1;
            r = e + 1;
            p = elements[l + 1].value;
            while (true) {
                while (elements[++l].value < p) IterationTick();
                while ((--r >= l) && (elements[r].value > p)) IterationTick();
                if (l <= r) SwapElements(elements, l, r);
                else break;
            }

            if (l < e) QS(elements, l, e);
            if (s < r) QS(elements, s, r);
        }


        protected override void SortElements(Element[] elements) {
            int start = 0;
            int end = elements.Length - 1;
            QS(elements, start, end);
            /* for (int i = 1; i <= end; i++)
             {
                 Console.WriteLine(elements[i].value);
             } */
        }
    }
}