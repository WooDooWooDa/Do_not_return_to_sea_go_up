using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager = null;
    [SerializeField] private GameObject landingPagePanel;

    public void HostLobby()
    {
        try
        {
            networkManager.StartHost();
            landingPagePanel.SetActive(false);
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
