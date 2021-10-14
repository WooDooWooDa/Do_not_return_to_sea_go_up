using Mirror;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkRoomManager
{
    [Header("Room")]
    [SerializeField] private LobbyManager lobbyManager;

    private bool _showStartButton;

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        if (sceneName == GameplayScene)
        {
            lobbyManager.StartGame();
        }
    }

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        // TODO:  Pass data from RoomPlayer to GamePlayer object - either by public properties or using a Component
        //PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
        //playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index; 
        return true;
    }

    public override void OnRoomServerPlayersReady()
    {
    #if UNITY_SERVER
            base.OnRoomServerPlayersReady();
    #else
            _showStartButton = true;
    #endif
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && _showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            _showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }

}
