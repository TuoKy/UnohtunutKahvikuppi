using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{

    public int Speed;
    public Transform cameraParent;

    private Vector2 oldMousePos;
    private float zoomlvl = 0.5f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * Time.deltaTime * Speed;
        cameraParent.Translate(movement, Space.Self);

        if (Input.GetMouseButton(1))
        {
            cameraParent.Rotate(0, oldMousePos.x - Input.mousePosition.x, 0);
        }
        oldMousePos = Input.mousePosition;



    }

    public void setSpeed(int value)
    {
        Speed = value;
    }

    public void setPosition(Transform kohde)
    {
        cameraParent.position = kohde.position;
    }

}
