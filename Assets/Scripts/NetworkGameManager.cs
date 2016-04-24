using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Network;
using System.Collections;
using System.Collections.Generic;

public class NetworkGameManager : NetworkBehaviour
{
    
    static public List<Player> sPlayers = new List<Player>();
    static public NetworkGameManager sInstance = null;

    private bool waited;
    protected bool running = true;

    void Awake()
    {
        sInstance = this;
        waited = false;
    }

    void Start()
    {

        for (int i = 0; i < sPlayers.Count; ++i)
        {
            sPlayers[i].GetComponent<PlayerNetworkSetup>().InitPlayer();
        }
        //We must first set up players and cameras. After that we can set up Name bars so that they can follow localplayer camera rotation
        StartCoroutine(SetUpHeadBars());
        StartCoroutine(WaitIRememberYou());
    }


    [ServerCallback]
    void Update()
    {
        if (running && waited)
        {

            bool weHaveWinner = false;
            bool noWeDoNOt = false;
            int whoWon = 0;
            for (int i = 0; i < sPlayers.Count; ++i)
            {
                if (sPlayers[i].Lives > 0 && !weHaveWinner)
                {
                    whoWon = i;
                    weHaveWinner = true;
                }
                else if (sPlayers[i].Lives > 0 && weHaveWinner)
                {
                    noWeDoNOt = true;
                }
            }

            if (weHaveWinner && !noWeDoNOt)
            {
                running = false;
                RpcDeclareWinner(whoWon);
                StartCoroutine(ReturnToLoby());
            }
        }
    }

    IEnumerator SetUpHeadBars()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < sPlayers.Count; ++i)
        {
            sPlayers[i].GetComponent<PlayerNetworkSetup>().InitHeadPlate();
        }
    }

    IEnumerator WaitIRememberYou()
    {
        //In the mountains...
        //There's a small time frame when other players are connecting and being added to player list on server
        
        yield return new WaitForSeconds(3.0f);
        waited = true;
    }

    IEnumerator ReturnToLoby()
    {
        running = false;
        yield return new WaitForSeconds(6.0f);
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }

    [ClientRpc]
    private void RpcDeclareWinner(int i)
    {
        PlayerScore scoreManagement = sPlayers[i].GetComponent<PlayerScore>();
        scoreManagement.setCountDownText("You Won :D");
    }

    
}
