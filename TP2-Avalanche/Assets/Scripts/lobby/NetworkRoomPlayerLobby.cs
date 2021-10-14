using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkRoomPlayerLobby : NetworkRoomPlayer
{
    [Header("UI")]
    [SerializeField] private GameObject playerRowPrefab;

    [SyncVar]
    private string _playerName;

    private string[] _names = new string[]
    {
        "Jay",
        "Jordy",
        "Bob",
        "Sam"
    };

    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }

    private bool IsRoomOwner => ((isServer && index > 0) || isServerOnly);

    private GameObject _playerRow;
    private LobbyGridRow row;

    public override void OnClientEnterRoom()
    {
        var grid = GameObject.FindGameObjectWithTag("PlayerGrid");
        var gridManager = grid.GetComponent<PlayerGridManager>();
        var manager = NetworkManager.singleton as NetworkRoomManager;

        foreach (var player in manager.pendingPlayers)
        {
            var pendingPlayerRow = _playerRow.GetComponent<LobbyGridRow>();
            var roomPlayer = player.roomPlayer.GetComponent<NetworkRoomPlayerLobby>();
            pendingPlayerRow.SetName(roomPlayer.PlayerName, index);

            gridManager.AddRow(row, grid);
        }

        row = _playerRow.GetComponent<LobbyGridRow>();
        row.SetName("Loading...", index);

        gridManager.AddRow(row, grid);
    }

    public override void OnStartClient()
    {
        _playerRow = Instantiate(playerRowPrefab, playerRowPrefab.transform.position, playerRowPrefab.transform.rotation);
    }
    
    public override void OnStartLocalPlayer()
    {
        _playerName = PlayerPrefs.GetString(PlayerNameInput.NAME_KEY);
        CmdSetPlayerName(_playerName);
    }

    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        _playerName = playerName;
        RpcAddPlayerRow();
    }

    [ClientRpc]
    private void RpcAddPlayerRow()
    {
        var grid = GameObject.FindGameObjectWithTag("PlayerGrid");
        var gridManager = grid.GetComponent<PlayerGridManager>();
        var row = _playerRow.GetComponent<LobbyGridRow>();
        row.SetName(_playerName, index);

        gridManager.AddRow(row, grid);
    }

    [Command]
    private void CmdChangeReady(bool readyState, string name)
    {
        _playerName = name;
        CmdChangeReadyState(readyState);
    }

    [ClientRpc]
    private void RpcUpdateUi()
    {
        var manager = NetworkManager.singleton as NetworkRoomManager;

        var grid = GameObject.FindGameObjectWithTag("PlayerGrid");
        var rows = GameObject.FindGameObjectsWithTag("GridRow");
        foreach (var player in manager.pendingPlayers)
        {
            foreach (var row in rows)
            {
                var lobbyRow = row.GetComponent<LobbyGridRow>();
                var roomPlayer = player.roomPlayer.GetComponent<NetworkRoomPlayerLobby>();
                if (lobbyRow.index == roomPlayer.index)
                { 
                    lobbyRow.SetName($"{roomPlayer.PlayerName}", index);
                }
            }
        }
    }

    public override void OnGUI()
    {
        // TODO: All your GUI code should be here!
        var room = NetworkManager.singleton as NetworkRoomManager;
        if (!room || !NetworkManager.IsSceneActive(room.RoomScene)) return;
        DrawPlayerReadyState();
        DrawPlayerReadyButton();
    }

    private void DrawPlayerReadyState()
    {
        row.SetName($"{PlayerName}", index);

        if (IsRoomOwner && GUILayout.Button("KICK"))
        {
            GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        }
    }

    private void DrawPlayerReadyButton()
    {
        if (!NetworkClient.active || !isLocalPlayer) return;

        if (readyToBegin)
        {
            if (GUILayout.Button("Cancel"))
            {
                var name = _names[Random.Range(0, _names.Length - 1)];
                CmdChangeReady(false, name);
            }
        }
        else
        {
            if (GUILayout.Button("Ready"))
            {
                var name = _names[Random.Range(0, _names.Length - 1)];
                CmdChangeReady(true, name);
            }
        }
    }
}
