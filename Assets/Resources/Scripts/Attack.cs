using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Attack : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float knockbackForce = 5f;

    void Awake()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            if (collision.TryGetComponent(out Rigidbody2D rigidBody))
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                rigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
