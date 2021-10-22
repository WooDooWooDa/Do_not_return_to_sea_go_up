using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : NetworkBehaviour
{
    [SerializeField] LayerMask cubeLayer;
    [SerializeField] LayerMask groundLayer;
    private GameObject water;
    private Rigidbody body;

    void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < water.transform.position.y)
        {
            Destroy(gameObject);
        }
        if (body.velocity.y > 0.1)
        {
            body.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((cubeLayer.value & (1 << collision.transform.gameObject.layer)) > 0 || (groundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            body.isKinematic = true;
        }
    }
}
