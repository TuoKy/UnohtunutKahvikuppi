using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Roundabout : MonoBehaviour {

    public List<GameObject> platform;
    public GameObject target;
    private float rotateSpeed = 10f;
    private float verticalSpeed = 3f;

    void FixedUpdate()
    {
        foreach (GameObject plat in platform)
        {
            plat.transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * rotateSpeed);

            int compare = (int)plat.transform.rotation.eulerAngles.y / 90;

            if (((compare % 4) % 2) == 1)   //Down
            {   
                plat.transform.position = new Vector3(plat.transform.position.x, plat.transform.position.y - verticalSpeed * Time.deltaTime, plat.transform.position.z);
            }
            else    //Up
            {
                plat.transform.position = new Vector3(plat.transform.position.x, plat.transform.position.y + verticalSpeed * Time.deltaTime, plat.transform.position.z);
            }
        }
    }
}
