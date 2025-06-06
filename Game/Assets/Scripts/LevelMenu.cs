using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttonsSweet;
    public Button[] buttonsNightmare;
    private void Awake()
    {
        int unlockedLevelSweet = SaveManager.GetUnlockedLevelSweet();
        int unlockedLevelNightmare = SaveManager.GetUnlockedLevelNightmare();


        for (int i = 0; i < buttonsSweet.Length; i++)
        {
            buttonsSweet[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevelSweet; i++)
        {
            buttonsSweet[i].interactable = true;
        }
        for(int i = 0; i < buttonsNightmare.Length; i++)
        {
            buttonsNightmare[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevelNightmare; i++)
        {
            buttonsNightmare[i].interactable = true;
        }
    }
    public void OpenLevel(int levelIndex)
    {
        string levelName = "Level " + levelIndex;
        SceneManager.LoadScene(levelName);
    } 
    public void CloseLevelMenu()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLevelMenu();
        }
    }
}