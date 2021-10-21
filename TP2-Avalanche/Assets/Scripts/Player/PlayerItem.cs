using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Effect activeEffect;
    [SerializeField] private float effectDuration;

    private float elapsed = 0f;

    public string GetItemName()
    {
        return activeEffect.ToString();
    }

    public string GetTimeLeft()
    {
        return elapsed.ToString("0");
    }

    public void CollectItem(Effect itemEffect, float duration)
    {
        activeEffect = itemEffect;
        effectDuration = duration;
    }

    private void Update()
    {
        if (activeEffect != Effect.None && elapsed <= effectDuration) { 
            elapsed += Time.deltaTime;
            if (elapsed >= 1f) {
                elapsed %= 1f;
                ApplyEffect();
            }
        } else {
            StopCurrentEffect();
        }
    }

    private void ApplyEffect()
    {
        switch (activeEffect) {
            case Effect.JumpBoost: gameObject.GetComponent<PlayerMovement>().ApplyJumpBoost(); break;
            case Effect.SpeedBoost: gameObject.GetComponent<PlayerMovement>().ApplySpeedBoost(); break;
            case Effect.FeatherFalling: gameObject.GetComponent<PlayerMovement>().ApplyFeatherFalling(); break;
            case Effect.HealthRegen: gameObject.GetComponent<PlayerHealth>().ApplyHealthRegen(); break;
        }
    }

    private void StopCurrentEffect()
    {
        elapsed = 0f;
        effectDuration = 0f;
        activeEffect = Effect.None;
        gameObject.GetComponent<PlayerMovement>().ResetToBase();
    }
}
