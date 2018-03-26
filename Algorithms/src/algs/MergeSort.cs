using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class MergeSort : SortingAlgorithm {

        public override string GetName() {
            return "Merge Sort";
        }

        public override bool IsInPlace() {
            return true;
        }

        void merge(Element[] elements, Element[] elements2, int l, int m, int r) {
            int j = l, k = m + 1;
            for (int i = l; i <= r; i++)
                elements2[i] = elements[i];
            while ((l <= m) && (k <= r)) {
                if (elements2[l].value <= elements2[k].value) {
                    elements[j++] = elements2[l++];
                } else {
                    elements[j++] = elements2[k++];
                }

            }
            if (l < m) {
                while (l <= m) {
                    elements[j++] = elements2[l++];
                }
            } else {
                while (k <= r) {
                    elements[j++] = elements2[k++];
                }
            }
        }

        void MS(Element[] elements, Element[] elements2, int l, int r) {
            if (l >= r) return;
            int m = (l + r) / 2;
            MS(elements, elements2, l, m);
            MS(elements, elements2, m + 1, r);
            merge(elements, elements2, l, m, r);

        }


        protected override void SortElements(Element[] elements) {
            int end = elements.Length - 1;
            Element[] elements2 = new Element[elements.Length];
            MS(elements, elements2, 0, end);
        }



    }
}