using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {

    void OnTriggerEnter(Collider info)
    {
        if (info.gameObject.CompareTag("Death"))
        {
            gameObject.GetComponent<PlayerScore>().ReSpawn();
        }
        if (info.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Osui");
        }
    }

    void OnCollisionStay(Collision info)
    {
        // check if player touches ground
        if (info.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerController>().player.Grounded = true;
        }
    }

    void OnCollisionExit(Collision info)
    {
        if (info.gameObject.CompareTag("Ground"))
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
