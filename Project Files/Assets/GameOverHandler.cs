using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [Tooltip("Tag of the object that triggers Game Over")]
    public string gameOverTag = "Obstacle"; // Tag of the colliding object that ends the game
    public GameObject Panel;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the specified tag
        if (collision.gameObject.CompareTag(gameOverTag))
        {
            // Stop the game
            Time.timeScale = 0f;
            Panel.SetActive(true);
        }
    }

    public void GameOver()
    {
        // Start the game
        Time.timeScale = 1f;
        Debug.Log("Game Over!");
        ReloadScene();
    }

    private void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
