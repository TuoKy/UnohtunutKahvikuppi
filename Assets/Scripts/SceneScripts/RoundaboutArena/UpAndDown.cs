using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class UpAndDown : NetworkBehaviour
{
    private float verticalSpeed = 0.2f;
    public List<GameObject> platform;

    private float maxY = 5.5f;
    public bool movingUp = false;

    [ServerCallback]
    void Start()
    {
        RpcFuckTheShit();
    }


    [ClientRpc]
	void RpcFuckTheShit () {
        StartCoroutine(moveUp());
	}

    private void MoveUpAndDown()
    {
          
    }

    IEnumerator moveUp()
    {
        foreach (GameObject plat in platform)
        {
            plat.transform.position = Vector3.Lerp(plat.transform.position, plat.transform.position + new Vector3(0, 5, 0), 15f);
        }
        yield return new WaitForSeconds(3f);
        
    }

    IEnumerator moveDown()
    {
        foreach (GameObject plat in platform)
        {
            plat.transform.position = Vector3.Lerp(plat.transform.position, plat.transform.position - new Vector3(0, 10, 0), 15f);
        }
        yield return new WaitForSeconds(3f);
    }

}
