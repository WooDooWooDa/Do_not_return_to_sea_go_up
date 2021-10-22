using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    [SerializeField] Collider spawnPlane;
    [SerializeField] List<GameObject> cubes;
    [SerializeField] GameManage gameManager;
    [SerializeField] List<PlayerScore> players;
    [SerializeField] List<GameObject> items;

    private float cubeSpawnRate = 1f;
    private float itemSpawnRate = 10f;
    private float cubeTimer = 0f;
    private float itemTimer = 0f;
    private float minHeight = 25f;

    public void Initialize(GameManage gameManager)
    {
        this.gameManager = gameManager;
        SetWidth();
        transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
    }

    void Update()
    {
        UpdatePosition();
        cubeTimer += Time.deltaTime;
        itemTimer += Time.deltaTime;

        if (cubeTimer >= cubeSpawnRate)
        {
            Spawn(cubes);
            cubeTimer = 0f + Random.Range(-0.2f, 0.2f);
        }

        if (itemTimer >= itemSpawnRate) {
            Spawn(items);
            itemTimer = 0f;
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
        foreach (PlayerScore player in players) {
            if (transform.position.y < player.GetMaxHeigth() + minHeight)
                transform.position = new Vector3(transform.position.x, player.GetMaxHeigth() + minHeight, transform.position.z);
        }
    }

    private void Spawn(List<GameObject> list)
    {
        if (!NetworkServer.active) return;
        NetworkServer.Spawn(Object.Instantiate(list[Random.Range(0, list.Count)], RandomPointInBounds(spawnPlane.bounds), Quaternion.identity));
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
