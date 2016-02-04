using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{

    public float speed;
    public float cameraSensitivity = 0.5f;
    public bool invertXAxis = false;
    public Transform cameraParent;

    private Vector2 oldMousePos;
    private float totalXRotation;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * Time.deltaTime * speed;
        cameraParent.Translate(movement, Space.Self);
        
        totalXRotation = (Input.mousePosition.x - oldMousePos.x) * cameraSensitivity;

        if (invertXAxis)
        {
            totalXRotation *= -1;
        }

        cameraParent.Rotate(0, totalXRotation, 0);
        
        oldMousePos = Input.mousePosition;



    }

    public void setSpeed(float value)
    {
        speed = value;
    }

    public void setPosition(Transform kohde)
    {
        cameraParent.position = kohde.position;
    }

}
