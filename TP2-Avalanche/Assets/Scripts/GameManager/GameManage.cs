using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : NetworkBehaviour
{
    [SerializeField] List<PlayerScore> players;
    [SerializeField] GameObject env;
    [SerializeField] GameObject spawnPointPrefab;
    [SerializeField] CubeSpawner cubeSpawner;

    private int nbConnPlayers;

    [Server]
    public void Initialize(int nbPlayers)
    {
        nbConnPlayers = nbPlayers;
        SetIslandSize();
        SpawnSpawnPoint();
        StartSpawningCubes();
    }

    public List<PlayerScore> GetPlayerList()
    {
        if (players.Count == 0) {
            GetPlayers();
        }
        return players;
    }

    public float GetIslandSize()
    {
        return (GameObject.FindGameObjectWithTag("Island")).GetComponent<Renderer>().bounds.size.x;
    }

    private void StartSpawningCubes()
    {
        cubeSpawner = GameObject.Find("CubeSpawner").GetComponent<CubeSpawner>();
        cubeSpawner.Initialize(this);
    }

    private void SetIslandSize()
    {
        float islandLength = nbConnPlayers > 1 ? (nbConnPlayers * 0.5f) : 0.5f;
        Debug.Log("Island Size : " + islandLength);
        GameObject.FindGameObjectWithTag("Island").transform.localScale = new Vector3(islandLength, 0.2f, 1);
        SpawnEnv();
        SpawnEnvRpc();
    }

    private void SpawnSpawnPoint()
    {
        var nbSpawn = Mathf.Round(GetIslandSize() / 15);
        var startX = 0f;
        if (nbSpawn >= 1) {
            startX = (-7f * nbSpawn) + 7f;
        }
        for (int i = 0; i < nbSpawn; i++) {
            NetworkServer.Spawn(Object.Instantiate(spawnPointPrefab, new Vector3(transform.position.x + (startX), 3, 0), spawnPointPrefab.transform.rotation));
            startX += 14f;
        }
    }

    [ClientRpc]
    private void SpawnEnvRpc()
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

    private void SpawnEnv()
    {
        var nbEnv = Mathf.Round(GetIslandSize() / 15);
        var startX = 0f;
        if (nbEnv >= 1)
        {
            startX = (-7f * nbEnv) + 7f;
        }
        for (int i = 0; i < nbEnv; i++)
        {
            Instantiate(env, new Vector3(transform.position.x + (startX), -0.2f, 0), transform.rotation);
            startX += 14f;
        }
    }

    private void GetPlayers()
    {
        var playersObj = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in playersObj)
        {
            if (obj != null && !players.Contains(obj.GetComponent<PlayerScore>()))
            {
                players.Add(obj.GetComponent<PlayerScore>());
            }
        }
    }
}
