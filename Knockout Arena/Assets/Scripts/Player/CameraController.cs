using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{
    public float speed = 700f;
    public Vector3 actualDirection, movement;
    public Transform parent;

    public float cameraSensitivity = 0.5f;
    public bool invertXAxis = false;

    private Vector2 oldMousePos;
    private float totalXRotation;

    private float zoomlvl = 0.5f;
    public int zoomMin, zoomMax;

    void Update()
    {
        CamXRotation();

        Zoom();
    }

    private void Zoom()
    {
        float moveZoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * speed;
        zoomlvl -= moveZoom;
        zoomlvl = Mathf.Clamp(zoomlvl, 0.0f, 1.0f);
        transform.position = transform.parent.position + transform.rotation * new Vector3(0.48f, 1.01f, zoomMin - zoomlvl * zoomMax);
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0.0f, verticalInput);

        actualDirection = transform.TransformDirection(direction);
    }

    private void CamXRotation()
    {
        totalXRotation = (Input.mousePosition.x - oldMousePos.x) * cameraSensitivity;

        if (invertXAxis)
        {
            totalXRotation *= -1;
        }
        oldMousePos = Input.mousePosition;

        parent.Rotate(0, totalXRotation, 0);
    }

    public void setSpeed(float value)
    {
        speed = value;
    }

    public void setPosition(Transform kohde)
    {
        parent.position = kohde.position;
    }

}
