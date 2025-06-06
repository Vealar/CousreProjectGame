using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostRecorder : MonoBehaviour
{
    public float recordInterval = 0.01f;
    public int maxRecordFrames = 300; 

    public struct Snapshot
    {
        public Vector3 position;
        
    }

    private List<Snapshot> snapshots = new List<Snapshot>();
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= recordInterval)
        {
            timer = 0f;
            RecordSnapshot();
        }
    }

    private void RecordSnapshot()
    {
        if (snapshots.Count >= maxRecordFrames)
        {
            snapshots.RemoveAt(0);
        }

        snapshots.Add(new Snapshot
        {
            position = transform.position
        });
    }

    public List<Snapshot> GetSnapshotCopy()
    {
        return new List<Snapshot>(snapshots);
    }
}