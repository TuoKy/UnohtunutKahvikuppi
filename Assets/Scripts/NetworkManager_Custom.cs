using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class NetworkManager_Custom : NetworkManager {

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

    /*public override void OnMatchCreate(CreateMatchResponse matchInfo)
    {
        if (matchInfo.success)
        {
            StartHost(new MatchInfo(matchInfo));
        }
    }*/

    public void MatchList()
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", NetworkManager.singleton.OnMatchList);
    }

    public override void OnMatchList(ListMatchResponse matchList)
    {
        List<MatchDesc> m_roomList = new List<MatchDesc>();
        m_roomList.Clear();
        foreach (MatchDesc match in matchList.matches)
        {
            m_roomList.Add(match);
        }
        //TestiFunktio!!!! REMOVE LATER!
        if(m_roomList != null)
        {
            MatchJoinTest(m_roomList[0]);
        }
    }

    public void MatchJoinTest(MatchDesc match)
    {
        NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "", NetworkManager.singleton.OnMatchJoined);
    }
}
