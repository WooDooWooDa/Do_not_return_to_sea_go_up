using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    [SerializeField] Player[] players;
    [SerializeField] GameObject island;

    void Start()
    {
        var islandLength = players.Length > 0 ? players.Length : 1;
        island.transform.localScale = new Vector3(islandLength, 1, 1);
    }


    void Update()
    {

    }
}
