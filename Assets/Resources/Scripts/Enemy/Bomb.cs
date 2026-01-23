using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosionEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        AudioManager.Instance.PlaySound(Sounds.Explosion);
        Destroy(gameObject);
    }
}
