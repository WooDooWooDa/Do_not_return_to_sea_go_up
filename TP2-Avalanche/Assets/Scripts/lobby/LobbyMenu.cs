using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;
    [SerializeField] private GameObject landingPagePanel;
    [SerializeField] private GameObject lobbyPanel;

    public void HostLobby()
    {
        lobbyPanel.SetActive(true);
        landingPagePanel.SetActive(false);
        networkManager.StartHost();
    }
}
