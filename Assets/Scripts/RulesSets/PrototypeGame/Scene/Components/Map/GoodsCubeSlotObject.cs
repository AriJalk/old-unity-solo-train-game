using CommonEngine.Interfaces;
using System;
using UnityEngine;

namespace PrototypeGame.Scene
{
	public class GoodsCubeSlotObject : MonoBehaviour, IIdentifiable
	{

		public Transform GoodsCubeObjectContainer;

		public Guid guid {  get; set; }
			
		public GoodsCubeObject GoodsCubeObject {  get; set; }

		public void Initialize(Guid guid)
		{
			this.guid = guid;
		}
	}
}
