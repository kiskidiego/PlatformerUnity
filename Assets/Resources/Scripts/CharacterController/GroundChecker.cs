using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] Transform[] groundCheckPoint;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] float maxGroundAngle = 45f;
    [SerializeField] LayerMask groundLayer;

    float minGroundDotProduct;
    bool cachedResult;
    bool useCache;

    void Awake()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }

    void FixedUpdate()
    {
        useCache = false;
    }

    public bool IsGrounded()
    {
        if (useCache)
        {
            return cachedResult;
        }
        foreach (Transform point in groundCheckPoint)
        {
            RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.down, groundCheckDistance, groundLayer);
            if (hit.collider != null)
            {
                float dot = Vector2.Dot(hit.normal, Vector2.up);
                if (dot >= minGroundDotProduct)
                {
                    cachedResult = true;
                    useCache = true;
                    return true;
                }
            }
        }
        cachedResult = false;
        useCache = true;
        return false;
    }
    public void ExcludeLayer(int layer)
    {
        groundLayer &= ~(1 << layer);
    }
    public void IncludeLayer(int layer)
    {
        groundLayer |= (1 << layer);
    }
}