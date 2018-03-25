using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class HeapSort : SortingAlgorithm {

        public override string GetName() {
            return "Heap Sort";
        }

        public override bool IsInPlace() {
            return true;
        }


        /*void Heap(Element[] elements, int i, int end)
        {
            int l, p;
            if (2 * i <= end)
            {
                l = 2 * i;
                if ((2 * i + 1) <= end)
                {
                    p = 2 * i;
                    if ((elements[l].value > elements[p].value))
                    {
                        if (elements[l].value > elements[i].value)
                        {
                            SwapElements(elements, l, i);
                            Heap(elements, l, end);
                        }
                    }
                    else if (elements[p].value > elements[i].value)
                    {
                        SwapElements(elements, p, i);
                        Heap(elements, p, end);
                    }

                }
                if (elements[l].value > elements[i].value)
                {
                    SwapElements(elements, l, i);
                    Heap(elements, l, end);
                }
            }

        }


        protected override void SortElements(Element[] elements) {
            int end = elements.Length - 1;
            for(int i=end/2; i > 0; i++)
            {
                Heap(elements, i,end);
                IterationTick();
            }
            while(end>1)
            {
                SwapElements(elements, 1, end);
                Heap(elements, 1, --end);
                IterationTick();
            }

        }*/

        private void Heap(Element[] elements, int index, int max) {
            IterationTick();
            if(index * 2 + 2 <= max) {
                if(elements[index].value < elements[index * 2 + 1].value && elements[index].value < elements[index * 2 + 2].value) {
                    int p = elements[index * 2 + 1].value > elements[index * 2 + 2].value ? index * 2 + 1 : index * 2 + 2;
                    SwapElements(elements, index, p);
                    Heap(elements, p, max);
                }
                else if(elements[index].value < elements[index * 2 + 1].value) {
                    SwapElements(elements, index, index * 2 + 1);
                    Heap(elements, index * 2 + 1, max);
                }
                else if(elements[index].value < elements[index * 2 + 2].value) {
                    SwapElements(elements, index, index * 2 + 2);
                    Heap(elements, index * 2 + 2, max);
                }
            }
            else if(index * 2 + 1 <= max) {
                if (elements[index].value < elements[index * 2 + 1].value) {
                    SwapElements(elements, index, index * 2 + 1);
                    Heap(elements, index * 2 + 1, max);
                }
            }

        }

        protected override void SortElements(Element[] elements) {
            for(int i = elements.Length - 1; i >= 0; i--) {
                Heap(elements, i, elements.Length -1);
            }
            for(int i = 0; i < elements.Length - 1; i++) {
                SwapElements(elements, 0, elements.Length - 1 - i);
                Heap(elements, 0, elements.Length - 1 - i);
            }
        }
    }
}
