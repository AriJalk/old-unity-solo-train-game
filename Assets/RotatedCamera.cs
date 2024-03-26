using SoloTrainGame.Core;
using UnityEngine;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 2f;


    [SerializeField]
    [Range(1f, 5f)]
    private float _speed = 3f;

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
        UpdateCameraPosition();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHex(_camera.ScreenPointToRay(Input.mousePosition));
        }

        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            RotateCameraWithMouse();
        }


        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction relative to the camera's forward direction
        Vector3 forward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Vector3.Scale(transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (horizontalInput * right + verticalInput * forward).normalized;

        if (moveDirection != Vector3.zero)
        {
            // Move the camera along the XZ plane
            transform.position += moveDirection * _speed * Time.deltaTime;
            HexTileObject tile = RaycastHex(_camera.ScreenPointToRay(GetCenterOfScreen()));
            if (tile != null)
            {
                _center = tile.transform.position;
            }
        }

        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0)
        {
            float y = Mathf.Clamp(_transform.position.y - Time.deltaTime * 20f, 1f, 10f);
            _transform.position = new Vector3(_transform.position.x, y, _transform.position.z);

        }
        if (scroll < 0)
        {
            float y = Mathf.Clamp(_transform.position.y + Time.deltaTime * 20f, 1f, 10f);
            _transform.position = new Vector3(_transform.position.x, y, _transform.position.z);
        }
    }

    void RotateCameraWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        _degrees -= mouseX * 10;
        if (_degrees < -360)
            _degrees += 360;
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Vector2 circlePosition = CalculateCirclePoint();
        _transform.position = new Vector3(circlePosition.x + _center.x, _transform.position.y, circlePosition.y + _center.z);
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

    HexTileObject RaycastHex(Ray ray)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);
        Physics.Raycast(ray, out hit, 15f, layerMask);
        if (hit.collider != null)
        {
            Transform parent = hit.collider.gameObject.transform.parent;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tiles") && parent != null)
            {
                HexTileObject tile = parent.GetComponent<HexTileObject>();
                if (tile != null)
                {
                    Debug.Log(tile.HexData.Hex.Position);
                    return tile;
                }
            }
        }
        else
        {
            Debug.Log("NO HIT");
        }

        return null;
    }
}
