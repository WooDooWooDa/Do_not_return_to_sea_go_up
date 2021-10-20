using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }

    void Start()
    {
        SetPlayerColor(new Color(0.9f, 0, 0, 1));
    }

    public void Kill()
    {
        Debug.Log("KILL PLAYER");
        GetComponent<PlayerHealth>().TakeDamage(999);
    }

    public void SetPlayerColor(Color color)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
    }
}
