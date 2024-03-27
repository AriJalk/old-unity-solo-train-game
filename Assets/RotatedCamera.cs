using SoloTrainGame.Core;
using UnityEngine;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 2f;

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
    [Range(50f, 500f)]
    private float _scrollSpeedBase = 50f;
    [SerializeField]
    [Range(1f, 50f)]
    private float _scrollSpeedMultiplier = 10f;

    Transform _transform;
    Camera _camera;
    Vector3 _center;
    float _degrees;

    int tileLayer;
    int backgroundLayer;
    int layerMask;

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
        float clampedX = Mathf.Clamp(_transform.position.x + moveDirection.x, HexGridController.MinX - _overBounds, HexGridController.MaxX + _overBounds);
        float clampedZ = Mathf.Clamp(_transform.position.z + moveDirection.z, HexGridController.MinZ - _overBounds, HexGridController.MaxZ + _overBounds);

        // Move the camera along the XZ plane
        _transform.position = new Vector3(clampedX, _transform.position.y, clampedZ);
        // Update center of camera
        RaycastHit hit = CameraRaycast(_camera.ScreenPointToRay(GetCenterOfScreen()), layerMask);
        if (hit.collider != null)
        {
            _center = hit.point;
        }
    }

    void ZoomCamera(float scroll)
    {
        Camera cameraComponent = GetComponent<Camera>();
        float newFOV = cameraComponent.fieldOfView - scroll * Time.deltaTime * _scrollSpeedBase * _scrollSpeedMultiplier;
        float clampedFOV = Mathf.Clamp(newFOV, 25f, 100f); // Adjust these values as needed
        cameraComponent.fieldOfView = clampedFOV;
    }

    void RotateCameraWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        _degrees -= mouseX * 10;
        if (_degrees < -360)
            _degrees += 360;
        UpdateCameraPosition(true);
    }

    void UpdateCameraPosition(bool lookAt)
    {
        Vector2 circlePosition = CalculateCirclePoint();
        _transform.position = new Vector3(circlePosition.x + _center.x, _transform.position.y, circlePosition.y + _center.z);
        if (lookAt == true)
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