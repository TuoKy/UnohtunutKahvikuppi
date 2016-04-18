using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
/*
Class used to setup player for network and also to remove from there.
Sets also camera in place

*/


public class PlayerNetworkSetup : NetworkBehaviour {
       
    public GameObject camPrefab;

    private GameObject playerCam;

    [SerializeField]
    Transform camPosition;

    void Awake()
    {
        NetworkGameManager.sPlayers.Add(GetComponent<Player>());
    }

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            InitCamera();
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerScore>().enabled = true;          
        }
	}

    private void InitCamera()
    {
        playerCam = Instantiate(camPrefab, camPosition.position, camPosition.rotation) as GameObject;
        playerCam.SetActive(true);
        playerCam.GetComponent<CamController>().Target = camPosition;
        playerCam.GetComponent<CamController>().Player = gameObject.transform;
        GetComponent<PlayerController>().Camera = playerCam;
    }    

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCam.SetActive(false);
        NetworkGameManager.sPlayers.Remove(GetComponent<Player>());
    }
}
