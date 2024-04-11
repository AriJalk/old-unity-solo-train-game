using UnityEngine;

namespace HexSystem
{
    internal class HexLayout
    {
        public HexOrientation Orientation
        {
            get; private set;
        }

        public Vector2 Size
        {
            get; private set;
        }

        public Vector2Int Origin
        {
            get; private set;
        }

        public HexLayout(HexOrientation orientation, Vector2 size, Vector2Int origin)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
        }
    }
}