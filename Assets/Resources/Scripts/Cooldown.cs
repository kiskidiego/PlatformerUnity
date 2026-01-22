using System;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] float cooldownTime = 1f;
    float currentCooldown = 0f;
    Action callbackOnCooldownEnd;

    public Cooldown(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
        currentCooldown = 0f;
    }

    public bool IsOffCooldown()
    {
        return currentCooldown <= 0f;
    }

    public void StartCooldown(Action callback = null)
    {
        currentCooldown = cooldownTime;
        callbackOnCooldownEnd = callback;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= deltaTime;
            if (currentCooldown <= 0f)
            {
                currentCooldown = 0f;
                callbackOnCooldownEnd?.Invoke();
                callbackOnCooldownEnd = null;
            }
        }
    }
}