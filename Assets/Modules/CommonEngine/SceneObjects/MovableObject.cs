using CommonEngine.IO;
using System;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
	[SerializeField]
	private InputEvents _inputEvents;
	[SerializeField]
	private Transform _forwardTransform;
	[SerializeField]
	private Vector3 _movementVector = Vector3.zero;
	[SerializeField]
	[Range(0f, 20f)]
	private float _speed = 5;

	void Start()
	{
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
