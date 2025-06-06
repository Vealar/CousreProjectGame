using System.Collections;
using UnityEngine;

public class SwordWeapon : IWeapon
{
    public override void Attack()
    {
        GameObject swoosh = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Destroy(swoosh, bulletLife);
    }
}
