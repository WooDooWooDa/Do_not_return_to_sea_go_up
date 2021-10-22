using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask crusher;
    [SerializeField] private LayerMask cubes;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerMovement.IsGrounded && (cubes.value & (1 << collision.gameObject.layer)) > 0) //kill player if crushed
        {
            Debug.Log("Crusher");
            Debug.Log("KILL PLAYER");
            //GetComponent<PlayerHealth>().TakeDamage(999);
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if ((crusher.value & (1 << collision.gameObject.layer)) > 0 && !collision.GetComponentInParent<Rigidbody>().isKinematic) //dmg player player if crushed
        {
            Debug.Log("take damage");
            var dmg = -collision.GetComponentInParent<Rigidbody>().velocity.y;
            if (dmg < 0)
                dmg = -dmg;
            if (dmg >= 100)
                dmg /= 2;
            GetComponent<PlayerHealth>().TakeDamage(dmg);
        }

        if (collision.tag == "Water" && gameObject.transform.position.y < collision.transform.position.y + 2) // kill if in water
        {
            Debug.Log("Water");
            Debug.Log("KILL PLAYER");
            GetComponent<PlayerHealth>().TakeDamage(999);
        }
    }
}
