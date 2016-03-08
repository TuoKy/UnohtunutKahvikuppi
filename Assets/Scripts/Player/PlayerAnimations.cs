using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAnimations : NetworkBehaviour
{
    public Animator anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdSetTrigger("RightPunch");
            }
        }
    }

    [Command]
    public void CmdSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
        RpcSetTrigger(trigger);
    }

    [ClientRpc]
    private void RpcSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
