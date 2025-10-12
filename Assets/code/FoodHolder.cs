using UnityEngine;

public class FoodHolder : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    public GameObject gunPrefab;

    private GameObject leftFood;
    private GameObject rightFood;
    private GameObject heldGun;

    // ✅ Menerima makanan ke tangan yang kosong
    public void ReceiveFood(GameObject foodPrefab)
    {
        Debug.Log("ReceiveFood dipanggil", this);

        if (IsHoldingGun())
        {
            Debug.Log("Tidak bisa ambil makanan saat memegang pistol.");
            return;
        }

        if (leftFood == null)
        {
            leftFood = Instantiate(foodPrefab, leftHand.position, leftHand.rotation, leftHand);
            Debug.Log("Makanan dimasukkan ke tangan kiri.");
        }
        else if (rightFood == null)
        {
            rightFood = Instantiate(foodPrefab, rightHand.position, rightHand.rotation, rightHand);
            Debug.Log("Makanan dimasukkan ke tangan kanan.");
        }
        else
        {
            Debug.Log("Kedua tangan sudah penuh.");
        }
    }

    // ✅ Membersihkan semua item dari tangan
    public void ClearHands()
    {
        if (leftFood != null)
        {
            Destroy(leftFood);
            leftFood = null;
        }

        if (rightFood != null)
        {
            Destroy(rightFood);
            rightFood = null;
        }

        if (heldGun != null)
        {
            Destroy(heldGun);
            heldGun = null;
        }

        Debug.Log("Semua item di tangan dibuang.");
    }

    // ✅ Ambil salah satu makanan (kiri diprioritaskan)
    public GameObject GetHeldFood()
    {
        if (leftFood != null) return leftFood;
        if (rightFood != null) return rightFood;
        return null;
    }

    // ✅ Cek apakah sedang memegang makanan
    public bool IsHoldingFood()
    {
        return leftFood != null || rightFood != null;
    }

    // ✅ Hapus makanan yang sedang dipegang
    public void RemoveFood(GameObject food)
    {
        if (food == leftFood)
        {
            Destroy(leftFood);
            leftFood = null;
        }
        else if (food == rightFood)
        {
            Destroy(rightFood);
            rightFood = null;
        }
    }

    // ✅ Cek apakah sedang memegang pistol
    public bool IsHoldingGun()
    {
        return heldGun != null;
    }

    // ✅ Spawn pistol di tangan kanan (ganti makanan kanan jika ada)
    public void SpawnGun()
    {
        if (IsHoldingGun())
        {
            Debug.Log("Sudah memegang pistol.");
            return;
        }

        if (rightFood != null)
        {
            Destroy(rightFood);
            rightFood = null;
            Debug.Log("Makanan di tangan kanan dibuang untuk pistol.");
        }

        heldGun = Instantiate(gunPrefab, rightHand.position, rightHand.rotation, rightHand);
        heldGun.transform.localPosition = Vector3.zero;
        heldGun.transform.localRotation = Quaternion.identity;

        Debug.Log("Pistol di-spawn ke tangan kanan.");
    }
}
