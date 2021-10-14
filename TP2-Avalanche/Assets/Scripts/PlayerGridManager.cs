using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGridManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyRowPrefab;
    [SerializeField] private TextMeshProUGUI playerCountText;
    private List<LobbyGridRow> playerRows;

    //[SyncVar(hook="UpdatePlayerCount")]
    private int nbPlayers;

    void Start()
    {
        playerRows = new List<LobbyGridRow>();
    }

    public void ReloadRows()
    {
        var playersRoom = FindObjectsOfType<NetworkRoomPlayerLobby>();
    }

    public void AddRow(LobbyGridRow row, GameObject grid)
    {
        Debug.Log($"Wtf: {row} {grid}");
        //playerRows.Add(row);
        row.transform.SetParent(grid.transform);
        //nbPlayers = playerRows.Count;
    }

    private void UpdatePlayerCount(int oldValue, int newValue)
    {
        playerCountText.text = "Players " + newValue + "/10";
    }
}
