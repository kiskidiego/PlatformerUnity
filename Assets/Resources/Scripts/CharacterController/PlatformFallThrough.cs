using UnityEngine;


public class PlatformFallThrough : MonoBehaviour
{
    [SerializeField] string platformLayerName = "Platforms";
    GroundChecker groundChecker;
    bool isFallingThrough = false;
    int platformLayer;

    void Awake()
    {
        platformLayer = LayerMask.NameToLayer(platformLayerName);
        groundChecker = GetComponent<GroundChecker>();
    }

    public void EnableFallThrough()
    {
        if (isFallingThrough) return;

        isFallingThrough = true;
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, true);
        Physics2D.SyncTransforms();
        if (groundChecker)
        {
            groundChecker.ExcludeLayer(platformLayer);
        }
    }

    public void DisableFallThrough()
    {
        if (!isFallingThrough) return;

        isFallingThrough = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, false);
        Physics2D.SyncTransforms();
        if (groundChecker)
        {
            groundChecker.IncludeLayer(platformLayer);
        }
    }
}