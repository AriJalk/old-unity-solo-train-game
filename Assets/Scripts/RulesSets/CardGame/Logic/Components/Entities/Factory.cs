using CardGame.Logic;
using System;
using UnityEngine;

public class Factory
{
	public readonly GoodsColor ProductionColor;

	public GoodsCubeSlot GoodsCubeSlot {  get; set; }

	public Factory(GoodsColor productionColor)
	{
		ProductionColor = productionColor;
	}
}
