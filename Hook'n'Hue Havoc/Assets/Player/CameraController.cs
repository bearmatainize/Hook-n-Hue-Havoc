using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // The player's transform
    public Vector3 offset; // The initial offset from the player
    public float smoothSpeed = 0.125f; // The speed of camera smoothing
    public LayerMask obstacleMask; // The layer mask of the obstacles

    private Vector3 currentVelocity;

    private float xRotation = 0f;
    public float sensitivity = 300f;

    private void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Cast a ray from the camera to the player
        RaycastHit hit;
        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, offset.magnitude, obstacleMask))
        {
            // If the ray hits an obstacle, move the camera to the hit point
            smoothedPosition = hit.point;
        }

        transform.position = smoothedPosition;

        transform.LookAt(player);
    }
}
