using CommonEngine.Interfaces;
using System;

public class GoodsCube : IIdentifiable
{
	public Guid guid { get; }

	public GoodsColor Color { get; }

	public GoodsCube(Guid guid, GoodsColor color)
	{
		this.guid = guid;
		Color = color;
	}
}
