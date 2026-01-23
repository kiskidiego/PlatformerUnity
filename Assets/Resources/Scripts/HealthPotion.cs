using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthPotion : MonoBehaviour
{
    [SerializeField] float healAmount = 20f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
