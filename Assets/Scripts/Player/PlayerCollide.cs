using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
            GetComponent<PlayerController>().Grounded = true;
        }
    }

    void OnCollisionExit(Collision info)
    {
        if (info.gameObject.CompareTag("Ground"))
            GetComponent<PlayerController>().Grounded = false;  
    }

    void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.CompareTag("Ground"))
        {
            GetComponent<PlayerAnimations>().CmdSetBool("Jumping", false);
        }
    }

}
