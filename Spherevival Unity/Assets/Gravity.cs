using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public Transform planetCenter; // Reference to the center of the planet
    public float gravityStrength = 9.81f; // Strength of the gravitational force

    private Rigidbody rb; // Rigidbody component of the character

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the character
        rb.useGravity = false; // Disable Unity's built-in gravity
    }

    void FixedUpdate()
    {
        // Calculate gravity direction
        Vector3 gravityDirection = (planetCenter.position - transform.position).normalized;

        // Calculate gravity force
        Vector3 gravity = gravityDirection * gravityStrength;

        // Apply gravity force to the character
        rb.AddForce(gravity, ForceMode.Acceleration);

        // Rotate character to align with surface normal
        RotateToAlignWithSurface(-gravityDirection);
    }

    void RotateToAlignWithSurface(Vector3 surfaceNormal)
    {
        // Calculate rotation to align character's up with surface normal
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;

        // Apply rotation gradually
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
    }
}
