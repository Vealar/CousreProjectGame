using System;
using UnityEngine;

public class LasEye : MonoBehaviour
{
    public Transform laserFirePoint;
    public LineRenderer laserLineRenderer;

    private float defDistanceRay = 100f;
    private float damageCooldown = 1.5f;
    private float lastDamageTime = -999f;
    [SerializeField] private GameObject bossObjectToIgnore;
    
    public void Init(Transform firePoint, GameObject ignoreObject)
    {
        laserFirePoint = firePoint;
        bossObjectToIgnore = ignoreObject;
    }

    void Update()
    {
        if (laserFirePoint != null)
            ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(laserFirePoint.position, laserFirePoint.right, defDistanceRay);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            if (bossObjectToIgnore != null && hit.collider.transform.IsChildOf(bossObjectToIgnore.transform))
                continue;

            Draw2DRay(laserFirePoint.position, hit.point);

            if (hit.collider.CompareTag("Player") && Time.time >= lastDamageTime + damageCooldown)
            {
                Health hp = hit.collider.GetComponent<Health>();
                if (hp != null)
                {
                    hp.takeDamage();
                    lastDamageTime = Time.time;
                }
            }

            break;
        }
    }


    void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    {
        laserLineRenderer.SetPosition(0, startPoint);
        laserLineRenderer.SetPosition(1, endPoint);
    }
}

