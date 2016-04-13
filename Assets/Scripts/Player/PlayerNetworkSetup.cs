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
        }
	}

    private void InitCamera()
    {
        GameObject playerCam = Instantiate(camPrefab, camPosition.position, camPosition.rotation) as GameObject;
        playerCam.SetActive(true);
        playerCam.GetComponent<CamController>().Target = camPosition;
        playerCam.GetComponent<CamController>().Player = gameObject.transform;
        GetComponent<PlayerController>().Camera = playerCam;
    }    
}
