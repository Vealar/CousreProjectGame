using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EyeBossController : MonoBehaviour
{
    [Header("Movement")]
    public Transform[] movePoints;
    public float moveSpeed = 5f;
    public float waitTime = 1f;

    [Header("State")]
    private bool isMoving = true;
    private int currentPointIndex = 0;

    [Header("Components")]
    private Animator animator;
    
    [Header("Attack 1 Settings")]
    public GameObject spherePrefab;
    public Transform[] sphereSpawnPoints;
    public int spheresToSpawn = 5;
    public float inversionDuration = 11f;
    private PlayerStateManager playerState;
    [Header("Attack 2 Settings")]
    public GameObject shadowPrefab;
    public float shadowDelay = 0.88f;
    public float shadowLifetime = 18f;
    [Header("Attack 3 Settings")]
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float laserRotationSpeed = 90f;
    private GameObject activeLaser;
    [Header("Attack 4 Settings")]
    public BlindnessEffect blindnessEffect;
    public Transform[] blindnessMovePoints;
    public float blindnessDuration = 6f;
    private bool isInBlindnessPath = false;
    [Header("Attack 5 Settings")]
    public GameObject portalPrefab;
    public Transform portalSpawnPoint;

    public GameObject darkShadowPrefab;
    public Transform[] pathStartPoints;
    public Transform[] pathEndPoints;
    public int shadowsToSpawn = 4;
    void Start()
    {
        playerState = FindObjectOfType<PlayerStateManager>();
        animator = GetComponent<Animator>();
        Invoke(nameof(Attack_5), 3f);
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            Transform[] currentPath = isInBlindnessPath ? blindnessMovePoints : movePoints;
            if (currentPath.Length == 0)
            {
                yield return null;
                continue;
            }

            if (isMoving)
            {
                Vector2 target = currentPath[currentPointIndex].position;

                while (Vector2.Distance(transform.position, target) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                transform.position = target;

                yield return new WaitForSeconds(waitTime);

                currentPointIndex = (currentPointIndex + 1) % currentPath.Length;
            }
            yield return null;
        }
    }


    public void Attack_1()
    {

        if (animator != null)
            animator.SetTrigger("SphereAttack");

        playerState.isControlInverted = true;
        Invoke(nameof(ResetInversion), inversionDuration);
        
        SpawnRandomSpheres();
    }
    void ResetInversion()
    {
        playerState.isControlInverted = false;
    }
    private void SpawnRandomSpheres()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < sphereSpawnPoints.Length; i++)
            availableIndices.Add(i);

        for (int i = 0; i < spheresToSpawn; i++)
        {
            int rand = Random.Range(0, availableIndices.Count);
            int index = availableIndices[rand];
            availableIndices.RemoveAt(rand);

            Instantiate(spherePrefab, sphereSpawnPoints[index].position, Quaternion.identity);
        }
    }
    
    
    

    public void Attack_2()
    {
        var player = FindObjectOfType<PlayerGhostRecorder>();
        if (player == null) return;

        GameObject shadow = Instantiate(shadowPrefab, player.transform.position, Quaternion.identity);
        var shadowScript = shadow.GetComponent<ShadowFollower>();
        if (shadowScript != null)
        {
            shadowScript.lifetime = shadowLifetime; 
            shadowScript.Init(player, delaySeconds: shadowDelay);
        }
    }

    public void Attack_3()
    {
        animator.SetTrigger("ChargeLaser");
        isMoving = false;

        Invoke(nameof(FireLaser), 1f);
    }

    private void FireLaser()
    {
        activeLaser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);

        LasEye laserScript = activeLaser.GetComponent<LasEye>();
        if (laserScript != null)
            laserScript.Init(laserSpawnPoint, gameObject);
        StartCoroutine(RotateLaserAndStop());
    }
    private IEnumerator RotateLaserAndStop()
    {
        float currentAngle = 0f;

        while (currentAngle < 360f)
        {
            float step = laserRotationSpeed * Time.deltaTime;

            laserSpawnPoint.Rotate(0f, 0f, step);

            currentAngle += step;

            yield return null;
        }

        StopLaser();
    }

    private void StopLaser()
    {
        if (activeLaser != null)
            Destroy(activeLaser);

        isMoving = true;

        laserSpawnPoint.localRotation = Quaternion.identity;
    }

    
    

   
    public void Attack_4()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && blindnessEffect != null)
        {
            Light2D playerLight = player.GetComponentInChildren<Light2D>(true);
            Debug.Log(playerLight);
            if (playerLight != null)
            {
                blindnessEffect.ActivateBlindness(playerLight,blindnessDuration);
            }
            else
            {
          
            }
        }

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        isInBlindnessPath = true;
        currentPointIndex = 0;

        Invoke(nameof(EndBlindnessAttack), blindnessDuration);
    }
    private void EndBlindnessAttack()
    {
        isInBlindnessPath = false;

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.white;
        }

        float minDist = float.MaxValue;
        int closestIndex = 0;
        for (int i = 0; i < movePoints.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, movePoints[i].position);
            if (dist < minDist)
            {
                minDist = dist;
                closestIndex = i;
            }
        }
        currentPointIndex = closestIndex;
    }



    public void Attack_5()
    {
        Debug.Log("Attack 5: Portal and Shadows");

        GameObject portal = Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);

        List<int> usedIndices = new List<int>();
        for (int i = 0; i < shadowsToSpawn; i++)
        {
            int randIndex;
            do {
                randIndex = Random.Range(0, pathStartPoints.Length);
            } while (usedIndices.Contains(randIndex));

            usedIndices.Add(randIndex);

            GameObject shadow = Instantiate(darkShadowPrefab, pathStartPoints[randIndex].position, Quaternion.identity);

            ShadowMover mover = shadow.GetComponent<ShadowMover>();
            if (mover != null)
            {
                Vector3[] path = new Vector3[] {
                    pathStartPoints[randIndex].position,
                    pathEndPoints[randIndex].position
                };
                mover.InitPath(path);
            }
        }
    }


    public void PerformAttack(int index)
    {
        switch (index)
        {
            case 1: Attack_1(); break;
            case 2: Attack_2(); break;
            case 3: Attack_3(); break;
            case 4: Attack_4(); break;
            case 5: Attack_5(); break;
            default: Debug.LogWarning("Invalid attack index"); break;
        }
    }
}
