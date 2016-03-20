﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerCollide : NetworkBehaviour{

    void OnTriggerEnter(Collider info)
    {
        if (info.gameObject.CompareTag("Death"))
        {
            gameObject.GetComponent<PlayerScore>().StartReSpawn();
        }
        if (isLocalPlayer && info.gameObject.CompareTag("Weapon"))
        {
            GetComponent<PlayerAnimations>().CmdSetTrigger("Knockback");
            Vector3 heading = info.GetComponentInParent<Transform>().position - this.GetComponentInParent<Transform>().position;
            info.gameObject.GetComponent<Attack>().UpdateDirection(heading);
            GetComponent<PlayerController>().GetHitByAttack(info.gameObject.GetComponent<Attack>());
        }
        if (info.gameObject.CompareTag("CameraTrigger"))
        {
            transform.FindChild("PlayerCamera").GetComponent<CameraController>().setFalltoDeathPosition();
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
        if (info.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerAnimations>().CmdSetBool("Jumping", false);
        }
    }

}
