using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class QuickSort
    {
        public static void QuicksortAscending(int[] arr, int start, int end)
        {
            int pivot = PartitionAscending(arr, start, end);
            QuicksortAscending(arr, start, pivot - 1);
            QuicksortAscending(arr, pivot + 1, end);
        }

        private static int PartitionAscending(int[] arr, int start, int end)
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
        public static void QuicksortDescending(int[] arr, int start, int end)
        {
            int pivot = PartitionDescending(arr, start, end);
            QuicksortDescending(arr, start, pivot - 1);
            QuicksortDescending(arr, pivot + 1, end);
        }
        private static int PartitionDescending(int[] arr, int start, int end)
        {
            int pivot = arr[end];
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (arr[i] >= pivot)
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
