using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterDirection))]
[RequireComponent(typeof(CharacterAttack))]
[RequireComponent(typeof(EnemyAnimatorController))]
public class EnemyPatrolController : MonoBehaviour
{
    [SerializeField] float patrolDirection = 1f;
    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float chaseSpeed = 4f;
    [SerializeField] float detectionRange = 4f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float obstacleCheckDistance = 0.5f;
    [SerializeField] LayerMask obstacleLayerMask;
    [SerializeField] LayerMask playerLayerMask;

    CharacterMovement characterMovement;
    CharacterDirection characterDirection;
    CharacterAttack characterAttack;
    EnemyAnimatorController enemyAnimator;
    Transform playerTransform;

    void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterDirection = GetComponent<CharacterDirection>();
        characterAttack = GetComponent<CharacterAttack>();
        enemyAnimator = GetComponent<EnemyAnimatorController>();
        
        characterMovement.maxSpeed = patrolSpeed;
        characterDirection.Direction = new Vector2(patrolDirection, 0f);
    }

    void Update()
    {
        if (playerTransform == null)
        {
            Patrol();
        }
        else
        {
            Chase();
        }
    }
    
    void Patrol()
    {
        characterMovement.movementInput = characterDirection.Facing;

        RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.right * characterDirection.Facing * detectionRange, Vector2.right * characterDirection.Facing, detectionRange * 2f, playerLayerMask);
        Debug.DrawRay(transform.position - Vector3.right * characterDirection.Facing * detectionRange, Vector2.right * characterDirection.Facing * detectionRange * 2f, Color.green);
        if (hit.collider != null )
        {
            Debug.Log("Player detected by enemy patrol");
            playerTransform = hit.collider.transform;
            characterMovement.maxSpeed = chaseSpeed;
        }

        hit = Physics2D.Raycast(transform.position, Vector3.right * characterDirection.Facing, obstacleCheckDistance, obstacleLayerMask);
        if (hit.collider != null)
        {
            characterDirection.Direction = new Vector2(-characterDirection.Facing, 0f);
            characterMovement.movementInput = characterDirection.Facing;
            return;
        }

        hit = Physics2D.Raycast(transform.position + Vector3.right * characterDirection.Facing * obstacleCheckDistance, Vector2.down, 1f, obstacleLayerMask);
        if (hit.collider == null)
        {
            characterDirection.Direction = new Vector2(-characterDirection.Facing, 0f);
            characterMovement.movementInput = characterDirection.Facing;
            return;
        }
    }

    void Chase()
    {
        if (playerTransform == null)
        {
            characterMovement.maxSpeed = patrolSpeed;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > detectionRange)
        {
            playerTransform = null;
            characterMovement.maxSpeed = patrolSpeed;
            return;
        }

        characterDirection.Direction = (playerTransform.position - transform.position).normalized;

        if (distanceToPlayer <= attackRange)
        {
            characterMovement.movementInput = 0f;
            if(characterAttack.Attack(SetCanMoveTrue))
            {
                enemyAnimator.TriggerAttack();
                SetCanMoveFalse();
            }
        }
        else
        {
            characterMovement.movementInput = characterDirection.Facing;
        }
    }
    void SetCanMoveTrue()
    {
        characterMovement.canMove = true;
    }
    void SetCanMoveFalse()
    {
        characterMovement.canMove = false;
    }
}
