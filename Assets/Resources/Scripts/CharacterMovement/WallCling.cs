using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(CharacterDirection))]
public class WallCling : MonoBehaviour
{
    [SerializeField] float gravityMultiplier = 0.5f;
    [SerializeField] float wallCheckDistance = 0.5f;
    [SerializeField] float maxWallAngle = 15f;
    [SerializeField] LayerMask wallLayerMask;
    FastFall fastFall;
    GroundChecker groundChecker;
    CharacterDirection characterDirection;
    Rigidbody2D rigidBody;
    public bool IsClinging => isClinging;
    bool isClinging = false;
    float minWallDotProduct;
    float baseGravityScale = 1f;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        fastFall = GetComponent<FastFall>();
        groundChecker = GetComponent<GroundChecker>();
        characterDirection = GetComponent<CharacterDirection>();
        minWallDotProduct = Mathf.Cos(maxWallAngle * Mathf.Deg2Rad);
        baseGravityScale = rigidBody.gravityScale;
    }
    
    void FixedUpdate()
    {
        CheckForWall();
    }

    void CheckForWall()
    {
        if (rigidBody.linearVelocityY > 0f || groundChecker.IsGrounded())
        {
            if (isClinging)
            {
                StopCling();
            }
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, characterDirection.Direction, wallCheckDistance, wallLayerMask);
        Debug.DrawRay(transform.position, characterDirection.Direction * wallCheckDistance, Color.red);
        if(hit.collider != null && Vector2.Dot(hit.normal, -characterDirection.Direction) >= minWallDotProduct)
        {
            if (!isClinging)
            {
                Debug.Log("Start Cling");
                StartCling();
            }
        }
        else
        {
            if (isClinging)
            {
                StopCling();
            }
        }
    }

    void StartCling()
    {
        if (isClinging) return;

        if (fastFall != null)
        {
            fastFall.enabled = false;
        }
        characterDirection.Flipped = true;
        isClinging = true;
        rigidBody.gravityScale *= gravityMultiplier;
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0f);
    }

    void StopCling()
    {
        if (!isClinging) return;

        if (fastFall != null)
        {
            fastFall.enabled = true;
        }
        characterDirection.Flipped = false;
        isClinging = false;
        rigidBody.gravityScale = baseGravityScale;
    }
}