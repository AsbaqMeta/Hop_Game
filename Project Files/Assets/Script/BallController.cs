using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float jumpForce = 7f; // Force applied when jumping
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement on X and Z axes
        float moveX = Input.GetAxis("Horizontal"); // For left/right movement (X-axis)
        float moveZ = Input.GetAxis("Vertical");   // For forward/backward movement (Z-axis)

        // Set velocity for movement
        rb.velocity = new Vector3(moveX * moveSpeed, rb.velocity.y, moveZ * moveSpeed);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent double jumps
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
