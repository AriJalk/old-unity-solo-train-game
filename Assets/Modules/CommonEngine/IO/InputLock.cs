using System.Collections.Generic;
using UnityEngine;

namespace CommonEngine.IO
{
	public class InputLock
	{
		private readonly HashSet<GameObject> _lockingObjects = new HashSet<GameObject>();

		public bool IsInputLocked
		{
			get
			{
				return _lockingObjects.Count > 0;
			}
		}

		public void AddLock(GameObject lockObject)
		{
			if (lockObject != null)
			{
				_lockingObjects.Add(lockObject);
			}
		}

		public void RemoveLock(GameObject lockObject)
		{
			if (lockObject != null)
			{
				_lockingObjects.Remove(lockObject);
			}
		}
	}
}
