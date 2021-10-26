using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Effect activeEffect;
    [SerializeField] private float effectDuration;

    private GameObject playerItemUI;

    private float timeLeft = 0f;
    private bool effectIsActive = false;

    public void SetUI(GameObject playerItem)
    {
        playerItemUI = playerItem;
    }

    public string GetItemName()
    {
        return string.Join(" ", Regex.Split(activeEffect.ToString(), @"(?<!^)(?=[A-Z])"));
    }

    public string GetTimeLeft()
    {
        return timeLeft.ToString("0");
    }

    public bool HasActiveEffect()
    {
        return effectIsActive;
    }

    public void CollectItem(Effect itemEffect, float duration)
    {
        if (effectIsActive) return;

        activeEffect = itemEffect;
        effectDuration = duration;
    }

    private void Update()
    {
        if (activeEffect != Effect.None && !effectIsActive) {
            timeLeft = effectDuration;
            effectIsActive = true;
            playerItemUI.SetActive(true);
            InvokeRepeating(nameof(ApplyEffect), 0, 1);
        } else if (timeLeft <= 0 && effectIsActive) {
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
        timeLeft--;
    }

    private void StopCurrentEffect()
    {
        CancelInvoke();
        timeLeft = 0f;
        effectIsActive = false;
        playerItemUI.SetActive(false);
        effectDuration = 0f;
        activeEffect = Effect.None;
        gameObject.GetComponent<PlayerMovement>().ResetToBase();
    }
}
