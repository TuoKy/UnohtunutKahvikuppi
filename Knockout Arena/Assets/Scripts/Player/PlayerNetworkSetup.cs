using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera cam;

	// Use this for initialization
	void Start () {
        Debug.Log("Spawning player.");
        Debug.Log(isLocalPlayer);
        if (isLocalPlayer)
        {
            Debug.Log("Activating local player.");
            ActivateChildObjects();
        }
	}

    void ActivateChildObjects()
    {
        transform.FindChild("PlayerCamera").gameObject.SetActive(true);
    }
}
