using UnityEngine;
using UnityEngine.UI;

public class CustomerCounter : MonoBehaviour
{
    public static CustomerCounter Instance;
    public Text customerText;
    private int customerServed = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCustomerServed()
    {
        customerServed++;
        UpdateText();
    }

    void UpdateText()
    {
        if (customerText != null)
        {
            customerText.text = "Selesai: " + customerServed;
        }
    }
}
