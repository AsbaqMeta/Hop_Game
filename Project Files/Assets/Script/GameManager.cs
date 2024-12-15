using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Color currentPlatformColor; // Current color for all platforms
    public TextMeshProUGUI scoreText; // Drag the UI Text object here
    private int score = 0; // Current score

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
        GenerateNewPlatformColor(); // Initialize with a random color
    }

    public void IncreaseScore(int amount)
    {
        score += amount; // Increase the score
        UpdateScoreText(); // Update the UI
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the UI Text
    }

    public void GenerateNewPlatformColor()
    {
        currentPlatformColor = new Color(Random.value, Random.value, Random.value); // Generate a random color
    }


}
