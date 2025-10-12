using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickWalk : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform cameraTransform;
    public float scrollSpeedFactor = 1f;
    private float scrollAccumulated = 0f;
    private float maxScroll = 3f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Scroll Mouse untuk kontrol kecepatan maju/mundur
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            scrollAccumulated += scrollInput * scrollSpeedFactor;
            scrollAccumulated = Mathf.Clamp(scrollAccumulated, -maxScroll, maxScroll);
        }

        // Jika tidak scroll dalam beberapa waktu, bisa perlahan kembali ke 0 (opsional)
        // scrollAccumulated = Mathf.MoveTowards(scrollAccumulated, 0f, Time.deltaTime * 1f);

        // Arah input dari user: horizontal dari joystick, vertical dari scroll
        float verticalInput = scrollAccumulated;

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Ubah ke arah kamera
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * inputDirection.z + camRight * inputDirection.x;

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}
