using UnityEngine;

public enum PerkType { ExtraLife, DamageBoost, SpeedBoost, DoubleJump, Revive }

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance;

    public bool doubleJumpUnlocked = false;
    public bool canRevive = false;
    public float damageMultiplier = 1f;
    public float speedMultiplier = 1f;
    public int extraHp = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ApplyPerk(PerkType type)
    {
        ResetPerks(); 
        switch (type)
        {
            case PerkType.ExtraLife:
                extraHp += 1;
                break;
            case PerkType.DamageBoost:
                damageMultiplier = 1.5f;
                break;
            case PerkType.SpeedBoost:
                speedMultiplier = 2f;
                break;
            case PerkType.DoubleJump:
                doubleJumpUnlocked = true;
                break;
            case PerkType.Revive:
                canRevive = true;
                break;
        }
    }

    public void ResetPerks()
    {
        doubleJumpUnlocked = false;
        canRevive = false;
        damageMultiplier = 1f;
        speedMultiplier = 1f;
        extraHp = 0;
    }
}