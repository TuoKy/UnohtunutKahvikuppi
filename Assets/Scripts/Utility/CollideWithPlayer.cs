using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CollideWithPlayer : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ServerCallback]
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player.Lives > 0)
            player.LoseLive();
            player.ResetKnockoutPercent();
        }
        /* Doesn't obviosly work duh
        else
        {
            collision.gameObject.GetComponent<PlayerScore>().DeclareLoss();
        }
        */
    }

}
