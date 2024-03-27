using UnityEngine;

namespace HexSystem
{
    /// <summary>
    /// Hex data
    /// </summary>
    public partial class Hex
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
        private Hex(int q, int r, int s)
        {
            Position = new Vector3Int(q, r, s);
        }

        private Hex(Vector3Int position)
        {
            if (IsPositionLegal(position))
                Position = position;
        }

        private Hex(Hex other)
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
            Hex hex_obj = obj as Hex;
            if (obj == null || hex_obj == null)
            {
                return false;
            }
            return Position.Equals(hex_obj.Position);
        }

    }

}