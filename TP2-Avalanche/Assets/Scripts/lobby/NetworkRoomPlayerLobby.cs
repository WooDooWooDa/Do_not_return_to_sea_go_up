using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRoomPlayerLobby : NetworkBehaviour
{
    public string PlayerName { get; set; }

    public void IsReady()
    {
        NetworkClient.ready = true;
    }
}
