using CommonEngine.Core;
using CommonEngine.Events;
using System;
using UnityEngine;


namespace CommonEngine.SceneObjects
{
	/// <summary>
	/// An object the responds to input events and moves the bound object accordingly
	/// </summary>
	public class MovableObject : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;
		/// <summary>
		/// A transform that defines movement forward orientation
		/// </summary>
		[SerializeField]
		private Transform _forwardTransform;
		/// <summary>
		/// Movement speed
		/// </summary>
		[SerializeField]
		[Range(0f, 20f)]
		private float _speed = 5;

		private Vector3 _movementVector = Vector3.zero;

		private InputEvents _inputEvents;

		void Start()
		{
			_inputEvents = _commonServices.InputEvents;
			_inputEvents.AxisMovedEvent += OnAxisMoved;
		}

		private void OnDestroy()
		{
			_inputEvents.AxisMovedEvent -= OnAxisMoved;
		}

		private void LateUpdate()
		{
			if (_movementVector != Vector3.zero)
			{
				transform.position += _movementVector * _speed * Time.deltaTime;
				_movementVector = Vector3.zero;
			}
		}

		private void OnAxisMoved(Vector2 movement)
		{
			if (!_commonServices.InputLock.IsInputLocked)
			{
				Vector3 axis = Vector3.right + Vector3.forward;
				Vector3 forward = Vector3.Scale(_forwardTransform.forward, axis).normalized;
				Vector3 right = Vector3.Scale(_forwardTransform.right, axis).normalized;

				_movementVector = right * movement.x + forward * movement.y;
			}
		}
	}
}