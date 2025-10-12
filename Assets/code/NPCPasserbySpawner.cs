using UnityEngine;

public class NPCPasserbySpawner : MonoBehaviour
{
    [Header("Passerby NPC Prefabs (Random Pick)")]
    public GameObject[] passerbyPrefabs;

    [Header("Waypoints")]
    public Transform startPoint;
    public Transform endPoint;

    [Header("Spawn Timing")]
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 12f;

    private float timer = 0f;
    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextSpawnTime)
        {
            SpawnPasserby();
            timer = 0f;
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnPasserby()
    {
        if (passerbyPrefabs.Length == 0 || startPoint == null || endPoint == null)
        {
            Debug.LogWarning("PasserbySpawner: Missing prefab or waypoint references.");
            return;
        }

        // Random prefab dari array
        GameObject selectedPrefab = passerbyPrefabs[Random.Range(0, passerbyPrefabs.Length)];
        GameObject passerby = Instantiate(selectedPrefab, startPoint.position, Quaternion.identity);

        NPCPasserby controller = passerby.GetComponent<NPCPasserby>();
        if (controller != null)
        {
            controller.startPoint = startPoint;
            controller.endPoint = endPoint;
        }
    }
}
