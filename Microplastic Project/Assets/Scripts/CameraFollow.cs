using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // The player's transform to follow
    public Vector3 offset;     // Offset position of the camera from the player
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement

    void Start()
    {
        // Initialize offset if not set in the Inspector
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Desired position of the camera
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally, make the camera look at the player
        transform.LookAt(player);
    }
}