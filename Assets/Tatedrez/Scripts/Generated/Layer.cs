namespace Tatedrez
{
    public static class Layer
    {
        public static int Default = 0;
        public static int TransparentFX = 1;
        public static int IgnoreRaycast = 2;
        public static int Water = 4;
        public static int UI = 5;
        public static int Piece = 6;
        public static int Node = 7;
        
        public static class Mask
        {
            public static int Default = 1 << 0;
            public static int TransparentFX = 1 << 1;
            public static int IgnoreRaycast = 1 << 2;
            public static int Water = 1 << 4;
            public static int UI = 1 << 5;
            public static int Piece = 1 << 6;
            public static int Node = 1 << 7;
        }
    }
}
