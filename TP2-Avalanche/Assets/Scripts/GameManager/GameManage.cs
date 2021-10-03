using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    [SerializeField] List<Player> players;
    [SerializeField] GameObject island;
    [SerializeField] GameObject env;

    void Start()
    {
        GetPlayers();
        float islandLength = players.Count > 1 ? (players.Count * 0.5f) + 0.5f : 1f;
        island.transform.localScale = new Vector3(islandLength, 1, 1);
        SpawnEnv();
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }

    public Vector3 GetIslandSize()
    {
        return island.GetComponent<Renderer>().bounds.size;
    }

    private void SpawnEnv()
    {
        var nbEnv = GetIslandSize().x / 30;
        Debug.Log(nbEnv);
    }

    private void GetPlayers()
    {
        var playersObj = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in playersObj)
        {
            if (obj != null)
            {
                players.Add(obj.GetComponent<Player>());
            }
        }
    }
}
