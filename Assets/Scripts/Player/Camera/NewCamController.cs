﻿using UnityEngine;
using System.Collections;

public class NewCamController : MonoBehaviour {

    public Transform Target { get { return target; } set { target = value; } }
    public Transform Player { get { return player; } set { player = value; } }
    public bool Falling { get { return falling; } set { falling = value; } }

    public int zoomMin, zoomMax;
    public float posX, posY,
        cameraSensitivity = 3f, cameraZoomSensitivity = 10f, cameraZoomDamp = 5f;
    public bool invertXAxis = false;

    private Transform target;
    private Transform player;
    private float totalXRotation, zoomlvl = 0.5f;
    private Vector3 vel = Vector3.zero;
    private bool falling;


    void Start()
    {
        falling = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //TODO: Jos pelaajan ja kameran välissä seinä. nini raycastaa enenn sijaintia pelaajan ja kameran väliin. jos törmää johonkin muuhun kuin pelajaaan ota osuman position 
    void Update()
    {
        if (!falling)
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
        newPosition = target.position + transform.rotation * new Vector3(posX, posY, zoomMin - zoomlvl * zoomMax);

        //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref vel, 0.1f);
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

}