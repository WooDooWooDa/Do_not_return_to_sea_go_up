using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager = null;
    [SerializeField] private GameObject landingPagePanel;
    [SerializeField] private TextMeshProUGUI personnalBest;

    private void Start()
    {
        personnalBest.text = PlayerPrefs.GetFloat("maxHeigth").ToString("0.0") + " Ft";
    }

    public void HostLobby()
    {
        if (networkManager == null) {
            networkManager = GameObject.FindObjectOfType<NetworkRoomManager>();
        }
        if (networkManager.isNetworkActive || networkManager.isActiveAndEnabled) {
            Debug.LogError("NETWORK IS ACTIVE");
            networkManager.StopHost();
            networkManager.StopClient();
            networkManager.StopServer();
        }
        try {
            networkManager.StartHost();
        } catch (Exception e) {
            Debug.Log(e);
        }
        landingPagePanel.SetActive(false);
    }
}
