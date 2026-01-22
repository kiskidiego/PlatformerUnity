using UnityEngine;

public class DoubleJumpPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterJumpHandler jumpHandler))
        {
            jumpHandler.canAirJump = true;
            Destroy(gameObject);
        }
    }
}
