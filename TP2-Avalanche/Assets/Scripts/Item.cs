using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Effect effect;

    private float duration;
    private bool hasTouchedGround = false;

    void Start()
    {
        if (effect == Effect.HealthRegen)
            duration = 3;
        duration = Random.Range(5, 8);
    }

    public string GetName()
    {
        return effect.ToString(); //todo override for friendly string
    }

    public float GetDuration()
    {
        return duration;
    }

    public Effect GetEffect()
    {
        return effect;
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Island" || collision.gameObject.tag == "MenuCubes") && !hasTouchedGround) {
            Debug.Log("Initiate destroy...");
            hasTouchedGround = true;
            Destroy(gameObject, 5);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerItem playerItem = other.gameObject.GetComponent<PlayerItem>();
            playerItem.CollectItem(gameObject.GetComponent<Item>()); //factory maybe?
            Destroy(gameObject);
        }
    }
}
