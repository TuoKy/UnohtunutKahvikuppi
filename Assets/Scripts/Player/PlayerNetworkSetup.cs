using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {
       
    public GameObject camPrefab;

    [SerializeField]
    Transform camPosition;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            InitCamera();
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerScore>().enabled = true;
            NetGameManager.instance.CmdAddPlayerToList(gameObject);
        }
	}

    private void InitCamera()
    {
        GameObject playerCam = Instantiate(camPrefab, camPosition.position, camPosition.rotation) as GameObject;
        playerCam.SetActive(true);
        playerCam.GetComponent<NewCamController>().Target = camPosition;
        playerCam.GetComponent<NewCamController>().Player = gameObject.transform;
        GetComponent<PlayerController>().Camera = playerCam;
    }
}
