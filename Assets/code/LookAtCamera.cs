using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            Vector3 direction = transform.position - Camera.main.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
