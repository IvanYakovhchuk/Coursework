namespace Classes
{
    //class of methods that are generating different types of arrays
    public static class GetArray
    {
        //generating organised array
        public static int[] GetOrganisedArray(int length, int min, int max)
        {
            int[] array = new int[length]; //creating an empty array
            Random rand = new(); //creating the new object of class Random
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max); //setting the array's element to the generated numbers within the range
            }
            Array.Sort(array); //organising the array
            return array;
        }
        public static int[] GetReversedArray(int length, int min, int max)
        {
            int[] array = new int[length]; //creating an empty array
            Random rand = new(); //creating the new object of class Random
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max); //setting the array's element to the generated numbers within the range
            }
            Array.Sort(array); //organising the array
            Array.Reverse(array); //reversing the array
            return array;
        }
        public static int[] GetRandomArray(int length, int min, int max)
        {
            int[] array = new int[length]; //creating an empty array
            Random rand = new(); //creating the new object of class Random
            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(min, max); //setting the array's element to the generated numbers within the range
            }
            return array;
        }
    }
}
