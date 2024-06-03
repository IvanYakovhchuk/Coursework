namespace Classes
{
    public static class QuickSort
    {
        public static void QuicksortAscending(int[] arr, int start, int end, ref int partitionTime)
        { 
            if (end <= start)
            {
                return;
            } 
            //checking if the length of an array is more than 1
            //if so, performs partition procedure and recursively sorts two parts of an array that was partitioned
            int pivot = PartitionAscending(arr, start, end, ref partitionTime);
            QuicksortAscending(arr, start, pivot - 1, ref partitionTime);
            QuicksortAscending(arr, pivot + 1, end, ref partitionTime);
            return;
        }
        private static int PartitionAscending(int[] arr, int start, int end, ref int partitionTime)
        {
            int mid = start + (end - start) / 2;
            if (arr[start] > arr[mid])
            {
                (arr[start], arr[mid]) = (arr[mid], arr[start]);
                partitionTime++;
                OnArraySwapped(start, mid);
            }
            if (arr[start] > arr[end])
            {
                (arr[start], arr[end]) = (arr[end], arr[start]);
                partitionTime++;
                OnArraySwapped(start, end);
            }
            if (arr[mid] > arr[end])
            {
                (arr[mid], arr[end]) = (arr[end], arr[mid]);
                partitionTime++;
                OnArraySwapped(mid, end);
            }
            int pivot = arr[mid];
            //choosing the pivot by comparing the first, last and middle elements and choosing the average one

            (arr[mid], arr[end]) = (arr[end], arr[mid]);
            //moving pivot to the end of an array
            partitionTime++; //counts practical complexity

            OnArraySwapped(mid, end); //messages the event BlocksSwapped about the blocks that were swapped

            int i = start - 1; //creating two iterators (i = -1, j = 0) two move through the array

            for (int j = start; j < end; j++) //for every element in the array
            {
                if (arr[j] <= pivot) //if the the element is less than pivot
                {
                    i += 1; //increases the i iterator
                    (arr[i], arr[j]) = (arr[j], arr[i]); //moves the element before the current in front of it
                    partitionTime++;
                    OnArraySwapped(i, j);
                }
                /*at the end of the cycle, all the elements that are bigger than pivot will be "dragged"
                 to the second part of an array, while the elements less than pivot will "stay" in the
                 first part*/
            }
            (arr[i + 1], arr[end]) = (arr[end], arr[i + 1]); //returning the pivot to the middle of an array to perform sorting both for first and last part of an array
            partitionTime++;
            OnArraySwapped(i + 1, end);
            return i + 1; //returning the index of pivot
        }

        /*the next two methods are working same as the first two, while they're moving elements LESS than pivot to
         the last part of an array, letting bigger elements stay in the first part*/
        public static void QuicksortDescending(int[] arr, int start, int end, ref int partitionTime)
        {
            if (end <= start)
            {
                return;
            }
            int pivot = PartitionDescending(arr, start, end, ref partitionTime);
            QuicksortDescending(arr, start, pivot - 1, ref partitionTime);
            QuicksortDescending(arr, pivot + 1, end, ref partitionTime);
            return;
        }
        private static int PartitionDescending(int[] arr, int start, int end, ref int partitionTime)
        {
            int mid = start + (end - start) / 2;
            if (arr[start] > arr[mid])
            {
                (arr[start], arr[mid]) = (arr[mid], arr[start]);
                partitionTime++;
                OnArraySwapped(start, mid);
            }
            if (arr[start] > arr[end])
            {
                (arr[start], arr[end]) = (arr[end], arr[start]);
                partitionTime++;
                OnArraySwapped(start, end);
            }
            if (arr[mid] > arr[end])
            {
                (arr[mid], arr[end]) = (arr[end], arr[mid]);
                partitionTime++;
                OnArraySwapped(mid, end);
            }
            int pivot = arr[mid];
            (arr[mid], arr[end]) = (arr[end], arr[mid]);
            partitionTime++;
            OnArraySwapped(mid, end);
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (arr[j] >= pivot)
                {
                    i += 1;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    partitionTime++;
                    OnArraySwapped(i, j);
                }
            }
            (arr[i + 1], arr[end]) = (arr[end], arr[i + 1]);
            partitionTime++;
            OnArraySwapped(end, i + 1);
            return i + 1;
        }

        //method that returns a reference to an object that causes BlocksSwapped event and event arguments (indexes of swapped elements)
        private static void OnArraySwapped(int index1, int index2)
        {
            BlocksSwapped?.Invoke(null, new BlockSwapEventArgs(index1, index2));
        }

        //event that is reacting when the elements are swapped
        public static event EventHandler<BlockSwapEventArgs> BlocksSwapped;
    }
}
