using UnityEngine;

public static class SaveManager
{
    public static int CurrentSaveSlot { get; private set; } = -1;

    public static void SelectSaveSlot(int slot)
    {
        CurrentSaveSlot = slot;
        PlayerPrefs.SetInt("lastSaveSlot", slot);
        PlayerPrefs.Save();
    }

    public static void ResetSaveSlot(int slot)
    {
        PlayerPrefs.DeleteKey($"save{slot}_unlockedLevelSweet");
        PlayerPrefs.DeleteKey($"save{slot}_unlockedLevelNightmare");
        PlayerPrefs.SetInt($"save{slot}_unlockedLevelSweet", 1);
        PlayerPrefs.SetInt($"save{slot}_unlockedLevelNightmare", 1);
        PlayerPrefs.Save();
    }

    public static int GetUnlockedLevelSweet() =>
        PlayerPrefs.GetInt($"save{CurrentSaveSlot}_unlockedLevelSweet", 1);

    public static int GetUnlockedLevelNightmare() =>
        PlayerPrefs.GetInt($"save{CurrentSaveSlot}_unlockedLevelNightmare", 1);

    public static void SetUnlockedLevelSweet(int value) =>
        PlayerPrefs.SetInt($"save{CurrentSaveSlot}_unlockedLevelSweet", value);

    public static void SetUnlockedLevelNightmare(int value) =>
        PlayerPrefs.SetInt($"save{CurrentSaveSlot}_unlockedLevelNightmare", value);
}