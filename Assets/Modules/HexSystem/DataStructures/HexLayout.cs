using UnityEngine;

namespace HexSystem
{
	/// <summary>
	/// Info regarding how to place the tiles
	/// </summary>
	public class HexLayout
	{
		public HexOrientation Orientation { get; }

		public float Size { get; }
		public float GapProportion { get; }

		public HexLayout(HexOrientation orientation, float size, float gapProportion)
		{
			Orientation = orientation;
			Size = size;
			GapProportion = gapProportion;
		}
	}

	public struct HexOrientation
	{
		public static readonly HexOrientation PointyLayout = new HexOrientation(
					Mathf.Sqrt(3f), Mathf.Sqrt(3f) / 2f, 0f, 3f / 2f,
					Mathf.Sqrt(3f) / 3f, -1f / 3f, 0f, 2f / 3f,
					0.5f);

		public static readonly HexOrientation FlatLayout = new HexOrientation(
					3f / 2f, 0f, Mathf.Sqrt(3f) / 2f, Mathf.Sqrt(3f),
					2f / 3f, 0f, -1f / 3f, Mathf.Sqrt(3f) / 3f,
					0f);

		public readonly float F0, F1, F2, F3;
		public readonly float B0, B1, B2, B3;

		public readonly float StartAngle;

		public HexOrientation(float f0, float f1, float f2, float f3, float b0, float b1, float b2, float b3, float startAngle)
		{
			F0 = f0;
			F1 = f1;
			F2 = f2;
			F3 = f3;
			B0 = b0;
			B1 = b1;
			B2 = b2;
			B3 = b3;
			StartAngle = startAngle;
		}
	}
}