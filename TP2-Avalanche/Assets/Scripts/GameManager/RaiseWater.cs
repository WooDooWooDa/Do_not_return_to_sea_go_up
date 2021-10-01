using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWater : MonoBehaviour
{
    [SerializeField] GameObject water;
    [SerializeField] float raiseLevel = 0.1f;
    
    private int nextUpdate = 1;
    private int firstSeconds = 10;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            if (waterCanRaise())
            {
                Raise();
            } else
            {
                firstSeconds--;
            }
        }
    }

    void Raise()
    {
        water.transform.position = new Vector3(0, water.transform.position.y + raiseLevel, 0);
    }

    private bool waterCanRaise()
    {
        return firstSeconds == 0;
    }
}
