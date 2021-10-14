using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMangerExt : NetworkManager
{
    [SerializeField] private GameManage gameManager;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        gameManager.Initialize();
        base.OnServerAddPlayer(conn);
    }

}
