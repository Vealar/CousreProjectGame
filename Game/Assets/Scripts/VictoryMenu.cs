using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VictoryMenu : MonoBehaviour
{
    public GameObject victoryCanvas;
    public PlayerInput playerInput;

    public void ShowVictory()
    {
        victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
            
        string currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt("achieved_" + currentLevel, 1);
        GameStatsManager.AddLevelCompleted();
        SaveProgress();
        
    }

    private void SaveProgress()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.StartsWith("Level "))
        {
            string numberPart = sceneName.Substring("Level ".Length);

            if (int.TryParse(numberPart, out int levelCode))
            {
                int levelNumber = levelCode / 10;
                int mode = levelCode % 10;

                if (mode == 1)
                {
                    int current = SaveManager.GetUnlockedLevelSweet();
                    if (levelNumber + 1 > current)
                        SaveManager.SetUnlockedLevelSweet(levelNumber + 1);
                }
                else
                {
                    int current = SaveManager.GetUnlockedLevelNightmare();
                    if (levelNumber + 1 > current)
                        SaveManager.SetUnlockedLevelNightmare(levelNumber + 1);
                }

                PlayerPrefs.Save();
            }
        }
    }


    public void LoadLevelMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }
}