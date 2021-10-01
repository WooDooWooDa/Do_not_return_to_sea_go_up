using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] LayerMask cubeLayer;
    private GameObject water;
    private Rigidbody body;

    void Start()
    {
        water = GameObject.FindGameObjectsWithTag("Water")[0];
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < water.transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((cubeLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            body.isKinematic = true;
        }
    }
}
