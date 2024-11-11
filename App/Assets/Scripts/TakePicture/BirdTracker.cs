using System.Collections.Generic;
using UnityEngine;

public class BirdTracker : MonoBehaviour
{
    private Dictionary<string, int> capturedBirds = new Dictionary<string, int>();

    private static BirdTracker instance;
    public static BirdTracker Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BirdTracker>();
                if (instance == null)
                {
                    GameObject trackerObj = new GameObject("BirdTracker");
                    instance = trackerObj.AddComponent<BirdTracker>();
                }
            }
            return instance;
        }
    }

    // Method to capture a bird
    public void CaptureBird(string birdId)
    {
        if (capturedBirds.ContainsKey(birdId))
        {
            capturedBirds[birdId] += 1; // Increment count if already captured
        }
        else
        {
            capturedBirds.Add(birdId, 1); // Add new bird to tracker
        }

        Debug.Log($"Captured {birdId} {capturedBirds[birdId]} time(s).");
    }

    // Get the capture count for a specific bird
    public int GetBirdCaptureCount(string birdId)
    {
        if (capturedBirds.ContainsKey(birdId))
        {
            return capturedBirds[birdId];
        }
        return 0;
    }

    // Get all captured birds for display (BirdDex)
    public Dictionary<string, int> GetCapturedBirds()
    {
        return capturedBirds;
    }
}
