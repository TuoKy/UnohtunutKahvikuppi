using UnityEngine;
using System.Collections;

public class NewCamController : MonoBehaviour {

    public Transform Target { get { return target; } set { target = value; } }
    public Transform Player { get { return player; } set { player = value; } }

    public int zoomMin, zoomMax;
    public float speed = 700f,
        cameraSensitivity = 3f, cameraZoomSensitivity = 10f, cameraZoomDamp = 5f;
    public bool invertXAxis = false;

    private Transform target;
    private Transform player;
    private float totalXRotation, zoomlvl = 0.5f;
    private Vector3 vel = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    //TODO: Jos pelaajan ja kameran välissä seinä. nini raycastaa enenn sijaintia pelaajan ja kameran väliin. jos törmää johonkin muuhun kuin pelajaaan ota osuman position 
    void Update()
    { 
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            SetRot();
            Zoom();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleCursorLock();
        }
    }

    void SetRot()
    {
        transform.rotation = player.rotation;
    }

    void Zoom()
    {       
        Vector3 newPosition = new Vector3();
        zoomlvl -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * cameraZoomSensitivity;
        zoomlvl = Mathf.Clamp(zoomlvl, 0.0f, 1.0f);
        newPosition = target.position + transform.rotation * new Vector3(0, 0, zoomMin - zoomlvl * zoomMax);

        //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref vel, 0.1f);
        transform.position = newPosition;
    }

    void ToggleCursorLock()
    {
        GameManager.instance.ToggleCursorLock();
    }

    public void setSpeed(float value)
    {
        speed = value;
    }
   
}
