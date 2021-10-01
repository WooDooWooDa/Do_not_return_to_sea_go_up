using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] Collider spawnPlane;
    [SerializeField] List<GameObject> cubes;
    [SerializeField] GameManage gameManager;

    private float spawnRate = 1f;
    private float timer = 0f;
    
    void Start()
    {
        timer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCube();
            timer = spawnRate + Random.Range(-0.2f, 1f);
        }
    }

    void SpawnCube()
    {

    }
}
