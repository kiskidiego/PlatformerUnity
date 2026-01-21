using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FastFall : MonoBehaviour
{
    [SerializeField] float fallMultiplier = 2.5f;
    Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float fallingSpeed = Vector2.Dot(rigidBody.linearVelocity, Physics2D.gravity.normalized);

        if (fallingSpeed > 0f)
        {
            rigidBody.linearVelocity += (fallMultiplier - 1f) * Time.fixedDeltaTime * Physics2D.gravity;
        }
    }

}