using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject otherBound;

    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.gameObject;
        if (collision.tag == "Player")
        {
            var xDecal = player.transform.position.x < 0 ? -1 : 1;
            Debug.Log("OLD POS" + collision.transform.position);
            var newPos = new Vector3(otherBound.transform.position.x + xDecal, player.transform.position.y, 0);
            Debug.Log("NEW POS : " + newPos);
            player.transform.position = newPos;
        }
    }
}
