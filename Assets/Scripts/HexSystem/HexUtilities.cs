using System.Collections.Generic;
using UnityEngine;

namespace HexSystem
{
    public partial class HexPosition
    {
        public enum HexDirection
        {
            NORTH,
            SOUTH,
            NORTH_WEST,
            NORTH_EAST,
            SOUTH_WEST,
            SOUTH_EAST,

        }


        public static HexPosition ZERO
        {
            get
            {
                return new HexPosition(0, 0, 0);
            }
        }

        public static Dictionary<HexDirection, Vector3Int> DirectionVectorDictionary = new Dictionary<HexDirection, Vector3Int>()
        {
            {HexDirection.NORTH, new Vector3Int(0, -1, +1) },
            {HexDirection.SOUTH, new Vector3Int(0, +1, -1) },
            {HexDirection.NORTH_WEST, new Vector3Int(-1, 0, +1) },
            {HexDirection.NORTH_EAST, new Vector3Int(+1, -1, 0) },
            {HexDirection.SOUTH_WEST, new Vector3Int(-1, +1, 0) },
            {HexDirection.SOUTH_EAST, new Vector3Int(+1, 0, -1) },
        };


        private static bool IsPositionLegal(Vector3Int coordinates)
        {
            return coordinates.x + coordinates.y + coordinates.z == 0;
        }

        public static HexPosition BuildHex(int q, int r, int s)
        {
            Vector3Int coordinates = new Vector3Int(q, r, s);
            if (IsPositionLegal(coordinates))
            {
                return new HexPosition(q, r, s);
            }
            return null;
        }

        public static HexPosition GetHexNeighbor(HexPosition hex, HexDirection direction)
        {
            if (hex != null)
            {
                return new HexPosition(hex.Position + DirectionVectorDictionary[direction]);
            }
            return null;
        }

        private static float CalculateGap(float hexSize, float gapProportion)
        {
            return hexSize * gapProportion;
        }

        public static Vector3 HexToWorld(HexPosition hex, float hexSize, float gapProportion, HexOrientation orientation)
        {
            float gap = CalculateGap(hexSize, gapProportion);

            float x = (orientation.F0 * hex.Position.x + orientation.F1 * hex.Position.y) * hexSize;

            if (x > 0)
                x += gap * Mathf.Abs(x);

            else if (x < 0)
                x -= gap * Mathf.Abs(x);

            float y = 0;  // Assuming hexagons are placed flat on the ground
            float z = (orientation.F2 * hex.Position.x + orientation.F3 * hex.Position.y) * hexSize;

            if (z > 0)
                z += gap * Mathf.Abs(z);

            else if (z < 0)
                z -= gap * Mathf.Abs(z);


            return new Vector3(x, y, z);
        }


    }
}