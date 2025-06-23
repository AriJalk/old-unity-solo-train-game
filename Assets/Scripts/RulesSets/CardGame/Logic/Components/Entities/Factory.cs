using System;
using UnityEngine;

public class Factory
{
	public readonly GoodsColor ProductionColor;

	public GoodsCube GoodsCube { get; set; }

	public Factory(GoodsColor productionColor)
	{
		ProductionColor = productionColor;
	}
}
