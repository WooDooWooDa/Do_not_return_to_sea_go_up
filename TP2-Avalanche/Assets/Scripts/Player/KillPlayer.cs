using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerMovement.IsGrounded && collision.relativeVelocity.y < 0) //kill player if crushed
        {
            GetComponent<Player>().Kill();
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.tag == "Water") // kill if in water
        {
            GetComponent<Player>().Kill();
        }
    }
}
