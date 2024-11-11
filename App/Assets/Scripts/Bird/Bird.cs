using UnityEngine;

public class Bird : MonoBehaviour
{
    public string birdName;            // Name of the bird
    public Vector2[] spawnLocations;   // Array of (longitude, latitude) coordinates
    public int points;                 // Points for capturing this bird
    public Rarity rarity;              // Rarity of the bird
    public Sprite birdImage;
    [TextArea(3, 10)]  
    public string birdDescription;

    // Enum to define bird rarity levels
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare

    }

    // Method to initialize bird data
    public void Initialize(string name, string description, Vector2[] locations, int pointValue, Rarity birdRarity, Sprite image)
    {
        birdName = name;
        birdDescription = description;
        spawnLocations = locations;
        points = pointValue;
        rarity = birdRarity;
        birdImage = image;

    }

    public void CaptureBird()
    {
        Debug.Log("Bird captured! Name: " + birdName + ", Points awarded: " + points);
    }
}
