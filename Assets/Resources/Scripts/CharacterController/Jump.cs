using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Jump
{
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float maxJumpHoldTime = 0.35f;

    public Vector2 JumpDirection
    {
        get { return jumpDirection; }
        set { jumpDirection = value.normalized; }
    }
    Vector2 jumpDirection = Vector2.up;
    bool active;
    float jumpHoldTime;

    public void StartJump()
    {
        active = true;
    }

    public void StopJump(Rigidbody2D rigidBody)
    {
        if (!active) return;
    
        rigidBody.linearVelocityY *= 0.5f;
        active = false;
        jumpHoldTime = 0f;
    }

    public void JumpUpdate(Rigidbody2D rigidBody)
    {
        if (active)
        {
            float timeMultiplier = 1f;
            if (maxJumpHoldTime > 0f)
            {
                timeMultiplier = 1f - (jumpHoldTime / maxJumpHoldTime);
            }

            float opposingSpeed = Vector2.Dot(rigidBody.linearVelocity, -jumpDirection);

            if (opposingSpeed > 0f)
            {
                rigidBody.linearVelocity -= opposingSpeed * (-jumpDirection);
            }

            rigidBody.linearVelocity += jumpDirection * jumpForce * timeMultiplier * Time.fixedDeltaTime;

            jumpHoldTime += Time.fixedDeltaTime;
            if (jumpHoldTime >= maxJumpHoldTime)
            {
                StopJump(rigidBody);
            }
        }
    }
}