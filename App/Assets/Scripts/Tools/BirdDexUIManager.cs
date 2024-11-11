using UnityEngine;
using System.Collections.Generic;

public class BirdDexManager : MonoBehaviour
{
    public AddButton addButton; // Reference to AddButton script to add buttons to the UI
    private HashSet<string> capturedBirdNames = new HashSet<string>(); // Stores unique captured bird names

    // Method to check if the bird is already in the BirdDex
    public bool IsBirdInDex(string birdName)
    {
        return capturedBirdNames.Contains(birdName);
    }

    public HashSet<string> GetCapturedBirdNames()
    {
        return new HashSet<string>(capturedBirdNames);
    }

    // Method to add a bird to the BirdDex and display it in the UI
    public void AddBirdToDex(Bird bird)
    {
        if (!capturedBirdNames.Contains(bird.birdName))
        {
            capturedBirdNames.Add(bird.birdName);
            addButton.AddButtonToScrollView(bird.birdName, bird.birdImage, bird); // Add bird name as a button to the scroll view
        }
    }
}
