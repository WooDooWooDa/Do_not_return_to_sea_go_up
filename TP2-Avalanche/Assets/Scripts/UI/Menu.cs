using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject playMenu;
    public void ToggleOptionActive()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);
    }

    public void TogglePlayActive()
    {
        playMenu.SetActive(!playMenu.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
