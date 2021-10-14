using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyGridRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private CheckBox readyCheckBox;

    public int Index { get; private set; }

    public void SetIndexAndName(int playerIndex, string nameValue)
    {
        nameText.text = $"P{playerIndex} : {nameValue}";
        Index = playerIndex;
    }

    public void SetReadyFlag(bool readyToBegin)
    {
        // TODO: Change the Ready UI element based on the state
        nameText.text += $" - {readyToBegin}";
        readyCheckBox.SetReady(readyToBegin);
    }
}
