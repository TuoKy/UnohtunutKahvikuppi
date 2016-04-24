using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
/*
Class used to setup player for network and also to remove from there.
Sets also camera in place

*/


public class PlayerNetworkSetup : NetworkBehaviour {
       
    public GameObject camPrefab;
    public GameObject namePlatePrefab;

    private GameObject namePlate;
    private Text namePlayer; 
    private GameObject playerCam;
    private Player playerInfo;

    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between player & manager is created first).
    protected bool wasInit = false;
    protected bool wasPlateInit = false;

    [SerializeField]
    Transform camPosition;

    void Awake()
    {
        NetworkGameManager.sPlayers.Add(GetComponent<Player>());
        
    }

    // Use this for initialization
    void Start () {
        playerInfo = GetComponent<Player>();
        
        if (NetworkGameManager.sInstance != null)
        {
            Debug.Log("Käydäänkö täällä? " + playerInfo.playerName);
            InitPlayer();
        }

    }

    public void InitPlayer()
    {
        if (wasInit)
        {
            Debug.Log("Oli tehty ukkeli " + playerInfo.playerName);
            return;
        }
            

        if (isLocalPlayer)
        {
            InitCamera();
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerScore>().enabled = true;
        }

        wasInit = true;
    }

    public void InitHeadPlate()
    {
        if (wasPlateInit)
        {
            Debug.Log("Oli tehty headeri " + playerInfo.playerName);
            return;
        }

        if (!isLocalPlayer)
        {
            namePlate = Instantiate(namePlatePrefab, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            namePlate.SetActive(true);
            namePlayer = namePlate.GetComponentInChildren<Text>();
            namePlayer.transform.Rotate(0, 180, 0);
            HeadTransform temp = namePlate.GetComponent<HeadTransform>();
            Debug.Log(GameObject.Find("Camera"));
            temp.cam = GameObject.Find("Camera");
            temp.playerTransform = transform;
            namePlayer.text = playerInfo.playerName;
            namePlayer.color = playerInfo.color;
        }

        wasPlateInit = true;
    }

    private void InitCamera()
    {
        playerCam = Instantiate(camPrefab, camPosition.position, camPosition.rotation) as GameObject;
        playerCam.name = "Camera";
        playerCam.SetActive(true);
        playerCam.GetComponent<CamController>().Target = camPosition;
        playerCam.GetComponent<CamController>().Player = gameObject.transform;
        GetComponent<PlayerController>().Camera = playerCam;
    }    

    void OnDestroy()
    {
        namePlate.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        NetworkGameManager.sPlayers.Remove(GetComponent<Player>());
    }
}
