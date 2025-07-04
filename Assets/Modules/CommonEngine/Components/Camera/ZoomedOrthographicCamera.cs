using CommonEngine.Core;
using CommonEngine.IO;
using UnityEngine;

namespace CommonEngine.Componenets
{
	/// <summary>
	/// Changed orthographic camera size according to input events
	/// </summary>
	public class ZoomedOrthographicCamera : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;


		[SerializeField]
		private Camera _camera;
		[SerializeField]
		[Range(0f, 1f)]
		private float _scrollSpeedBase = 0.05f;
		[SerializeField]
		[Range(0f, 20f)]
		private float _scrollDampener = 2f;

		private InputEvents _inputEvents;

		private float _scroll = 0;


		private void Start()
		{
			_inputEvents = _commonServices.InputEvents;
			_inputEvents.MouseScrolledEvent += OnScroll;
		}

		private void OnDestroy()
		{
			_inputEvents.MouseScrolledEvent -= OnScroll;
		}

		private void LateUpdate()
		{
			if (_scroll == 0)
			{
				return;
			}
			if (_scroll > 0)
			{
				_scroll -= _scrollDampener * Time.deltaTime;
				_scroll = Mathf.Clamp(_scroll, 0, _scrollSpeedBase);
			}
			else if (_scroll < 0)
			{
				{
					_scroll += _scrollDampener * Time.deltaTime;
					_scroll = Mathf.Clamp(_scroll, -_scrollSpeedBase, 0);
				}
			}
			_camera.orthographicSize -= _scroll;
			_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 0.5f, 10f);
		}

		private void OnScroll(float scroll)
		{
			if (!_commonServices.InputLock.IsInputLocked)
				_scroll += scroll;
		}
	}
}