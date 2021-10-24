using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField] private GameObject cameraRigPrefab;

    private GameObject rig;

    void Awake()
    {
        
    }

    public void DeadScreen(float score)
    {
        rig.GetComponent<CameraRig>().DeadScreen(score);
    }

    public GameObject Rig()
    {
        return rig;
    }

    public override void OnStartAuthority()
    {
        var cameraRig = Instantiate(cameraRigPrefab, transform.position - (new Vector3(0, -5, 25)), new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w));
        cameraRig.GetComponentInChildren<CameraRig>().AssociatedPlayer = gameObject;
        rig = cameraRig;
    }

    
}
