using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class NetworkManager_Custom : NetworkManager {

    public GameObject matchButtonPrefab;
    public Transform buttonGrid;
    /*private static NetworkManager_Custom _instance;

    public static NetworkManager_Custom instance
    {
        get
        {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<NetworkManager_Custom>();
            return _instance;
        }
    }*/

    public void LanHostStart()
    {
        NetworkManager.singleton.StartHost();
    }

    public void LanGameJoin()
    {
        //NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartClient();
    }

    public void MatchMakerStart()
    {
        NetworkManager.singleton.StartMatchMaker();
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
        foreach (MatchDesc match in matchList.matches)
        {
            GameObject temp = Instantiate(matchButtonPrefab) as GameObject;
            Button tempButton = temp.GetComponent<Button>();
            temp.transform.SetParent(buttonGrid, false);
            tempButton.GetComponentInChildren<Text>().text = match.name;
            tempButton.onClick.AddListener(() => { MatchJoinTest(match); });
        }
    }

    public void MatchJoinTest(MatchDesc match)
    {
        NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", NetworkManager.singleton.OnMatchJoined);
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            SetupMenuSceneButtons();
        }
        else
        {
            SetupOtherSceneButtons();
        }
    }

    IEnumerable SetupMenuSceneButtons()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("LanHostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("LanHostButton").GetComponent<Button>().onClick.AddListener(LanHostStart);

        GameObject.Find("LanClientButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("LanClientButton").GetComponent<Button>().onClick.AddListener(LanGameJoin);

        GameObject.Find("InternetButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InternetButton").GetComponent<Button>().onClick.AddListener(MatchMakerStart);

        GameObject.Find("InternetHostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InternetHostButton").GetComponent<Button>().onClick.AddListener(MatchCreate);

        //Ei ehkä toimi testaa!
        GameObject.Find("FindMatchButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("FindMatchButton").GetComponent<Button>().onClick.AddListener(MatchList);
    }

    void SetupOtherSceneButtons()
    {

    }
}
