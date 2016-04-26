using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerCollide : NetworkBehaviour{

    private PlayerController controls;
    private PlayerAnimations anim;
    private PlayerScore score;
    protected BoxCollider _collider;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        //We don't want to handle collision on client, so disable collider there
        _collider.enabled = isServer;
        controls = GetComponent<PlayerController>();
        anim = GetComponent<PlayerAnimations>();
        score = GetComponent<PlayerScore>();
    }

    [ServerCallback]
    void OnTriggerEnter(Collider info)
    {
        if (info.gameObject.CompareTag("Death"))
        {
            if(controls.player.Lives > 0)
            {
                StartCoroutine(score.StartReSpawn());
                GameManager.instance.TakePlayerLifeToken();
                GameManager.instance.UpdateKnockoutPercent(controls.player.KnockoutPercent);
            }
            else{
                score.DeclareLoss();
            }
        }
        if (info.gameObject.CompareTag("Weapon"))
        {
            anim.CmdSetTrigger("Knockback");
            info.gameObject.GetComponent<Attack>().UpdateDirection(info.GetComponentInParent<Transform>().rotation.eulerAngles);
            controls.GetHitByAttack(info.gameObject.GetComponent<Attack>());
            // Update UI
            GameManager.instance.UpdateKnockoutPercent(controls.player.KnockoutPercent);
        }
        if (info.gameObject.CompareTag("CameraTrigger"))
        {
            controls.Camera.GetComponent<CamController>().setFalltoDeathPosition();
        }
    }

    [ServerCallback]
    void OnCollisionStay(Collision info)
    {
        // check if player touches ground
        if (info.gameObject.CompareTag("Ground"))
        {
            CmdTouchground();
        }
    }

    [ClientCallback]
    private void CmdTouchground()
    {
        controls.player.DoubleJumped = false;
        controls.player.Grounded = true;
    }

    [ServerCallback]
    void OnCollisionExit(Collision info)
    {
        if (info.gameObject.CompareTag("Ground"))
            controls.player.Grounded = false;
    }

    [ServerCallback]
    void OnCollisionEnter(Collision info)
    { 

        if (info.gameObject.CompareTag("Ground"))
        {
            anim.CmdSetBool("Jumping", false);
        }
    }
}
