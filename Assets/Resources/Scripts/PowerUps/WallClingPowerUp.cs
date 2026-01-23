using UnityEngine;

public class WallClingPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WallCling wallCling))
        {
            wallCling.canWallCling = true;
            Destroy(gameObject);
        }
    }
}
