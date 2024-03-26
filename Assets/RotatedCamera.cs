using SoloTrainGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatedCamera : MonoBehaviour
{
    const float RADIUS = 2f;
    Transform _transform;
    Vector3 Center;
    float degrees;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        Center = HexGridController.Center;
        degrees = 0;
        Vector2 circlePosition = CalculateCirclePoint();
        _transform.position = new Vector3(circlePosition.x + Center.x, _transform.position.y, circlePosition.y + Center.z);
        _transform.LookAt(Center);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            degrees += Time.deltaTime / 10;
            if (degrees > 360)
                degrees -= 360;
            Vector2 circlePosition = CalculateCirclePoint();
            _transform.position = new Vector3(circlePosition.x + Center.x, _transform.position.y, circlePosition.y + Center.z);
            _transform.LookAt(Center);
        }
        if (Input.GetKey(KeyCode.D))
        {
            degrees -= Time.deltaTime / 10;
            if (degrees < -360)
                degrees += 360;
            Vector2 circlePosition = CalculateCirclePoint();
            _transform.position = new Vector3(circlePosition.x + Center.x, _transform.position.y, circlePosition.y + Center.z);
            _transform.LookAt(Center);
        }
    }

    Vector2 CalculateCirclePoint()
    {
        float radians = Mathf.Rad2Deg * degrees;
        float x = Mathf.Cos(radians) * RADIUS;
        float y = Mathf.Sin(radians) * RADIUS;

        return new Vector2(x, y);

    }
}
