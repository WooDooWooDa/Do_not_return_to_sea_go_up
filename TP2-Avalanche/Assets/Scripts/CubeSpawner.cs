using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] Collider spawnPlane;
    [SerializeField] List<GameObject> cubes;
    [SerializeField] GameManage gameManager;
    [SerializeField] private List<Player> players;

    private float spawnRate = 1f;
    private float timer = 0f;
    private float minHeight = 25f;
    
    void Start()
    {
        timer = spawnRate;
        players = gameManager.GetPlayerList();
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
            timer = spawnRate + Random.Range(-0.2f, 1f);
        }
    }

    private void SetWidth()
    {
        spawnPlane.transform.localScale = new Vector3(gameManager.GetIslandSize().x / 10, 0, 0.1f); ;
    }

    private void UpdatePosition()
    {
        foreach (Player player in players)
        {
            if (transform.position.y < player.GetMaxHeigth())
                transform.position = new Vector3(transform.position.x, player.GetMaxHeigth() + minHeight, transform.position.z);
        }
    }

    void SpawnCube()
    {
        GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Count)]);
        cube.transform.position = RandomPointInBounds(spawnPlane.bounds);
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
