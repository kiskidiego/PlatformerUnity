using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class CharacterJumpHandler : MonoBehaviour
{
    [SerializeField] Jump groundJump = new Jump();
    [SerializeField] Jump[] airJumps = new Jump[0];

    Rigidbody2D rigidBody;
    GroundChecker groundChecker;
    Jump activeJump;
    int currentJumpIndex = 0;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponent<GroundChecker>();
    }
    public bool StartJump()
    {
        if (groundChecker.IsGrounded())
        {
            StartGroundedJump();
            return true;
        }
        else
        {
            return StartAirJump();
        }
    }
    void StartGroundedJump()
    {
        activeJump = groundJump;
        activeJump.StartJump();
        ResetJumps();
    }
    bool StartAirJump()
    {
        if (currentJumpIndex >= airJumps.Length)
        {
            return false;
        }
        activeJump = airJumps[currentJumpIndex];
        activeJump.StartJump();
        currentJumpIndex++;
        return true;
    }
    public void StopJump()
    {
        if (activeJump != null)
        {
            activeJump.StopJump(rigidBody);
        }
    }
    public void ResetJumps()
    {
        currentJumpIndex = 0;
    }
    void FixedUpdate()
    {
        if (activeJump != null)
        {
            activeJump.JumpUpdate(rigidBody);
        }
    }
}