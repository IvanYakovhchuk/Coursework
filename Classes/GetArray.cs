namespace Classes
{
    public static class GetArray
    {
        public static int[] GetOrganisedArray(int length, int min, int max)
        {
            int[] array = new int[length];
            Random rand = new();
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max);
            }
            Array.Sort(array);
            return array;
        }
        public static int[] GetReversedArray(int length, int min, int max)
        {
            int[] array = new int[length];
            Random rand = new();
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max);
            }
            Array.Sort(array);
            Array.Reverse(array);
            return array;
        }
        public static int[] GetRandomArray(int length, int min, int max)
        {
            int[] array = new int[length];
            Random rand = new();
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max);
            }
            return array;
        }
    }
}
