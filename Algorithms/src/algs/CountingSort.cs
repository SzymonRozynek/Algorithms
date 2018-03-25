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
            int end = elements.Length;
            int[] k= new int[end+1];
            Element[] elements2 = new Element[end];
            for (int i=0; i<end; i++)
            {
                k[elements[i].value]++;
            }
            for (int i = 1; i <= 450; i++)
            {
                k[i + 1] += k[i];
            }
            for(int i= end-1; i>=0; i--)
            {
                elements2[--k[elements[i].value]] = elements[i];
            }
            for (int i = 1 ; i < end; i++)
            {
                Console.WriteLine(elements2[i].value+"  #"+elements2[i].index);
            } 
            
        }

    }
}
