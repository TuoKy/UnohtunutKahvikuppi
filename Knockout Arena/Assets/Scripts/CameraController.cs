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

    void Update()
    {
        CalculateActualDirection();

        movement = actualDirection * Time.deltaTime * speed;

        CalculateTotalXRotation();

        parent.Rotate(0, totalXRotation, 0);

    }

    void FixedUpdate()
    {
        transform.parent.GetComponent<Rigidbody>().AddForce(movement, ForceMode.Force);
    }

    private void CalculateActualDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0.0f, verticalInput);

        actualDirection = transform.TransformDirection(direction);
    }

    private void CalculateTotalXRotation()
    {
        totalXRotation = (Input.mousePosition.x - oldMousePos.x) * cameraSensitivity;

        if (invertXAxis)
        {
            totalXRotation *= -1;
        }
        oldMousePos = Input.mousePosition;
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
