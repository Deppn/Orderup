using UnityEngine;
using UnityEngine.EventSystems;

public class UnifiedInteractor : MonoBehaviour
{
    public float rayDistance = 3f;
    public LayerMask interactableLayers;
    private FoodHolder foodHolder;

    void Start()
    {
        foodHolder = FindObjectOfType<FoodHolder>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        // 🔘 Tombol A / klik kiri untuk interaksi
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, interactableLayers))
            {
                GameObject hitObject = hit.collider.gameObject;

                // 🔫 Kalau sedang pegang pistol → tembak
                if (foodHolder.IsHoldingGun())
                {
                    FireGun(hitObject);
                    return;
                }

                // 🍔 Kalau sedang pegang makanan → coba kasih ke NPC
                if (foodHolder.IsHoldingFood())
                {
                    NPCController npc = hitObject.GetComponent<NPCController>();
                    if (npc != null)
                    {
                        string foodTag = foodHolder.GetHeldFood().tag;

                        if (npc.TryReceiveFood(foodTag))
                        {
                            foodHolder.RemoveFood(foodHolder.GetHeldFood());
                            Debug.Log("Makanan diberikan ke NPC: " + foodTag);
                        }
                        else
                        {
                            Debug.Log("NPC tidak butuh makanan ini.");
                        }

                        return;
                    }
                }

                // ⚙️ Interaksi manual (untuk tombol tank/tembakan)
                IInteractable interactable = hitObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    Debug.Log("Interaksi objek berhasil (manual via raycast)");
                    return;
                }

                // ⚠️ Tidak perlu panggil IPointerClickHandler → biarkan GVR yang urus
            }
        }

        // ❌ Tombol B → buang makanan/pistol
        if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
        {
            foodHolder.ClearHands();
            Debug.Log("Tangan dikosongkan.");
        }
    }

    void FireGun(GameObject target)
    {
        NPCController npc = target.GetComponentInParent<NPCController>();
        if (npc != null)
        {
            npc.MakeRagdoll();
            Debug.Log("NPC ditembak!");
        }
        else
        {
            Debug.Log("Tidak ada NPC yang ditembak.");
        }
    }
}
