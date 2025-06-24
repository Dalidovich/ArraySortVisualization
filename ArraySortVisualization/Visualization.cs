using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static ArraySortVisualization.SortMethods;

namespace ArraySortVisualization
{
    public static class Visualization
    {
        private static bool _withFrameLimit = false;
        private static int _frameLimit = 1;
        private static int _maxLength = 900;
        private static int _maxValue = 100;
        private static int _stepOnSizeX = 300;

        private static int _sizeX = 400;
        private static int _collumnYScale = 10;
        private static int _collumnXScale = 10;
        private static bool _canDraw = false;


        private static ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private static Thread _sortThread;

        private static List<RectangleShape> _collumns = new List<RectangleShape>();
        private static MyArray _myArray;


        public static void ReloadCollums()
        {
            for (int i = 0; i < _myArray.Length; i++)
            {
                _collumns[i].Size = new Vector2f(_collumnXScale, -_myArray._array[i] * _collumnYScale);
            }
            _canDraw = true;
            if (_sortThread.IsAlive)
            {
                _pauseEvent.WaitOne();
                _pauseEvent.Reset();
            }
        }


        private static void _calculateScale()
        {
            var newXSize = 0;
            while (_myArray.Length * 2 > newXSize)
            {
                newXSize += _stepOnSizeX;
            }

            _sizeX = newXSize;
            _collumnXScale = (_sizeX / (_myArray.Length * 2)) * 2;
            _collumnYScale = (_sizeX / 2) / _myArray._array.Max();
        }
        private static bool _checkArrayState()
        {
            if (_myArray.Length >= _maxLength) return false;
            if (_myArray._array.Max() > _maxValue) return false;
            return true;
        }

        public static void Init(MyArray arr, VisualisationSettings settings, SortMethod sortMethod)
        {
            _myArray = arr;

            _withFrameLimit = settings.WithFrameLimit;
            _frameLimit = settings.FrameLimit;
            _maxLength = settings.MaxLength;
            _maxValue = settings.MaxValue;
            _stepOnSizeX = settings.StepOnSizeX;

            if (!_checkArrayState())
            {
                throw new Exception($"unvailable array arr.len>={_maxLength} or arr.max>{_maxValue}");
            }
            _calculateScale();
            _sortThread = new Thread(() => { sortMethod(_myArray); });

            for (int i = 0; i < _myArray.Length; i++)
            {
                var rec = new RectangleShape(new Vector2f(1, 1));
                rec.OutlineThickness = -0.5f;
                rec.OutlineColor = Color.Black;
                rec.Position = new Vector2f(i * _collumnXScale, _sizeX / 2);
                _collumns.Add(rec);

            }
        }

        public static void Show()
        {
            RenderWindow window = new RenderWindow(new VideoMode((uint)_sizeX, (uint)_sizeX / 2), "sort array");
            window.SetActive();
            if (_withFrameLimit)
            {
                window.SetFramerateLimit((uint)_frameLimit);
            }
            window.Closed += (object? sender, EventArgs e) => window.Close();

            _sortThread.Start();

            while (window.IsOpen)
            {
                window.Clear();
                if (!_sortThread.IsAlive)
                {
                    ReloadCollums();
                }
                if (!_canDraw) continue;
                window.DispatchEvents();

                for (var i = 0; i < _collumns.Count; i++)
                {
                    window.Draw(_collumns[i]);
                }


                window.Display();
                _canDraw = false;
                _pauseEvent.Set();
            }
        }
    }
}
