using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerCollide : NetworkBehaviour{

    private PlayerController controls;
    private PlayerAnimations anim;

    void Start()
    {
        controls = GetComponent<PlayerController>();
        anim = GetComponent<PlayerAnimations>();
    }

    void OnTriggerEnter(Collider info)
    {
        if (info.gameObject.CompareTag("Death") && isLocalPlayer)
        {
            if(controls.player.Lives > 0)
            {
                StartCoroutine(gameObject.GetComponent<PlayerScore>().StartReSpawn());
                GameManager.instance.TakePlayerLifeToken();
                GameManager.instance.UpdateKnockoutPercent(controls.player.KnockoutPercent);
            }
            else{
                gameObject.GetComponent<PlayerScore>().DeclareLoss();
            }
        }
        if (info.gameObject.CompareTag("Weapon"))
        {
            anim.CmdSetTrigger("Knockback");
            //heading is never used?
            Vector3 heading = this.GetComponentInParent<Transform>().position - info.GetComponentInParent<Transform>().position;
            info.gameObject.GetComponent<Attack>().UpdateDirection(info.GetComponentInParent<Transform>().rotation.eulerAngles);
            controls.GetHitByAttack(info.gameObject.GetComponent<Attack>());
            // Update UI
            GameManager.instance.UpdateKnockoutPercent(controls.player.KnockoutPercent);
        }
        if (info.gameObject.CompareTag("CameraTrigger"))
        {
            if (isLocalPlayer)
                controls.Camera.GetComponent<CamController>().setFalltoDeathPosition();
        }
    }

    void OnCollisionStay(Collision info)
    {
        // check if player touches ground
        if (isLocalPlayer && info.gameObject.CompareTag("Ground"))
        {
            controls.player.DoubleJumped = false;
            controls.player.Grounded = true;
        }
    }

    void OnCollisionExit(Collision info)
    {
        if (isLocalPlayer && info.gameObject.CompareTag("Ground"))
            controls.player.Grounded = false;
    }

    void OnCollisionEnter(Collision info)
    {
        if (isLocalPlayer && info.gameObject.CompareTag("Ground"))
        {
            anim.CmdSetBool("Jumping", false);
        }
    }

}
