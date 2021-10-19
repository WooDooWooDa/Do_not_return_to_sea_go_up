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
            GameObject.Find("GameManager").GetComponent<GameManage>().Initialize();
        }
    }

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        gamePlayer.GetComponent<Player>().Name = roomPlayer.GetComponent<NetworkRoomPlayerLobby>().PlayerName;
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
        var button = GameObject.FindGameObjectWithTag("StartGameButton");
        if (button != null)
            button.gameObject.transform.localScale = !allPlayersReady ? new Vector3(0,0,0) : new Vector3(1,1,1);

        if (allPlayersReady && _showStartButton && GUI.Button(new Rect(1150, 600, 200, 50), "START GAME"))
        {
            _showStartButton = false;
            ServerChangeScene(GameplayScene);
        }
    }

}
