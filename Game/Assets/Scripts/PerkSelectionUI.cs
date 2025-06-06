using UnityEngine;
using UnityEngine.UI;

public class PerkSelectionUI : MonoBehaviour
{
    public Button[] perkButtons;

    private int selectedPerkIndex = -1;

    void Start()
    {
        for (int i = 0; i < perkButtons.Length; i++)
        {
            int index = i;
            perkButtons[i].onClick.AddListener(() => SelectPerk(index));
        }
    }

    void SelectPerk(int index)
    {
        for (int i = 0; i < perkButtons.Length; i++)
        {
            perkButtons[i].GetComponent<Image>().color = Color.white;
        }
        
        perkButtons[index].GetComponent<Image>().color = Color.green;
        selectedPerkIndex = index;
        if (PerkManager.Instance != null)
        {
            PerkManager.Instance.ApplyPerk((PerkType)selectedPerkIndex);
        }
        Time.timeScale = 1f;
    }
}