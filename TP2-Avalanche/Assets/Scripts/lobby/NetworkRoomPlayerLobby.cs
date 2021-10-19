using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkRoomPlayerLobby : NetworkRoomPlayer
{
    [Header("UI")]
    [SerializeField] private GameObject playerRowPrefab;

    [field: SyncVar]
    public string PlayerName { get; set; }

    private GameObject _grid;
    private Dictionary<int, LobbyGridRow> _lobbyRows;

    public override void OnClientEnterRoom()
    {
        AddNewRowToGrid(_grid, index, PlayerName);
    }

    public override void OnStartClient()
    {
        _grid = GameObject.FindGameObjectWithTag("PlayerGrid");
        _lobbyRows = new Dictionary<int, LobbyGridRow>();
    }

    public override void OnStartLocalPlayer()
    {
        PlayerName = PlayerPrefs.GetString(PlayerNameInput.NAME_KEY);
        Button readyButton = GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>();
        readyButton.onClick.AddListener(buttonClick);
        CmdJoinAsNewPlayer(PlayerName);
    }

    public override void OnGUI()
    {
        var room = NetworkManager.singleton as NetworkRoomManager;
        if (!room || !NetworkManager.IsSceneActive(room.RoomScene)) return;
        RenderPlayerInformation();
    }

    [Command]
    private void CmdJoinAsNewPlayer(string playerName)
    {
        PlayerName = playerName;
        RpcAddNewPlayerRowToClientUi();
    }

    [ClientRpc]
    private void RpcAddNewPlayerRowToClientUi()
    {
        AddNewRowToGrid(_grid, index, PlayerName);
    }

    [Command]
    private void CmdUpdatePlayerState(int playerIndex, bool readyState)
    {
        readyToBegin = readyState;
        NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
        if (room != null)
        {
            room.ReadyStatusChanged();
        }

        RpcUpdateClientUi(playerIndex, readyState);
    }

    [ClientRpc]
    private void RpcUpdateClientUi(int playerIndex, bool newReadyState)
    {
        // TODO: Not exactly sure why the RPC needs these lines? SyncVar should sync them all together?
        readyToBegin = newReadyState;

        if (!_lobbyRows.TryGetValue(playerIndex, out var lobbyRow)) return;

        lobbyRow.SetReadyFlag(readyToBegin);
    }

    private void AddNewRowToGrid(GameObject grid, int playerIndex, string playerName)
    {
        // TODO: This is a hack? I didn't have time to figure out the OnClientEnterRoom event and we already had the collection available.
        // Do nothing if we already have the row added for the current player index
        if (_lobbyRows.ContainsKey(playerIndex)) return;

        var playerRow = Instantiate(playerRowPrefab, playerRowPrefab.transform.position, playerRowPrefab.transform.rotation);
        var gridManager = grid.GetComponent<PlayerGridManager>();
        var lobbyGridRow = playerRow.GetComponent<LobbyGridRow>();
        lobbyGridRow.SetIndexAndName(playerIndex, playerName);
        lobbyGridRow.SetReadyFlag(readyToBegin);
        gridManager.AddRow(lobbyGridRow, grid);

        _lobbyRows.Add(playerIndex, lobbyGridRow);
    }

    private void RenderPlayerInformation()
    {
        if (!_lobbyRows.TryGetValue(index, out var currentPlayerRow)) return;

        currentPlayerRow.SetIndexAndName(index, PlayerName);
        currentPlayerRow.SetReadyFlag(readyToBegin);
        var room = NetworkManager.singleton as NetworkRoomManager;
        TextMeshProUGUI count = GameObject.FindGameObjectWithTag("PlayerCount").GetComponentInChildren<TextMeshProUGUI>();
        count.text = room.numPlayers + " / 4 players";
    }

    private void buttonClick()
    {
        if (!NetworkClient.active || !isLocalPlayer) return;

        CmdUpdatePlayerState(index, !readyToBegin);
        TextMeshProUGUI readyText = GameObject.FindGameObjectWithTag("ReadyButton").GetComponentInChildren<TextMeshProUGUI>();
        readyText.text = readyToBegin ? "Cancel" : "Ready up!";
    }
}
