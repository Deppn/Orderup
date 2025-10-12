using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public GameObject tankPrefab;
    public Transform spawnPoint;
    public DestructibleWall targetWall;

    public void SpawnAndDestroy()
    {
        // Spawn tank di posisi tembok
        Instantiate(tankPrefab, spawnPoint.position, spawnPoint.rotation);

        // Hancurkan tembok
        if (targetWall != null)
        {
            targetWall.TriggerDestruction();
        }
    }
}
