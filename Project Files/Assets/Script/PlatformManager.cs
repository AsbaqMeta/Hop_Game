using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // Platform prefab
    public GameObject collectiblePrefab; // Collectible prefab
    public int platformCount = 20; // Number of platforms
    public float platformSpacing = 3f; // Distance between platforms
    public float collectibleChance = 0.2f; // Chance to spawn a collectible
    public float range = 6f; // X-axis range for platform position
    public float Speed = 2f; // Speed

    private List<GameObject> platforms = new List<GameObject>(); // Pool for platforms
    private Queue<GameObject> collectibles = new Queue<GameObject>(); // Pool for collectibles

    void Start()
    {
        // Initialize platform pool
        for (int i = 0; i < platformCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-range, range), 0, i * platformSpacing);
            GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
            platforms.Add(platform);

            // Spawn collectible with some chance
            if (Random.value < collectibleChance)
            {
                GameObject collectible = Instantiate(collectiblePrefab, position + Vector3.up * 1f, Quaternion.identity);
                collectible.SetActive(false); // Initially inactive
                collectibles.Enqueue(collectible);
                collectible.transform.SetParent(platform.transform);
            }
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
                Vector3 newPosition = new Vector3(Random.Range(-range, range), 0, furthestZ + platformSpacing);
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

}
