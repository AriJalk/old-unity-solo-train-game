using UnityEngine;

namespace HexSystem
{
    /// <summary>
    /// HexPosition data
    /// </summary>
    public partial class HexPosition
    {
        private Vector3Int _position;
        public Vector3Int Position
        {
            get
            {
                return _position;
            }
            private set
            {
                _position = value;
            }
        }

        // CONSTRUCTORS
        private HexPosition(int q, int r, int s)
        {
            Position = new Vector3Int(q, r, s);
        }

        private HexPosition(Vector3Int position)
        {
            if (IsPositionLegal(position))
                Position = position;
        }

        private HexPosition(HexPosition other)
        {
            Position = other.Position;
        }


        // OVERRIDES
        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            HexPosition hex_obj = obj as HexPosition;
            if (obj == null || hex_obj == null)
            {
                return false;
            }
            return Position.Equals(hex_obj.Position);
        }

    }

}