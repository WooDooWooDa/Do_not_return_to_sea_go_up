using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color lowHealthColor = Color.red;

    private bool alive = true;
    [SyncVar(hook = "OnHealthChange")]
    private float currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    private void OnEnable()
    {
        alive = true;
    }

    [Server]
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    [Server]
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0 && alive)
        {
            RpcOnDeath();
        }
    }

    public void ApplyHealthRegen()
    {
        Heal(20);
    }

    public void OnHealthChange(float oldValue, float newValue)
    {
        SetSliderHealth();
    }

    private void SetSliderHealth()
    {
        healthSlider.value = currentHealth;
        fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, currentHealth / startingHealth);
    }

    [ClientRpc]
    private void RpcOnDeath()
    {
        alive = false;
        gameObject.SetActive(false);
    }
}
