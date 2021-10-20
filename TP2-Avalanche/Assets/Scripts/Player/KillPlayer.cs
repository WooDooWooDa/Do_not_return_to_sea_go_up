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
        if (playerMovement.IsGrounded && false) //kill player if crushed
        {
            Debug.Log("Crushed");
            Debug.Log("KILL PLAYER");
            GetComponent<PlayerHealth>().TakeDamage(999);
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.tag == "Water") // kill if in water
        {
            Debug.Log("Water");
            Debug.Log("KILL PLAYER");
            GetComponent<PlayerHealth>().TakeDamage(999);
        }
    }
}
