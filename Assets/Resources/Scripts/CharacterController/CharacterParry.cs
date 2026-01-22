using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class CharacterParry : MonoBehaviour
{
    [SerializeField] float parryDuration = 0.5f;
    [SerializeField] Cooldown parryCooldown = new Cooldown(2f);

    PlayerHealth healthBar;

    bool isParrying = false;
    float parryTimeRemaining = 0f;

    Action onEndParry;
    Action onParryHit;

    void Awake()
    {
        healthBar = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (isParrying)
        {
            parryTimeRemaining -= Time.deltaTime;
            if (parryTimeRemaining <= 0f)
            {
                EndParry();
            }
        }
        parryCooldown.UpdateCooldown(Time.deltaTime);
    }

    public bool StartParry(Action onEndParryCallback, Action onParryHitCallback)
    {
        if (parryCooldown.IsOffCooldown() && !isParrying)
        {
            onEndParry = onEndParryCallback;
            onParryHit = onParryHitCallback;
            healthBar.onHitCallback += OnParryHit;
            healthBar.canTakeDamage = false;
            isParrying = true;
            parryTimeRemaining = parryDuration;
            return true;
        }
        return false;
    }

    void EndParry()
    {
        if (!isParrying)
        {
            return;
        }
        onEndParry?.Invoke();
        healthBar.canTakeDamage = true;
        healthBar.onHitCallback -= OnParryHit;
        isParrying = false;
        parryCooldown.StartCooldown();
    }

    void OnDestroy()
    {
        if (isParrying)
        {
            healthBar.onHitCallback -= OnParryHit;
        }
    }

    void OnParryHit()
    {
        Debug.Log("Parry successful!");
        onParryHit?.Invoke();
        EndParry();
    }
}