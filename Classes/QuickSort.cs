namespace Classes
{
    public static class QuickSort
    {
        public static void QuicksortAscending(int[] arr, int start, int end)
        {
            if (end <= start)
            { 
                return;
            }
            
            int pivot = PartitionAscending(arr, start, end);
            QuicksortAscending(arr, start, pivot - 1);
            QuicksortAscending(arr, pivot + 1, end);
        }
        private static int PartitionAscending(int[] arr, int start, int end)
        {
            int mid = start + (end - start) / 2;
            if (arr[start] > arr[mid])
            {
                (arr[start], arr[mid]) = (arr[mid], arr[start]);
                OnArraySwapped(start, mid);
            }
            if (arr[start] > arr[end])
            {
                (arr[start], arr[end]) = (arr[end], arr[start]);
                OnArraySwapped(start, end);
            }
            if (arr[mid] > arr[end])
            {
                (arr[mid], arr[end]) = (arr[end], arr[mid]);
                OnArraySwapped(mid, end);
            }
            int pivot = arr[end];
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (arr[j] <= pivot)
                {
                    i += 1;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    OnArraySwapped(i, j);
                }
            }
            (arr[i + 1], arr[end]) = (arr[end], arr[i + 1]);
            OnArraySwapped(i + 1, end);
            return i + 1;
        }
        public static void QuicksortDescending(int[] arr, int start, int end)
        {
            if (end <= start)
            {
                return;
            }
            int pivot = PartitionDescending(arr, start, end);
            QuicksortDescending(arr, start, pivot - 1);
            QuicksortDescending(arr, pivot + 1, end);
        }
        private static int PartitionDescending(int[] arr, int start, int end)
        {
            int mid = start + (end - start) / 2;
            if (arr[start] > arr[mid])
            {
                (arr[start], arr[mid]) = (arr[mid], arr[start]);
                OnArraySwapped(start, mid);
            }
            if (arr[start] > arr[end])
            {
                (arr[start], arr[end]) = (arr[end], arr[start]);
                OnArraySwapped(start, end);
            }
            if (arr[mid] > arr[end])
            {
                (arr[mid], arr[end]) = (arr[end], arr[mid]);
                OnArraySwapped(mid, end);
            }
            int pivot = arr[end];
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (arr[j] >= pivot)
                {
                    i += 1;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    OnArraySwapped(i, j);
                }
            }
            (arr[i + 1], arr[end]) = (arr[end], arr[i + 1]);
            OnArraySwapped(end, i + 1);
            return i + 1;
        }
        private static void OnArraySwapped(int index1, int index2)
        {
            BlocksSwapped?.Invoke(null, new BlockSwapEventArgs(index1, index2));
        }
        public static event EventHandler<BlockSwapEventArgs> BlocksSwapped;
    }
}
