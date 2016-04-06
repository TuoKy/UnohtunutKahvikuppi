using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class NetworkManager_Custom : NetworkManager {

    public Transform buttonGrid;
    public GameObject matchButtonPrefab;
    public List<GameObject> matchButtonList;

    public void LanHostStart()
    {
        NetworkManager.singleton.StartHost();
    }

    public void LanGameJoin()
    {
        string ipAddress = GameObject.Find("IPAddressField").transform.FindChild("Text").GetComponent<Text>().text;
        if (ipAddress != "")
        {
            NetworkManager.singleton.networkAddress = ipAddress;
        }
        else
        {
            NetworkManager.singleton.networkAddress = "localhost";
        }
        NetworkManager.singleton.StartClient();
    }

    public void MatchMakerStart()
    {
        Debug.Log("Match");
        NetworkManager.singleton.StartMatchMaker();
    }

    public void MatchMakerStop()
    {
        NetworkManager.singleton.StopMatchMaker();
    }

    public void MatchCreate()
    {
        string matchName = GameObject.Find("MatchNameField").transform.FindChild("Text").GetComponent<Text>().text;

        //Check the match has a name
        if(matchName != "")
        {
            NetworkManager.singleton.matchMaker.CreateMatch(matchName, 4, true, "", NetworkManager.singleton.OnMatchCreate);
        }
    }

    public void MatchList()
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", NetworkManager.singleton.OnMatchList);
    }

    public override void OnMatchList(ListMatchResponse matchList)
    {
        if(matchButtonList != null)
        {
            DestroyMatchButtons();
        }
        matchButtonList = new List<GameObject>();
        foreach (MatchDesc match in matchList.matches)
        {
            GameObject temp = Instantiate(matchButtonPrefab) as GameObject;
            Button tempButton = temp.GetComponent<Button>();
            temp.transform.SetParent(buttonGrid, false);
            tempButton.GetComponentInChildren<Text>().text = match.name;
            tempButton.onClick.AddListener(() => { MatchJoinTest(match); });
            matchButtonList.Add(temp);
        }
    }

    void DestroyMatchButtons()
    {
        foreach(GameObject objectToDestroy in matchButtonList)
        {
            Destroy(objectToDestroy);
        }
        matchButtonList.Clear();
    }

    public void MatchJoinTest(MatchDesc match)
    {
        NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", NetworkManager.singleton.OnMatchJoined);
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            StartCoroutine(SetupMenuSceneButtons());
            // ToDo: Korjaa tämä niin että hostia ei yritetä pysäyttää jos sitä ei ole!!!
            NetworkManager.singleton.StopHost();
        }
        else
        {
            SetupOtherSceneButtons();
        }
    }

    IEnumerator SetupMenuSceneButtons()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("TempLanHostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("TempLanHostButton").GetComponent<Button>().onClick.AddListener(LanHostStart);

        GameObject.Find("LanHostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("LanHostButton").GetComponent<Button>().onClick.AddListener(LanHostStart);
        //Debug.Log(GameObject.Find("LanHostButton").GetComponent<Button>());
        GameObject.Find("LanClientButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("LanClientButton").GetComponent<Button>().onClick.AddListener(LanGameJoin);

        GameObject.Find("InternetButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InternetButton").GetComponent<Button>().onClick.AddListener(MatchMakerStart);

        //GameObject.Find("MMBackButton").GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("MMBackButton").GetComponent<Button>().onClick.AddListener(MatchMakerStop);
        //GameObject.Find("MMBackButton").GetComponent<Button>().onClick.AddListener(GameManager.);

        GameObject.Find("InternetHostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InternetHostButton").GetComponent<Button>().onClick.AddListener(MatchCreate);

        //Ei ehkä toimi testaa!
        GameObject.Find("FindMatchButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("FindMatchButton").GetComponent<Button>().onClick.AddListener(MatchList);
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
    
    override
    public void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        //var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //GameObject.Find("NetGameManager").GetComponent<NetGameManager>().CmdAddPlayerToList(player);
        Debug.Log(conn);
    }
    /*
    override
    public  void OnServerRemovePlayer(NetworkConnection conn, short playerControllerId)
    {
        PlayerController player;
        if (conn.GetPlayer(playerControllerId, out player))
        {
            if (player.NetworkIdentity != null && player.NetworkIdentity.gameObject != null)
                NetworkServer.Destroy(player.NetworkIdentity.gameObject);
        }
    }
    */
}
