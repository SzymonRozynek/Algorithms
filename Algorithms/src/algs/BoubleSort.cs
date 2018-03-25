using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class BoubleSort : SortingAlgorithm {

        public override string GetName() {
            return "Bouble Sort";
        }

        public override bool IsInPlace()
        {
            return true;
        }

        protected override void SortElements(Element[] elements) {
            int end = elements.Length - 2;
            bool zmiany;
            for(int i = 0; i < end; i++) {
                zmiany = false;
                for (int j = end; j >= i; j--)
                {   if (elements[j].value > elements[j + 1].value)
                    {
                        SwapElements(elements, j, j + 1);
                        zmiany = true;
                        IterationTick();
                    }
                   
                }
                if (!zmiany) break;
            }
        }


    }
}
