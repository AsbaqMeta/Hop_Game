using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Sound to play on collection
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // When the player collects the object
        {
            // Increase score by 100
            GameManager.Instance.IncreaseScore(10);

            // to change the platform color
            GameManager.Instance.GenerateNewPlatformColor();

            // Play sound
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // Destroy the collectible after collection
            Destroy(gameObject);
        }
    }
}
