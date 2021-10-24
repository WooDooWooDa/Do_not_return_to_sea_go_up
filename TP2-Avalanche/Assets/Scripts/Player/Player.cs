using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public string Name { get { return playerName; } set { playerName = value; } }

    [SyncVar]
    private string playerName;

    void Start()
    {
        SetPlayerColor(new Color(0.9f, 0, 0, 1));
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
