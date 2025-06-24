namespace ArraySortVisualization
{
    public class MyArray
    {
        public int[] _array;
        public int Length;

        public MyArray(int[] array)
        {
            _array = array;
            Length = array.Length;
        }

        public int this[int i]
        {
            get
            {
                Visualization.ReloadCollums();
                return _array[i];
            }
            set
            {
                Visualization.ReloadCollums();
                _array[i] = value;
            }
        }
    }
}
