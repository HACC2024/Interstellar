using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance;  // Singleton instance to be accessed globally
    public TMP_Text scoreText;  // Reference to the UI TextMeshPro component to display the score

    private int currentScore = 0;

    void Awake()
    {
        // Ensure only one instance of PlayerScore exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally, this makes the score persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }

        // Update the score UI on startup
        UpdateScoreDisplay();
    }

    // Method to add points to the player's score
    public void AddPoints(int points)
    {
        currentScore += points;
        Debug.Log("Points added! Current score: " + currentScore);
        UpdateScoreDisplay();  // Update the UI with the new score
    }

    // Method to update the score UI
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pictokens: " + currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text UI element is not assigned!");
        }
    }
}
