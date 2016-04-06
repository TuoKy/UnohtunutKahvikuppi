using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetGameManager : NetworkBehaviour{

    private List<GameObject> players;
    private static NetGameManager _instance;

    public static NetGameManager instance
    {
        get
        {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<NetGameManager>();
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
        players = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CheckIfPlayerHasWon()
    {
        foreach (var player in players)
        {
            if (player.GetComponent<Player>().Lives == 0)
            {               
                players.Remove(player);
            }
                
        }
        if (players.Count == 1)
            RpcWeHaveWinner();
    }

    [Command]
    public void CmdAddPlayerToList(GameObject player)
    {
        players.Add(player);
        CheckIfPlayerHasWon();

    }

    [Command]
    public void CmdRemovePlayerFromList(GameObject player)
    {
        players.Remove(player);
        CheckIfPlayerHasWon();
    }

    [ClientRpc]
    public void RpcWeHaveWinner()
    {
        //Not currently working
        //players[0].GetComponent<PlayerScore>().setCountDownText("You won :D");
    }

    [ClientRpc]
    public void RpcClearText()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerScore>().setCountDownText("");
        }        
    }
}
