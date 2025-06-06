using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float bulletSpeed = 15f;
    public float bulletLife = 2f;
    public float fireRate = 0.13f;
    
    private float nextFire = 0.0f;
    private bool initialShoot = true;

    void Awake()
    {
    }

    public void Shoot(Quaternion targetRotation)
    {
        if (initialShoot)
        {
            initialShoot = false;
        }

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, targetRotation);
            Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody2D.linearVelocity = bullet.transform.right * bulletSpeed;
            Destroy(bullet, bulletLife);
        }
    }

    public void StopShooting()
    {
        nextFire = 0.0f;
        initialShoot = true;
    }
}