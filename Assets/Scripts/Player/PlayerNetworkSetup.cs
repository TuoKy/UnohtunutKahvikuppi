using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera cam;

    public NetworkAnimator netAnim;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            ActivateChildObjects();
            GetComponent<PlayerController>().enabled = true;

            GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
        }
	}


    //TODO: make variable in which tell how many parameters there are
    public override void OnStartLocalPlayer()
    {
        for (int i = 0; i < 5; i++)
        {
            netAnim.SetParameterAutoSend(i, true);
        }
    }

    public override void PreStartClient()
    {
        for (int i = 0; i < 5; i++)
        {
            netAnim.SetParameterAutoSend(i, true);
        }
    }

    void ActivateChildObjects()
    {
        transform.FindChild("PlayerCamera").gameObject.SetActive(true);
    }
}
