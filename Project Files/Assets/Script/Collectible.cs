using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Sound to play on collection
    private AudioSource audioSource;
    public Platform platform;

    private void Awake()
    {
        platform = FindObjectOfType<Platform>();
    }

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
            platform.UpdateColor(new Color(Random.value, Random.value, Random.value));


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
