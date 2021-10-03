using System.Collections;
using System.Collections.Generic;
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
    
    public float GetMaxHeigth()
    {
        return maxHeigth;
    }

    public void SetPlayerColor(Color color)
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = color;
    }

    private void updateMaxHeight()
    {
        if (transform.position.y > maxHeigth)
        {
            maxHeigth = transform.position.y;
        }
    }
}
