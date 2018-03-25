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

        public override bool IsInPlace()
        {
            return true;
        }


        void Heap(Element[] elements, int i, int end)
        {
            if (((2*i <= end) &&(elements[2 * i].value > elements[i].value))||( (2 * i + 1 <= end) && (elements[2 * i + 1].value > elements[i].value)))
            {
                if (elements[2 * i + 1].value > elements[2 * i].value)
                {
                    if (2 * i + 1 < end / 2)
                    {
                        SwapElements(elements, 2 * i + 1, i);
                        Heap(elements, 2 * i + 1, end);
                    }                  
                }
                else {
                    if (2 * i < end / 2)
                    {
                        SwapElements(elements, 2 * i, i);
                        Heap(elements, 2 * i, end);
                    }
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

        }

    }
}
