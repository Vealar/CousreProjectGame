using UnityEngine;

public class SpearProjectile : MonoBehaviour
{
    public float lifeTime = 5f;             
    public int damage = 1;                  
    public float destroyAfterHitDelay = 0.05f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);     
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 50f || Mathf.Abs(transform.position.y) > 30f)
        {
            Destroy(gameObject);
        }
    }
}