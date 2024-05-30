namespace Classes
{
    public static class HeapSort
    {
        private static void HeapifyUp(int[] array, int size, int index, ref int heapifyUpTime)
        { 
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            if (leftChild < size && array[leftChild] > array[largestIndex])
            {
                largestIndex = leftChild;
            }
            if (rightChild < size && array[rightChild] > array[largestIndex])
            {
                largestIndex = rightChild;
            }
            if (largestIndex != index)
            {
                (array[index], array[largestIndex]) = (array[largestIndex], array[index]);
                heapifyUpTime++;
                OnArraySwapped(index, largestIndex);
                HeapifyUp(array, size, largestIndex, ref heapifyUpTime);
            }
        }
        private static void HeapifyDown(int[] array, int size, int index, ref int heapifyDownTime)
        {
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            if (leftChild < size && array[leftChild] < array[largestIndex])
            {
                largestIndex = leftChild;
            }
            if (rightChild < size && array[rightChild] < array[largestIndex])
            {
                largestIndex = rightChild;
            }
            if (largestIndex != index)
            {
                (array[index], array[largestIndex]) = (array[largestIndex], array[index]);
                heapifyDownTime++;
                OnArraySwapped(index, largestIndex);
                HeapifyDown(array, size, largestIndex, ref heapifyDownTime);
            }
        }
        public static int[] HeapsortAscending(int[] array, int size, ref int heapifyUpTime)
        {
            if (size <= 1)
            {
                return array;
            }
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                HeapifyUp(array, size, i, ref heapifyUpTime);
            }
            for (int i = size - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                heapifyUpTime++;
                OnArraySwapped(0, i);
                HeapifyUp(array, i, 0, ref heapifyUpTime);
            }
            return array;
        }
        public static int[] HeapsortDescending(int[] array, int size, ref int heapifyDownTime)
        {
            if (size <= 1)
            {
                return array;
            }
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(array, size, i, ref heapifyDownTime);
            }
            for (int i = size - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                heapifyDownTime++;
                OnArraySwapped(0, i);
                HeapifyDown(array, i, 0, ref heapifyDownTime);
            }
            return array;
        }
        private static void OnArraySwapped(int index1, int index2)
        {
            BlocksSwapped?.Invoke(null, new BlockSwapEventArgs(index1, index2));
        }
        public static event EventHandler<BlockSwapEventArgs> BlocksSwapped;
    }
}
