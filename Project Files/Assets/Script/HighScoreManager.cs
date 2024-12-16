using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TMP_Text currentScoreText; // Text to display current score
    public TMP_Text highScoreText; // Text to display high score
    private int currentScore = 0; // Current score
    private int highScore = 0; // High score

    private const string HighScoreKey = "HighScore"; // Key for PlayerPrefs

    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateUI();
    }

    void Update()
    {

    }

    public void AddScore()
    {
       
        // Check if current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HighScoreKey, highScore); // Save new high score
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + highScore);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update the score UI
        if (currentScoreText != null)
            currentScoreText.text = "Score: " + currentScore;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    public void ResetHighScore()
    {
        // Reset the high score
        PlayerPrefs.DeleteKey(HighScoreKey);
        highScore = 0;
        UpdateUI();
    }
}
