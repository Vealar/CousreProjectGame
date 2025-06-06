using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager2 : MonoBehaviour
{
    private bool bossDefeated = false;

    [Header("Boss")] public SecondBossController bossController;
    public EnemyHealth enemyHealth;

    [Header("Settings")] public float timeBetweenAttacks = 2f;

    public enum AttackType
    {
        FlyingHead,
        Bombs,
        Spears
    }

    void Start()
    {
        StartCoroutine(StartAttackLoop());
    }

    public void StopAttacks()
    {
        bossDefeated = true;
        StopAllCoroutines();
    }

    IEnumerator StartAttackLoop()
    {
        yield return new WaitForSeconds(2f);

        while (!bossDefeated)
        {
            AttackType nextAttack = ChooseAttack();
            yield return StartCoroutine(ExecuteAttack(nextAttack));
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    AttackType ChooseAttack()
    {
        List<AttackType> attackPool = new List<AttackType> { AttackType.FlyingHead, AttackType.Bombs, AttackType.Spears };
        Shuffle(attackPool);
        return attackPool[0];
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    IEnumerator ExecuteAttack(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.FlyingHead:
                yield return bossController.DoFirstAttack();
                break;

            case AttackType.Bombs:
                yield return bossController.DoSecondAttack();
                break;

            case AttackType.Spears:
                yield return bossController.DoThirdAttack();
                break;
        }
    }
}