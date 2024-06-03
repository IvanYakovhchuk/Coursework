namespace Classes
{
    public static class HeapSort
    {
        /* This method maintains the max-heap property by performing a heapify-up operation.
        It ensures that the element at the given index follows the max-heap property,
        and if not, it swaps elements and recursively fixes the heap property upwards.*/
        private static void HeapifyUp(int[] array, int size, int index, ref int heapifyUpTime)
        { 
            int largestIndex = index; // Initialize largestIndex with the current index
            int leftChild = 2 * index + 1; // Calculate the index of the left child
            int rightChild = 2 * index + 2; // Calculate the index of the right child

            // Check if the left child exists and is greater than the current largest element
            if (leftChild < size && array[leftChild] > array[largestIndex])
            {
                largestIndex = leftChild; // Update largestIndex to the left child's index
            }

            // Check if the right child exists and is greater than the current largest element
            if (rightChild < size && array[rightChild] > array[largestIndex])
            {
                largestIndex = rightChild; // Update largestIndex to the right child's index
            }

            // If the largest element is not the current index, swap and heapify up recursively
            if (largestIndex != index)
            {
                // Swap the elements at the current index and the largest index
                (array[index], array[largestIndex]) = (array[largestIndex], array[index]);

                heapifyUpTime++; //calculates the practical complexity
                OnArraySwapped(index, largestIndex); //messages the event BlockSwapped which elements were swapped

                // Recursively call HeapifyUp on the largest index
                HeapifyUp(array, size, largestIndex, ref heapifyUpTime);
            }
        }

        /*This method works same as previous, but it maintains the min-heap property, meaning
          that largest index is not the biggest, but the smallest one*/
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

        /* This method sorts an array in ascending order using the Heapsort algorithm.
          It uses the max-heap property to sort the array.*/
        public static int[] HeapsortAscending(int[] array, int size, ref int heapifyUpTime)
        {
            // If the array has one or no elements, it is already sorted
            if (size <= 1)
            {
                return array;
            }

            // Build the max-heap by heapifying each non-leaf node from the bottom up
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                HeapifyUp(array, size, i, ref heapifyUpTime);
            }

            // Extract elements from the heap one by one and rebuild the heap
            for (int i = size - 1; i >= 0; i--)
            {
                // swap the root of the tree (the biggest element in array with the last element of the heap
                (array[0], array[i]) = (array[i], array[0]);

                heapifyUpTime++; // calculates practical complexity
                OnArraySwapped(0, i); //messages the event BlockSwapped which elements were swapped

                // Call HeapifyUp on the reduced heap to maintain the max-heap property
                HeapifyUp(array, i, 0, ref heapifyUpTime);
            }
            return array;
        }

        // This method works same as previous, while it uses the min-heap property to sort the array.
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

        //method that returns a reference to an object that causes BlocksSwapped event and event arguments (indexes of swapped elements)
        private static void OnArraySwapped(int index1, int index2)
        {
            BlocksSwapped?.Invoke(null, new BlockSwapEventArgs(index1, index2));
        }

        //event that is reacting when the elements are swapped
        public static event EventHandler<BlockSwapEventArgs> BlocksSwapped;
    }
}
