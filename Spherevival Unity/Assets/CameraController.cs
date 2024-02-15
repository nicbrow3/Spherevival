using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the target to follow (the character)
    public Transform planetCenter; // Reference to the center of the planet
    public float distance = 10f; // Distance from the target
    public float height = 5f; // Height of the camera above the target
    public float smoothSpeed = 0.125f; // Smoothness of camera movement

    private Vector3 cameraOffset; // Offset from the target position

    void Start()
    {
        // Calculate the initial camera offset from the target
        cameraOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Calculate the desired position of the camera relative to the planet's center
        Vector3 desiredPosition = (target.position - planetCenter.position).normalized * distance + planetCenter.position + Vector3.up * height;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the new position of the camera
        transform.position = smoothedPosition;

        // Make the camera look at the target
        transform.LookAt(target.position);
    }
}
