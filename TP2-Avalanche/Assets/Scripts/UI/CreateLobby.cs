using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateLobby : MonoBehaviour
{
    [SerializeField] GameObject inputField;

    public void Create()
    {
        string text = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log(text);
    }
}
