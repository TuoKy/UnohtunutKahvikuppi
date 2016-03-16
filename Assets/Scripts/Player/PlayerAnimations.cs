using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerAnimations : NetworkBehaviour
{
    public Animator anim;

    //For attacks
    public GameObject attackHitboxLeftHand;
    public GameObject attackHitboxRightHand;
<<<<<<< HEAD
	
=======

>>>>>>> 04416bef47853c214f4ef08104fe8f0a759e06b5
    [Command]
    public void CmdSetTrigger(string trigger)
    {
        RpcSetTrigger(trigger);
    }

    [Command]
    public void CmdSetBool(string trigger, bool value)
    {
        RpcSetBool(trigger, value);
    }

    [ClientRpc]
    void RpcSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    [ClientRpc]
    void RpcSetBool(string trigger, bool value)
    {
        anim.SetBool(trigger, value);
    }


    //Hitbox activations etc
    [Command]
    public void CmdActivateRightHandAttackBox()
    {
            attackHitboxRightHand.SetActive(true);
            attackHitboxRightHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
            RpcActivateRightHandAttackBox();
    }

    [ClientCallback]
    void RpcActivateRightHandAttackBox()
    {
        attackHitboxRightHand.SetActive(true);
        attackHitboxRightHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
    }
    
}
