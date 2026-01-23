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
            player.DisableInput();
            clickAction.action.performed += BackToMenu;
            clickAction.action.Enable();
        }
    }
    void BackToMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            clickAction.action.performed -= BackToMenu;
            clickAction.action.Disable();
        }
    }
}
