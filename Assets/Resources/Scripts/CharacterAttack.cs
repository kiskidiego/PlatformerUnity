using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Cooldown attackCooldown = new Cooldown(0.5f);
    [SerializeField] GameObject attackObject;
    Rigidbody2D rigidBody;
    GroundChecker groundChecker;
    Vector2 attackPositionOffset = new Vector2(1f, 0f);

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();
        attackPositionOffset = attackObject.transform.localPosition;
    }

    public void SetDirectionInput(Vector2 input)
    {
        if (!Mathf.Approximately(input.x, 0f))
        {
            attackObject.transform.localPosition = new Vector2
            (
                attackPositionOffset.x * Mathf.Sign(input.x), 
                attackPositionOffset.y
            );
            if(input.x > 0f)
            {
                attackObject.transform.localScale = Vector3.one;
            }
            else
            {
                attackObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }

    public bool Attack(Action callback)
    {
        if (attackCooldown.IsOffCooldown())
        {
            if(groundChecker.IsGrounded())
            {
                rigidBody.linearVelocity = new Vector2(0f, 0f);
            }
            attackCooldown.StartCooldown(callback);
            return true;
        }
        return false;
    }
    void Update()
    {
        attackCooldown.UpdateCooldown(Time.deltaTime);
    }
}