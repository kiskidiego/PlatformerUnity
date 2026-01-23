using UnityEngine;

public class ExplosionDeleter : MonoBehaviour
{
    public bool delete = false;

    void Update()
    {
        if (delete)
        {
            Destroy(gameObject);
        }
    }
}
