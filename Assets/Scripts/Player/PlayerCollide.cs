using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerCollide : NetworkBehaviour{

    void OnTriggerEnter(Collider info)
    {
        if (isLocalPlayer)
        {
            if (info.gameObject.CompareTag("Death"))
            {
                gameObject.GetComponent<PlayerScore>().KillMe();
                gameObject.GetComponent<PlayerScore>().StartReSpawn();
                GameManager.instance.TakePlayerLifeToken();

            }
            if (info.gameObject.CompareTag("Weapon"))
            {
                GetComponent<PlayerAnimations>().CmdSetTrigger("Knockback");
                Vector3 heading = this.GetComponentInParent<Transform>().position - info.GetComponentInParent<Transform>().position;
                info.gameObject.GetComponent<Attack>().UpdateDirection(info.GetComponentInParent<Transform>().rotation.eulerAngles);
                GetComponent<PlayerController>().GetHitByAttack(info.gameObject.GetComponent<Attack>());
                // Update UI
                GameManager.instance.UpdateKnockoutPercent(GetComponent<PlayerController>().player.KnockoutPercent);
            }
            if (info.gameObject.CompareTag("CameraTrigger"))
            {
                if (isLocalPlayer)
                    GetComponent<PlayerController>().Camera.GetComponent<NewCamController>().setFalltoDeathPosition();
            }
        }
    }

    void OnCollisionStay(Collision info)
    {
        // check if player touches ground
        if (isLocalPlayer && info.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerController>().player.DoubleJumped = false;
            GetComponent<PlayerController>().player.Grounded = true;
        }
    }

    void OnCollisionExit(Collision info)
    {
        if (isLocalPlayer && info.gameObject.CompareTag("Ground"))
            GetComponent<PlayerController>().player.Grounded = false;  
    }

    void OnCollisionEnter(Collision info)
    {
        if (isLocalPlayer && info.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerAnimations>().CmdSetBool("Jumping", false);
        }
    }

}
