using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSynchPos : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        transmitPos();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    //Client calls server "Cmd" is must in name
    [Command]
    void CmdProvidePosToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void transmitPos()
    {
        if (isLocalPlayer)
        {
            CmdProvidePosToServer(myTransform.position);
        }
    }

}
