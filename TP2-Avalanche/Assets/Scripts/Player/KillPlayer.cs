using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : NetworkBehaviour
{
    [SerializeField] private LayerMask crusher;
    [SerializeField] private LayerMask cubes;

    private PlayerMovement playerMovement;
    [SyncVar]
    private bool isGod = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                isGod = !isGod;
                CmdMakeGod(isGod);
            }
        }

        if (!isServer) return;

        if (isGod) return;

        if (GetComponent<PlayerHealth>().IsAlive() && GameObject.Find("Ocean").transform.position.y - 1 > gameObject.transform.position.y)
        {
            Debug.Log("Water");
            GetComponent<PlayerHealth>().TakeDamage(999);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return;

        if (isGod) return;

        if (playerMovement.IsGrounded && (cubes.value & (1 << collision.gameObject.layer)) > 0 && GetComponent<PlayerHealth>().IsAlive()) //kill player if crushed
        {
            Debug.Log("Crusher");
            GetComponent<PlayerHealth>().TakeDamage(999);
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (!isServer) return;

        if (isGod) return;

        if ((crusher.value & (1 << collision.gameObject.layer)) > 0 && !collision.GetComponentInParent<Rigidbody>().isKinematic) //dmg player player if crushed but not grounded
        {
            Debug.Log("take damage");
            var dmg = -collision.GetComponentInParent<Rigidbody>().velocity.y;
            if (dmg < 0)
                dmg = -dmg;
            if (dmg >= 100)
                dmg /= 2;
            GetComponent<PlayerHealth>().TakeDamage(dmg);
        }
    }

    [Command]
    private void CmdMakeGod(bool mode)
    {
        isGod = mode;
    }
}
