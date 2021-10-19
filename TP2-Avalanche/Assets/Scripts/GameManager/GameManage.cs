using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : NetworkBehaviour
{
    [SerializeField] List<Player> players;
    [SerializeField] GameObject env;
    [SerializeField] GameObject spawnPointPrefab;

    void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        GetPlayers();
    }

    public void Initialize()
    {
        GetPlayers();
        SetIslandSize();
        SpawnSpawnPoint();
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }

    public float GetIslandSize()
    {
        return (GameObject.FindGameObjectWithTag("Island")).GetComponent<Renderer>().bounds.size.x;
    }

    private void SetIslandSize()
    {
        float islandLength = players.Count > 1 ? (players.Count * 0.5f) : 0.5f;
        (GameObject.FindGameObjectWithTag("Island")).transform.localScale = new Vector3(islandLength, 1, 1);
        SpawnEnv();
    }

    private void SpawnSpawnPoint()
    {
        var nbSpawn = Mathf.Round(GetIslandSize() / 15);
        var startX = 0f;
        if (nbSpawn >= 1)
        {
            startX = (-7f * nbSpawn) + 7f;
        }
        for (int i = 0; i < nbSpawn; i++)
        {
            Instantiate(spawnPointPrefab, new Vector3(transform.position.x + (startX), 3, 0), spawnPointPrefab.transform.rotation);
            startX += 14f;
        }
    }

    private void SpawnEnv()
    {
        var nbEnv = Mathf.Round(GetIslandSize() / 15);
        var startX = 0f;
        if (nbEnv >= 1) {
            startX = (-7f * nbEnv) + 7f;
        }
        for (int i = 0; i < nbEnv; i++) {
            Instantiate(env, new Vector3(transform.position.x + (startX), -0.2f, 0), transform.rotation);
            startX += 14f;
        }
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
