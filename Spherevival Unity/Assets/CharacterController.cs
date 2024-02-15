using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the character
    public Transform planetCenter; // Reference to the center of the planet

    private Rigidbody rb; // Rigidbody component of the character

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the character
    }

    void Update()
    {
        // Get input for movement
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // If there is movement input, move the character
        if (moveDirection.magnitude >= 0.1f)
        {
            // Convert move direction from local to world space
            moveDirection = transform.TransformDirection(moveDirection);

            // Calculate movement relative to the planet's center
            Vector3 fromCenterToCharacter = transform.position - planetCenter.position;
            Vector3 surfaceNormal = fromCenterToCharacter.normalized; // Normalized direction vector from the center to the character
            Vector3 surfaceTangent = Vector3.Cross(surfaceNormal, Vector3.up); // Tangent to the surface at the character's position
            moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal); // Project movement direction onto the tangent plane

            // Move the character
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
