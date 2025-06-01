using static ArraySortVisualization.SortMethods;

namespace ArraySortVisualization
{
    public class Program
    {
        static void Main(string[] args)
        {
            var arraySize = 50;
            var array=new int[arraySize];
            var rnd = new Random();
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = rnd.Next(100);
            }
            var arr = new MyArray(array);

            Visualization.Init(arr, new VisualisationSettings(true, 900, 100, 300,30), SortMethods.BubbleSortAlgorithm);

            Visualization.Show();
        }
    }
}
