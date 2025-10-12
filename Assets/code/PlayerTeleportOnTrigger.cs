using UnityEngine;
using System.Collections;

public class PlayerTeleportOnTrigger : MonoBehaviour
{
    [Header("Target trigger yang disentuh")]
    public GameObject targetObject;

    [Header("Teleport tujuan")]
    public Transform firstTeleportDestination;
    public Transform secondTeleportDestination;

    private bool hasTeleported = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTeleported && other.gameObject == targetObject)
        {
            if (firstTeleportDestination != null)
            {
                transform.position = firstTeleportDestination.position;
                Debug.Log("Teleport pertama via trigger!");
                hasTeleported = true;

                StartCoroutine(TeleportAfterDelay());
            }
        }
    }

    IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(10f);

        if (secondTeleportDestination != null)
        {
            transform.position = secondTeleportDestination.position;
            Debug.Log("Teleport kedua setelah 10 detik!");
        }
    }
}
