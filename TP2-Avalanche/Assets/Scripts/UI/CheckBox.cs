using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private GameObject filler;

    private bool ready = false;

    public void ToggleReady()
    {
        ready = !ready;
        filler.SetActive(ready);
    }
}
