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
    }

    void OnCollide(Collider info)
    {
        if (info.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Osui");
        }
    }
}
