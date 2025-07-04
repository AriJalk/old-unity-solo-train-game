using CommonEngine.Componenets;
using CommonEngine.Interfaces;
using System;

public class GoodsCubeObject : MeshComponent, IIdentifiable
{
	public Guid guid { get; set; }
}
