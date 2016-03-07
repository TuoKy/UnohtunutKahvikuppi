using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera cam;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            ActivateChildObjects();
            GetComponent<PlayerController>().enabled = true;

            GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
        }
	}

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
    }

    void ActivateChildObjects()
    {
        transform.FindChild("PlayerCamera").gameObject.SetActive(true);
    }
}
