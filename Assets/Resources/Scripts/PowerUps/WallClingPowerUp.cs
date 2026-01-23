using UnityEngine;

public class WallClingPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WallCling wallCling))
        {
            AudioManager.Instance.PlaySound(Sounds.PowerUp);
            wallCling.canWallCling = true;
            Destroy(gameObject);
        }
    }
}
