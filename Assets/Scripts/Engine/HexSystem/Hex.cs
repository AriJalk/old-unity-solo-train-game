using UnityEngine;

namespace HexSystem
{
    /// <summary>
    /// HexTile data
    /// </summary>
    public partial class Hex
    {
        public int Q { get; private set; }
        public int R { get; private set; }
        public int S { get; private set; }
        // CONSTRUCTORS
        private Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }


        private Hex(Hex other)
        {
            Q = other.Q;
            R = other.R;
            S = other.S;
        }


        // OVERRIDES
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 31 + Q.GetHashCode();
                hash = hash * 31 + R.GetHashCode();
                hash = hash * 31 + S.GetHashCode();
                return hash;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj is Hex hex_obj)
            {
                return Q == hex_obj.Q && R == hex_obj.R && S == hex_obj.S;
            }
            return false;
        }

        public override string ToString()
        {
            return "[" + Q + "," + R + "," + S + "]";
        }
    }

}