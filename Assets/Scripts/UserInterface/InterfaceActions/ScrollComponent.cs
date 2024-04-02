using Engine;
using UnityEngine;
using UnityEngineInternal;

public class ScrollComponent : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 500f)]
    private float _scrollSpeed = 100f;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _scrollDampener = 0.5f;
    [SerializeField]
    [Range(0.1f, 2f)]
    private float _scrollDuration = 1f;
    [SerializeField]
    private RectTransform _rectTransform;

    private OnUpdateTimer _timer;
    private float _currentSpeed;
    private float _movement;


    public bool IsScrolling { get; private set; }

    private void Awake()
    {
        ServiceLocator.InputManager.MouseButtonUpEvent.AddListener(MouseUp);
    }

    private void OnDestroy()
    {
        ServiceLocator.InputManager.MouseButtonUpEvent.RemoveListener(MouseUp);
    }

    private void Scroll()
    {
        float stepMovement = _rectTransform.position.x + _movement * _currentSpeed * Time.deltaTime;
        _rectTransform.anchoredPosition = new Vector2(stepMovement, 0);
        _currentSpeed -= Time.deltaTime * _scrollDampener;
        if (_currentSpeed < 0.1f)
        {
            _timer.StopTimer();
        }    
    }

    private void MouseUp(int index, Vector2 movement)
    {
        if (_timer != null)
        {
            _timer.StopTimer();
        }
        //Debug.Log(movement);
        IsScrolling = false;

        // Calculate the duration based on movement speed and a factor
        float duration = Mathf.Abs(movement.x) * _scrollDuration;
        _movement = movement.normalized.x;
        // Start a timer with the calculated duration
        _timer = ServiceLocator.TimerManager.GetOnUpdateTimer(duration, Scroll);
        Debug.Log("Duration: " + duration +", Movement: " + movement.x);
    }

    public void StartScroll(float movement)
    {
        if (movement != 0)
        {
            Vector2 currentPosition = _rectTransform.anchoredPosition;

            float newX = currentPosition.x + movement * _scrollSpeed * Time.deltaTime;

            _rectTransform.anchoredPosition = new Vector2(newX, currentPosition.y);
            IsScrolling = true;
        }
    }

}