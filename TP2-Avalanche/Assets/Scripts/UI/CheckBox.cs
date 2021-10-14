using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private GameObject filler;

    public void SetReady(bool ready)
    {
        filler.SetActive(ready);
    }
}
