using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCamera : MonoBehaviour
{
    [SerializeField] private GameObject cameraRigPrefab;
    void Awake()
    {
        var camera = Instantiate(cameraRigPrefab, transform.position - (new Vector3(0, -5, 25)), new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w));
        camera.GetComponentInChildren<FollowPlayer>().AssociatedPlayer = gameObject;
    }
}
