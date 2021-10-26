using Mirror;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerLobby : NetworkRoomManager
{
    [Header("Room")]
    //[SerializeField] private GameManage gameManager;

    private bool _showStartButton;
    private GameObject startButton = null; 

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        if (sceneName == onlineScene)
        {
            AddStartGameListener();
        }
        if (sceneName == GameplayScene)
        {
            //gameManager.Initialize(numPlayers);
            //gameManager.StartGame();
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

        if (startButton != null)
            startButton.gameObject.transform.localScale = !allPlayersReady ? new Vector3(0, 0, 0) : new Vector3(1, 1, 1);

        if (allPlayersReady && _showStartButton)
        {
            _showStartButton = false;
        }
    }

    private void StartGame()
    {
        _showStartButton = false;
        ServerChangeScene(GameplayScene);
    }

    private void AddStartGameListener()
    {
        startButton = GameObject.FindGameObjectWithTag("StartGameButton");
        startButton.GetComponent<Button>().onClick.AddListener(StartGame);
    }

}
