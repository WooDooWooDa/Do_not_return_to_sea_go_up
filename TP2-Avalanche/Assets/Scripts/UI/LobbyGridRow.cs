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
        Index = playerIndex + 1;
    }

    public void SetReadyFlag(bool readyToBegin)
    {
        readyCheckBox.SetReady(readyToBegin);
    }
}
