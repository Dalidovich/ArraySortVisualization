namespace ArraySortVisualization
{
    public static class SortMethods
    {
        public delegate void SortMethod(MyArray array);

        public static void BubbleSortAlgorithm(MyArray array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }

        public static void SelectionSort(MyArray array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                    if (array[j] < array[minIndex])
                        minIndex = j;

                (array[i], array[minIndex]) = (array[minIndex], array[i]);
            }
        }

        public static void InsertionSort(MyArray array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static void CountingSort(MyArray array)
        {
            int max = array._array.Max();
            int[] count = new int[max + 1];

            foreach (int num in array._array)
                count[num]++;

            int index = 0;
            for (int i = 0; i < count.Length; i++)
                while (count[i]-- > 0)
                    array[index++] = i;
        }
    }
}
