using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager3 : MonoBehaviour
{
    [Header("Boss")]
    public EyeBossController bossController;

    [Header("Settings")]
    public float timeBetweenAttacks = 6f;

    private bool bossDefeated = false;

    void Start()
    {
        StartCoroutine(AttackLoop());
    }

    public void StopAttacks()
    {
        bossDefeated = true;
        StopAllCoroutines();
    }

    IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(2f);

        while (!bossDefeated)
        {
            int randomAttackIndex = Random.Range(1, 6);

            bossController.PerformAttack(randomAttackIndex);

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }
}