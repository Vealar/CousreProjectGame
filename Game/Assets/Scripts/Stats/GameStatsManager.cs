using UnityEngine;

public static class GameStatsManager
{
    const string TimeKey = "TotalPlayTime";
    const string DeathsKey = "TotalDeaths";
    const string LevelsKey = "LevelsCompleted";

    public static void AddPlayTime(float seconds)
    {
        float current = PlayerPrefs.GetFloat(TimeKey, 0f);
        PlayerPrefs.SetFloat(TimeKey, current + seconds);
    }

    public static void AddDeath()
    {
        int deaths = PlayerPrefs.GetInt(DeathsKey, 0);
        PlayerPrefs.SetInt(DeathsKey, deaths + 1);
    }

    public static void AddLevelCompleted()
    {
        int levels = PlayerPrefs.GetInt(LevelsKey, 0);
        PlayerPrefs.SetInt(LevelsKey, levels + 1);
    }

    public static float GetTotalPlayTime() => PlayerPrefs.GetFloat(TimeKey, 0f);
    public static int GetTotalDeaths() => PlayerPrefs.GetInt(DeathsKey, 0);
    public static int GetLevelsCompleted() => PlayerPrefs.GetInt(LevelsKey, 0);

    public static void ResetStats()
    {
        PlayerPrefs.DeleteKey(TimeKey);
        PlayerPrefs.DeleteKey(DeathsKey);
        PlayerPrefs.DeleteKey(LevelsKey);
    }
}