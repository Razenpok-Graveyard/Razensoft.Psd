namespace Razensoft.Psd
{
    public readonly struct RectInt32
    {
        public RectInt32(int top, int left, int bottom, int right)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }

        public int Top { get; }

        public int Left { get; }

        public int Bottom { get; }

        public int Right { get; }
    }
}
