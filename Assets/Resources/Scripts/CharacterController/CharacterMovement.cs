using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float deceleration = 15f;
    [SerializeField] float airControlFactor = 0.5f;

    [HideInInspector] public float movementInput;

    Rigidbody2D rigidBody;
    GroundChecker groundChecker;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();
    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
    }

    void HandleHorizontalMovement()
    {
        float velocity = rigidBody.linearVelocityX;
        float targetSpeed = movementInput * maxSpeed;
        float accelerationToUse = acceleration;
        if (velocity * targetSpeed <= 0)
        {
            accelerationToUse = deceleration;
        }
        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            accelerationToUse += acceleration;
        }
        if (!groundChecker.IsGrounded())
        {
            accelerationToUse *= airControlFactor;
        }
        velocity = Mathf.MoveTowards(velocity, targetSpeed, accelerationToUse * Time.fixedDeltaTime);
        rigidBody.linearVelocityX = velocity;
    }
}
