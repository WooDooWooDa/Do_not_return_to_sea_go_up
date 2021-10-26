using Mirror;
using UnityEngine;

public class Item : NetworkBehaviour
{
    [SerializeField] Effect effect;
    [SerializeField] ParticleSystem particles;

    private float duration;
    private float timeOnFloor = 0;
    private bool hasTouchedGround = false;

    void Start()
    {
        duration = Random.Range(6, 10);
        if (effect == Effect.HealthRegen)
            duration = 3;
        if (Random.Range(1, 5) == 5) {
            particles.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Stretch;
            duration *= 2;
        }
    }

    private void Update()
    {
        if (hasTouchedGround && timeOnFloor >= 5) {
            CmdDestroyItem(gameObject);
        } else if (hasTouchedGround) {
            timeOnFloor += Time.deltaTime;
        }
    }

    public float GetDuration()
    {
        return duration;
    }

    public Effect GetEffect()
    {
        return effect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Island") || collision.gameObject.CompareTag("MenuCubes")) {
            hasTouchedGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerItem playerItem = other.gameObject.GetComponent<PlayerItem>();
            var item = gameObject.GetComponent<Item>();
            playerItem.CollectItem(item.GetEffect(), item.GetDuration());
            CmdDestroyItem(gameObject);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdDestroyItem(GameObject item)
    {
        NetworkServer.Destroy(item);
    }

}
