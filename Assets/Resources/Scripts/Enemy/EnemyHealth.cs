using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 30;
    [SerializeField] Cooldown blink = new Cooldown(0.1f);
    float currentHealth;
    bool canTakeDamage = true;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
    void Update()
    {
        blink.UpdateCooldown(Time.deltaTime);
        if (blink.IsOffCooldown())
        {
            spriteRenderer.enabled = true;
        }
    }
    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) return;

        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage, current health: {currentHealth}");

        spriteRenderer.enabled = false;
        blink.StartCooldown();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}