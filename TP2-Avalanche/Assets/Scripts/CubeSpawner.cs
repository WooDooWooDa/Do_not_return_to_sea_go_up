using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    [SerializeField] Collider spawnPlane;
    [SerializeField] List<GameObject> cubes;
    [SerializeField] GameManage gameManager;
    [SerializeField] List<Player> players;

    private float spawnRate = 1f;
    private float timer = 0f;
    private float minHeight = 25f;

    public void Initialize(GameManage gameManager)
    {
        this.gameManager = gameManager;
        timer = spawnRate;
        SetWidth();
        transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
    }

    void Update()
    {
        UpdatePosition();
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCube();
            timer = spawnRate + Random.Range(-0.2f, 0.5f);
        }
    }

    private void SetWidth()
    {
        spawnPlane.transform.localScale = new Vector3(gameManager.GetIslandSize() / 10, 0, 0.1f);
    }

    private void UpdatePosition()
    {
        if (players.Count == 0) {
            players = gameManager.GetPlayerList();
        }
        foreach (Player player in players) {
            var score = player.GetComponent<PlayerScore>();
            if (transform.position.y < score.GetMaxHeigth() + minHeight)
                transform.position = new Vector3(transform.position.x, score.GetMaxHeigth() + minHeight, transform.position.z);
        }
    }

    void SpawnCube()
    {
        if (!NetworkServer.active) return;
        NetworkServer.Spawn(Object.Instantiate(cubes[Random.Range(0, cubes.Count)], RandomPointInBounds(spawnPlane.bounds), Quaternion.identity));
        //GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Count)]);
        //cube.transform.position = RandomPointInBounds(spawnPlane.bounds);
    }

    private static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y) - 1,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
