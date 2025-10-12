using UnityEngine;

public class NPCOrderDisplay : MonoBehaviour
{
    public Transform orderAnchor; // Tempat munculnya makanan (di atas kepala)
    public GameObject[] foodPrefabs; // List makanan dengan prefab yang punya tag sesuai

    private GameObject currentOrderObject;

    public void SetOrder(string orderTag)
{
    ClearOrder();

    foreach (GameObject prefab in foodPrefabs)
    {
        if (prefab.CompareTag(orderTag))
        {
            Quaternion tiltedRotation = Quaternion.Euler(45f, 0f, 0f); // Tambahkan kemiringan
            currentOrderObject = Instantiate(prefab, orderAnchor.position, tiltedRotation, orderAnchor);
            currentOrderObject.transform.localPosition = Vector3.zero;

            // Auto-scale sesuai prefab
            Vector3 originalScale = prefab.transform.localScale;
            currentOrderObject.transform.localScale = originalScale;

            // Tambahkan rotasi berputar
            currentOrderObject.AddComponent<Rotator>();

            break;
        }
    }
}


    public void ClearOrder()
    {
        if (currentOrderObject != null)
        {
            Destroy(currentOrderObject);
            currentOrderObject = null;
        }
    }
}
