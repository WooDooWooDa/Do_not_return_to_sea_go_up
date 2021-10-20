using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Item currentItem;

    private float elapsed = 0f;

    public string GetItemName()
    {
        return currentItem.GetName();
    }

    public void CollectItem(Item item)
    {
        Debug.Log("Collected : " + item.GetName());
        currentItem = item;
    }

    private void Update()
    {
        if (currentItem != null && elapsed <= currentItem.GetDuration()) { // item is always null
            elapsed += Time.deltaTime;
            if (elapsed >= 1f) {
                elapsed %= 1f;
                ApplyEffect(currentItem.GetEffect());
            }
        } else {
            StopCurrentEffect();
        }
    }

    private void ApplyEffect(Effect effect)
    {
        switch (effect) {
            case Effect.JumpBoost: gameObject.GetComponent<PlayerMovement>().ApplyJumpBoost(); break;
            case Effect.SpeedBoost: gameObject.GetComponent<PlayerMovement>().ApplySpeedBoost(); break;
            case Effect.FeatherFalling: gameObject.GetComponent<PlayerMovement>().ApplyFeatherFalling(); break;
            case Effect.HealthRegen: gameObject.GetComponent<PlayerHealth>().ApplyHealthRegen(); break;
        }
    }

    private void StopCurrentEffect()
    {
        elapsed = 0f;
        currentItem = null;
        gameObject.GetComponent<PlayerMovement>().ResetToBase();
    }
}
