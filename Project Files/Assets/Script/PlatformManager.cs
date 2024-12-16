using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // Platform prefab
    public GameObject collectiblePrefab; // Collectible prefab
    public int platformCount = 20; // Number of platforms
    public float platformSpacing = 3f; // Distance between platforms
    public int collectiblesToDistribute = 10; // Total number of collectibles
    public float collectibleChance = 0.2f; // Chance to spawn a collectible
    public float range = 6f; // X-axis range for platform position
    public float Speed = 2f; // Initial Speed
    public float speedIncrease = 0.2f; // Speed increment
    public int platformsToPassForSpeedIncrease = 5; // Platforms needed to increase speed

    private List<GameObject> platforms = new List<GameObject>(); // Pool for platforms
    private Queue<GameObject> collectibles = new Queue<GameObject>(); // Pool for collectibles
    private int platformsPassed = 0; // Count of platforms passed

    void Start()
    {
        // Initialize platform pool
        for (int i = 0; i < platformCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-range, range), -1, i * platformSpacing);
            GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
            platforms.Add(platform);

            /*// Distribute collectibles uniformly across platforms
            DistributeCollectibles();*/
        }
    }

    void Update()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            // Move platforms backward along the Z-axis
            platforms[i].transform.Translate(Vector3.back * Time.deltaTime * Speed);

            // Recycle platform when out of view
            if (platforms[i].transform.position.z < -10f)
            {
                // Increment platformsPassed counter
                platformsPassed++;

                // Increase speed every 5th platform
                if (platformsPassed % platformsToPassForSpeedIncrease == 0)
                {
                    Speed += speedIncrease;
                    Debug.Log($"Speed increased to {Speed}");
                }

                // Find the furthest platform along the Z-axis
                float furthestZ = float.MinValue;
                foreach (var platform in platforms)
                {
                    if (platform.transform.position.z > furthestZ)
                    {
                        furthestZ = platform.transform.position.z;
                    }
                }

                // Recycle the current platform to a new position in front of the furthest platform
                Vector3 newPosition = new Vector3(Random.Range(-range, range), -1, furthestZ + platformSpacing);
                platforms[i].transform.position = newPosition;

                // Reuse or spawn collectible
                if (platforms[i].transform.childCount > 0)
                {
                    foreach (Transform child in platforms[i].transform)
                    {
                        child.gameObject.SetActive(false); // Deactivate current collectible
                        collectibles.Enqueue(child.gameObject); // Add back to pool
                    }
                }

                // Spawn a new collectible with some chance
                if (Random.value < collectibleChance && collectibles.Count > 0)
                {
                    GameObject collectible = collectibles.Dequeue();
                    collectible.transform.position = newPosition + Vector3.up * 1f;
                    collectible.SetActive(true); // Activate collectible
                    collectible.transform.SetParent(platforms[i].transform);
                }
        }
        }
    }

    private void DistributeCollectibles()
    {
        // Calculate the spacing between platforms for collectible placement
        int step = Mathf.Max(1, platformCount / collectiblesToDistribute);

        for (int i = 0; i < platformCount; i += step)
        {
            if (collectibles.Count < collectiblesToDistribute)
            {
                GameObject collectible = Instantiate(collectiblePrefab, Vector3.zero, Quaternion.identity);
                collectible.SetActive(false); // Initially inactive
                collectibles.Enqueue(collectible);
            }

            if (i < platforms.Count && collectibles.Count > 0)
            {
                GameObject collectible = collectibles.Dequeue();
                Vector3 position = platforms[i].transform.position + Vector3.up * 1f;
                collectible.transform.position = position;
                collectible.SetActive(true);
                collectible.transform.SetParent(platforms[i].transform);
            }
        }
    }

    private void HandlePlatformReset(GameObject platform, Vector3 platformPosition)
    {
        // Remove existing collectibles from the platform
        foreach (Transform child in platform.transform)
        {
            child.gameObject.SetActive(false); // Deactivate current collectible
            collectibles.Enqueue(child.gameObject); // Return to pool
        }

        // Add a collectible only if the platform should have one based on uniform distribution logic
        if (Random.value < 0.5f && collectibles.Count > 0) // Slight randomization to avoid strict predictability
        {
            GameObject collectible = collectibles.Dequeue();
            collectible.transform.position = platformPosition + Vector3.up * 1f;
            collectible.SetActive(true);
            collectible.transform.SetParent(platform.transform);
        }
    }

}
