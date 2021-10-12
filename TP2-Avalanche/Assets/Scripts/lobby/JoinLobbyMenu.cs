using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager;

    [Header("UI")]
    [SerializeField] private GameObject lobbyPagePanel;
    [SerializeField] private TMP_InputField ipAdressField;
    [SerializeField] private Button joinButton;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisonnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisonnected;
    }

    public void JoinLobby()
    {
        string ip = ipAdressField.text;
        networkManager.networkAddress = ip;
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        lobbyPagePanel.SetActive(true);
    }

    private void HandleClientDisonnected()
    {
        joinButton.interactable = true;
    }
}
