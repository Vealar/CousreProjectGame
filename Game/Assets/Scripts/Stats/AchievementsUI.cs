using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{
    [System.Serializable]
    public class AchievementIcon
    {
        public string levelName;       
        public Image icon;             
    }

    public AchievementIcon[] achievements;

    void OnEnable()
    {
        foreach (var a in achievements)
        {
            bool unlocked = PlayerPrefs.GetInt("achieved_" + a.levelName, 0) == 1;

            if (unlocked)
                a.icon.color = Color.white;
            else
                a.icon.color = new Color(47, 47, 47, 0.3f); 
        }
    }
    public void CloseAchievements()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAchievements();
        }
    }


}