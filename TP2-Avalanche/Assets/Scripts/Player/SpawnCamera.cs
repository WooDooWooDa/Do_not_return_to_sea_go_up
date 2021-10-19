using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCamera : NetworkBehaviour
{
    [SerializeField] private GameObject cameraRigPrefab;
    void Awake()
    {
        
    }

    public override void OnStartAuthority()
    {
        var cameraRig = Instantiate(cameraRigPrefab, transform.position - (new Vector3(0, -5, 25)), new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w));
        cameraRig.GetComponentInChildren<FollowPlayer>().AssociatedPlayer = gameObject;
    }
}
