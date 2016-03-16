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
