using UnityEngine;

public class SaveSlotSelector : MonoBehaviour
{
    public GameObject saveSlotPanel;   
    public GameObject levelMenuPanel;  
    public void ShowSaveSlotPanel()
    {
        saveSlotPanel.SetActive(true);
    }

    public void HideSaveSlotPanel()
    {
        saveSlotPanel.SetActive(false);
    }

    public void StartNewGame(int slot)
    {
        SaveManager.SelectSaveSlot(slot);
        SaveManager.ResetSaveSlot(slot);
        ShowLevelMenu();
    }

    public void ContinueLastGame()
    {
        int lastSlot = PlayerPrefs.GetInt("lastSaveSlot", -1);
        if (lastSlot == -1)
        {
            Debug.LogWarning("Нет предыдущего сохранения!");
            return;
        }

        SaveManager.SelectSaveSlot(lastSlot);
        ShowLevelMenu();
    }

    private void ShowLevelMenu()
    {
        saveSlotPanel.SetActive(false);
        if (levelMenuPanel != null)
            levelMenuPanel.SetActive(true);
    }
}