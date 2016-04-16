using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        Player sPlayers = gamePlayer.GetComponent<Player>();

        sPlayers.Lives = 3;
    }
}
