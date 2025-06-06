using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;

public class AttackManager : MonoBehaviour
{
    private bool bossDefeated = false;

    [Header("Spider")] public GameObject spiderPrefab;
    public Transform spiderSpawnRight;

    [Header("Fly")] public GameObject flyPrefab;
    public Transform[] flySpawnPoint;

    [Header("Fly Paths ")]
    public FlyPathGroup[] flyPaths;

    [Header("Spears")] public SpearAttack[] spears;

    [Header("Boss")] public BossController bossController;
    public EnemyHealth enemyHealth;

    [Header("Settings")] public float timeBetweenAttackBlocks = 11f;
    private bool phase2Activated = false;

    enum AttackType
    {
        Spikes,
        Spider,
        Bird,
        BossBalls,
        BossLaser
    }

    enum AttackBlock
    {
        Spikes_Laser,
        Laser_Fly,
        Balls_Spider,
        Spider_Spikes,
        Fly_Balls
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
        while (!bossDefeated)
        {
            
            yield return new WaitForSeconds(2f);
            UpdatePhase();
            if (bossDefeated) yield break;
            
            AttackBlock nextBlock = ChooseBlock();
            yield return StartCoroutine(ExecuteBlock(nextBlock));
            yield return new WaitForSeconds(5f);
        }
    }

    void UpdatePhase()
    {
        if (!phase2Activated && enemyHealth.GetHealth() <= 500)
        {
            phase2Activated = true;
            Debug.Log("Фаза 2");
        }
    }

    AttackBlock ChooseBlock()
    {
        List<AttackBlock> pool = new List<AttackBlock>();

        if (!phase2Activated)
        {
            pool.Add(AttackBlock.Spikes_Laser);
            pool.Add(AttackBlock.Laser_Fly);
        }
        else
        {
            pool.AddRange(new[]
            {
                AttackBlock.Balls_Spider,
                AttackBlock.Spider_Spikes,
                AttackBlock.Fly_Balls
            });
        }

        Shuffle(pool);

        return pool[0];
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


    IEnumerator ExecuteBlock(AttackBlock block)
    {
        switch (block)
        {
            case AttackBlock.Spikes_Laser:
                bossController.FireLaser();
                yield return new WaitForSeconds(0.35f);
                StartCoroutine(LaunchCustomSpearAttack(0.7f));
                break;

            case AttackBlock.Laser_Fly:
                LaunchFlyAttack();
                yield return new WaitForSeconds(4f);
        
                bossController.FireLaser();
                yield return new WaitForSeconds(4f);

                bossController.FireLaser();
                yield return new WaitForSeconds(4f);

                bossController.FireLaser();

                yield return new WaitForSeconds(4f);
        
                LaunchFlyAttack();
                yield return new WaitForSeconds(4f);

                bossController.FireLaser();
                yield return new WaitForSeconds(4f);
        
                bossController.FireLaser();
                yield return new WaitForSeconds(4f);
                break;

            case AttackBlock.Balls_Spider:
                bossController.FireTripleShot();
                for (int i = 0; i < 6; i++)
                {
                    LaunchSpiderAttack();
                    yield return new WaitForSeconds(0.3f + i * 0.1f);
                    if (i == 4)
                    {
                        bossController.FireTripleShot();
                    }
                }
        
                for (int i = 0; i < 9; i++)
                {
                    LaunchSpiderAttack();
                    yield return new WaitForSeconds(0.2f + i*0.2f);
                    if (i == 4 || i == 8)
                    {
                        bossController.FireTripleShot();
                    }
                }
                break;

            case AttackBlock.Spider_Spikes:
                for (int i = 0; i < 6; i++)
                {
                    LaunchSpiderAttack();
                    yield return new WaitForSeconds(0.1f + i * 0.1f);
                    if (i == 4)
                    {
                        StartCoroutine(LaunchCustomSpearAttack(0.7f));
                    }
                }
        
                for (int i = 0; i < 9; i++)
                {
                    LaunchSpiderAttack();
                    yield return new WaitForSeconds(0.2f + i*0.2f);
                    if (i == 5)
                    {
                        StartCoroutine(LaunchCustomSpearAttack(0.7f));
                    }
                }
                break;

            case AttackBlock.Fly_Balls:
                yield return new WaitForSeconds(1f);
                LaunchFlyAttack();
                bossController.FireTripleShot();
                yield return new WaitForSeconds(5f);
                bossController.FireTripleShot();
                yield return new WaitForSeconds(5f);
        
                LaunchFlyAttack();
                bossController.FireTripleShot();
                yield return new WaitForSeconds(5f);
                bossController.FireTripleShot();
                yield return new WaitForSeconds(5f);
                bossController.FireTripleShot();
                break;
        }
    }


    void LaunchSpiderAttack()
    {
        Instantiate(spiderPrefab, spiderSpawnRight.position, Quaternion.identity);
    }

    void LaunchFlyAttack()
    {
        for (int i = 0; i < flyPaths.Length; i++)
        {
            GameObject fly = Instantiate(flyPrefab, flySpawnPoint[i].position, Quaternion.identity);
            FlyPath flyPath = fly.GetComponent<FlyPath>();
            flyPath.pathPoints = flyPaths[i].pathPoints;
        }
    }


    IEnumerator LaunchCustomSpearAttack(float delayBetweenAttacks)
    {
        List<int[]> spearGroups = new List<int[]>
        {
            new int[] { 0, 1, 2 },
            new int[] { 2, 1, 0 },
            
            new int[] { 0, 1, -1, 2 },
            new int[] { 0, 2, -1, 1},
            new int[] { 1, 2, -1, 0 },
            
            new int[] { 1, -1, 0, 2 },
            new int[] { 0, -1, 1, 2 },
            new int[] { 2, -1, 0, 1 },
        };
        int[] selectedGroup = spearGroups[Random.Range(0, spearGroups.Count)];
        if (selectedGroup.Length == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                spears[selectedGroup[i]].StartAttackSequence();
                yield return new WaitForSeconds(delayBetweenAttacks);
            }
            
        }else
        {
            if (selectedGroup[2] == -1)
            {
                spears[selectedGroup[0]].StartAttackSequence();
                spears[selectedGroup[1]].StartAttackSequence();
                yield return new WaitForSeconds(delayBetweenAttacks);
                spears[selectedGroup[3]].StartAttackSequence();
            }
            else
            {
                spears[selectedGroup[0]].StartAttackSequence();
                yield return new WaitForSeconds(delayBetweenAttacks);
                spears[selectedGroup[2]].StartAttackSequence();
                spears[selectedGroup[3]].StartAttackSequence();
            }
        }

        
    }
}