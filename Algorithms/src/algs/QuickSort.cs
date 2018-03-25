using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Algorithms {
    class QuickSort : SortingAlgorithm
    {

        public override string GetName()
        {
            return "Quick Sort";
        }

        public override bool IsInPlace()
        {
            return true;
        }

        void QS(Element[] elements, int s, int e)
        {
            int p, l, r;
            l = s;
            r = e;
            p = elements[l].value;
            while (l < r)
            {
                while (elements[l].value <p) { l++; IterationTick(); }
                while (elements[r].value >p) { r--; IterationTick(); }
                if(l<r) SwapElements(elements, l++, r);
            }

            if (s < r ) QS(elements, s, r-1);
            if (r < e) QS(elements, r+1, e);
        }


        protected override void SortElements(Element[] elements)
        {
            int start = 0;
            int end = elements.Length - 1;
            QS(elements, start, end);
            for (int i = 1; i <= end; i++)
            {
                Console.WriteLine(elements[i].value);

            } 
        }
    }
 }

