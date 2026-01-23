using Unity.Mathematics;
using UnityEngine;

public class EnemyProjectileSpawn : MonoBehaviour
{
    [SerializeField] Vector2 projectileVelocity;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform shootPoint;

    bool hasShooted = false;
    public bool shoot;
    void Update()
    {
        if(!shoot && hasShooted)
        {
            hasShooted = false;
        }
        if (shoot && !hasShooted)
        {
            GameObject bomb = Instantiate(projectilePrefab, shootPoint.position, quaternion.identity);
            bomb.GetComponentInChildren<SpriteRenderer>().flipX = shootPoint.localScale.x < 0;
            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(projectileVelocity.x * shootPoint.localScale.x, projectileVelocity.y);
            shoot = false;
            hasShooted = true;
        }
    }
}
