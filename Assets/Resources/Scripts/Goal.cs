using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Goal : MonoBehaviour
{
    [SerializeField] InputActionReference clickAction;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            Debug.Log("Level Complete!");
            player.DisableInput();
            clickAction.action.Enable();
            clickAction.action.performed += BackToMenu;
        
            AudioManager.Instance.PlaySound(Sounds.GoalReached);
        }
    }
    void BackToMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Back to Menu");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            clickAction.action.performed -= BackToMenu;
            clickAction.action.Disable();
        }
    }
}
