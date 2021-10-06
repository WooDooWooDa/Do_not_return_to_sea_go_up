using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject playMenu;
    [SerializeField] GameObject nameMenu;

    public void ToggleOptionActive()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);
    }

    public void TogglePlayActive()
    {
        playMenu.SetActive(!playMenu.activeSelf);
    }

    public void ToggleNameActive()
    {
        nameMenu.SetActive(!nameMenu.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
