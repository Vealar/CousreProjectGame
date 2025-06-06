using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
    protected Animator animator;
    protected Quaternion rotation;
    
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    public float bulletSpeed = 15f;
    public float bulletLife = 2f;
    public float fireRate = 0.17f;
    
    protected float nextFire = 0.0f;
    protected bool initialShoot = true;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    public virtual void Shoot(Quaternion targetRotation)
    {
        rotation = targetRotation;
        if (initialShoot)
            initialShoot = false;

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            if (animator != null)
                animator.SetBool("isShooting", true);
        }
    }

    public void StopShooting()
    {
        nextFire = 0.0f;
        initialShoot = true;
        animator.SetBool("isShooting", false);
    }
    public abstract void Attack();
}
