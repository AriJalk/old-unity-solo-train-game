using System;
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

		public bool WasInputReleasedThisFrame { get; set; }

		public void AddLock(GameObject lockOwner)
		{
			if (lockOwner != null)
			{
				_lockingObjects.Add(lockOwner);
			}
		}

		public void RemoveLock(GameObject lockObject)
		{
			if (lockObject != null)
			{
				_lockingObjects.Remove(lockObject);
				if (_lockingObjects.Count == 0)
				{
					WasInputReleasedThisFrame = true;
				}
			}
		}
	}
}
