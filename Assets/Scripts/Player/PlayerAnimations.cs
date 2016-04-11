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
    public GameObject attackHitboxRightLong;
    public GameObject attackHitboxKick;

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
    //Right Punch
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
    
    //Right long punch
    [Command]
    public void CmdActivateRightLongAttackBox()
    {
        attackHitboxRightLong.SetActive(true);
        attackHitboxRightLong.GetComponent<DeactivateMe>().setCooldown(0.5f);
        RpcActivateRightLongAttackBox();
    }

    [ClientCallback]
    void RpcActivateRightLongAttackBox()
    {
        attackHitboxRightLong.SetActive(true);
        attackHitboxRightLong.GetComponent<DeactivateMe>().setCooldown(0.5f);
    }

    //Left punch
    [Command]
    public void CmdActivateLeftHandAttackBox()
    {
        attackHitboxLeftHand.SetActive(true);
        attackHitboxLeftHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
        RpcActivateLeftHandAttackBox();
    }

    [ClientCallback]
    void RpcActivateLeftHandAttackBox()
    {
        attackHitboxLeftHand.SetActive(true);
        attackHitboxLeftHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
    }

    //Kick
    [Command]
    public void CmdActivateKickAttackBox()
    {
        attackHitboxKick.SetActive(true);
        attackHitboxKick.GetComponent<DeactivateMe>().setCooldown(0.5f);
        RpcActivateKickAttackBox();
    }

    [ClientCallback]
    void RpcActivateKickAttackBox()
    {
        attackHitboxKick.SetActive(true);
        attackHitboxKick.GetComponent<DeactivateMe>().setCooldown(0.5f);
    }

    public bool IsAnimationPlaying(string animationName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) || anim.GetCurrentAnimatorStateInfo(1).IsName(animationName))
        {
            return true;
        }
        return false;
    }
}
