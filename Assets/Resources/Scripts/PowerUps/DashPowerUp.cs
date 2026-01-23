using UnityEngine;

public class DashPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterDash dashHandler))
        {
            dashHandler.canDash = true;
            Destroy(gameObject);
        }
    }
}
