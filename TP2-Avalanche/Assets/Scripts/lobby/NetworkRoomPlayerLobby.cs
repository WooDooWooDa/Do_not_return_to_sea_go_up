using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NetworkRoomPlayerLobby : NetworkRoomPlayer
{
    [Header("UI")]
    [SerializeField] private GameObject playerRowPrefab;

    [field: SyncVar]
    private string PlayerName { get; set; }

    private GameObject _grid;
    private Dictionary<int, LobbyGridRow> _lobbyRows;

    // TODO: Remove this. Used for testing state sync between clients.
    private static readonly string[] Names = {
        "The Legend Jay",
        "Jordy",
        "Little Bobby",
        "Jacked Sam",
        "Lil Josh",
        "Big Tom",
        "Pikapika"
    };

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
        CmdJoinAsNewPlayer(PlayerName);
    }

    public override void OnGUI()
    {
        var room = NetworkManager.singleton as NetworkRoomManager;
        if (!room || !NetworkManager.IsSceneActive(room.RoomScene)) return;
        RenderPlayerInformation();
        RenderLocalPlayerInformation();
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
    private void CmdUpdatePlayerState(int playerIndex, bool readyState, string playerName)
    {
        PlayerName = playerName;

        // TODO: See RpcUpdateClientUi TODO. Is the CmdChangeReadyState required or should we just do readyToBegin = readyState?
        readyToBegin = readyState;
        NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
        if (room != null)
        {
            room.ReadyStatusChanged();
        }

        RpcUpdateClientUi(playerIndex, playerName, readyState);
    }

    [ClientRpc]
    private void RpcUpdateClientUi(int playerIndex, string newName, bool newReadyState)
    {
        // TODO: Not exactly sure why the RPC needs these lines? SyncVar should sync them all together?
        PlayerName = newName;
        readyToBegin = newReadyState;

        if (!_lobbyRows.TryGetValue(playerIndex, out var lobbyRow)) return;

        lobbyRow.SetIndexAndName(index, newName);
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
    }

    private void RenderLocalPlayerInformation()
    {
        if (!NetworkClient.active || !isLocalPlayer) return;

        if (readyToBegin)
        {
            if (GUILayout.Button("Cancel"))
            {
                var randomName = Names[Random.Range(0, Names.Length - 1)];
                CmdUpdatePlayerState(index, false, randomName);
            }
        } else
        {
            if (GUILayout.Button("Ready"))
            {
                var randomName = Names[Random.Range(0, Names.Length - 1)];
                CmdUpdatePlayerState(index, true, randomName);
            }
        }
    }
}
