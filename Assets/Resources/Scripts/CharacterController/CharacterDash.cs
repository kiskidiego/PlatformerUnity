using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterDirection))]
public class CharacterDash : MonoBehaviour
{
    public bool canDash = false;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] Cooldown dashCooldown = new Cooldown(1f);

    Rigidbody2D rigidBody;
    CharacterDirection characterDirection;
    
    Vector2 dashDirection;
    bool isDashing = false;
    float dashTimeRemaining = 0f;
    public Action onDashEnd;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        characterDirection = GetComponent<CharacterDirection>();
    }

    void Update()
    {
        dashCooldown.UpdateCooldown(Time.deltaTime);
    }

    public bool StartDash()
    {
        if (!canDash)
        {
            return false;
        }

        if (dashCooldown.IsOffCooldown() && !isDashing)
        {
            isDashing = true;
            dashTimeRemaining = dashDuration;
            dashCooldown.StartCooldown();
            if(characterDirection.Direction != Vector2.zero)
            {
                dashDirection = characterDirection.Direction;
            }
            else
            {
                dashDirection = new Vector2(characterDirection.Facing, 0f);
            }
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if(!isDashing)
        {
            return;
        }
        if (dashTimeRemaining > 0f)
        {
            dashTimeRemaining -= Time.fixedDeltaTime;
            rigidBody.linearVelocity = dashDirection * dashSpeed;
            if (dashTimeRemaining <= 0f)
            {
                Debug.Log("Dash ended");
                isDashing = false;
                rigidBody.linearVelocity = Vector2.zero;
                onDashEnd?.Invoke();
            }
        }
    }
}