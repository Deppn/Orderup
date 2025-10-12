using UnityEngine;

public class VRTankButton : MonoBehaviour, IInteractable
{
    public GameObject tankPrefab;
    public Transform spawnPoint;
    public DestructibleWall targetWall;

    private bool hasSpawned = false;

    public void Interact()
    {
        Debug.Log("Tombol disentuh!");

        if (hasSpawned)
        {
            Debug.Log("Tank sudah pernah spawn.");
            return;
        }

        if (tankPrefab == null)
        {
            Debug.LogError("❌ Tank prefab belum diassign!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("❌ Spawn point belum diassign!");
            return;
        }

        if (targetWall == null)
        {
            Debug.LogWarning("⚠️ Target wall belum diassign. Melanjutkan tanpa menghancurkan tembok.");
        }

        hasSpawned = true;

        Instantiate(tankPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("✅ Tank berhasil di-spawn.");

        if (targetWall != null)
        {
            targetWall.TriggerDestruction();
            Debug.Log("💥 Tembok dihancurkan.");
        }
    }
}
