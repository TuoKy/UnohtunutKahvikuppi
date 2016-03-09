using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAnimations : NetworkBehaviour
{
    public Animator anim;

    //For attacks
    public GameObject attackHitboxLeftHand;
    public GameObject attackHitboxRightHand;

	
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
        //anim.SetTrigger(trigger);
        RpcSetTrigger(trigger);
    }

    [ClientRpc]
    private void RpcSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void ActivateRightHandAttackBox()
    {
        if (isLocalPlayer)
        {
            attackHitboxRightHand.SetActive(true);
            attackHitboxRightHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
        }
    }

    public void DisableRightHandAttackBox()
    {

    }
}
