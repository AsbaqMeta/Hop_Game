using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;       // Reference to the ball
    public Vector3 offset;       // Offset between the camera and the ball
    public float smoothSpeed = 0.125f; // Smooth factor for camera movement

    void LateUpdate()
    {
        if (ball != null)
        {
            // Target position for the camera
            Vector3 targetPosition = ball.position + offset;

            // Smoothly move the camera to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // (Optional) Make the camera look at the ball
            transform.LookAt(ball);
        }
    }
}
