using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Cooldown invulnerabilityCooldown = new Cooldown(1.5f);
    [SerializeField] Cooldown flashCooldown = new Cooldown(0.1f);
    public Action onHitCallback;
    public Action onDamageCallback;
    SpriteRenderer spriteRenderer;
    float currentHealth;
    public bool canTakeDamage = true;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void TakeDamage(float damage)
    {
        onHitCallback?.Invoke();

        if (!canTakeDamage) return;


        canTakeDamage = false;
        invulnerabilityCooldown.StartCooldown();
        
        currentHealth -= damage;
        Debug.Log($"Character took {damage} damage, current health: {currentHealth}");

        onDamageCallback?.Invoke();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Update()
    {
        if (canTakeDamage) return;

        flashCooldown.UpdateCooldown(Time.deltaTime);
        invulnerabilityCooldown.UpdateCooldown(Time.deltaTime);
        if (flashCooldown.IsOffCooldown())
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            flashCooldown.StartCooldown();
        }
        if (invulnerabilityCooldown.IsOffCooldown())
        {
            spriteRenderer.enabled = true;
            canTakeDamage = true;
        }
    }

    void Die()
    {
        Debug.Log("Character has died.");
    }
}