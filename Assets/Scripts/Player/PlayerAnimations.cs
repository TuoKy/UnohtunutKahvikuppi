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
    public Text debugText;
	
    void Start()
    {
        debugText = GameObject.Find("debugText").GetComponent<Text>();
    }

	void Update () {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdSetTrigger("RightPunch");        
            }

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                CmdSetBool("Jumping", true);
            }

            //TODO: Fix block animation premature looping
            if (Input.GetMouseButton(1))
            {
                CmdSetBool("Moving", false);
                CmdSetBool("Blocking", true);

            }

            if (Input.GetMouseButtonUp(1))
            {
                CmdSetBool("Blocking", false);
            }

            // Hadouken animation
            if (Input.GetKeyDown(KeyCode.H))
            {
                CmdSetTrigger("Hadouken");
            }



        }
    }

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
