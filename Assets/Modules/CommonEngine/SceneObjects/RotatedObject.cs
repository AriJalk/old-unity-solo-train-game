using CommonEngine.Core;
using CommonEngine.EngineEvents;
using UnityEngine;

namespace CommonEngine.SceneObjects
{
	/// <summary>
	/// Gives rotation functionality to a gameObject in response to input events
	/// </summary>
	public class RotatedObject : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;

		/// <summary>
		/// The pivot transform to rotate around
		/// </summary>
		[SerializeField]
		private Transform _rotateAround;
		/// <summary>
		/// Rotation radius
		/// </summary>
		[SerializeField]
		private float _radius = 4;
		/// <summary>
		/// Constant offset to the position of the object after rotation
		/// </summary>
		[SerializeField]
		private Vector3 _offset = Vector3.zero;
		[SerializeField]
		private float _acceleration = 1;
		/// <summary>
		/// Initial rotation degrees
		/// </summary>
		[SerializeField]
		[Range(-360f, 360f)]
		private float _initialRotation = -90;

		private InputEvents _inputEvents;

		private Vector2 _dragVector = Vector2.zero;
		private float _orbitRotation = 0;

		private static float ClampTo360Degrees(float angle)
		{
			if (angle < -360)
				return angle + 360;

			else if (angle > 360)
				return angle - 360;
			return angle;
		}


		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_inputEvents = _commonServices.InputEvents;
			_orbitRotation = _initialRotation;
			_inputEvents.WorldDraggedEvent += RotateObject;
			SetPositionOnCircle();
		}


		private void OnDestroy()
		{
			_inputEvents.WorldDraggedEvent -= RotateObject;
		}

		private void LateUpdate()
		{
			if (_dragVector != Vector2.zero)
			{
				_orbitRotation -= _dragVector.x * Time.deltaTime * _acceleration;
				SetPositionOnCircle();
				_dragVector = Vector2.zero;
			}
		}


		private void SetPositionOnCircle()
		{
			_orbitRotation = ClampTo360Degrees(_orbitRotation);

			float radiansY = Mathf.Deg2Rad * _orbitRotation;

			float axis1 = Mathf.Cos(radiansY) * _radius;
			float axis2 = Mathf.Sin(radiansY) * _radius;

			Vector3 changeVector = Vector3.right * axis1 + Vector3.forward * axis2;
			Vector3 newPosition = _rotateAround.position + changeVector + _offset;

			transform.position = newPosition;

			transform.LookAt(_rotateAround.position, Vector3.up);
		}

		private void RotateObject(Vector2 movement)
		{
			if(!_commonServices.InputLock.IsInputLocked)
				_dragVector = movement;
		}
	}
}