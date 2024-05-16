using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class SmoothSort
    {
        private static int Leonardo(int k)
        {
            if (k < 2)
            {
                return 1;
            }
            return Leonardo(k - 1) + Leonardo(k - 2) + 1;
        }
        private static void Heapify(int[] arr, int start, int end)
        {
            int i = start;
            int j = 0;
            int k = 0;
            while (k < end - start + 1)
            {
                if ((k & 0xAAAAAAAA) == 0xAAAAAAAA)
                {
                    j += i;
                    i >>= 1;
                }
                else
                {
                    i += j;
                    j >>= 1;
                }

                k++;
            }
            while (i > 0)
            {
                j >>= 1;
                int l = i + j;
                while (l < end)
                {
                    if (arr[l] > arr[l - i])
                    {
                        break;
                    }
                    (arr[l], arr[l - i]) = (arr[l - i], arr[l]);
                    OnArraySwapped(l, l - i);
                    l += i;
                }
                i = j;
            }
        }
        public static int[] SmoothSortAscending(int[] arr)
        {
            int n = arr.Length;
            int p = n - 1;
            int q = p;
            int r = 0;
            while (p > 0)
            {
                if ((r & 0x03) == 0)
                {
                    Heapify(arr, r, q);
                }
                if (Leonardo(r) == p)
                {
                    r++;
                }
                else
                {
                    r--;
                    q -= Leonardo(r);
                    Heapify(arr, r, q);
                    q = r - 1;
                    r++;
                }
                (arr[0], arr[p]) = (arr[p], arr[0]);
                OnArraySwapped(0, p);
                p--;
            }
            for (int i = 0; i < n - 1; i++)
            {
                int j = i + 1;
                while (j > 0 && arr[j] < arr[j - 1])
                {
                    (arr[j], arr[j - 1]) = (arr[j - 1], arr[j]);
                    OnArraySwapped(j, j - 1);
                    j--;
                }
            }
            return arr;
        }
        public static int[] SmoothSortDescending(int[] arr)
        {
            int n = arr.Length;
            int p = n - 1;
            int q = p;
            int r = 0;
            while (p > 0)
            {
                if ((r & 0x03) == 0)
                {
                    Heapify(arr, r, q);
                }
                if (Leonardo(r) == p)
                {
                    r++;
                }
                else
                {
                    r--;
                    q -= Leonardo(r);
                    Heapify(arr, r, q);
                    q = r - 1;
                    r++;
                }
                (arr[0], arr[p]) = (arr[p], arr[0]);
                OnArraySwapped(0, p);
                p--;
            }
            for (int i = 0; i < n - 1; i++)
            {
                int j = i + 1;
                while (j > 0 && arr[j] > arr[j - 1])
                {
                    (arr[j], arr[j - 1]) = (arr[j - 1], arr[j]);
                    OnArraySwapped(j, j - 1);
                    j--;
                }
            }
            return arr;
        }
        private static void OnArraySwapped(int index1, int index2)
        {
            BlocksSwapped?.Invoke(null, new BlockSwapEventArgs(index1, index2));
        }
        public static event EventHandler<BlockSwapEventArgs> BlocksSwapped;
    }
}
