using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    private PlayerGhostRecorder recorder;
    private int frameOffset;
    public float lifetime = 18f;
    public float updateInterval = 0.005f;
    private Coroutine followRoutine;

    public void Init(PlayerGhostRecorder targetRecorder, float delaySeconds)
    {
        recorder = targetRecorder;
        frameOffset = Mathf.RoundToInt(delaySeconds / recorder.recordInterval);
        followRoutine = StartCoroutine(FollowLive());
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
    
        if (followRoutine != null)
            StopCoroutine(followRoutine);

        Destroy(gameObject);
    }

    private IEnumerator FollowLive()
    {
        while (true)
        {
            var history = recorder.GetSnapshotCopy();

            int targetIndex = Mathf.Max(0, history.Count - frameOffset);
            if (targetIndex < history.Count)
            {
                transform.position = history[targetIndex].position;
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }
}