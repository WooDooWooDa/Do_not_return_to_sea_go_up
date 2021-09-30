using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] Collider spawnPlane;
    [SerializeField] List<GameObject> cubes;

    private float spawnRate = 1f;
    private float timer = 0f;
    private int counter = 0;
    private int maxCubes = 75;

    void Start()
    {
        timer = spawnRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            counter++;
            timer = spawnRate + Random.Range(-0.2f, 1f);
        }
        if (counter >= maxCubes)
        {
            counter = 0;
            var cubes = GameObject.FindGameObjectsWithTag("MenuCubes");
            foreach (var cube in cubes)
            {
                Destroy(cube);
            }
        }
    }

    void Spawn()
    {
        GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Count)]);
        cube.transform.position = RandomPointInBounds(spawnPlane.bounds);
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y) - 1,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
