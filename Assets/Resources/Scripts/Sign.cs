using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Sign : MonoBehaviour
{
    TextMeshPro textMesh;

    void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        textMesh.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            ShowSignText();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            HideSignText();
        }
    }
    void ShowSignText()
    {
        textMesh.gameObject.SetActive(true);
    }
    void HideSignText()
    {
        textMesh.gameObject.SetActive(false);
    }
}
