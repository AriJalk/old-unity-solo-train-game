using System;
using Unity.VisualScripting;
using UnityEngine;

public class GoodsCubeObject : MeshComponent, IIdentifiable
{
	public Guid guid { get; set; }
}
