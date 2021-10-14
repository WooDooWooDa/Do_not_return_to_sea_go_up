using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager = null;
    [SerializeField] private GameObject landingPagePanel;

    public void HostLobby()
    {
        landingPagePanel.SetActive(false);
        networkManager.StartHost();
    }
}
