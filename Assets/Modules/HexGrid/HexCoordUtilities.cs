using System.Collections.Generic;
using UnityEngine;


namespace HexGridSystem
{
	/// <summary>
	/// Utilities for hex coordinates
	/// </summary>
	public partial struct HexCoord
	{
		public enum Direction
		{
			NORTH = 0,
			SOUTH = 1,
			NORTH_WEST = 2,
			NORTH_EAST = 3,
			SOUTH_WEST = 4,
			SOUTH_EAST = 5,
		}

		public static Dictionary<Direction, HexCoord> DirectionDictionary = new Dictionary<Direction, HexCoord>()
		{
            {Direction.NORTH, GetCoord(0, +1)},
			{Direction.SOUTH, GetCoord(0, -1)},
			{Direction.NORTH_WEST, GetCoord(+ 1, 0)},
			{Direction.NORTH_EAST, GetCoord(-1, + 1)},
			{Direction.SOUTH_WEST, GetCoord(+ 1, -1)},
			{Direction.SOUTH_EAST, GetCoord(-1, 0)},
		};

		private static float CalculateGap(float hexSize, float gapProportion)
		{
			return hexSize * gapProportion;
		}

		public static Vector3 CoordToWorld(HexCoord hexCoord, HexLayout layout)
		{
			float gap = CalculateGap(layout.Size, layout.GapProportion);

			float x = (layout.Orientation.F0 * hexCoord.Q + layout.Orientation.F1 * hexCoord.R) * layout.Size;

			if (x > 0)
				x += gap * Mathf.Abs(x);

			else if (x < 0)
				x -= gap * Mathf.Abs(x);

			float y = 0;  // Assuming hexagons are placed flat on the ground
			float z = (layout.Orientation.F2 * hexCoord.Q + layout.Orientation.F3 * hexCoord.R) * layout.Size;

			if (z > 0)
				z += gap * Mathf.Abs(z);

			else if (z < 0)
				z -= gap * Mathf.Abs(z);


			return new Vector3(x, y, z);
		}

		public static HexCoord operator +(HexCoord hex1, HexCoord hex2)
		{
			return new HexCoord(hex1.Q + hex2.Q, hex1.R + hex2.R);
		}
	}
}
