using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyGridRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private CheckBox readyCheckBox;

    public int index;

    public void SetName(string nameValue, int playerIndex)
    {
        nameText.text = "P" + playerIndex + " : " +  nameValue;
        index = playerIndex;
    }

}
