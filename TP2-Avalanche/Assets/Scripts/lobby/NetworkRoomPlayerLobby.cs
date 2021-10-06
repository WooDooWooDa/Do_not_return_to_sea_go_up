using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRoomPlayerLobby : NetworkBehaviour
{
    private string playerName;
    private bool ready = false;

    public bool IsReady()
    {
        return ready;
    }
}
