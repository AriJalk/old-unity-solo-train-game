using UnityEngine;

namespace CommonEngine.Componenets
{
	/// <summary>
	/// Component to allow direction manipulation of the mesh of an object, attach to every GameObject that has a mesh that needs to be manipulated without using Find
	/// </summary>
	public class MeshComponent : MonoBehaviour
	{
		public MeshRenderer MeshRenderer;
	}
}
