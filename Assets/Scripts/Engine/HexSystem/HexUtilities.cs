using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexSystem
{
    public partial class Hex
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

        private static Hex _zero = new Hex(0, 0, 0);
        public static Hex ZERO
        {
            get
            {
                return _zero;
            }
        }

        public static Dictionary<HexDirection, Vector3Int> DirectionVectorDictionary = new Dictionary<HexDirection, Vector3Int>()
        {
            /*
            {HexDirection.NORTH, new Vector3Int(0, -1, +1) },
            {HexDirection.SOUTH, new Vector3Int(0, +1, -1) },
            {HexDirection.NORTH_WEST, new Vector3Int(-1, 0, +1) },
            {HexDirection.NORTH_EAST, new Vector3Int(+1, -1, 0) },
            {HexDirection.SOUTH_WEST, new Vector3Int(-1, +1, 0) },
            {HexDirection.SOUTH_EAST, new Vector3Int(+1, 0, -1) },*/
            // Mirrored
            {HexDirection.NORTH, new Vector3Int (0, +1, -1)},
            {HexDirection.SOUTH, new Vector3Int (0, -1, +1)},
            {HexDirection.NORTH_WEST, new Vector3Int (+1, 0, -1)},
            {HexDirection.NORTH_EAST, new Vector3Int (-1, +1, 0)},
            {HexDirection.SOUTH_WEST, new Vector3Int (+1, -1, 0)},
            {HexDirection.SOUTH_EAST, new Vector3Int (-1, 0, +1)},
        };



        private static bool IsPositionLegal(Vector3Int coordinates)
        {
            return coordinates.x + coordinates.y + coordinates.z == 0;
        }


        private static bool IsPositionLegal(int q, int r, int s)
        {
            return q + r + s == 0;
        }

        public static Hex BuildHex(int q, int r, int s)
        {
            if (IsPositionLegal(q, r, s))
            {
                return new Hex(q, r, s);
            }
            return null;
        }

        public static Hex GetHexNeighbor(Hex hex, HexDirection direction)
        {
            if (hex != null)
            {
                return hex + DirectionVectorDictionary[direction];
            }
            return null;
        }

        public static List<Hex> GetAllNeighbors(Hex hex)
        {
            Debug.Log(hex + " HEX NEIGHBORS: ");
            List<Hex> list = new List<Hex>();
            foreach (HexDirection direction in Enum.GetValues(typeof(HexDirection)))
            {
                list.Add(GetHexNeighbor(hex, direction));
                Debug.Log(GetHexNeighbor(hex, direction));
            }
            return list;
        }

        private static float CalculateGap(float hexSize, float gapProportion)
        {
            return hexSize * gapProportion;
        }

        public static Vector3 HexToWorld(Hex hex, float hexSize, float gapProportion, HexOrientation orientation)
        {
            float gap = CalculateGap(hexSize, gapProportion);

            float x = (orientation.F0 * hex.Q + orientation.F1 * hex.R) * hexSize;

            if (x > 0)
                x += gap * Mathf.Abs(x);

            else if (x < 0)
                x -= gap * Mathf.Abs(x);

            float y = 0;  // Assuming hexagons are placed flat on the ground
            float z = (orientation.F2 * hex.Q + orientation.F3 * hex.R) * hexSize;

            if (z > 0)
                z += gap * Mathf.Abs(z);

            else if (z < 0)
                z -= gap * Mathf.Abs(z);


            return new Vector3(x, y, z);
        }

        public static Hex operator +(Hex hex1, Hex hex2)
        {
            return new Hex(hex1.Q + hex2.Q, hex1.R + hex2.R, hex1.S + hex2.S);
        }

        public static Hex operator +(Hex hex1, Vector3Int direction)
        {
            return new Hex(hex1.Q + direction.x, hex1.R + direction.y, hex1.S + direction.z);
        }


        public static Hex operator -(Hex hex1, Hex hex2)
        {
            return new Hex(hex1.Q - hex2.Q, hex1.R - hex2.R, hex1.S - hex2.S);
        }

        public static Hex operator -(Hex hex1, Vector3Int direction)
        {
            return new Hex(hex1.Q - direction.x, hex1.R - direction.y, hex1.S - direction.z);
        }

        public static Hex operator *(Hex hex, int scalar)
        {
            return new Hex(hex.Q * scalar, hex.R * scalar, hex.S * scalar);
        }

        public static Hex operator *(int scalar, Hex hex)
        {
            return new Hex(hex.Q * scalar, hex.R * scalar, hex.S * scalar);
        }
    }
}