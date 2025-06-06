using UnityEngine;

public class GhostClawWeapon : IWeapon
{
    public override void Shoot(Quaternion targetRotation)
    {
        if (animator != null)
            animator.SetBool("isShooting", true);
        rotation = targetRotation;
        if (initialShoot)
            initialShoot = false;

        if (Time.time >= nextFire)
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
        float angle = Mathf.Atan2(bulletRigidbody2D.linearVelocity.y, bulletRigidbody2D.linearVelocity.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Destroy(bullet, bulletLife);
    }

}