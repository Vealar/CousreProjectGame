using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRevolverWeapon : IWeapon
{
    public override void Shoot(Quaternion targetRotation)
    {
        if (animator != null)
            animator.SetBool("isShooting", true);
        rotation = targetRotation;
        if (initialShoot)
            initialShoot = false;

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Attack();
        }
    }
    public override void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);
        Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody2D.linearVelocity = bullet.transform.right * bulletSpeed;
        Destroy(bullet, bulletLife);
    }
}