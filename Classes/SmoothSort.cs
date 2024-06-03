using System.Numerics;

namespace Classes
{
    public static class SmoothSort
    {
        //An array of first 21 Leonardo numbers
        private static readonly int[] LP = [ 1, 1, 3, 5, 9, 15, 25, 41, 67, 109,
            177, 287, 465, 753, 1219, 1973, 3193, 5167, 8361, 13529, 21891];

        /* This method performs the sift-down operation to restore the heap property
         during heap creation or after extracting the maximum element in a heap-based sort.
         It ensures the subheap rooted at the given head position follows the heap property.*/
        private static void SiftAscending(int[] A, int pshift, int head, ref int complexity)
        {
            // Store the value at the head position
            int val = A[head];

            // Continue sifting down as long as the pshift indicates there are child nodes
            while (pshift > 1)
            {
                // Calculate the indices of the right and left children in the Leonardo heap
                int right = head - 1;
                int left = head - 1 - LP[pshift - 2];

                // If the value is greater than or equal to both children, the heap property is satisfied
                if (Compare(val, A[left]) >= 0 && Compare(val, A[right]) >= 0)
                    break;

                // If the left child is greater than or equal to the right child
                if (Compare(A[left], A[right]) >= 0)
                {
                    A[head] = A[left]; // Move the left child's value to the current head position
                    complexity++; //calculates practical complexity
                    OnArraySwapped(head, left); //messages the event BlockSwapped which elements were swapped
                    head = left; // Update head to the left child's position
                    pshift -= 1; // Move down the tree, reducing the pshift by 1 (since we moved to the left child)
                }
                else
                {
                    A[head] = A[right]; // Move the right child's value to the current head position
                    complexity++; 
                    OnArraySwapped(head, right);
                    head = right; // Update head to the right child's position
                    pshift -= 2; // Move down the tree, reducing the pshift by 2 (since we moved to the right child)
                }
            }
            A[head] = val; // Place the original value in its correct position
        }

        /*This method works tha same as the previous one, since it is looking for childs, which are less
         than the head, not the bigger one*/
        private static void SiftDescending(int[] A, int pshift, int head, ref int complexity)
        {
            int val = A[head];
            while (pshift > 1)
            {
                int right = head - 1;
                int left = head - 1 - LP[pshift - 2];

                if (Compare(val, A[left]) <= 0 && Compare(val, A[right]) <= 0)
                    break;

                if (Compare(A[left], A[right]) <= 0)
                {
                    A[head] = A[left];
                    complexity++;
                    OnArraySwapped(head, left);
                    head = left;
                    pshift -= 1;
                }
                else
                {
                    A[head] = A[right];
                    complexity++;
                    OnArraySwapped(head, right);
                    head = right;
                    pshift -= 2;
                }
            }
            A[head] = val;
        }

        /* This method performs the trinkle operation, which is used in smoothsort to maintain
         the heap property while considering both ascending order and a Leonardo heap structure.*/
        private static void TrinkleAscending(int[] A, int p, int pshift, int head, bool isTrusty, ref int complexity)
        {
            // Store the value at the head position
            int val = A[head];

            // Continue the trinkle operation while p is not 1
            while (p != 1)
            {
                // Calculate the stepson index using the Leonardo heap property
                int stepson = head - LP[pshift];

                // If the stepson's value is less than or equal to val, the heap property is satisfied
                if (Compare(A[stepson], val) <= 0)
                    break;

                // If the node is not trusty and there are at least two elements in the heap
                if (!isTrusty && pshift > 1)
                {
                    // Calculate the indices of the right and left children
                    int right = head - 1;
                    int left = head - 1 - LP[pshift - 2];

                    // If either child is greater than or equal to the stepson, stop the trinkle operation
                    if (Compare(A[right], A[stepson]) >= 0 || Compare(A[left], A[stepson]) >= 0)
                        break;
                }
                // Move the stepson's value to the current head position
                A[head] = A[stepson];

                complexity++; //calculates practical complexity
                OnArraySwapped(head, stepson); //messages the event BlockSwapped which elements were swapped

                // Update head to the stepson's position
                head = stepson;

                // Calculate the number of trailing zero bits in p & ~1
                int trail = BitOperations.TrailingZeroCount((uint)(p & ~1));

                // Right shift p by the number of trailing zero bits
                p >>= trail;

                // Increase pshift by the number of trailing zero bits
                pshift += trail;

                // Mark the node as not trusty
                isTrusty = false;
            }

            // If the node was not trusty, restore the heap property by sifting the value down
            if (!isTrusty)
            {
                A[head] = val; // Place the original value in its correct position

                // Call SiftAscending to ensure the subheap rooted at the head follows the heap property
                SiftAscending(A, pshift, head, ref complexity);
            }
        }

        //This method works same as previous, since it looks if stepson is bigger than or equal to val
        private static void TrinkleDescending(int[] A, int p, int pshift, int head, bool isTrusty, ref int complexity)
        {
            int val = A[head];

            while (p != 1)
            {
                int stepson = head - LP[pshift];

                if (Compare(A[stepson], val) >= 0)
                    break;

                if (!isTrusty && pshift > 1)
                {
                    int right = head - 1;
                    int left = head - 1 - LP[pshift - 2];
                    if (Compare(A[right], A[stepson]) <= 0 || Compare(A[left], A[stepson]) <= 0)
                        break;
                }
                A[head] = A[stepson];
                complexity++;
                OnArraySwapped(head, stepson);
                head = stepson;
                int trail = BitOperations.TrailingZeroCount((uint)(p & ~1));
                p >>= trail;
                pshift += trail;
                isTrusty = false;
            }

            if (!isTrusty)
            {
                A[head] = val;
                SiftDescending(A, pshift, head, ref complexity);
            }
        }

        /* This method sorts an array in ascending order using the Smoothsort algorithm.
         Smoothsort is a variation of heapsort that uses Leonardo heaps to achieve O(n log n) performance in the worst case.*/
        public static void SmoothSortAscending(int[] A, int low, int high, ref int complexity)
        {
            // Initialize the head to the starting index and set up the position and positional shift
            int head = low;
            int p = 1;
            int pshift = 1;

            // Build the heap structure using Leonardo heaps
            while (head < high)
            {
                // Check if the last two bits of p are set (i.e., p & 3 == 3).
                if ((p & 3) == 3)
                {
                    // Perform a sift operation and adjust p and pshift
                    SiftAscending(A, pshift, head, ref complexity);
                    p >>= 2;
                    pshift += 2;
                }
                else
                {
                    // If the next Leonardo heap would exceed the array bounds, perform a trinkle operation.
                    if (LP[pshift - 1] >= high - head)
                    {
                        TrinkleAscending(A, p, pshift, head, false, ref complexity);
                    }

                    // Otherwise, perform a sift operation.
                    else
                    {
                        SiftAscending(A, pshift, head, ref complexity);
                    }

                    // Adjust p and pshift based on the current pshift value.
                    if (pshift == 1)
                    {
                        p <<= 1;
                        pshift--;
                    }
                    else
                    {
                        p <<= (pshift - 1);
                        pshift = 1;
                    }
                }
                p |= 1;
                head++;
            }

            // Perform a final trinkle operation after building the heap.
            TrinkleAscending(A, p, pshift, head, false, ref complexity);

            // Sort the array by extracting elements from the heap.
            while (pshift != 1 || p != 1)
            {
                if (pshift <= 1)
                {
                    // Adjust p and pshift by finding the trailing zeros in p & ~1.
                    int trail = BitOperations.TrailingZeroCount((uint)(p & ~1));
                    p >>= trail;
                    pshift += trail;
                }
                else
                {
                    // Perform a trinkle operation and adjust p and pshift.
                    p <<= 2;
                    p ^= 7;
                    pshift -= 2;

                    // Perform two trinkle operations to restore the heap property.
                    TrinkleAscending(A, p >> 1, pshift + 1, head - LP[pshift] - 1, true, ref complexity);
                    TrinkleAscending(A, p, pshift, head - 1, true, ref complexity);
                }
                head--;
            }
        }

        /*This method works same as previous, but it uses SiftDescending and TrinkleDescending instead of
         SiftAscending and TrinkleDescending (look the difference) to sort an array in descending order.*/
        public static void SmoothSortDescending(int[] A, int low, int high, ref int complexity)
        {
            int head = low;
            int p = 1;
            int pshift = 1;

            while (head < high)
            {
                if ((p & 3) == 3)
                {
                    SiftDescending(A, pshift, head, ref complexity);
                    p >>= 2;
                    pshift += 2;
                }
                else
                {
                    if (LP[pshift - 1] >= high - head)
                    {
                        TrinkleDescending(A, p, pshift, head, false, ref complexity);
                    }
                    else
                    {
                        SiftDescending(A, pshift, head, ref complexity);
                    }

                    if (pshift == 1)
                    {
                        p <<= 1;
                        pshift--;
                    }
                    else
                    {
                        p <<= (pshift - 1);
                        pshift = 1;
                    }
                }
                p |= 1;
                head++;
            }

            TrinkleDescending(A, p, pshift, head, false, ref complexity);

            while (pshift != 1 || p != 1)
            {
                if (pshift <= 1)
                {
                    int trail = BitOperations.TrailingZeroCount((uint)(p & ~1));
                    p >>= trail;
                    pshift += trail;
                }
                else
                {
                    p <<= 2;
                    p ^= 7;
                    pshift -= 2;

                    TrinkleDescending(A, p >> 1, pshift + 1, head - LP[pshift] - 1, true, ref complexity);
                    TrinkleDescending(A, p, pshift, head - 1, true, ref complexity);
                }
                head--;
            }
        }
        public static int Compare(int left, int right)
        {
            int cmpVal;
            if (left > right)
                cmpVal = 1;
            else if (left < right)
                cmpVal = -1;
            else 
                cmpVal = 0;
            return cmpVal;
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
