using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSynchPos : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;
    [SyncVar]
    private Quaternion synchPlayerRotation;
    [SyncVar]
    private Quaternion synchCamRotation;

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

    private Vector3 lastPos;
    private float threshold = 0.5f;
    private Quaternion lastRot;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        TransmitPos();
        TransmitRot();
        LerpPosition();
        
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            transform.rotation = Quaternion.Lerp(transform.rotation, synchPlayerRotation, Time.deltaTime * lerpRate);
        }
    }

    //Client calls server "Cmd" is must in name
    [Command]
    void CmdProvidePosToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [Command]
    void CmdProvideRotToServer(Quaternion rot)
    {
        synchPlayerRotation = rot;
    }

    [ClientCallback]
    void TransmitPos()
    {
        if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            CmdProvidePosToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    [ClientCallback]
    void TransmitRot()
    {
        if (isLocalPlayer)
        {
            if(Quaternion.Angle(myTransform.rotation, lastRot) > threshold)
            {
                CmdProvideRotToServer(myTransform.rotation);
                lastRot = myTransform.rotation;
            }
        }
    }

}
