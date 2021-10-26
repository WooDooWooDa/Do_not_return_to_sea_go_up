using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeMenu : MonoBehaviour
{
    [SerializeField] Collider spawnPlane;
    [SerializeField] List<GameObject> cubes;

    private float spawnRate = 1f;
    private float timer = 0f;
    private int counter = 0;
    private int maxCubes = 50;

    void Start()
    {
        timer = spawnRate;
    }

    void Update()
    {
        if (maxCubes > counter) {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Spawn();
                counter++;
                timer = spawnRate + Random.Range(-0.2f, 1f);
            }
        }
    }

    void Spawn()
    {
        GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Count)]);
        cube.transform.position = RandomPointInBounds(spawnPlane.bounds);
        cube.transform.SetParent(spawnPlane.transform);
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
