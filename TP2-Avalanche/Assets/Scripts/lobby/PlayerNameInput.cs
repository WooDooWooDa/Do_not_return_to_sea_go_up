using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    public static string NAME_KEY = "playerName";

    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button continueButton;

    public static string DisplayName { get; private set; }
    private string lastNameValue = "";
    private int maxLength = 10;

    void Start()
    {
        SetupInputField();
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(NAME_KEY, DisplayName);
    }

    private void SetupInputField()
    {
        if (!PlayerPrefs.HasKey(NAME_KEY)) return;

        string defaultName = PlayerPrefs.GetString(NAME_KEY);
        nameInputField.text = defaultName;
        nameInputField.onValueChanged.AddListener(delegate { SetContinueButton(); } );
        nameInputField.onValueChanged.AddListener(delegate { CheckLength(); });
    }

    private void CheckLength()
    {
        if (lastNameValue != nameInputField.text && nameInputField.text.Length > maxLength) {
            nameInputField.text = lastNameValue;
        } else {
            lastNameValue = nameInputField.text;
        }
    }

    private void SetContinueButton()
    {
        continueButton.interactable = !string.IsNullOrEmpty(nameInputField.text);
    }

    
}
