using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WallCling))]
[RequireComponent(typeof(CharacterDirection))]
public class WallJump : MonoBehaviour
{
    [SerializeField] public bool canWallJump = true;
    [SerializeField] Jump wallJump = new Jump();
    [SerializeField] float jumpAngle = 45f;
    Rigidbody2D rigidBody;
    WallCling wallCling;
    CharacterDirection characterDirection;
    Vector2 wallJumpDirection;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        wallCling = GetComponent<WallCling>();
        characterDirection = GetComponent<CharacterDirection>();
        wallJumpDirection = new Vector2(Mathf.Cos(jumpAngle * Mathf.Deg2Rad), Mathf.Sin(jumpAngle * Mathf.Deg2Rad)).normalized;
    }

    public bool StartWallJump()
    {
        if (!canWallJump)
        {
            return false;
        }
        if (wallCling.IsClinging)
        {
            Vector2 jumpDir = new Vector2(wallJumpDirection.x * characterDirection.Facing, wallJumpDirection.y);
            wallJump.JumpDirection = jumpDir;
            wallJump.StartJump();
            return true;
        }
        return false;
    }
    public void StopWallJump()
    {
        wallJump.StopJump(rigidBody);
    }
    void FixedUpdate()
    {
        wallJump.JumpUpdate(rigidBody);
    }
}