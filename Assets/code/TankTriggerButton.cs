using UnityEngine;

public class TankTriggerButton : MonoBehaviour
{
    public TankController tank;

    public void OnButtonPressed()
    {
        if (tank != null)
        {
            tank.StartMoving();
        }
    }
}
