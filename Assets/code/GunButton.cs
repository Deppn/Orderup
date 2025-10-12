using UnityEngine;

public class GunButton : MonoBehaviour, IInteractable
{
    public FoodHolder foodHolder;

    public void Interact()
    {
        if (foodHolder != null)
        {
            foodHolder.SpawnGun();
            Debug.Log("Pistol di-spawn ke tangan!");
        }
    }
}
