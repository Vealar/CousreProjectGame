using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public PlayerInput playerInput;

    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevelMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }
}