using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : NetworkBehaviour
{
    [SerializeField] List<PlayerScore> players;
    [SerializeField] GameObject env;
    [SerializeField] GameObject spawnPointPrefab;
    [SerializeField] CubeSpawner cubeSpawner;

    private int nbConnPlayers;

    public List<PlayerScore> GetPlayerList()
    {
        GetPlayers();
        return players;
    }

    [Server]
    public void Initialize(int nbPlayers)
    {
        nbConnPlayers = nbPlayers;
        if (isServer)
        {

            SetIslandSize();
            SpawnSpawnPoint();
            StartSpawningCubes();
            GetPlayers();
        }
    }

    [Server]
    public void StartGame()
    {
        if (isServer)
        {
            StartCoroutine(GameLoop());
        }
    }

    [Server]
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStart());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnd());

        NetworkServer.Shutdown();
        SceneManager.LoadScene("Menu");
    }

    [Server]
    private IEnumerator RoundStart()
    {
        yield return new WaitForSeconds(1);
    }

    [Server]
    private IEnumerator RoundPlaying()
    {
        while (OnePlayerAlive())
        {
            yield return null;
        }
    }

    [Server]
    private IEnumerator RoundEnd()
    {
        PlayerScore winner = FindWinner();
        if (players.Count == 1) {
            RpcShowSoloScreen(winner.GetMaxHeigth());
        } else {
            RpcShowWinnerScreen(winner.GetComponent<Player>().Name, winner.GetMaxHeigth());
        }

        yield return new WaitForSeconds(5);
    }

    [Server]
    private bool OnePlayerAlive()
    {
        foreach (PlayerScore player in players)
        {
            if (player.GetComponentInParent<PlayerHealth>().IsAlive())
            {
                return true;
            }
        }
        return false;
    }

    public float GetIslandSize()
    {
        return (GameObject.FindGameObjectWithTag("Island")).GetComponent<Renderer>().bounds.size.x;
    }

    [Server]
    private void StartSpawningCubes()
    {
        if (!isServer) return;
        cubeSpawner = GameObject.Find("CubeSpawner").GetComponent<CubeSpawner>();
        cubeSpawner.Initialize(this);
    }

    [Server]
    private void SetIslandSize()
    {
        float islandLength = nbConnPlayers > 1 ? (nbConnPlayers * 0.5f) : 0.5f;
        RpcSpawnEnv(islandLength);
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

    private void RpcSpawnEnv(float size)
    {
        GameObject.FindGameObjectWithTag("Island").transform.localScale = new Vector3(size, 0.2f, 1);
        var nbEnv = Mathf.Round(GetIslandSize() / 15);
        var startX = 0f;
        if (nbEnv >= 1)
        {
            startX = (-7f * nbEnv) + 7f;
        }
        Debug.Log("nb env : " + nbEnv);
        for (int i = 0; i < nbEnv; i++)
        {
            Instantiate(env, new Vector3(transform.position.x + (startX), -0.2f, 0), transform.rotation);
            startX += 14f;
        }
    }

    [ClientRpc]
    private void RpcShowWinnerScreen(string name, float score)
    {
        GameObject.FindObjectOfType<CameraRig>().WinnerScreen(name, score);
    }

    [ClientRpc]
    private void RpcShowSoloScreen(float score)
    {
        GameObject.FindObjectOfType<CameraRig>().SoloWinnerScreen(score);
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

    private PlayerScore FindWinner()
    {
        PlayerScore winner = null;
        foreach (PlayerScore player in players)
        {
            if (winner == null)
                winner = player;
            else if (winner.GetMaxHeigth() < player.GetMaxHeigth())
                winner = player;
        }
        return winner;
    }
}
