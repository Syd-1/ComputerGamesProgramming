using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    private const float yMin = -90.0f;
    private const float yMax = 90.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f;

    private float X = 0.0f;
    private float Y = 45.0f;
    
    private float xSensitivity = 20.0f;
    private float ySsensitivityY = 2.0f;

    private void Start()
    {
        camTransform = transform;
    }

    private void Update()
    {
        X += Input.GetAxis("Mouse X")* xSensitivity;
        Y += Input.GetAxis("Mouse Y")* ySsensitivityY;
        Y = Mathf.Clamp(Y, yMin, yMax);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(Y, X, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
