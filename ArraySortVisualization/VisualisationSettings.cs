namespace ArraySortVisualization
{
    public struct VisualisationSettings
    {
        public bool WithFrameLimit;
        public int FrameLimit;
        public int MaxLength;
        public int MaxValue;
        public int StepOnSizeX;

        public VisualisationSettings(bool withFrameLimit, int maxLength, int maxValue, int stepOnSizeX, int frameLimit = 60)
        {
            WithFrameLimit = withFrameLimit;
            FrameLimit = frameLimit;
            MaxLength = maxLength;
            MaxValue = maxValue;
            StepOnSizeX = stepOnSizeX;
        }
    }
}
