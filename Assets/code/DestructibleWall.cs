using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private bool hasTriggered = false;

    void Start()
    {
        // Pastikan semua anak punya Rigidbody kinematic
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }

    public void TriggerDestruction()
    {
        if (hasTriggered) return;

        hasTriggered = true;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }

        // (Opsional) efek suara / partikel bisa ditambahkan di sini
    }
    void OnCollisionEnter(Collision collision)
{
    if (!hasTriggered && collision.gameObject.CompareTag("Tank"))
    {
        TriggerDestruction();
    }
}

}
