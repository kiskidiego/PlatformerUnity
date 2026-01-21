using System;
using UnityEngine;

public class HealthBar : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 100f;
    public Action onHitCallback;
    public Action onDamageCallback;
    float currentHealth;
    public bool canTakeDamage = true;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        onHitCallback?.Invoke();

        if (!canTakeDamage) return;

        onDamageCallback?.Invoke();
        
        currentHealth -= damage;
        Debug.Log($"Character took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Character has died.");
    }
}