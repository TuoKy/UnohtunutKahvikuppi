using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);
        
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        
        Player player  = gamePlayer.GetComponent<Player>();
        
        /*
        spaceship.name = lobby.name;
        spaceship.color = lobby.playerColor;
        spaceship.score = 0;
        */
        player.Lives = 3;
        
    }
}
