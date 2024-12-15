using UnityEngine;
using UnityEngine.UI; // For UI elements
using TMPro; // Optional: Use if working with TextMeshPro
using System.Collections;

public class GroundAndCountdownManager : MonoBehaviour
{
    public GameObject ground; // Reference to the ground object
    public GameObject d; // Reference to the ground object
    public TMP_Text countdownText; // Reference to a TextMeshPro UI Text object (or use Text if not using TMP)
    private float countdownDuration = 5f; // Duration before the ground disappears

    void Start()
    {
        // Start the countdown coroutine
        StartCoroutine(GroundCountdown());
    }

    private IEnumerator GroundCountdown()
    {
        float timeLeft = countdownDuration;

        // Loop to update countdown
        while (timeLeft > 0)
        {
            countdownText.text = Mathf.CeilToInt(timeLeft).ToString(); // Display countdown as an integer
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timeLeft--; // Decrease time left
        }

        // After countdown ends
        countdownText.text = ""; // Clear the countdown text
        if (ground != null)
        {
            ground.SetActive(false); // Disable the ground (or destroy it if needed)
            d.SetActive(false);
        }
    }
}
