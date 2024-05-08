using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class QuickSort
    {
        public static void Quicksort(int[] arr, int start, int end)
        {
            int pivot = Partition(arr, start, end);
            Quicksort(arr, start, pivot - 1);
            Quicksort(arr, pivot + 1, end);
        }

        private static int Partition(int[] arr, int start, int end)
        {
            int pivot = arr[end];
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (arr[i] <= pivot)
                {
                    i += 1;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[end]) = (arr[end], arr[i + 1]);
            return i + 1;
        }
    }
}
