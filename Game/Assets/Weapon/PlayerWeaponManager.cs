using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Transform shootPoint; 
    public int selectedWeaponIndex = 0;
    private List<IWeapon> weapons = new List<IWeapon>();
    private IWeapon currentWeapon;

    void Awake()
    {
        if (shootPoint == null)
        {
            Debug.LogError("ShootPoint is not assigned!");
            return;
        }

        weapons.AddRange(shootPoint.GetComponents<IWeapon>());
    }

    void Start()
    {
        //selectedWeaponIndex = 0;
        selectedWeaponIndex = PlayerPrefs.GetInt("SelectedWeaponIndex");
        SetWeapon(selectedWeaponIndex);
    }

    public void SetWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count)
        {
            Debug.LogWarning("Invalid weapon index");
            return;
        }

        foreach (var weapon in weapons)
        {
            if (weapon != null)
                weapon.enabled = false;
        }

        currentWeapon = weapons[index];
        currentWeapon.enabled = true;
    }

    public void Shoot(Quaternion rotation)
    {
        if (currentWeapon != null && currentWeapon.gameObject.activeInHierarchy)
        {
            currentWeapon.Shoot(rotation);
        }
    }

    public void StopShooting()
    {
        if (currentWeapon != null)
        {
            currentWeapon.StopShooting();
        }
    }
    
    public void TriggerWeapon()
    {
        currentWeapon.Attack();
    }

}