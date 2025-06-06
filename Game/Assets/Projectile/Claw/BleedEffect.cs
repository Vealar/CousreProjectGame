using UnityEngine;

using System.Collections;
using UnityEngine;

public class BleedEffect : MonoBehaviour
{
    public float totalDamage = 3f;
    public float duration = 3f;
    public int ticks = 6;

    private EnemyHealth enemyHealth;
    private bool isBleeding = false;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null && !isBleeding)
        {
            StartCoroutine(ApplyBleed());
        }
    }

    IEnumerator ApplyBleed()
    {
        isBleeding = true;
        float damagePerTick = totalDamage / ticks;
        float interval = duration / ticks;

        for (int i = 0; i < ticks; i++)
        {
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerTick);
            }
            yield return new WaitForSeconds(interval);
        }

        isBleeding = false;
        Destroy(this);
    }
}
