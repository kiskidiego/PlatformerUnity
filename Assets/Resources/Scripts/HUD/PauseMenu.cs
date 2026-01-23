using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public InputActionReference pauseAction;
    void Awake()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnTogglePause;
        gameObject.SetActive(false); // Ensure the pause menu is hidden at start
    }
    void OnDestroy()
    {
        pauseAction.action.performed -= OnTogglePause;
        pauseAction.action.Disable();
    }
    void OnEnable()
    {
        Time.timeScale = 0f; // Pause the game
    }
    void OnDisable()
    {
        Time.timeScale = 1f; // Resume the game
    }
    public void OnTogglePause(InputAction.CallbackContext context)
    {
        gameObject.SetActive(!gameObject.activeSelf); // Toggle pause menu off
    }
    public void OnResumeButtonPressed()
    {
        gameObject.SetActive(false); // Resume the game by hiding the pause menu
    }
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
