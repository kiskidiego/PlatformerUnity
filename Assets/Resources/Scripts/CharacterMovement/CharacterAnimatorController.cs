using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WallCling))]
[RequireComponent(typeof(CharacterDirection))]
public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speedMultiplier = 1f;

    SpriteRenderer spriteRenderer;
    GroundChecker groundChecker;
    WallCling wallCling;
    CharacterDirection characterDirection;
    Rigidbody2D rigidBody;

    [SerializeField] const string SPEED_PARAM = "HorizontalSpeed";
    [SerializeField] const string VERTICAL_VELOCITY_PARAM = "VerticalSpeed";
    [SerializeField] const string JUMP_TRIGGER = "Jump";
    [SerializeField] const string DASH_TRIGGER = "Dash";
    [SerializeField] const string ATTACK_TRIGGER = "Attack";
    [SerializeField] const string PARRY_TRIGGER = "Parry";
    [SerializeField] const string PARRY_HIT_TRIGGER = "ParryHit";
    [SerializeField] const string IS_GROUNDED_PARAM = "IsGrounded";

    void Awake()
    {
        groundChecker = GetComponent<GroundChecker>();
        wallCling = GetComponent<WallCling>();
        rigidBody = GetComponent<Rigidbody2D>();
        characterDirection = GetComponent<CharacterDirection>();
        spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
    }
    public void TriggerJump()
    {
        animator.SetTrigger(JUMP_TRIGGER);
    }
    public void TriggerDash()
    {
        animator.SetTrigger(DASH_TRIGGER);
    }
    public void TriggerAttack()
    {
        animator.SetTrigger(ATTACK_TRIGGER);
    }
    public void TriggerParry()
    {
        animator.SetTrigger(PARRY_TRIGGER);
    }
    public void TriggerParryHit()
    {
        animator.SetTrigger(PARRY_HIT_TRIGGER);
    }
    void Update()
    {
        if(characterDirection.Facing < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if(characterDirection.Facing > 0f)
        {
            spriteRenderer.flipX = false;
        }
        animator.SetBool(IS_GROUNDED_PARAM, groundChecker.IsGrounded());
        animator.SetFloat(SPEED_PARAM, Mathf.Abs(rigidBody.linearVelocityX) * speedMultiplier);
        animator.SetFloat(VERTICAL_VELOCITY_PARAM, rigidBody.linearVelocityY);
    }
}