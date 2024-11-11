using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Authentication;
using System.Collections.Generic;
using Newtonsoft.Json; // You need to install Newtonsoft.Json package for JSON serialization

public class CaptureManager : MonoBehaviour
{
    public Button captureButton;         // UI button for capturing birds
    public TMP_Text scoreText;           // TextMeshPro UI element to display the score
    public BirdDexManager birdDexManager; // Reference to BirdDexManager
    public int userScore;                // Track the user's score

    private string playerId;             // To store the current player's ID
    public GameObject birdAppPanel;      // Assign this in the Inspector

    void Start()
    {
        captureButton.onClick.AddListener(OnCaptureButtonPressed);
        InitializeUnityServices();

        if (birdAppPanel != null)
        {
            birdAppPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("BirdApp Panel not assigned in CaptureManager.");
        }
    }

    public void SetPlayerId(string id)
    {
        playerId = id;
    }

    private void InitializeUnityServices()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            playerId = AuthenticationService.Instance.PlayerId;
            LoadScore();
            LoadBirdDex();
        }
        else
        {
            Debug.LogError("Player is not signed in.");
        }
    }

    void OnCaptureButtonPressed()
    {
        Bird[] birds = FindObjectsOfType<Bird>();

        foreach (Bird bird in birds)
        {
            CheckVisibility checkVisibility = bird.GetComponent<CheckVisibility>();
            if (checkVisibility.IsInView())
            {
                bird.CaptureBird();           // Capture the bird and award points
                userScore += bird.points;     // Add points to the user's score

                // Check and add the bird to the BirdDex if not already there
                if (!birdDexManager.IsBirdInDex(bird.birdName))
                {
                    birdDexManager.AddBirdToDex(bird);  // Add to BirdDex and UI
                }

                Destroy(bird.gameObject);     // Remove the bird from the scene
                UpdateScoreDisplay();         // Update the score UI
                SaveScore();                  // Save the updated score
                SaveBirdDex();                // Save the updated BirdDex
            }
        }
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + userScore.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text UI element is not assigned!");
        }
    }

    public void SaveScore()
    {
        if (string.IsNullOrEmpty(playerId))
        {
            Debug.LogError("Player ID is null or empty. Cannot save score.");
            return;
        }

        string scoreKey = "PlayerScore_" + playerId;
        PlayerPrefs.SetInt(scoreKey, userScore);
        PlayerPrefs.Save();

        Debug.Log("Score saved: " + userScore);
    }

    private void LoadScore()
    {
        if (string.IsNullOrEmpty(playerId))
        {
            Debug.LogError("Player ID is null or empty. Cannot load score.");
            return;
        }

        string scoreKey = "PlayerScore_" + playerId;
        userScore = PlayerPrefs.GetInt(scoreKey, 0);
        UpdateScoreDisplay();
    }

    // Save BirdDex data
    public void SaveBirdDex()
    {
        if (string.IsNullOrEmpty(playerId))
        {
            Debug.LogError("Player ID is null or empty. Cannot save BirdDex.");
            return;
        }

        string birdDexKey = "BirdDex_" + playerId;
        HashSet<string> capturedBirdNames = birdDexManager.GetCapturedBirdNames();
        string birdDexJson = JsonConvert.SerializeObject(capturedBirdNames); // Convert HashSet to JSON string
        PlayerPrefs.SetString(birdDexKey, birdDexJson);
        PlayerPrefs.Save();

        Debug.Log("BirdDex saved.");
    }

    // Load BirdDex data
    private void LoadBirdDex()
    {
        if (string.IsNullOrEmpty(playerId))
        {
            Debug.LogError("Player ID is null or empty. Cannot load BirdDex.");
            return;
        }

        string birdDexKey = "BirdDex_" + playerId;
        if (PlayerPrefs.HasKey(birdDexKey))
        {
            string birdDexJson = PlayerPrefs.GetString(birdDexKey);
            HashSet<string> capturedBirdNames = JsonConvert.DeserializeObject<HashSet<string>>(birdDexJson);

            // Restore BirdDex in BirdDexManager
            foreach (string birdName in capturedBirdNames)
            {
                Bird bird = new Bird { birdName = birdName }; // Creating dummy Bird for UI addition
                birdDexManager.AddBirdToDex(bird); // Add bird to BirdDex UI
            }

            Debug.Log("BirdDex loaded.");
        }
        else
        {
            Debug.Log("No BirdDex data found for the user.");
        }
    }
}
