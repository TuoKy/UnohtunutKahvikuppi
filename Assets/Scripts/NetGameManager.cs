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
        Debug.Log(players);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CheckIfPlayerHasWon()
    {
        foreach (var player in players)
        {
            Debug.Log(player);
            Debug.Log(player.GetComponent<Player>());
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
        players[0].GetComponent<PlayerScore>().setCountDownText("You won :D");
    }
}
