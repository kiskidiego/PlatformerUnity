using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int pixels = 80;
    Image image;
    PlayerHealth playerHealth;
    float fillStep;
    void Awake()
    {
        image = GetComponent<Image>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.onDamageCallback += UpdateHealthBar;
        playerHealth.onHealCallback += UpdateHealthBar;
        fillStep = 1f / pixels;
    }

    void UpdateHealthBar()
    {
        float healthPercentage = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        int filledPixels = Mathf.CeilToInt(healthPercentage * pixels);
        image.fillAmount = filledPixels * fillStep;
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.onDamageCallback -= UpdateHealthBar;
        }
    }
}
