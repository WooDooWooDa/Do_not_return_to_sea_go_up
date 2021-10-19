using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : NetworkBehaviour
{
    public GameObject AssociatedPlayer { get; set; }

    private Vector3 basePos = new Vector3(0, 3, -20);

    void Update()
    {
        if (AssociatedPlayer == null)
        {
            Destroy(gameObject);
            //reasignAssociatedPlayer to leader
        }
        transform.position = AssociatedPlayer.transform.position + basePos;
    }
}
