using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PrototypeGame.Scene
{
	public class GoodsCubeSlotObject : MonoBehaviour, IIdentifiable
	{
		public Guid guid {  get; set; }

		public Transform GoodsCubeObjectTransform { get; set; }

		public void Initialize(Guid guid)
		{
			this.guid = guid;
		}
	}
}
