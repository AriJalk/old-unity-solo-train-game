using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


namespace HexSystem
{
	public partial class HexCoord
	{
		private static Dictionary<Vector2Int, HexCoord> _cache = new Dictionary<Vector2Int, HexCoord>();

		public readonly int Q;
		public readonly int R;
		public readonly int S;

		private HexCoord(int Q, int R)
		{
			this.Q = Q;
			this.R = R;
			this.S = -Q - R;
		}

		public static HexCoord GetCoord(int Q, int R)
		{
			Vector2Int vec = new Vector2Int(Q, R);
			if (_cache.ContainsKey(vec))
			{
				return _cache[vec];
			}
			HexCoord coord = new HexCoord(Q, R);
			_cache.Add(vec, coord);
			return coord;
		}

		public ICollection<HexCoord> GetNeighbors()
		{
			List<HexCoord> neighbors = new List<HexCoord>();
			foreach (HexCoord neighbor in DirectionDictionary.Values)
			{
				neighbors.Add(this + neighbor);
			}

			return neighbors;
		}

		public override string ToString()
		{
			return $"Q: {Q}| R: {R}| S: {S}";
		}
	}
}
