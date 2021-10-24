using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar]
    public int Position = 0;

    [SyncVar]
    private float maxHeigth = 0;

    void Update()
    {
        updateMaxHeight();
    }

    public float GetMaxHeigth()
    {
        return maxHeigth;
    }

    private void updateMaxHeight()
    {
        if (transform.position.y > maxHeigth)
        {
            maxHeigth = transform.position.y;
        }
    }
}
