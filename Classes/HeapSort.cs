using System;
using System.Drawing;
using System.Reflection;

namespace Classes
{
    public class HeapSort
    {
        private static void HeapifyUp(int[] array, int size, int index)
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
                OnArraySwapped(index, largestIndex);
                HeapifyUp(array, size, largestIndex);
            }
        }
        private static void HeapifyDown(int[] array, int size, int index)
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
                OnArraySwapped(index, largestIndex);
                HeapifyDown(array, size, largestIndex);
            }
        }
        public static int[] HeapsortAscending(int[] array, int size)
        {
            if (size <= 1)
            {
                return array;
            }
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                HeapifyUp(array, size, i);
            }
            for (int i = size - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                OnArraySwapped(0, i);
                HeapifyUp(array, i, 0);
            }
            return array;
        }
        public static int[] HeapsortDescending(int[] array, int size)
        {
            if (size <= 1)
            {
                return array;
            }
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(array, size, i);
            }
            for (int i = size - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                OnArraySwapped(0, i);
                HeapifyDown(array, i, 0);
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
