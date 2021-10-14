using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    [ClientRpc]
    public void UpdateUIClient(NetworkRoomPlayerLobby roomPlayerLobby)
    {
        Debug.Log("CLient caller icitte!");
    }

    public void StartGame()
    {
        
    }
}
