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
        NetworkManager.singleton.matchMaker.CreateMatch("testi", 4, true, "", NetworkManager.singleton.OnMatchCreate);
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
}
