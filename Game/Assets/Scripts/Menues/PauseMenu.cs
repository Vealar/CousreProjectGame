using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseGameMenu;
    private bool isPaused = false;
    public PlayerInput playerInput;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            (isPaused ? (Action)Resume : Pause)();
        }
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
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