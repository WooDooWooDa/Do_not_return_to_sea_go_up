using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager;

    [Header("UI")]
    [SerializeField] private GameObject lobbyPagePanel;
    [SerializeField] private TMP_InputField ipAdressField;

    public void JoinLobby()
    {
        string ip = ipAdressField.text;
        if (ip == "") {
            ip = "localhost";
        }
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
            networkManager.networkAddress = ip;
            networkManager.StartClient();
        } catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Wrong ip");
        }
        
    }
}
