using UnityEngine;

public class ClawWeapon : IWeapon
{
    public override void Attack()
    {
        GameObject clawHit = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Destroy(clawHit, bulletLife);
    }
}
