using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] Cooldown cooldown = new Cooldown(1f);
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        cooldown.UpdateCooldown(Time.deltaTime);
        if (cooldown.IsOffCooldown())
        {
            cooldown.StartCooldown();
            animator.SetTrigger("Activate");
        }
    }
}
