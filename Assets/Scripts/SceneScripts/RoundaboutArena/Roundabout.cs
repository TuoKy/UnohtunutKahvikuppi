using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Roundabout : NetworkBehaviour
{

    public List<GameObject> platform;
    public GameObject target;
    private float rotateSpeed = 10f;
    private float verticalSpeed = 0.1f;

    [ClientCallback]
    void FixedUpdate()
    {
        rotate();
    }

    private void rotate()
    {
        foreach (GameObject plat in platform)
        {
            plat.transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * rotateSpeed);

            int compare = (int)plat.transform.rotation.eulerAngles.y / 90;

            if (((compare % 4) % 2) == 1)   //Down
            {
                plat.transform.position = Vector3.Lerp(plat.transform.position, new Vector3(plat.transform.position.x, (plat.transform.position.y - verticalSpeed), plat.transform.position.z), verticalSpeed);
            }
            else    //Up // Old comment about old code. In old version platforms went up -> down ->up etc. Add - or + to y component to get it back.
            {
                plat.transform.position = Vector3.Lerp(plat.transform.position, new Vector3(plat.transform.position.x, (plat.transform.position.y + verticalSpeed), plat.transform.position.z), verticalSpeed);
            }
        }
    }
       
}
