using Engine;
using UnityEngine;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 3f;


    [SerializeField]
    Transform _cameraTransform;
    [SerializeField]
    [Range (-89.9f, 89.9f)]
    private float _minAngle = 5f;
    [SerializeField]
    [Range(-89.9f, 89.9f)]
    private float _maxAngle = 80f;
    [SerializeField]
    [Range (0f, 5f)]
    private float minRadius = 0.75f;
    [SerializeField]
    [Range(0.1f, 20f)]
    private float maxRadius = 12f;

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
    private float _horizontalRotationSpeed = 5f;
    [SerializeField]
    [Range(0.5f, 20f)]
    private float _verticalRotationSpeed = 5f;
    [SerializeField]
    [Range(1f, 500f)]
    private float _scrollSpeedBase = 50f;
    [SerializeField]
    [Range(0f, 20f)]
    private float _scrollInertion = 2f;

    private Transform _transform;
    private Camera _camera;

    // Degress for camera rotation
    private float _horizontalRotation;
    private float _verticalRotation;
    private bool _isCameraRotated;
    private float _currentRadius = RADIUS;

    // For momentum scroll
    private float _scroll;
    // For camera movement
    private Vector3 _movement;

    // Bounds
    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = ServiceLocator.InputManager;
        _transform = transform;
        _camera = _cameraTransform.GetComponent<Camera>();

    }
    void Start()
    {
        AddInputListeners();
        // Place at center of board
        _transform.position = new Vector3((
            _maxBounds.x + _minBounds.x) * 0.5f, 
            _transform.position.y, 
            (_maxBounds.y + _minBounds.y) * 0.5f);
        _verticalRotation = (_maxAngle + _minAngle) * 0.5f;
        UpdateCameraPosition(true);
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        bool _isStateChanged = false;
        // Zoom momentum
        if (_scroll > 0)
        {
            _scroll -= Time.deltaTime * _scrollSpeedBase;

            _scroll = Mathf.Clamp(_scroll, 0, 100);
            ZoomCamera(_scroll);
            _isStateChanged = true;
        }
        else if (_scroll < 0)
        {
            _scroll += Time.deltaTime * _scrollSpeedBase;
            _scroll = Mathf.Clamp(_scroll, -100, 0);
            ZoomCamera(_scroll);
            _isStateChanged = true;
        }
        if (_isCameraRotated)
        {
            _isStateChanged = true;
        }
        if (_movement != Vector3.zero)
        {
            Vector3 newPosition = ClampPosition(_transform.position + _movement);
            _transform.position = newPosition;
            _isStateChanged = true;
        }
        if (_isStateChanged)
        {
            UpdateCameraPosition(true);
            _movement = Vector3.zero;
            _isCameraRotated = false;
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
        if (index == 0 && !ServiceLocator.UserInterfaceService.IsUILocked)
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
        _movement = (horizontalInput * right + verticalInput * forward).normalized * Time.deltaTime * newSpeed;
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
        _currentRadius -= scroll * _scrollSpeedBase * Time.deltaTime;
        _currentRadius = Mathf.Clamp(_currentRadius, minRadius, maxRadius);
    }

    void MouseScrolled(float scroll)
    {
        if (!ServiceLocator.UserInterfaceService.IsUILocked)
        {
            _scroll = scroll * _scrollInertion;
        }
    }


    void RotateCameraWithMouse(Vector2 movement)
    {
        if (!ServiceLocator.UserInterfaceService.IsUILocked)
        {
            // Horizontal rotation around Y axis
            _horizontalRotation -= movement.x * _horizontalRotationSpeed;
            if (_horizontalRotation < -360)
                _horizontalRotation += 360;
            else if (_horizontalRotation > 360)
            {
                _horizontalRotation -= 360;
            }

            // Vertical rotation around X axis
            _verticalRotation -= movement.y * _verticalRotationSpeed;
            // Allows below 0 rotation
            if (_verticalRotation > 180f)
            {
                _verticalRotation -= 360f;
            }
            _verticalRotation = Mathf.Clamp(_verticalRotation, _minAngle, _maxAngle);
            _isCameraRotated = true;
        }
    }



    void UpdateCameraPosition(bool lookAt)
    {
        float radiansX = Mathf.Deg2Rad * _verticalRotation;
        float radiansY = Mathf.Deg2Rad * _horizontalRotation;

        // Calculate the spherical coordinates
        float x = Mathf.Cos(radiansX) * Mathf.Cos(radiansY) * _currentRadius;
        float y = Mathf.Sin(radiansX) * _currentRadius;
        float z = Mathf.Cos(radiansX) * Mathf.Sin(radiansY) * _currentRadius;

        Vector3 newPosition = new Vector3(x, y, z) + _transform.position;
        _cameraTransform.position = newPosition;

        if (lookAt)
            _cameraTransform.LookAt(_transform.position);
    }


    Vector2 CalculateCirclePoint()
    {
        float radians = Mathf.Deg2Rad * _horizontalRotation;
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