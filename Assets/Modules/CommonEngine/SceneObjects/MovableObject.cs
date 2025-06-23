using CommonEngine.Core;
using CommonEngine.IO;
using System;
using UnityEngine;


namespace CommonEngine.SceneObjects
{
	public class MovableObject : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private Transform _forwardTransform;
		[SerializeField]
		private Vector3 _movementVector = Vector3.zero;
		[SerializeField]
		[Range(0f, 20f)]
		private float _speed = 5;

		private InputEvents _inputEvents;

		void Start()
		{
			_inputEvents = _commonServices.InputEvents;
			_inputEvents.AxisMovedEvent?.AddListener(OnAxisMoved);
		}

		private void OnDestroy()
		{
			_inputEvents.AxisMovedEvent?.RemoveListener(OnAxisMoved);
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
			Vector3 axis = Vector3.right + Vector3.forward;
			Vector3 forward = Vector3.Scale(_forwardTransform.forward, axis).normalized;
			Vector3 right = Vector3.Scale(_forwardTransform.right, axis).normalized;

			_movementVector = right * movement.x + forward * movement.y;
		}
	}
}