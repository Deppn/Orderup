using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public Transform[] waypoints; // 0=spawn, 1–3=queue, 4=cashier, 5=exit
    public int maxNPCs = 4;

    private float spawnTimer = 0f;
    private float nextSpawnTime = 0f;
    private List<NPCController> activeNPCs = new List<NPCController>();

    void Start()
    {
        SetRandomSpawnDelay();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= nextSpawnTime && activeNPCs.Count < maxNPCs)
        {
            SpawnNPC();
            spawnTimer = 0f;
            SetRandomSpawnDelay();
        }
    }

    void SetRandomSpawnDelay()
    {
        nextSpawnTime = Random.Range(5f, 10f);
    }

    void SpawnNPC()
    {
        if (npcPrefabs.Length == 0) return;

        GameObject selectedPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        GameObject npcGO = Instantiate(selectedPrefab, waypoints[0].position, Quaternion.identity);

        NPCController npc = npcGO.GetComponent<NPCController>();
        npc.manager = this;

        int queuePosition = activeNPCs.Count;
        Transform[] pathToCashier = GetQueuePath(queuePosition);

        npc.SetPath(pathToCashier, waypoints[5]); // 5 = exit
        activeNPCs.Add(npc);
    }

    Transform[] GetQueuePath(int queuePosition)
    {
        List<Transform> path = new List<Transform>();

        if (queuePosition <= 3) path.Add(waypoints[1]);
        if (queuePosition <= 2) path.Add(waypoints[2]);
        if (queuePosition <= 1) path.Add(waypoints[3]);
        if (queuePosition == 0) path.Add(waypoints[4]);

        return path.ToArray();
    }

    public void RemoveNPC(NPCController npc)
    {
        if (activeNPCs.Contains(npc))
        {
            activeNPCs.Remove(npc);
            Destroy(npc.gameObject);
            UpdateQueue();
        }
    }

    public void RemoveNPCWithoutDestroy(NPCController npc)
    {
        if (activeNPCs.Contains(npc))
        {
            activeNPCs.Remove(npc);
        }
    }

    public void UpdateQueue()
    {
        bool kasirOccupied = false;
        foreach (var npc in activeNPCs)
        {
            if (!npc.receivedFood && npc.isAtCashier)
            {
                kasirOccupied = true;
                break;
            }
        }
        if (kasirOccupied) return;

        activeNPCs.Sort((a, b) =>
        {
            float aDist = Vector3.Distance(a.transform.position, waypoints[3].position);
            float bDist = Vector3.Distance(b.transform.position, waypoints[3].position);
            return aDist.CompareTo(bDist);
        });

        List<Transform> targetQueue = new List<Transform>() {
            waypoints[4],
            waypoints[3],
            waypoints[2],
            waypoints[1],
        };

        int index = 0;
        foreach (var npc in activeNPCs)
        {
            if (npc.receivedFood) continue;
            if (index < targetQueue.Count)
            {
                npc.MoveToNextPosition(targetQueue[index]);
                index++;
            }
        }
    }
} 
