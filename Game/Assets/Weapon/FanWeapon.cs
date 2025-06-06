using UnityEngine;

public class FanWeapon : IWeapon
{
    public Transform target;
    [SerializeField]
    private float fixedDistance = 5.2f;
    [SerializeField]
    private float[] arcHeights = new float[] { 3.0f, 1.1f, -1.1f, -3.0f };

    public override void Attack()
    {
        Vector2 direction = rotation * Vector2.right;
        Vector2 endPoint = (Vector2)bulletSpawn.position + direction.normalized * fixedDistance;

        for (int i = 0; i < arcHeights.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);
            FanProjectile fanProjectile = bullet.GetComponent<FanProjectile>();
            fanProjectile.SetTrajectory(bulletSpawn.position, endPoint, arcHeights[i]);
        }
    }
}