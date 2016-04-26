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
    private GameObject percentPanel;
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
            InitPlayer();
        }

    }

    public void InitPlayer()
    {
        if (wasInit)
        {
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
            return;
        }
        //Own plate is not initialized on our own screen
        if (!isLocalPlayer)
        {
            namePlate = Instantiate(namePlatePrefab, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            namePlate.SetActive(true);
            HeadTransform temp = namePlate.GetComponent<HeadTransform>();
            //Every client has camera other clients can't see
            temp.cam = GameObject.Find("Camera");
            temp.playerTransform = transform;

            //Transform.LookAt() fuck ups rotation. so we force child objects to be otherway
            //Most likely has nicer solution. 
            namePlayer = namePlate.GetComponentInChildren<Text>();
            namePlayer.text = playerInfo.playerName;
            namePlayer.transform.Rotate(0, 180, 0);

            //In prefab panel which color we want to change is second. DO NOT ALTER.
            percentPanel = namePlate.transform.GetChild(1).gameObject;
            percentPanel.GetComponent<Image>().color = playerInfo.color;

            //nameplate has no way to get a hold of correct player information otherwise
            temp.playerInfo = playerInfo;
            //Percentage text is updatet in its own update loop 
            temp.percentText = percentPanel.GetComponentInChildren<Text>();
            temp.percentText.transform.Rotate(0, 180, 0);
        }

        wasPlateInit = true;
    }

    private void InitCamera()
    {
        //Destroy other player camera
        Destroy(GameObject.Find("Camera"));
        playerCam = Instantiate(camPrefab, camPosition.position, camPosition.rotation) as GameObject;
        playerCam.name = "Camera";
        playerCam.SetActive(true);
        playerCam.GetComponent<CamController>().Target = camPosition;
        playerCam.GetComponent<CamController>().Player = gameObject.transform;
        GetComponent<PlayerController>().Camera = playerCam;
    }    

    void OnDestroy()
    {
        Destroy(namePlate);
        Destroy(namePlayer);
        Destroy(percentPanel);
        Destroy(playerCam);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        NetworkGameManager.sPlayers.Remove(GetComponent<Player>());
    }
}
