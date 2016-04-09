using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncPos : NetworkBehaviour
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

    void Start()
    {
    }

    void FixedUpdate()
    {
        TransmitPos();
        TransmitRot();
        LerpPosition();
    }

    void LerpPosition()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, synchPlayerRotation, Time.deltaTime * lerpRate);
    }

    [Command]
    public void CmdProvidePosToServer(Vector3 pos)
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
        if (Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            CmdProvidePosToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    [ClientCallback]
    void TransmitRot()
    {
        if (Quaternion.Angle(myTransform.rotation, lastRot) > threshold)
        {
            CmdProvideRotToServer(myTransform.rotation);
            lastRot = myTransform.rotation;
        }

    }
}
