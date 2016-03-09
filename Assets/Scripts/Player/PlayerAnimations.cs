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
        }
    }

    [Command]
    public void CmdSetTrigger(string trigger)
    {
        //anim.SetTrigger(trigger);
        RpcSetTrigger(trigger);
    }

    [ClientRpc]
    void RpcSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    [Command]
    public void CmdActivateRightHandAttackBox()
    {
        if (isLocalPlayer)
        {
            Debug.Log("local");
            debugText.text = "local";
            attackHitboxRightHand.SetActive(true);
            attackHitboxRightHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
            RpcActivateRightHandAttackBox();
        }
    }

    [ClientCallback]
    void RpcActivateRightHandAttackBox()
    {
        Debug.Log("server");
        debugText.text = "server";
        attackHitboxRightHand.SetActive(true);
        attackHitboxRightHand.GetComponent<DeactivateMe>().setCooldown(0.5f);
    }
    
}
