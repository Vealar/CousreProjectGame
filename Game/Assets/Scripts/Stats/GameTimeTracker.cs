using UnityEngine;

using UnityEngine;

public class GameTimeTracker : MonoBehaviour
{
    void Update()
    {
        GameStatsManager.AddPlayTime(Time.deltaTime);
    }
}
