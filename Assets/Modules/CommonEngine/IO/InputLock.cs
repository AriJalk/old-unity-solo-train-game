using System.Collections.Generic;
using UnityEngine;

namespace CommonEngine.IO
{
	/// <summary>
	/// A shared lock service for input events, each listener for input events can either enforce or ignore the lock giving freedom based on the use case
	/// </summary>
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

		private bool _wasInputReleasedThisFrame = false;
		public bool WasInputReleasedThisFrame
		{
			get
			{
				if (_wasInputReleasedThisFrame)
				{
					_wasInputReleasedThisFrame = false;
					return true;
				}
				return false;
			}
		}

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
					_wasInputReleasedThisFrame = true;
				}
			}
		}
	}
}
