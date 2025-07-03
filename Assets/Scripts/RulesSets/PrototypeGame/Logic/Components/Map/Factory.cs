using CommonEngine.Interfaces;
using PrototypeGame.Logic;
using System;

public class Factory : IIdentifiable
{
	public Guid guid { get; private set; }

	public readonly GoodsColor ProductionColor;

	public GoodsCubeSlot GoodsCubeSlot { get; set; }

	public Factory(Guid guid, GoodsColor productionColor)
	{
		this.guid = guid;
		ProductionColor = productionColor;
	}
}
