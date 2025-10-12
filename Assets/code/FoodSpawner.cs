using UnityEngine;
using UnityEngine.EventSystems;

public class FoodSpawner : MonoBehaviour, IPointerClickHandler
{
    public GameObject foodPrefab;

    private FoodHolder foodHolder;

    private void Start()
    {
        foodHolder = FindObjectOfType<FoodHolder>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foodHolder.ReceiveFood(foodPrefab);
    }
}
