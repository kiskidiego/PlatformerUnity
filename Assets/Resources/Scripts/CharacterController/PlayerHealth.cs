using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Cooldown invulnerabilityCooldown = new Cooldown(1.5f);
    [SerializeField] Cooldown flashCooldown = new Cooldown(0.1f);
    public Action onHitCallback;
    public Action onDamageCallback;
    public Action onHealCallback;
    SpriteRenderer spriteRenderer;
    float currentHealth;
    public bool canTakeDamage = true;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    bool inInvulnerability = false;

    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        onHealCallback?.Invoke();
        Debug.Log($"Character healed by {amount}, current health: {currentHealth}");
    }

    public void TakeDamage(float damage)
    {
        onHitCallback?.Invoke();

        Debug.Log(canTakeDamage);

        if (!canTakeDamage) return;


        canTakeDamage = false;
        invulnerabilityCooldown.StartCooldown();
        inInvulnerability = true;
        
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
        if(onHitCallback != null)
        {
            Debug.Log(canTakeDamage);
        }
        if (!inInvulnerability) return;

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
            inInvulnerability = false;
        }
    }

    void Die()
    {
        Debug.Log("Character has died.");
    }
}