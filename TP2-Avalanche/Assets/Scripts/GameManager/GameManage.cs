using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : NetworkBehaviour
{
    [SerializeField] List<PlayerScore> players;
    [SerializeField] List<GameObject> envs;
    [SerializeField] GameObject spawnPointPrefab;
    [SerializeField] CubeSpawner cubeSpawner;

    [Header("Music")]
    [SerializeField] List<AudioClip> playlist;

    private int nbConnPlayers;

    public List<PlayerScore> GetPlayerList()
    {
        GetPlayers();
        return players;
    }

    public override void OnStartServer()
    {
        Initialize(NetworkManager.singleton.numPlayers);
        StartGame();
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
        RpcPlayMusic(Random.Range(0, playlist.Count - 1));
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
            foreach (PlayerScore player in players)
            {
                RpcShowWinnerScreen(player.GetComponent<NetworkIdentity>().connectionToClient, winner.GetComponent<Player>().Name, winner.GetMaxHeigth(), player.GetMaxHeigth());
            }
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
        cubeSpawner.Initialize(this, nbConnPlayers);
    }

    [Server]
    private void SetIslandSize()
    {
        float islandLength = nbConnPlayers > 1 ? (nbConnPlayers * 0.5f) : 0.5f;
        SpawnEnv(islandLength);
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

    private void SpawnEnv(float size)
    {
        GameObject.FindGameObjectWithTag("Island").transform.localScale = new Vector3(size, 0.2f, 1);
        var nbEnv = Mathf.Round(GetIslandSize() / 15);
        var startX = 0f;
        if (nbEnv >= 1) {
            startX = (-7f * nbEnv) + 7f;
        }
        for (int i = 0; i < nbEnv; i++) {
            NetworkServer.Spawn(Instantiate(envs[Random.Range(0, envs.Count - 1)], new Vector3(transform.position.x + (startX), -0.2f, 0), transform.rotation));
            startX += 14f;
        }
    }

    [ClientRpc]
    private void RpcPlayMusic(int clipIndex)
    {
        Debug.Log(clipIndex);
        GameObject.Find("MenuMusic").GetComponent<AudioSource>().Stop();
        AudioSource audioSource = GameObject.Find("BgMusic").GetComponent<AudioSource>();
        audioSource.clip = playlist[clipIndex];
        audioSource.Play();

    }

    [TargetRpc]
    private void RpcShowWinnerScreen(NetworkConnection target, string name, float score, float personnalScore)
    {
        Debug.Log("BEFORE WINNER SCREEN");
        GameObject.FindObjectOfType<CameraRig>().WinnerScreen(name, score, personnalScore);
    }

    [ClientRpc]
    private void RpcShowSoloScreen(float score)
    {
        Debug.Log("BEFORE SOLO SCREEN");
        GameObject.FindObjectOfType<CameraRig>().SoloWinnerScreen(score);
    }

    private void GetPlayers()
    {
        var playersObj = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in playersObj) {
            if (obj != null && !players.Contains(obj.GetComponent<PlayerScore>())) {
                players.Add(obj.GetComponent<PlayerScore>());
            }
        }
    }

    private PlayerScore FindWinner()
    {
        PlayerScore winner = null;
        foreach (PlayerScore player in players) {
            if (winner == null)
                winner = player;
            else if (winner.GetMaxHeigth() < player.GetMaxHeigth())
                winner = player;
        }
        return winner;
    }
}
