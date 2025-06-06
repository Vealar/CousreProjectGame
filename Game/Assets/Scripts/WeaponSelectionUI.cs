using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    public Button[] weaponButtons;
    private int selectedWeaponIndex = -1;
    void Start()
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            int index = i;
            weaponButtons[i].onClick.AddListener(() => SelectWeapon(index));
        }
    }
    void SelectWeapon(int index)
    {
        selectedWeaponIndex = index;
        PlayerPrefs.SetInt("SelectedWeaponIndex", selectedWeaponIndex);
        PlayerPrefs.Save();
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            weaponButtons[i].GetComponent<Image>().color = (i == selectedWeaponIndex) ? Color.green : Color.white;
        }
    }

}