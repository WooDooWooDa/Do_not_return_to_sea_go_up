using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxHeigth;

    void Start()
    {
        SetPlayerColor(new Color(0.9f, 0, 0, 1));
    }


    void Update()
    {
        updateMaxHeight();
    }

    public void Kill()
    {
        Debug.Log("Kill Player");
    }

    public float GetMaxHeigth()
    {
        return maxHeigth;
    }

    public void SetPlayerColor(Color color)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = GetRandomColor();
    }

    private void updateMaxHeight()
    {
        if (transform.position.y > maxHeigth)
        {
            maxHeigth = transform.position.y;
        }
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
