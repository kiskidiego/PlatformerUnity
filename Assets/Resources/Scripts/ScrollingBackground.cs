using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] Vector2 scrollSpeed = new Vector2(1f, 1f);

    Material backgroundMaterial;

    void Awake()
    {
        backgroundMaterial = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        Vector2 offset = new Vector2(
            transform.position.x * scrollSpeed.x,
            transform.position.y * scrollSpeed.y
        );

        offset.x = Mathf.Repeat(offset.x, 1f);
        offset.y = Mathf.Repeat(offset.y, 1f);

        backgroundMaterial.SetVector("_Offset", offset);
    }
}
