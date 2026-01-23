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

    GroundChecker groundChecker;
    Rigidbody2D rigidBody;

    [SerializeField] const string SPEED_PARAM = "HorizontalSpeed";
    [SerializeField] const string VERTICAL_VELOCITY_PARAM = "VerticalSpeed";
    [SerializeField] const string JUMP_TRIGGER = "Jump";
    [SerializeField] const string DASH_TRIGGER = "Dash";
    [SerializeField] const string ATTACK_TRIGGER = "Attack";
    [SerializeField] const string PARRY_TRIGGER = "Parry";
    [SerializeField] const string PARRY_HIT_TRIGGER = "ParryHit";
    [SerializeField] const string IS_GROUNDED_PARAM = "IsGrounded";
    [SerializeField] const string TAKE_DAMAGE_TRIGGER = "TakeDamage";

    void Awake()
    {
        groundChecker = GetComponent<GroundChecker>();
        rigidBody = GetComponent<Rigidbody2D>();
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
    public void TriggerTakeDamage()
    {
        animator.SetTrigger(TAKE_DAMAGE_TRIGGER);
    }
    void Update()
    {
        animator.SetBool(IS_GROUNDED_PARAM, groundChecker.IsGrounded());
        animator.SetFloat(SPEED_PARAM, Mathf.Abs(rigidBody.linearVelocityX) * speedMultiplier);
        animator.SetFloat(VERTICAL_VELOCITY_PARAM, rigidBody.linearVelocityY);
    }
}