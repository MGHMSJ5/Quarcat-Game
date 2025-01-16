using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      
    public Vector3 offset;        
    public float smoothSpeed = 0.125f; 
    public Vector3 framingOffset; 

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Calculates our camera's desired position
        Vector3 desiredPosition = player.position + offset;

        // Interpolates our camera's desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Adjuss the framing of our camera
        Vector3 lookTarget = player.position + framingOffset;
        transform.LookAt(lookTarget);
    }
}
