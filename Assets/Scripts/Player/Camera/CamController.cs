using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {

    public Transform Target { get { return target; } set { target = value; } }
    public Transform Player { get { return player; } set { player = value; } }
    public bool Falling { get { return falling; } set { falling = value; } }

    public int zoomMin, zoomMax;
    public float speed = 700f,
        cameraSensitivity = 3f, cameraZoomSensitivity = 10f, cameraZoomDamp = 5f;

    private Transform target;
    private Transform player;
    public bool invertVertical = true;
    private float totalXRotation, zoomlvl = 0.5f, xRotation = 0, minVerticalLimit = 5, maxVerticalLimit = 70;
    private bool falling;


    void Start()
    {
        falling = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //TODO: Jos pelaajan ja kameran välissä seinä. nini raycastaa enenn sijaintia pelaajan ja kameran väliin. jos törmää johonkin muuhun kuin pelajaaan ota osuman position 
    void Update()
    {
        if (!falling && Cursor.lockState == CursorLockMode.Locked)
        {
            SetRot();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleCursorLock();
        }
        Zoom();
    }

    void SetRot()
    {
        xRotation = CalculateXRotation();
        target.transform.localEulerAngles = new Vector3(xRotation, target.transform.localEulerAngles.y, target.transform.localEulerAngles.z);
        transform.rotation = target.rotation;
    }

    void Zoom()
    {       
        Vector3 newPosition = new Vector3();
        zoomlvl -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * cameraZoomSensitivity;
        zoomlvl = Mathf.Clamp(zoomlvl, 0.0f, 1.0f);
        newPosition = target.position + transform.rotation * new Vector3(0, 0, zoomMin - zoomlvl * zoomMax);
        transform.position = newPosition;
    }

    void ToggleCursorLock()
    {
        GameManager.instance.ToggleCursorLock();
    }

    public void setFalltoDeathPosition()
    {
        falling = true;
        transform.Rotate(90f,0,0, Space.Self);
    }

    public float CalculateXRotation()
    {
        if (invertVertical)
        {
            xRotation += cameraSensitivity * Input.GetAxis("Mouse Y");
        }
        else
        {
            xRotation -= cameraSensitivity * Input.GetAxis("Mouse Y");
        }
        return Mathf.Clamp(xRotation, minVerticalLimit, maxVerticalLimit);

    }
}
