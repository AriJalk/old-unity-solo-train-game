
using Engine;
using SoloTrainGame.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 3f;
    const float MIN_RADIUS = 0.75f;
    const float MAX_RADIUS = 12f;

    [SerializeField]
    Transform _cameraTransform;
    [SerializeField]
    [Range(1f, 20f)]
    private float _speed = 8f;
    [SerializeField]
    [Range(1f, 5f)]
    private float _speedMultiplier = 2f;
    [SerializeField]
    [Range(0f, 5f)]
    private float _overBounds = 1f;
    [SerializeField]
    [Range(0.5f, 20f)]
    private float verticalRotationSpeed = 5f;
    [SerializeField]
    [Range(1f, 500f)]
    private float _scrollSpeedBase = 50f;
    [SerializeField]
    [Range(0f, 20f)]
    private float _scrollInertion = 2f;

    Transform _transform;
    Camera _camera;

    // Degress for camera rotation
    private float _degrees;
    private float currentRadius = RADIUS;
    // For momentum scroll
    float _scroll;

    // Bounds
    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = ServiceLocator.InputManager;
        _transform = transform;
        _camera = _cameraTransform.GetComponent<Camera>();
        _degrees = 0;
        _scroll = 0;
    }
    void Start()
    {
        AddInputListeners();
        _transform.position = new Vector3((_maxBounds.x + _minBounds.x) / 2f, 0, (_maxBounds.y + _minBounds.y) / 2f);
        UpdateCameraPosition(true);
    }

    void Update()
    {
        // Zoom momentum
        if (_scroll > 0)
        {
            _scroll -= Time.deltaTime * _scrollSpeedBase;

            _scroll = Mathf.Clamp(_scroll, 0, 100);
            ZoomCamera(_scroll);
        }
        else if (_scroll < 0)
        {
            _scroll += Time.deltaTime * _scrollSpeedBase;
            _scroll = Mathf.Clamp(_scroll, -100, 0);
            ZoomCamera(_scroll);
        }
    }

    private void OnDestroy()
    {
        RemoveInputListeners();
    }

    public void Initialize(Vector2 minBounds, Vector2 maxBounds)
    {
        _minBounds = minBounds;
        _maxBounds = maxBounds;
        
    }

    void AddInputListeners()
    {
        _inputManager.MouseButtonHeldEvent?.AddListener(ProccessHeldEvent);
        _inputManager.AxisMovedEvent?.AddListener(MoveCamera);
        _inputManager.MouseButtonClickedDownEvent?.AddListener(ProccessMouseClick);
        _inputManager.MouseScrolledEvent?.AddListener(MouseScrolled);

    }

    void RemoveInputListeners()
    {
        _inputManager.MouseButtonHeldEvent?.RemoveListener(ProccessHeldEvent);
        _inputManager.AxisMovedEvent?.RemoveListener(MoveCamera);
        _inputManager.MouseButtonClickedDownEvent?.RemoveListener(ProccessMouseClick);
        _inputManager.MouseScrolledEvent?.RemoveListener(MouseScrolled);
    }

    void ProccessHeldEvent(int index, Vector2 movement)
    {
        if (index == 1)
        {
            RotateCameraWithMouse(movement);
        }
    }

    void ProccessMouseClick(int index, Vector2 position)
    {
        if (index == 0 && !GraphicUserInterface.IsMouseOver)
        {
            RaycastHit hit = CameraRaycast(_camera.ScreenPointToRay(Input.mousePosition));
            if (hit.collider != null)
                Debug.Log(hit.collider.transform.position);
        }
    }


    void MoveCamera(Vector2 movement)
    {
        float horizontalInput = movement.x;
        float verticalInput = movement.y;

        float newSpeed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
            newSpeed *= _speedMultiplier;

        // Calculate the movement direction relative to the camera's forward direction
        Vector3 forward = Vector3.Scale(_cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Vector3.Scale(_cameraTransform.right, new Vector3(1, 0, 1)).normalized;

        // Construct the movement direction using the adjusted forward and right directions
        Vector3 moveDirection = (horizontalInput * right + verticalInput * forward).normalized * Time.deltaTime * newSpeed;

        // Calculate the new position
        Vector3 newPosition = _transform.position + moveDirection;

        // Clamp the new position to stay within the rectangular boundary
        newPosition = ClampPosition(newPosition);

        // Move the camera to the new position
        _transform.position = newPosition;
    }




    Vector3 ClampPosition(Vector3 position)
    {
        // Clamp the position to stay within the rectangular boundary
        float clampedX = Mathf.Clamp(position.x, _minBounds.x - _overBounds, _maxBounds.x + _overBounds);
        float clampedZ = Mathf.Clamp(position.z, _minBounds.y - _overBounds, _maxBounds.y + _overBounds);

        return new Vector3(clampedX, position.y, clampedZ);
    }


    /// <summary>
    /// Change zoom by changing the radius of the camera sphere around the center
    /// </summary>
    /// <param name="scroll"></param>
    void ZoomCamera(float scroll)
    {
        currentRadius -= scroll * _scrollSpeedBase * Time.deltaTime;
        currentRadius = Mathf.Clamp(currentRadius, MIN_RADIUS, MAX_RADIUS);
        UpdateCameraPosition(true);
    }

    void MouseScrolled(float scroll)
    {
        if (!GraphicUserInterface.IsMouseOver)
        {
            _scroll = scroll * _scrollInertion;
        }
    }


    void RotateCameraWithMouse(Vector2 movement)
    {

        // Horizontal rotation around Y axis
        _degrees -= movement.x * 10;
        if (_degrees < -360)
            _degrees += 360;

        // Vertical rotation around X axis
        float verticalRotationDelta = movement.y * verticalRotationSpeed;

        Vector3 currentRotation = _cameraTransform.localEulerAngles;
        float newRotationX = currentRotation.x - verticalRotationDelta;
        if (newRotationX > 180f)
            newRotationX -= 360f;

        float clampedRotationX = Mathf.Clamp(newRotationX, 5f, 80f);

        _cameraTransform.localEulerAngles = new Vector3(clampedRotationX, currentRotation.y, currentRotation.z);

        // Update camera position
        UpdateCameraPosition(true);
    }



    void UpdateCameraPosition(bool lookAt)
    {
        float radiansX = Mathf.Deg2Rad * _cameraTransform.localEulerAngles.x;
        float radiansY = Mathf.Deg2Rad * _degrees;

        // Calculate the spherical coordinates
        float x = Mathf.Cos(radiansX) * Mathf.Cos(radiansY) * currentRadius;
        float y = Mathf.Sin(radiansX) * currentRadius;
        float z = Mathf.Cos(radiansX) * Mathf.Sin(radiansY) * currentRadius;

        Vector3 newPosition = new Vector3(x, y, z) + _transform.position;
        _cameraTransform.position = newPosition;

        if (lookAt)
            _cameraTransform.LookAt(_transform.position);
    }


    Vector2 CalculateCirclePoint()
    {
        float radians = Mathf.Deg2Rad * _degrees;
        float x = Mathf.Cos(radians) * RADIUS;
        float y = Mathf.Sin(radians) * RADIUS;
        return new Vector2(x, y);
    }

    Vector3 GetCenterOfScreen()
    {
        // Calculate the center of the screen
        Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        return center;
    }

    RaycastHit CameraRaycast(Ray ray, int mask = -1)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);
        Physics.Raycast(ray, out hit, 15f, mask);
        return hit;
    }
}