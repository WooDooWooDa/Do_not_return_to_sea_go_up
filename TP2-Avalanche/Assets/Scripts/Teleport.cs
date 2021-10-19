using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject otherBound;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            var xDecal = collision.transform.position.x < 0 ? -1 : 1;
            Debug.Log("OLD POS" + collision.transform.position);
            var newPos = new Vector3(otherBound.transform.position.x + xDecal, collision.transform.position.y, 0);
            Debug.Log("NEW POS : " + newPos);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(5 * xDecal, 0, 0), ForceMode.Impulse);
            collision.transform.position = newPos;
        }
    }
}
