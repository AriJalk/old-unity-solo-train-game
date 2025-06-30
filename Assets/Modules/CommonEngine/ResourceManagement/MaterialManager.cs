using System.Collections.Generic;
using UnityEngine;

namespace CommonEngine.ResourceManagement
{
	/// <summary>
	/// Repository for materials
	/// </summary>
	public class MaterialManager
	{
		public readonly Dictionary<string, Material> Materials = new Dictionary<string, Material>();
	}
}
