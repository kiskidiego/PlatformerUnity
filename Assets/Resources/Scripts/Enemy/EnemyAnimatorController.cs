using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimatorController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;

    [SerializeField] const string SPEED_PARAM = "Speed";
    [SerializeField] const string ATTACK_TRIGGER = "Attack";

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(ATTACK_TRIGGER);
    }
    void Update()
    {
        float speed = Mathf.Abs(rigidBody.linearVelocityX);
        animator.SetFloat(SPEED_PARAM, speed);
    }
}
