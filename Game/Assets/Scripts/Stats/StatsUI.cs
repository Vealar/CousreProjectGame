using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatsUI : MonoBehaviour
{

    public TMP_Text levelsText;
    public TMP_Text timeText;
    public TMP_Text deathsText;

    void OnEnable()
    {
        levelsText.text = $"Levels Completed: {GameStatsManager.GetLevelsCompleted()}";
        
        float seconds = GameStatsManager.GetTotalPlayTime();
        System.TimeSpan t = System.TimeSpan.FromSeconds(seconds);
        timeText.text = $"Total Play Time: {t:hh\\:mm\\:ss}";

        deathsText.text = $"Total Deaths: {GameStatsManager.GetTotalDeaths()}";
    }
    public void CloseStats()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseStats();
        }
    }
}