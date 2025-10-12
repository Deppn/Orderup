using UnityEngine;

public class WallPart : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            // Cari parent dan aktifkan pecah total
            DestructibleWall wall = GetComponentInParent<DestructibleWall>();
            if (wall != null)
            {
                wall.TriggerDestruction();
            }
        }
    }
}
