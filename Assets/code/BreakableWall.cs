using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Rigidbody[] fragments;
    private bool isBroken = false;

    void Start()
    {
        fragments = GetComponentsInChildren<Rigidbody>();

        // Nonaktifkan physics di awal
        foreach (var rb in fragments)
        {
            rb.isKinematic = true;
        }
    }

    public void BreakWall()
    {
        if (isBroken) return;
        isBroken = true;

        foreach (var rb in fragments)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(300f, transform.position, 5f);
        }

        Debug.Log("Tembok hancur!");
    }

    // Ini dipanggil oleh Tank saat tabrakan
    void OnCollisionEnter(Collision collision)
    {
        if (!isBroken && collision.gameObject.CompareTag("Tank"))
        {
            BreakWall();
        }
    }
}
