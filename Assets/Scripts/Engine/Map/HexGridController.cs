using HexGridSystem;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridController : MonoBehaviour
{

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		HexLayout layout = new HexLayout(HexOrientation.FlatLayout, 0.5f, 0.1f);
		GameObject prefab = Resources.Load<GameObject>("Prefabs/HexTile");

		HexCoord coord = HexCoord.GetCoord(0, 0);

		GameObject obj = Instantiate(prefab);
		obj.transform.SetParent(transform);

		obj.transform.position = HexCoord.CoordToWorld(coord, layout);

		ICollection<HexCoord> neighbors = coord.GetNeighbors();

		foreach (HexCoord neighbor in neighbors)
		{
			obj = Instantiate(prefab);
			obj.transform.SetParent(transform);

			obj.transform.position = HexCoord.CoordToWorld(neighbor, layout);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
