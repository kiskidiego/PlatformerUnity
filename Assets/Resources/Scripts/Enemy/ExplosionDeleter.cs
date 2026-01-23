using UnityEngine;

public class ExplosionDeleter : MonoBehaviour
{
    public bool delete = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController _))
        {
            gameObject.layer = LayerMask.NameToLayer("NoCollision");
        }
    }

    void Update()
    {
        if (delete)
        {
            Destroy(gameObject);
        }
    }
}
