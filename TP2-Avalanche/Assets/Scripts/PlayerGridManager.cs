using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGridManager : NetworkBehaviour
{
    [SerializeField] private GameObject lobbyRowPrefab;
    [SerializeField] private TextMeshProUGUI playerCountText;
    private List<LobbyGridRow> playerRows;

    [SyncVar(hook="UpdatePlayerCount")]
    private int nbPlayers;

    void Start()
    {
        playerRows = new List<LobbyGridRow>();
    }

    public void ReloadRows()
    {
        var playersRoom = FindObjectsOfType<NetworkRoomPlayerLobby>();
    }

    public void AddRow(NetworkRoomPlayerLobby roomPlayer)
    {
        var playerGrid = GameObject.FindGameObjectWithTag("PlayerGrid");
        var row = Instantiate(lobbyRowPrefab, playerGrid.transform.position, playerGrid.transform.rotation);
        playerRows.Add(row.GetComponent<LobbyGridRow>());
        (row.GetComponent<LobbyGridRow>()).SetName(roomPlayer.PlayerName, 1);
        row.transform.SetParent(playerGrid.transform);
        nbPlayers = playerRows.Count;
    }

    private void UpdatePlayerCount(int oldValue, int newValue)
    {
        playerCountText.text = "Players " + newValue + "/10";
    }
}
