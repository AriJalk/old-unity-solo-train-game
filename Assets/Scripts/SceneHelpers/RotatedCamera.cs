
using Engine;
using SoloTrainGame.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 3f;
    const float MIN_RADIUS = 1f;
    const float MAX_RADIUS = 12f;


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
    Vector3 _center;

    // Degress for camera rotation
    private float _degrees;
    private float currentRadius = RADIUS;
    // For momentum scroll
    float _scroll;

    // For raycasting
    private int tileLayer;
    private int backgroundLayer;
    private int layerMask;

    private InputManager _inputManager;

    void Start()
    {
        _transform = transform;
        _center = HexGridController.Center;
        _degrees = 0;
        _camera = GetComponent<Camera>();
        tileLayer = 1 << LayerMask.NameToLayer("Tiles");
        backgroundLayer = 1 << LayerMask.NameToLayer("Background");
        layerMask = tileLayer | backgroundLayer;
        _inputManager = ServiceLocator.InputManager;
        _scroll = 0;
        AddInputListeners();

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
        if (index == 0)
        {
            HexTileObject tile = RaycastHitToHexTile(CameraRaycast(_camera.ScreenPointToRay(Input.mousePosition), layerMask));
            if (tile != null)
                Debug.Log(tile.HexGameData.Hex.Position);
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
        Vector3 forward = Vector3.Scale(_transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Vector3.Scale(_transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (horizontalInput * right + verticalInput * forward).normalized * Time.deltaTime * newSpeed;
        float clampedX = Mathf.Clamp(_transform.position.x + moveDirection.x,
            HexGridController.MinX - _overBounds, HexGridController.MaxX + _overBounds);
        float clampedZ = Mathf.Clamp(_transform.position.z + moveDirection.z, HexGridController.MinZ - _overBounds,
            HexGridController.MaxZ + _overBounds);

        // Move the camera along the XZ plane
        _transform.position = new Vector3(clampedX, _transform.position.y, clampedZ);
        // Update center of camera
        RaycastHit hit = CameraRaycast(_camera.ScreenPointToRay(GetCenterOfScreen()), layerMask);
        if (hit.collider != null)
        {
            _center = hit.point;
        }
    }

    /// <summary>
    /// Change zoom by changing the radius of the camera sphere around the center
    /// </summary>
    /// <param name="scroll"></param>
    void ZoomCamera(float scroll)
    {
        Camera cameraComponent = GetComponent<Camera>();
        currentRadius -= scroll * _scrollSpeedBase * Time.deltaTime;
        currentRadius = Mathf.Clamp(currentRadius, MIN_RADIUS, MAX_RADIUS);
        UpdateCameraPosition(true);
    }

    void MouseScrolled(float scroll)
    {
        _scroll = scroll * _scrollInertion;
    }


    void RotateCameraWithMouse(Vector2 movement)
    {

        // Horizontal rotation around Y axis
        _degrees -= movement.x * 10;
        if (_degrees < -360)
            _degrees += 360;

        // Vertical rotation around X axis
        float verticalRotationDelta = movement.y * verticalRotationSpeed;

        Vector3 currentRotation = _transform.localEulerAngles;
        float newRotationX = currentRotation.x - verticalRotationDelta;
        if (newRotationX > 180f)
            newRotationX -= 360f;

        float clampedRotationX = Mathf.Clamp(newRotationX, 5f, 80f);

        _transform.localEulerAngles = new Vector3(clampedRotationX, currentRotation.y, currentRotation.z);

        // Update camera position
        UpdateCameraPosition(true);
    }






    void UpdateCameraPosition(bool lookAt)
    {
        float radiansX = Mathf.Deg2Rad * _transform.localEulerAngles.x;
        float radiansY = Mathf.Deg2Rad * _degrees;

        // Calculate the spherical coordinates
        float x = Mathf.Cos(radiansX) * Mathf.Cos(radiansY) * currentRadius;
        float y = Mathf.Sin(radiansX) * currentRadius;
        float z = Mathf.Cos(radiansX) * Mathf.Sin(radiansY) * currentRadius;

        Vector3 newPosition = new Vector3(x, y, z) + _center;
        _transform.position = newPosition;

        if (lookAt)
            _transform.LookAt(_center);
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

    RaycastHit CameraRaycast(Ray ray, int mask)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);
        Physics.Raycast(ray, out hit, 15f, mask);
        return hit;
    }

    HexTileObject RaycastHitToHexTile(RaycastHit hit)
    {
        if (hit.collider != null && hit.collider.transform.parent?.GetComponent<HexTileObject>() is HexTileObject tileObject)
        {
            return tileObject;
        }
        return null;
    }
}