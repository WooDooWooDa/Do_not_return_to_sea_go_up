using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGridManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyRowPrefab;
    [SerializeField] private TextMeshProUGUI playerCountText;

    public void AddRow(LobbyGridRow row, GameObject grid)
    {
        row.transform.SetParent(grid.transform);
    }
}
