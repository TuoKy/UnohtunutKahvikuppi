using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera cam;

	// Use this for initialization
	void Start () {
        Debug.Log("Häräsin");
        Debug.Log(isLocalPlayer);
        if (isLocalPlayer)
        {
            Debug.Log("asd");
            transform.FindChild("PlayerCamera").gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
