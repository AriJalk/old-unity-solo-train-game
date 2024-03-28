using SoloTrainGame.Core;
using UnityEngine;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 2f;
    const float MIN_RADIUS = 1f;
    const float MAX_RADIUS = 8f;


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
    [Range(1f, 500f)]
    private float _scrollSpeedBase = 50f;
    [SerializeField]
    [Range(0.5f, 20f)]
    private float verticalRotationSpeed = 5f;

    Transform _transform;
    Camera _camera;
    Vector3 _center;

    // Degress for camera rotation
    private float _degrees;
    private float currentRadius = RADIUS;

    // For raycasting
    private int tileLayer;
    private int backgroundLayer;
    private int layerMask;

    void Start()
    {
        _transform = transform;
        _center = HexGridController.Center;
        _degrees = 0;
        _camera = GetComponent<Camera>();
        tileLayer = 1 << LayerMask.NameToLayer("Tiles");
        backgroundLayer = 1 << LayerMask.NameToLayer("Background");
        layerMask = tileLayer | backgroundLayer;
        UpdateCameraPosition(true);

    }

    void Update()
    {
        // Mouse click
        if (Input.GetMouseButtonDown(0))
        {
            HexTileObject tile = RaycastHitToHexTile(CameraRaycast(_camera.ScreenPointToRay(Input.mousePosition), layerMask));
            if (tile != null)
                Debug.Log(tile.HexData.Hex.Position);

        }

        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            RotateCameraWithMouse();
        }


        // Camera movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
            MoveCamera(horizontalInput, verticalInput);

        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
            ZoomCamera(scroll);
    }

    void MoveCamera(float horizontalInput, float verticalInput)
    {
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


    void RotateCameraWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Horizontal rotation around Y axis
        _degrees -= mouseX * 10;
        if (_degrees < -360)
            _degrees += 360;

        // Vertical rotation around X axis
        float verticalRotationDelta = mouseY * verticalRotationSpeed;

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
        if (hit.collider != null && hit.collider.transform.parent.GetComponent<HexTileObject>() is HexTileObject tileObject)
        {
            return tileObject;
        }
        return null;
    }
}