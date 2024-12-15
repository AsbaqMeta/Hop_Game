using UnityEngine;

public class Platform : MonoBehaviour
{
    private new Renderer renderer;
    private bool scoreAwarded = false; // Tracks if score has been awarded for this platform

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        UpdateColor(GameManager.Instance.currentPlatformColor); // Initialize with the manager's color
    }

    public void UpdateColor(Color newColor)
    {
        renderer.material.color = newColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides and the score hasn't been awarded yet
        if (collision.gameObject.CompareTag("Player") && !scoreAwarded)
        {
            GameManager.Instance.IncreaseScore(1); // Increase score by 1
            scoreAwarded = true; // Mark score as awarded for this platform
        }
    }
}
