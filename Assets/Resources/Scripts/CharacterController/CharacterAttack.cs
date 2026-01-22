using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(CharacterDirection))]
public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Cooldown attackCooldown = new Cooldown(0.5f);
    [SerializeField] GameObject attackObject;
    Rigidbody2D rigidBody;
    GroundChecker groundChecker;
    CharacterDirection characterDirection;
    Vector2 attackPositionOffset = new Vector2(1f, 0f);

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();
        characterDirection = GetComponent<CharacterDirection>();
        attackPositionOffset = attackObject.transform.localPosition;
    }

    public bool Attack(Action callback)
    {
        if (attackCooldown.IsOffCooldown())
        {
            if(groundChecker.IsGrounded())
            {
                rigidBody.linearVelocity = new Vector2(0f, 0f);
            }

            if(characterDirection.Facing > 0f)
            {
                attackObject.transform.localScale = Vector3.one;
            }
            else
            {
                attackObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            attackObject.transform.localPosition = new Vector2
            (
                attackPositionOffset.x * characterDirection.Facing, 
                attackPositionOffset.y
            );

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