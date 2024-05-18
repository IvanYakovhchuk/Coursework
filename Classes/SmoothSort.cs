using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class SmoothSort
    {
        private static readonly int[] LP = [ 1, 1, 3, 5, 9, 15, 25, 41, 67, 109,
            177, 287, 465, 753, 1219, 1973, 3193, 5167, 8361, 13529, 21891,
            35421, 57313, 92735, 150049, 242785, 392835, 635621, 1028457,
            1664079, 2692537, 4356617, 7049155, 11405773, 18454929, 29860703,
            48315633, 78176337, 126491971, 204668309, 331160281, 535828591,
            866988873];
        private static void SiftAscending(int[] A, int pshift, int head)
        {
            int val = A[head];
            while (pshift > 1)
            {
                int rt = head - 1;
                int lf = head - 1 - LP[pshift - 2];

                if (Compare(val, A[lf]) >= 0 && Compare(val, A[rt]) >= 0)
                    break;

                if (Compare(A[lf], A[rt]) >= 0)
                {
                    A[head] = A[lf];
                    OnArraySwapped(head, lf);
                    head = lf;
                    pshift -= 1;
                }
                else
                {
                    A[head] = A[rt];
                    OnArraySwapped(head, rt);
                    head = rt;
                    pshift -= 2;
                }
            }
            A[head] = val;
        }
        private static void SiftDescending(int[] A, int pshift, int head)
        {
            int val = A[head];
            while (pshift > 1)
            {
                int rt = head - 1;
                int lf = head - 1 - LP[pshift - 2];

                if (Compare(val, A[lf]) <= 0 && Compare(val, A[rt]) <= 0)
                    break;

                if (Compare(A[lf], A[rt]) <= 0)
                {
                    A[head] = A[lf];
                    OnArraySwapped(head, lf);
                    head = lf;
                    pshift -= 1;
                }
                else
                {
                    A[head] = A[rt];
                    OnArraySwapped(head, rt);
                    head = rt;
                    pshift -= 2;
                }
            }
            A[head] = val;
        }
        private static void TrinkleAscending(int[] A, int p, int pshift, int head, bool isTrusty)
        {
            int val = A[head];

            while (p != 1)
            {
                int stepson = head - LP[pshift];

                if (Compare(A[stepson], val) <= 0)
                    break;

                if (!isTrusty && pshift > 1)
                {
                    int rt = head - 1;
                    int lf = head - 1 - LP[pshift - 2];
                    if (Compare(A[rt], A[stepson]) >= 0 || Compare(A[lf], A[stepson]) >= 0)
                        break;
                }
                A[head] = A[stepson];
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
                SiftAscending(A, pshift, head);
            }
        }
        private static void TrinkleDescending(int[] A, int p, int pshift, int head, bool isTrusty)
        {
            int val = A[head];

            while (p != 1)
            {
                int stepson = head - LP[pshift];

                if (Compare(A[stepson], val) >= 0)
                    break;

                if (!isTrusty && pshift > 1)
                {
                    int rt = head - 1;
                    int lf = head - 1 - LP[pshift - 2];
                    if (Compare(A[rt], A[stepson]) <= 0 || Compare(A[lf], A[stepson]) <= 0)
                        break;
                }
                A[head] = A[stepson];
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
                SiftDescending(A, pshift, head);
            }
        }
        public static void SmoothSortAscending(int[] A, int lo, int hi)
        {
            int head = lo;
            int p = 1;
            int pshift = 1;

            while (head < hi)
            {
                if ((p & 3) == 3)
                {
                    SiftAscending(A, pshift, head);
                    p >>= 2;
                    pshift += 2;
                }
                else
                {
                    if (LP[pshift - 1] >= hi - head)
                    {
                        TrinkleAscending(A, p, pshift, head, false);
                    }
                    else
                    {
                        SiftAscending(A, pshift, head);
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

            TrinkleAscending(A, p, pshift, head, false);

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

                    TrinkleAscending(A, p >> 1, pshift + 1, head - LP[pshift] - 1, true);
                    TrinkleAscending(A, p, pshift, head - 1, true);
                }
                head--;
            }
        }
        public static void SmoothSortDescending(int[] A, int lo, int hi)
        {
            int head = lo;
            int p = 1;
            int pshift = 1;

            while (head < hi)
            {
                if ((p & 3) == 3)
                {
                    SiftDescending(A, pshift, head);
                    p >>= 2;
                    pshift += 2;
                }
                else
                {
                    if (LP[pshift - 1] >= hi - head)
                    {
                        TrinkleDescending(A, p, pshift, head, false);
                    }
                    else
                    {
                        SiftDescending(A, pshift, head);
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

            TrinkleDescending(A, p, pshift, head, false);

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

                    TrinkleDescending(A, p >> 1, pshift + 1, head - LP[pshift] - 1, true);
                    TrinkleDescending(A, p, pshift, head - 1, true);
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
        private static void OnArraySwapped(int index1, int index2)
        {
            BlocksSwapped?.Invoke(null, new BlockSwapEventArgs(index1, index2));
        }
        public static event EventHandler<BlockSwapEventArgs> BlocksSwapped;
    }
}
