using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CardGame.Scene
{
	public class GoodsCubeSlotObject : MonoBehaviour, IIdentifiable
	{
		public Guid guid {  get; set; }

		public GoodsCubeObject CubeObject { get; set; }
	}
}
